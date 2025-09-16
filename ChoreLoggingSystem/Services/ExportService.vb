Imports System.IO
Imports System.Text
Imports ChoreLoggingSystem.Models

Namespace Services
    Public Class ExportService

        Public Sub ExportToPDF(data As List(Of ChoreLogEntry), filePath As String)
            ' Create a text-based "PDF" report (HTML format that can be printed to PDF)
            Try
                Dim html As New StringBuilder()
                html.AppendLine("<!DOCTYPE html>")
                html.AppendLine("<html>")
                html.AppendLine("<head>")
                html.AppendLine("<title>Task Completion Report</title>")
                html.AppendLine("<style>")
                html.AppendLine("body { font-family: Arial, sans-serif; margin: 20px; }")
                html.AppendLine("h1 { text-align: center; color: #003366; }")
                html.AppendLine("h2 { text-align: center; color: #666; }")
                html.AppendLine("table { width: 100%; border-collapse: collapse; margin-top: 20px; }")
                html.AppendLine("th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }")
                html.AppendLine("th { background-color: #f2f2f2; font-weight: bold; }")
                html.AppendLine("tr:nth-child(even) { background-color: #f9f9f9; }")
                html.AppendLine(".date { font-size: 12px; color: #666; text-align: center; margin: 10px; }")
                html.AppendLine("</style>")
                html.AppendLine("</head>")
                html.AppendLine("<body>")

                html.AppendLine("<h1>EXCELLENCE CARE SOLUTIONS</h1>")
                html.AppendLine("<h2>Professional Task Management System</h2>")
                html.AppendLine("<div class='date'>Generated: " & DateTime.Now.ToString("MM/dd/yyyy HH:mm") & "</div>")
                html.AppendLine("<h2>Task Completion Report</h2>")

                html.AppendLine("<table>")
                html.AppendLine("<tr>")
                html.AppendLine("<th>Staff Name</th>")
                html.AppendLine("<th>Branch Name</th>")
                html.AppendLine("<th>Shift Name</th>")
                html.AppendLine("<th>Task Name</th>")
                html.AppendLine("<th>Completed Date</th>")
                html.AppendLine("<th>Notes</th>")
                html.AppendLine("</tr>")

                For Each entry In data
                    html.AppendLine("<tr>")
                    html.AppendLine("<td>" & entry.StaffName & "</td>")
                    html.AppendLine("<td>" & entry.BranchName & "</td>")
                    html.AppendLine("<td>" & entry.ShiftName & "</td>")
                    html.AppendLine("<td>" & entry.TaskName & "</td>")
                    html.AppendLine("<td>" & entry.CompletedDateTime.ToString("MM/dd/yyyy HH:mm") & "</td>")
                    html.AppendLine("<td>" & If(String.IsNullOrEmpty(entry.Notes), "", entry.Notes) & "</td>")
                    html.AppendLine("</tr>")
                Next

                html.AppendLine("</table>")
                html.AppendLine("</body>")
                html.AppendLine("</html>")

                File.WriteAllText(filePath, html.ToString(), Encoding.UTF8)

            Catch ex As Exception
                Throw New Exception("HTML Report Export Error: " & ex.Message, ex)
            End Try
        End Sub

        Public Sub ExportToExcel(data As List(Of ChoreLogEntry), filePath As String)
            ' Create CSV format that Excel can open
            Try
                Dim csv As New StringBuilder()

                ' Add header information
                csv.AppendLine("EXCELLENCE CARE SOLUTIONS")
                csv.AppendLine("Task Completion Report")
                csv.AppendLine("Generated: " & DateTime.Now.ToString("MM/dd/yyyy HH:mm"))
                csv.AppendLine() ' Empty line

                ' Column headers
                csv.AppendLine("Staff Name,Branch Name,Shift Name,Task Name,Completed Date,Notes")

                ' Data rows
                For Each entry In data
                    Dim notes As String = If(String.IsNullOrEmpty(entry.Notes), "", entry.Notes.Replace(",", ";").Replace(Chr(13), " ").Replace(Chr(10), " "))
                    csv.AppendLine(String.Format("""{0}"",""{1}"",""{2}"",""{3}"",""{4}"",""{5}""",
                        entry.StaffName,
                        entry.BranchName,
                        entry.ShiftName,
                        entry.TaskName,
                        entry.CompletedDateTime.ToString("MM/dd/yyyy HH:mm"),
                        notes))
                Next

                File.WriteAllText(filePath, csv.ToString(), Encoding.UTF8)

            Catch ex As Exception
                Throw New Exception("Excel Export Error: " & ex.Message, ex)
            End Try
        End Sub

        Public Sub ExportToCSV(data As List(Of ChoreLogEntry), filePath As String)
            Try
                Dim csv As New StringBuilder()

                ' Column headers
                csv.AppendLine("Staff Name,Branch Name,Shift Name,Task Name,Completed Date,Notes")

                ' Data rows
                For Each entry In data
                    Dim notes As String = If(String.IsNullOrEmpty(entry.Notes), "", entry.Notes.Replace(",", ";").Replace(Chr(13), " ").Replace(Chr(10), " "))
                    csv.AppendLine(String.Format("""{0}"",""{1}"",""{2}"",""{3}"",""{4}"",""{5}""",
                        entry.StaffName,
                        entry.BranchName,
                        entry.ShiftName,
                        entry.TaskName,
                        entry.CompletedDateTime.ToString("MM/dd/yyyy HH:mm"),
                        notes))
                Next

                File.WriteAllText(filePath, csv.ToString(), Encoding.UTF8)

            Catch ex As Exception
                Throw New Exception("CSV Export Error: " & ex.Message, ex)
            End Try
        End Sub

    End Class
End Namespace