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

        ' Get staff by UserID - FIXED to use correct column names
        Public Function GetStaffByUserID(userID As String) As Staff
            Using connection As New SqlConnection(_connectionString)
                connection.Open()
                ' FIXED: Use 'UserID' column instead of 'Initials' to match your table structure
                Dim query As String = "SELECT StaffID, UserID, FullName, EmployeeID, PIN, IsActive, CreatedDate, LastLoginDate, BranchID FROM Staff WHERE UserID = @UserID AND IsActive = 1"

                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@UserID", userID)

                    Using reader As SqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            Return New Staff With {
                                .StaffID = reader.GetInt32("StaffID"),
                                .UserID = reader.GetString("UserID"),
                                .FullName = reader.GetString("FullName"),
                                .EmployeeID = If(reader.IsDBNull("EmployeeID"), String.Empty, reader.GetString("EmployeeID")),
                                .PIN = reader.GetString("PIN"),
                                .IsActive = reader.GetBoolean("IsActive"),
                                .CreatedDate = reader.GetDateTime("CreatedDate"),
                                .BranchID = If(reader.IsDBNull("BranchID"), Nothing, reader.GetInt32("BranchID"))
                            }
                        End If
                    End Using
                End Using
            End Using

            Return Nothing
        End Function

        ' Log completed tasks - FIXED to use correct column names
        Public Function LogCompletedTasks(userID As String, branchId As Integer, shiftId As Integer,
                                completedTaskIds As List(Of Integer), Optional notes As String = "",
                                Optional authenticatedStaffId As Integer = 0) As Boolean
            Using connection As New SqlConnection(_connectionString)
                connection.Open()
                Using transaction As SqlTransaction = connection.BeginTransaction()
                    Try
                        For Each taskId As Integer In completedTaskIds
                            ' FIXED: Use 'UserID' column instead of 'StaffInitials' to match your table structure
                            Dim query As String = "INSERT INTO ChoreLog (UserID, BranchID, ShiftID, TaskID, CompletedDateTime, Status, Notes, AuthenticatedStaffID) " &
                                        "VALUES (@UserID, @BranchID, @ShiftID, @TaskID, @CompletedDateTime, @Status, @Notes, @AuthenticatedStaffID)"

                            Using command As New SqlCommand(query, connection, transaction)
                                command.Parameters.AddWithValue("@UserID", userID)
                                command.Parameters.AddWithValue("@BranchID", branchId)
                                command.Parameters.AddWithValue("@ShiftID", shiftId)
                                command.Parameters.AddWithValue("@TaskID", taskId)
                                command.Parameters.AddWithValue("@CompletedDateTime", DateTime.Now)
                                command.Parameters.AddWithValue("@Status", "Completed")
                                command.Parameters.AddWithValue("@Notes", If(notes, String.Empty))
                                command.Parameters.AddWithValue("@AuthenticatedStaffID", authenticatedStaffId)

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

        ' Get filtered chore log entries - FIXED to use correct column names
        Public Function GetChoreLogEntries(Optional fromDate As DateTime? = Nothing, Optional toDate As DateTime? = Nothing,
                                         Optional branchId As Integer? = Nothing, Optional shiftId As Integer? = Nothing,
                                         Optional staffInitials As String = Nothing) As List(Of ChoreLogEntry)
            Dim entries As New List(Of ChoreLogEntry)

            ' DEBUG: First test the basic query without filters
            Dim testQuery As String = "SELECT COUNT(*) FROM ChoreLog"
            Using testConnection As New SqlConnection(_connectionString)
                testConnection.Open()
                Using testCommand As New SqlCommand(testQuery, testConnection)
                    Dim totalRecords As Integer = CInt(testCommand.ExecuteScalar())
                    ' You can add a debug message here if needed
                    System.Diagnostics.Debug.WriteLine($"Total ChoreLog records: {totalRecords}")
                End Using
            End Using

            ' FIXED: Use LEFT JOINs to show data even if related records are missing
            Dim query As String = "SELECT cl.LogID, cl.UserID, cl.BranchID, " &
                      "ISNULL(b.BranchName, 'Unknown Branch') as BranchName, " &
                      "cl.ShiftID, ISNULL(s.ShiftName, 'Unknown Shift') as ShiftName, " &
                      "cl.TaskID, ISNULL(t.TaskName, 'Unknown Task') as TaskName, " &
                      "cl.CompletedDateTime, cl.Status, " &
                      "ISNULL(cl.Notes, '') as Notes, " &
                      "ISNULL(cl.AuthenticatedStaffID, 0) as AuthenticatedStaffID " &
                      "FROM ChoreLog cl " &
                      "LEFT JOIN Branches b ON cl.BranchID = b.BranchID " &
                      "LEFT JOIN Shifts s ON cl.ShiftID = s.ShiftID " &
                      "LEFT JOIN Tasks t ON cl.TaskID = t.TaskID " &
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

            If branchId.HasValue AndAlso branchId.Value <> -1 Then
                query &= " AND cl.BranchID = @BranchID"
                parameters.Add(New SqlParameter("@BranchID", branchId.Value))
            End If

            If shiftId.HasValue AndAlso shiftId.Value <> -1 Then
                query &= " AND cl.ShiftID = @ShiftID"
                parameters.Add(New SqlParameter("@ShiftID", shiftId.Value))
            End If

            If Not String.IsNullOrEmpty(staffInitials) Then
                query &= " AND cl.UserID LIKE @StaffInitials"
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
                                .UserID = reader.GetString("UserID"),
                                .BranchID = reader.GetInt32("BranchID"),
                                .BranchName = reader.GetString("BranchName"),
                                .ShiftID = reader.GetInt32("ShiftID"),
                                .ShiftName = reader.GetString("ShiftName"),
                                .TaskID = reader.GetInt32("TaskID"),
                                .TaskName = reader.GetString("TaskName"),
                                .CompletedDateTime = reader.GetDateTime("CompletedDateTime"),
                                .Status = reader.GetString("Status"),
                                .Notes = reader.GetString("Notes"),
                                .AuthenticatedStaffID = reader.GetInt32("AuthenticatedStaffID")
                            })
                        End While
                    End Using
                End Using
            End Using

            Return entries
        End Function

        ' Add this new method to get all active staff
        Public Function GetAllActiveStaff() As List(Of Staff)
            Dim staff As New List(Of Staff)

            Using connection As New SqlConnection(_connectionString)
                connection.Open()
                Dim query As String = "SELECT StaffID, UserID, FullName FROM Staff WHERE IsActive = 1 ORDER BY FullName"

                Using command As New SqlCommand(query, connection)
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            staff.Add(New Staff With {
                                .StaffID = reader.GetInt32("StaffID"),
                                .UserID = reader.GetString("UserID"),
                                .FullName = reader.GetString("FullName")
                            })
                        End While
                    End Using
                End Using
            End Using

            Return staff
        End Function
    End Class
End Namespace