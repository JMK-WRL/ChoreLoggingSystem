Imports System.IO
Imports System.Text
Imports ChoreLoggingSystem.Models
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports OfficeOpenXml
Imports CsvHelper

Namespace Services
    Public Class ExportService

        Public Sub ExportToPDF(data As List(Of ChoreLogEntry), filePath As String)
            Using document As New Document(PageSize.A4.Rotate())
                Using writer As PdfWriter = PdfWriter.GetInstance(document, New FileStream(filePath, FileMode.Create))
                    document.Open()

                    ' Company Header
                    AddCompanyHeader(document)

                    ' Add title
                    Dim title As New Paragraph("Task Completion Report", New Font(Font.FontFamily.HELVETICA, 16, Font.BOLD))
                    title.Alignment = Element.ALIGN_CENTER
                    title.SpacingAfter = 20
                    document.Add(title)

                    ' Create table
                    Dim table As New PdfPTable(6)
                    table.WidthPercentage = 100
                    table.SetWidths({3, 3, 2, 4, 2, 4})

                    ' Add headers
                    AddTableHeader(table, "Staff Name")
                    AddTableHeader(table, "Branch Name")
                    AddTableHeader(table, "Shift Name")
                    AddTableHeader(table, "Task Name")
                    AddTableHeader(table, "Completed Date")
                    AddTableHeader(table, "Notes")

                    ' Add data
                    For Each entry In data
                        table.AddCell(New PdfPCell(New Phrase(entry.StaffName)))
                        table.AddCell(New PdfPCell(New Phrase(entry.BranchName)))
                        table.AddCell(New PdfPCell(New Phrase(entry.ShiftName)))
                        table.AddCell(New PdfPCell(New Phrase(entry.TaskName)))
                        table.AddCell(New PdfPCell(New Phrase(entry.CompletedDateTime.ToString("MM/dd/yyyy"))))
                        table.AddCell(New PdfPCell(New Phrase(entry.Notes)))
                    Next

                    document.Add(table)
                    document.Close()
                End Using
            End Using
        End Sub

        Public Sub ExportToExcel(data As List(Of ChoreLogEntry), filePath As String)
            Using package As New ExcelPackage()
                Dim worksheet = package.Workbook.Worksheets.Add("Task Report")

                ' Company Header
                worksheet.Cells("A1").Value = "EXCELLENCE CARE SOLUTIONS"
                worksheet.Cells("A1:F1").Merge = True
                worksheet.Cells("A1").Style.Font.Bold = True
                worksheet.Cells("A1").Style.Font.Size = 16
                worksheet.Cells("A1").Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center

                worksheet.Cells("A2").Value = "Task Completion Report"
                worksheet.Cells("A2:F2").Merge = True
                worksheet.Cells("A2").Style.Font.Bold = True
                worksheet.Cells("A2").Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center

                ' Headers
                Dim headers() As String = {"Staff Name", "Branch Name", "Shift Name", "Task Name", "Completed Date", "Notes"}
                For i As Integer = 0 To headers.Length - 1
                    worksheet.Cells(4, i + 1).Value = headers(i)
                    worksheet.Cells(4, i + 1).Style.Font.Bold = True
                Next

                ' Data
                Dim row As Integer = 5
                For Each entry In data
                    worksheet.Cells(row, 1).Value = entry.StaffName
                    worksheet.Cells(row, 2).Value = entry.BranchName
                    worksheet.Cells(row, 3).Value = entry.ShiftName
                    worksheet.Cells(row, 4).Value = entry.TaskName
                    worksheet.Cells(row, 5).Value = entry.CompletedDateTime.ToString("MM/dd/yyyy")
                    worksheet.Cells(row, 6).Value = entry.Notes
                    row += 1
                Next

                ' Auto-fit columns
                worksheet.Cells.AutoFitColumns()

                ' Save
                package.SaveAs(New FileInfo(filePath))
            End Using
        End Sub

        Public Sub ExportToCSV(data As List(Of ChoreLogEntry), filePath As String)
            Using writer As New StreamWriter(filePath, False, Encoding.UTF8)
                Using csv As New CsvWriter(writer, Globalization.CultureInfo.InvariantCulture)
                    ' Write headers
                    csv.WriteField("Staff Name")
                    csv.WriteField("Branch Name")
                    csv.WriteField("Shift Name")
                    csv.WriteField("Task Name")
                    csv.WriteField("Completed Date")
                    csv.WriteField("Notes")
                    csv.NextRecord()

                    ' Write data
                    For Each entry In data
                        csv.WriteField(entry.StaffName)
                        csv.WriteField(entry.BranchName)
                        csv.WriteField(entry.ShiftName)
                        csv.WriteField(entry.TaskName)
                        csv.WriteField(entry.CompletedDateTime.ToString("MM/dd/yyyy"))
                        csv.WriteField(entry.Notes)
                        csv.NextRecord()
                    Next
                End Using
            End Using
        End Sub

        Private Sub AddCompanyHeader(document As Document)
            Dim headerTable As New PdfPTable(1)
            headerTable.WidthPercentage = 100

            Dim companyName As New Paragraph("EXCELLENCE CARE SOLUTIONS", New Font(Font.FontFamily.HELVETICA, 18, Font.BOLD))
            companyName.Alignment = Element.ALIGN_CENTER

            Dim subtitle As New Paragraph("Professional Task Management System", New Font(Font.FontFamily.HELVETICA, 12, Font.ITALIC))
            subtitle.Alignment = Element.ALIGN_CENTER

            document.Add(companyName)
            document.Add(subtitle)
            document.Add(New Paragraph(" ")) ' Space
        End Sub

        Private Sub AddTableHeader(table As PdfPTable, text As String)
            Dim cell As New PdfPCell(New Phrase(text, New Font(Font.FontFamily.HELVETICA, 10, Font.BOLD)))
            cell.BackgroundColor = BaseColor.LIGHT_GRAY
            cell.HorizontalAlignment = Element.ALIGN_CENTER
            table.AddCell(cell)
        End Sub

    End Class
End Namespace