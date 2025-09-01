Imports System.IO
Imports ChoreLoggingSystem.Models
Imports Microsoft.Data.SqlClient
Imports Microsoft.Extensions.Configuration

Namespace Services
    Public Class DatabaseService
        Private ReadOnly _connectionString As String

        Public Sub New()
            Try
                ' Build configuration from appsettings.json
                Dim builder As New ConfigurationBuilder()
                builder.SetBasePath(Directory.GetCurrentDirectory())
                builder.AddJsonFile("appsettings.json", optional:=False)

                Dim configuration As IConfiguration = builder.Build()
                _connectionString = configuration.GetConnectionString("DefaultConnection")

                ' Fallback if config fails
                If String.IsNullOrEmpty(_connectionString) Then
                    _connectionString = "Server=(localdb)\MSSQLLocalDB;Database=ChoreLoggingDB;Integrated Security=true;"
                End If
            Catch ex As Exception
                ' Emergency fallback
                _connectionString = "Server=(localdb)\MSSQLLocalDB;Database=ChoreLoggingDB;Integrated Security=true;"
            End Try
        End Sub

        ' Get all active branches
        Public Function GetBranches() As List(Of Branch)
            Dim branches As New List(Of Branch)

            Using connection As New SqlConnection(_connectionString)
                connection.Open()
                Dim query As String = "SELECT BranchID, BranchName, IsActive, CreatedDate FROM Branches WHERE IsActive = 1"

                Using command As New SqlCommand(query, connection)
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            branches.Add(New Branch With {
                                .BranchID = reader.GetInt32("BranchID"),
                                .BranchName = reader.GetString("BranchName"),
                                .IsActive = reader.GetBoolean("IsActive"),
                                .CreatedDate = reader.GetDateTime("CreatedDate")
                            })
                        End While
                    End Using
                End Using
            End Using

            Return branches
        End Function

        ' Get all active shifts
        Public Function GetShifts() As List(Of Shift)
            Dim shifts As New List(Of Shift)

            Using connection As New SqlConnection(_connectionString)
                connection.Open()
                Dim query As String = "SELECT ShiftID, ShiftName, StartTime, EndTime, IsActive, CreatedDate FROM Shifts WHERE IsActive = 1"

                Using command As New SqlCommand(query, connection)
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            shifts.Add(New Shift With {
                        .ShiftID = reader.GetInt32("ShiftID"),
                        .ShiftName = reader.GetString("ShiftName"),
                        .StartTime = TimeSpan.Parse(reader("StartTime").ToString()),
                        .EndTime = TimeSpan.Parse(reader("EndTime").ToString()),
                        .IsActive = reader.GetBoolean("IsActive"),
                        .CreatedDate = reader.GetDateTime("CreatedDate")
                    })
                        End While
                    End Using
                End Using
            End Using

            Return shifts
        End Function

        ' Get tasks by shift ID
        Public Function GetTasksByShift(shiftId As Integer) As List(Of ChoreTask)
            Dim tasks As New List(Of ChoreTask)

            Using connection As New SqlConnection(_connectionString)
                connection.Open()
                Dim query As String = "SELECT TaskID, TaskName, ShiftID, EstimatedMinutes, IsActive, CreatedDate FROM Tasks WHERE ShiftID = @ShiftID AND IsActive = 1"

                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@ShiftID", shiftId)

                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            tasks.Add(New ChoreTask With {
                                .TaskID = reader.GetInt32("TaskID"),
                                .TaskName = reader.GetString("TaskName"),
                                .ShiftID = reader.GetInt32("ShiftID"),
                                .EstimatedMinutes = reader.GetInt32("EstimatedMinutes"),
                                .IsActive = reader.GetBoolean("IsActive"),
                                .CreatedDate = reader.GetDateTime("CreatedDate")
                            })
                        End While
                    End Using
                End Using
            End Using

            Return tasks
        End Function

        ' Log completed tasks
        Public Function LogCompletedTasks(staffInitials As String, branchId As Integer, shiftId As Integer,
                                        completedTaskIds As List(Of Integer), Optional notes As String = "") As Boolean
            Using connection As New SqlConnection(_connectionString)
                connection.Open()
                Using transaction As SqlTransaction = connection.BeginTransaction()
                    Try
                        For Each taskId As Integer In completedTaskIds
                            Dim query As String = "INSERT INTO ChoreLog (StaffInitials, BranchID, ShiftID, TaskID, CompletedDateTime, Status, Notes) " &
                                                "VALUES (@StaffInitials, @BranchID, @ShiftID, @TaskID, @CompletedDateTime, @Status, @Notes)"

                            Using command As New SqlCommand(query, connection, transaction)
                                command.Parameters.AddWithValue("@StaffInitials", staffInitials)
                                command.Parameters.AddWithValue("@BranchID", branchId)
                                command.Parameters.AddWithValue("@ShiftID", shiftId)
                                command.Parameters.AddWithValue("@TaskID", taskId)
                                command.Parameters.AddWithValue("@CompletedDateTime", DateTime.Now)
                                command.Parameters.AddWithValue("@Status", "Completed")
                                command.Parameters.AddWithValue("@Notes", If(notes, String.Empty))

                                command.ExecuteNonQuery()
                            End Using
                        Next

                        transaction.Commit()
                        Return True
                    Catch ex As Exception
                        transaction.Rollback()
                        Return False
                    End Try
                End Using
            End Using
        End Function

        ' Get filtered chore log entries
        Public Function GetChoreLogEntries(Optional fromDate As DateTime? = Nothing, Optional toDate As DateTime? = Nothing,
                                         Optional branchId As Integer? = Nothing, Optional shiftId As Integer? = Nothing,
                                         Optional staffInitials As String = Nothing) As List(Of ChoreLogEntry)
            Dim entries As New List(Of ChoreLogEntry)

            Dim query As String = "SELECT cl.LogID, cl.StaffInitials, cl.BranchID, b.BranchName, " &
                                "cl.ShiftID, s.ShiftName, cl.TaskID, t.TaskName, " &
                                "cl.CompletedDateTime, cl.Status, cl.Notes " &
                                "FROM ChoreLog cl " &
                                "INNER JOIN Branches b ON cl.BranchID = b.BranchID " &
                                "INNER JOIN Shifts s ON cl.ShiftID = s.ShiftID " &
                                "INNER JOIN Tasks t ON cl.TaskID = t.TaskID " &
                                "WHERE 1=1"

            Dim parameters As New List(Of SqlParameter)

            If fromDate.HasValue Then
                query &= " AND CAST(cl.CompletedDateTime AS DATE) >= @FromDate"
                parameters.Add(New SqlParameter("@FromDate", fromDate.Value.Date))
            End If

            If toDate.HasValue Then
                query &= " AND CAST(cl.CompletedDateTime AS DATE) <= @ToDate"
                parameters.Add(New SqlParameter("@ToDate", toDate.Value.Date))
            End If

            If branchId.HasValue Then
                query &= " AND cl.BranchID = @BranchID"
                parameters.Add(New SqlParameter("@BranchID", branchId.Value))
            End If

            If shiftId.HasValue Then
                query &= " AND cl.ShiftID = @ShiftID"
                parameters.Add(New SqlParameter("@ShiftID", shiftId.Value))
            End If

            If Not String.IsNullOrEmpty(staffInitials) Then
                query &= " AND cl.StaffInitials LIKE @StaffInitials"
                parameters.Add(New SqlParameter("@StaffInitials", $"%{staffInitials}%"))
            End If

            query &= " ORDER BY cl.CompletedDateTime DESC"

            Using connection As New SqlConnection(_connectionString)
                connection.Open()
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddRange(parameters.ToArray())

                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            entries.Add(New ChoreLogEntry With {
                                .LogID = reader.GetInt32("LogID"),
                                .StaffInitials = reader.GetString("StaffInitials"),
                                .BranchID = reader.GetInt32("BranchID"),
                                .BranchName = reader.GetString("BranchName"),
                                .ShiftID = reader.GetInt32("ShiftID"),
                                .ShiftName = reader.GetString("ShiftName"),
                                .TaskID = reader.GetInt32("TaskID"),
                                .TaskName = reader.GetString("TaskName"),
                                .CompletedDateTime = reader.GetDateTime("CompletedDateTime"),
                                .Status = reader.GetString("Status"),
                                .Notes = If(reader.IsDBNull("Notes"), String.Empty, reader.GetString("Notes"))
                            })
                        End While
                    End Using
                End Using
            End Using

            Return entries
        End Function
    End Class
End Namespace