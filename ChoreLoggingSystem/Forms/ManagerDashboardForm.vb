Imports ChoreLoggingSystem.Models
Imports ChoreLoggingSystem.Services

Namespace Forms
    Partial Public Class ManagerDashboardForm
        Inherits Form

        Private ReadOnly _databaseService As DatabaseService
        Private ReadOnly _currentManager As Staff

        Public Sub New(loggedInManager As Staff)
            ' Initialize the form components first
            InitializeComponent()

            ' Set up the manager and database service
            _currentManager = loggedInManager
            _databaseService = New DatabaseService()

            ' Initialize the dashboard
            InitializeManagerDashboard()

            ' Load initial data
            LoadInitialData()
        End Sub

        Private Sub InitializeManagerDashboard()
            ' Set window title with manager name
            Me.Text = $"Excellence Care Solutions - Manager Dashboard - {_currentManager.FullName}"

            ' Apply styling
            ApplyManagerStyling()
        End Sub

        Private Sub LoadInitialData()
            Try
                ' Load branches and shifts
                LoadAllBranches()
                LoadAllShifts()
                LoadAllStaffForFilter()

                ' Set default date range
                dateTimePickerFrom.Value = DateTime.Today.AddDays(-7)
                dateTimePickerTo.Value = DateTime.Today.AddDays(1)

                ' Load initial stats
                labelTotalTasks.Text = "Total Tasks: 0"
                labelUniqueStaff.Text = "Staff Members: 0"
                labelBranchesActive.Text = "Active Branches: 0"
                labelMostActiveBranch.Text = "Most Active: N/A"

                ' Clear results
                dataGridViewResults.DataSource = Nothing

                ' Load dashboard data
                LoadDashboardData()

            Catch ex As Exception
                MessageBox.Show($"Error loading dashboard: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub LoadAllBranches()
            Try
                ' Load branches for both task logging and filtering
                Dim branches As List(Of Branch) = _databaseService.GetBranches()

                ' Task logging branch dropdown
                comboBoxTaskBranch.DataSource = New List(Of Branch)(branches)
                comboBoxTaskBranch.DisplayMember = "BranchName"
                comboBoxTaskBranch.ValueMember = "BranchID"
                comboBoxTaskBranch.SelectedIndex = -1

                ' Filter branch dropdown (with "All Branches" option)
                Dim filterBranches As New List(Of Branch) From {
                    New Branch With {.BranchID = -1, .BranchName = "All Branches"}
                }
                filterBranches.AddRange(branches)

                comboBoxFilterBranch.DataSource = filterBranches
                comboBoxFilterBranch.DisplayMember = "BranchName"
                comboBoxFilterBranch.ValueMember = "BranchID"
                comboBoxFilterBranch.SelectedIndex = 0
            Catch ex As Exception
                MessageBox.Show($"Error loading branches: {ex.Message}", "Error")
            End Try
        End Sub

        Private Sub LoadAllShifts()
            Try
                ' Load shifts for both task logging and filtering
                Dim shifts As List(Of Shift) = _databaseService.GetShifts()

                ' Task logging shift dropdown
                comboBoxTaskShift.DataSource = New List(Of Shift)(shifts)
                comboBoxTaskShift.DisplayMember = "ShiftName"
                comboBoxTaskShift.ValueMember = "ShiftID"
                comboBoxTaskShift.SelectedIndex = -1

                ' Filter shift dropdown (with "All Shifts" option)
                Dim filterShifts As New List(Of Shift) From {
                    New Shift With {.ShiftID = -1, .ShiftName = "All Shifts"}
                }
                filterShifts.AddRange(shifts)

                comboBoxFilterShift.DataSource = filterShifts
                comboBoxFilterShift.DisplayMember = "ShiftName"
                comboBoxFilterShift.ValueMember = "ShiftID"
                comboBoxFilterShift.SelectedIndex = 0
            Catch ex As Exception
                MessageBox.Show($"Error loading shifts: {ex.Message}", "Error")
            End Try
        End Sub

        ' AUTO-FILL STAFF NAME FUNCTIONALITY
        Private Sub textBoxUserID_TextChanged(sender As Object, e As EventArgs) Handles textBoxUserID.TextChanged
            Try
                If String.IsNullOrWhiteSpace(textBoxUserID.Text) Then
                    textBoxStaffName.Clear()
                    Return
                End If

                ' Auto-fill staff name based on UserID
                Dim staff As Staff = _databaseService.GetStaffByUserID(textBoxUserID.Text.Trim())

                If staff IsNot Nothing Then
                    textBoxStaffName.Text = staff.FullName
                    textBoxStaffName.ForeColor = Color.Black
                Else
                    textBoxStaffName.Text = "Staff not found"
                    textBoxStaffName.ForeColor = Color.Red
                End If

            Catch ex As Exception
                textBoxStaffName.Text = "Error loading staff"
                textBoxStaffName.ForeColor = Color.Red
            End Try
        End Sub

        ' TASK LOGGING FUNCTIONALITY
        Private Sub comboBoxTaskShift_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboBoxTaskShift.SelectedIndexChanged
            If comboBoxTaskShift.SelectedValue IsNot Nothing AndAlso TypeOf comboBoxTaskShift.SelectedValue Is Integer Then
                Dim shiftId As Integer = CInt(comboBoxTaskShift.SelectedValue)
                LoadTasksForShift(shiftId)
            End If
        End Sub

        Private Sub LoadTasksForShift(shiftId As Integer)
            Try
                checkedListBoxTasks.Items.Clear()
                Dim tasks As List(Of ChoreTask) = _databaseService.GetTasksByShift(shiftId)

                For Each task As ChoreTask In tasks
                    checkedListBoxTasks.Items.Add(task, False)
                Next
            Catch ex As Exception
                MessageBox.Show($"Error loading tasks: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub buttonSubmitTasks_Click(sender As Object, e As EventArgs) Handles buttonSubmitTasks.Click
            Try
                ' Validation
                If String.IsNullOrWhiteSpace(textBoxUserID.Text) Then
                    MessageBox.Show("Please enter a User ID.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                If textBoxStaffName.Text = "Staff not found" OrElse String.IsNullOrWhiteSpace(textBoxStaffName.Text) Then
                    MessageBox.Show("Please enter a valid User ID.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                If comboBoxTaskBranch.SelectedValue Is Nothing Then
                    MessageBox.Show("Please select a location.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                If comboBoxTaskShift.SelectedValue Is Nothing Then
                    MessageBox.Show("Please select a work shift.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                ' Get completed tasks
                Dim completedTaskIds As New List(Of Integer)
                For i As Integer = 0 To checkedListBoxTasks.Items.Count - 1
                    If checkedListBoxTasks.GetItemChecked(i) Then
                        Dim task As ChoreTask = CType(checkedListBoxTasks.Items(i), ChoreTask)
                        completedTaskIds.Add(task.TaskID)
                    End If
                Next

                If completedTaskIds.Count = 0 Then
                    MessageBox.Show("Please select at least one completed task.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                ' Submit to database (manager logging on behalf of staff)
                Dim success As Boolean = _databaseService.LogCompletedTasks(
                    textBoxUserID.Text.Trim(),
                    CInt(comboBoxTaskBranch.SelectedValue),
                    CInt(comboBoxTaskShift.SelectedValue),
                    completedTaskIds,
                    textBoxTaskNotes.Text.Trim(),
                    _currentManager.StaffID ' Manager's ID for audit trail
                )

                If success Then
                    MessageBox.Show($"{completedTaskIds.Count} task(s) logged successfully for {textBoxStaffName.Text}!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ResetTaskForm()
                    LoadDashboardData() ' Refresh reports
                Else
                    MessageBox.Show("Error logging tasks. Please try again.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Catch ex As Exception
                MessageBox.Show($"Error submitting tasks: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ' REPORTING FUNCTIONALITY
        Private Sub buttonApplyFilters_Click(sender As Object, e As EventArgs) Handles buttonApplyFilters.Click
            LoadDashboardData()
        End Sub

        Private Sub LoadDashboardData()
            Try

                'Me.Cursor = Cursors.WaitCursor
                'labelRecordsCount.Text = "Loading..."

                Dim fromDate As DateTime = dateTimePickerFrom.Value.Date
                Dim toDate As DateTime = dateTimePickerTo.Value.Date

                Dim branchId As Integer? = Nothing
                If comboBoxFilterBranch.SelectedValue IsNot Nothing AndAlso
                        CInt(comboBoxFilterBranch.SelectedValue) <> -1 Then
                    branchId = CInt(comboBoxFilterBranch.SelectedValue)
                End If

                Dim shiftId As Integer? = Nothing
                If comboBoxFilterShift.SelectedValue IsNot Nothing AndAlso
                        CInt(comboBoxFilterShift.SelectedValue) <> -1 Then
                    shiftId = CInt(comboBoxFilterShift.SelectedValue)
                End If

                Dim staffInitials As String = Nothing
                If comboBoxFilterStaff.SelectedValue IsNot Nothing AndAlso
                        Not String.IsNullOrEmpty(comboBoxFilterStaff.SelectedValue.ToString()) Then
                    staffInitials = comboBoxFilterStaff.SelectedValue.ToString()
                End If

                ' DEBUG: Show what parameters we're using
                MessageBox.Show($"Parameters:" & vbCrLf &
                       $"From: {fromDate.ToShortDateString()}" & vbCrLf &
                       $"To: {toDate.ToShortDateString()}" & vbCrLf &
                       $"Branch: {branchId}" & vbCrLf &
                       $"Shift: {shiftId}" & vbCrLf &
                       $"Staff: {staffInitials}", "Debug - Filter Parameters")

                Dim entries As List(Of ChoreLogEntry) = _databaseService.GetChoreLogEntries(fromDate, toDate, branchId, shiftId, staffInitials)

                ' DEBUG: Show how many records we got
                MessageBox.Show($"Records returned from database: {entries.Count}", "Debug - Data Count")


                dataGridViewResults.DataSource = entries
                FormatDataGrid()

                labelRecordsCount.Text = $"Records Found: {entries.Count}"

                ' Update summary statistics
                UpdateDashboardSummary(entries)

            Catch ex As Exception
                MessageBox.Show($"Error loading dashboard data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub FormatDataGrid()
            Try
                If dataGridViewResults.DataSource IsNot Nothing Then
                    dataGridViewResults.AutoResizeColumns()
                    dataGridViewResults.AllowUserToAddRows = False
                    dataGridViewResults.AllowUserToDeleteRows = False
                    dataGridViewResults.ReadOnly = True
                    dataGridViewResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                End If
            Catch ex As Exception
                ' Continue without formatting if there are issues
            End Try
        End Sub

        Private Sub UpdateDashboardSummary(entries As List(Of ChoreLogEntry))
            Try
                ' Calculate summary statistics
                Dim totalTasks As Integer = entries.Count
                Dim uniqueStaff As Integer = entries.Select(Function(e) e.UserID).Distinct().Count()
                Dim branchesUsed As Integer = entries.Select(Function(e) e.BranchID).Distinct().Count()

                ' Update summary labels
                labelTotalTasks.Text = $"Total Tasks: {totalTasks}"
                labelUniqueStaff.Text = $"Staff Members: {uniqueStaff}"
                labelBranchesActive.Text = $"Active Branches: {branchesUsed}"

                ' Most active branch
                If entries.Count > 0 Then
                    Dim mostActiveBranch = entries.GroupBy(Function(e) e.BranchName).
                                                 OrderByDescending(Function(g) g.Count()).
                                                 First()
                    labelMostActiveBranch.Text = $"Most Active: {mostActiveBranch.Key} ({mostActiveBranch.Count()} tasks)"
                End If

            Catch ex As Exception
                ' Continue without summary if there are issues
            End Try
        End Sub

        Private Sub ResetTaskForm()
            textBoxUserID.Clear()
            textBoxStaffName.Clear()
            textBoxTaskNotes.Clear()
            comboBoxTaskBranch.SelectedIndex = -1
            comboBoxTaskShift.SelectedIndex = -1
            checkedListBoxTasks.Items.Clear()
        End Sub

        Private Sub buttonResetTaskForm_Click(sender As Object, e As EventArgs) Handles buttonResetTaskForm.Click
            ResetTaskForm()
        End Sub

        Private Sub buttonClearFilters_Click(sender As Object, e As EventArgs) Handles buttonClearFilters.Click
            ' Reset all filter controls
            comboBoxFilterBranch.SelectedIndex = 0 ' "All Branches"
            comboBoxFilterShift.SelectedIndex = 0 ' "All Shifts"
            comboBoxFilterStaff.SelectedIndex = 0 ' "All Staff"
            dateTimePickerFrom.Value = DateTime.Now.AddDays(-30)
            dateTimePickerTo.Value = DateTime.Now

            ' Reload data with cleared filters
            LoadDashboardData()
        End Sub

        Private Sub buttonExportData_Click(sender As Object, e As EventArgs) Handles buttonExportData.Click
            Try
                If dataGridViewResults.DataSource Is Nothing OrElse dataGridViewResults.Rows.Count = 0 Then
                    MessageBox.Show("No data to export. Please apply filters to load data first.",
                           "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                Dim exportService As New ExportService()
                Dim currentData As List(Of ChoreLogEntry) = CType(dataGridViewResults.DataSource, List(Of ChoreLogEntry))

                ' Show format selection dialog
                Dim result As DialogResult = MessageBox.Show("Choose export format:" & vbCrLf & vbCrLf &
                                                    "YES = PDF" & vbCrLf &
                                                    "NO = Excel" & vbCrLf &
                                                    "CANCEL = CSV",
                                                    "Export Format",
                                                    MessageBoxButtons.YesNoCancel,
                                                    MessageBoxIcon.Question)

                Dim fileName As String = $"TaskReport_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}"
                Dim desktopPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)

                Select Case result
                    Case DialogResult.Yes ' PDF
                        Dim pdfPath As String = Path.Combine(desktopPath, fileName & ".pdf")
                        exportService.ExportToPDF(currentData, pdfPath)
                        MessageBox.Show($"PDF exported successfully to:{vbCrLf}{pdfPath}", "Export Complete")

                    Case DialogResult.No ' Excel
                        Dim excelPath As String = Path.Combine(desktopPath, fileName & ".xlsx")
                        exportService.ExportToExcel(currentData, excelPath)
                        MessageBox.Show($"Excel file exported successfully to:{vbCrLf}{excelPath}", "Export Complete")

                    Case DialogResult.Cancel ' CSV
                        Dim csvPath As String = Path.Combine(desktopPath, fileName & ".csv")
                        exportService.ExportToCSV(currentData, csvPath)
                        MessageBox.Show($"CSV file exported successfully to:{vbCrLf}{csvPath}", "Export Complete")
                End Select

            Catch ex As Exception
                MessageBox.Show($"Export error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub ApplyManagerStyling()
            Try
                ' Manager-specific color scheme
                Me.BackColor = Color.FromArgb(240, 248, 255) ' Light blue

                ' Tab styling
                tabControlManager.BackColor = Color.FromArgb(230, 240, 250)

                ' Button styling
                buttonSubmitTasks.BackColor = Color.FromArgb(0, 120, 70)
                buttonSubmitTasks.ForeColor = Color.White

                buttonApplyFilters.BackColor = Color.FromArgb(70, 130, 180)
                buttonApplyFilters.ForeColor = Color.White
            Catch ex As Exception
                ' Continue without styling if there are issues
            End Try
        End Sub


        ' NEW: Load all staff for the filter ComboBox
        Private Sub LoadAllStaffForFilter()
            Try
                Dim allStaff As List(Of Staff) = _databaseService.GetAllActiveStaff()

                ' Create filter list with "All Staff" option at the top
                Dim filterStaff As New List(Of Object) From {
                    New With {.UserID = "", .DisplayName = "All Staff"}
                }

                ' Add individual staff members sorted alphabetically by full name
                For Each staff In allStaff.OrderBy(Function(s) s.FullName)
                    filterStaff.Add(New With {
                        .UserID = staff.UserID,
                        .DisplayName = $"{staff.FullName} ({staff.UserID})"
                    })
                Next

                comboBoxFilterStaff.DataSource = filterStaff
                comboBoxFilterStaff.DisplayMember = "DisplayName"
                comboBoxFilterStaff.ValueMember = "UserID"
                comboBoxFilterStaff.SelectedIndex = 0 ' Select "All Staff" by default

            Catch ex As Exception
                MessageBox.Show($"Error loading staff filter: {ex.Message}", "Error")
            End Try
        End Sub

        Private Sub labelReportsTitle_Click(sender As Object, e As EventArgs) Handles labelReportsTitle.Click

        End Sub
    End Class
End Namespace