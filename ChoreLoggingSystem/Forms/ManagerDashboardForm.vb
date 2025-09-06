Imports ChoreLoggingSystem.Models
Imports ChoreLoggingSystem.Services

Namespace Forms
    Public Class ManagerDashboardForm
        Inherits Form

        Private ReadOnly _databaseService As DatabaseService
        Private ReadOnly _currentManager As Staff

        Public Sub New(loggedInManager As Staff)
            MessageBox.Show("Constructor called for: " & loggedInManager.FullName, "Debug")

            InitializeComponent()
            MessageBox.Show("InitializeComponent completed", "Debug")

            _currentManager = loggedInManager
            _databaseService = New DatabaseService()

            InitializeManagerDashboard()
            MessageBox.Show("InitializeManagerDashboard completed", "Debug")

            LoadInitialData()
            MessageBox.Show("LoadInitialData completed", "Debug")
        End Sub

        Private Sub LoadInitialData()
            Try
                ' Load branches and shifts
                LoadAllBranches()
                LoadAllShifts()

                ' Set default date range
                dateTimePickerFrom.Value = DateTime.Now.AddDays(-30)
                dateTimePickerTo.Value = DateTime.Now

                ' Load initial stats
                labelTotalTasks.Text = "Total Tasks: 0"
                labelUniqueStaff.Text = "Staff Members: 0"
                labelBranchesActive.Text = "Active Branches: 0"
                labelMostActiveBranch.Text = "Most Active: N/A"

                ' Clear results
                dataGridViewResults.Rows.Clear()

                ' Optionally load full dashboard data
                LoadDashboardData()

            Catch ex As Exception
                MessageBox.Show($"Error loading dashboard: {ex.Message}", "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub


        Private Sub LoadAllBranches()
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
        End Sub

        Private Sub LoadAllShifts()
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
                Dim fromDate As DateTime = dateTimePickerFrom.Value.Date
                Dim toDate As DateTime = dateTimePickerTo.Value.Date
                Dim branchId As Integer? = If(CInt(comboBoxFilterBranch.SelectedValue) = -1, Nothing, CInt(comboBoxFilterBranch.SelectedValue))
                Dim shiftId As Integer? = If(CInt(comboBoxFilterShift.SelectedValue) = -1, Nothing, CInt(comboBoxFilterShift.SelectedValue))
                Dim staffSearch As String = If(String.IsNullOrWhiteSpace(textBoxFilterStaff.Text), Nothing, textBoxFilterStaff.Text.Trim())

                Dim entries As List(Of ChoreLogEntry) = _databaseService.GetChoreLogEntries(fromDate, toDate, branchId, shiftId, staffSearch)

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

        ' Added missing FormatDataGrid method
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
            textBoxFilterStaff.Clear()
            dateTimePickerFrom.Value = DateTime.Now.AddDays(-30)
            dateTimePickerTo.Value = DateTime.Now

            ' Reload data with cleared filters
            LoadDashboardData()
        End Sub

        Private Sub buttonExportData_Click(sender As Object, e As EventArgs) Handles buttonExportData.Click
            Try
                ' Simple export functionality - can be enhanced later
                MessageBox.Show("Export functionality will be implemented in a future version.",
                              "Feature Coming Soon",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show($"Export error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub ApplyManagerStyling()
            ' Manager-specific color scheme
            Me.BackColor = Color.FromArgb(240, 248, 255) ' Light blue

            ' Tab styling
            tabControlManager.BackColor = Color.FromArgb(230, 240, 250)

            ' Button styling
            buttonSubmitTasks.BackColor = Color.FromArgb(0, 120, 70)
            buttonSubmitTasks.ForeColor = Color.White

            buttonApplyFilters.BackColor = Color.FromArgb(70, 130, 180)
            buttonApplyFilters.ForeColor = Color.White
        End Sub

        Friend WithEvents tabControlManager As TabControl
        Friend WithEvents tabPageTaskLogging As TabPage
        Friend WithEvents tabPageReports As TabPage

        Private Sub InitializeComponent()
            tabControlManager = New TabControl()
            tabPageTaskLogging = New TabPage()
            tabPageReports = New TabPage()
            panelTaskLogging = New Panel()
            labelTaskLoggingTitle = New Label()
            groupBoxTaskEntry = New GroupBox()
            labelUserID = New Label()
            textBoxUserID = New TextBox()
            labelStaffName = New Label()
            textBoxStaffName = New TextBox()
            labelTaskBranch = New Label()
            comboBoxTaskBranch = New ComboBox()
            labelTaskShift = New Label()
            comboBoxTaskShift = New ComboBox()
            labelTasks = New Label()
            checkedListBoxTasks = New CheckedListBox()
            labelTaskNotes = New Label()
            textBoxTaskNotes = New TextBox()
            buttonSubmitTasks = New Button()
            buttonResetTaskForm = New Button()
            Panel1 = New Panel()
            labelReportsTitle = New Label()
            groupBoxSummary = New GroupBox()
            labelTotalTasks = New Label()
            labelUniqueStaff = New Label()
            labelBranchesActive = New Label()
            labelMostActiveBranch = New Label()
            groupBoxFilters = New GroupBox()
            labelFilterFrom = New Label()
            dateTimePickerFrom = New DateTimePicker()
            labelFilterTo = New Label()
            dateTimePickerTo = New DateTimePicker()
            labelFilterBranch = New Label()
            comboBoxFilterBranch = New ComboBox()
            labelFilterShift = New Label()
            comboBoxFilterShift = New ComboBox()
            labelFilterStaff = New Label()
            textBoxFilterStaff = New TextBox()
            buttonApplyFilters = New Button()
            buttonClearFilters = New Button()
            buttonExportData = New Button()
            labelRecordsCount = New Label()
            dataGridViewResults = New DataGridView()
            tabControlManager.SuspendLayout()
            tabPageTaskLogging.SuspendLayout()
            tabPageReports.SuspendLayout()
            panelTaskLogging.SuspendLayout()
            groupBoxTaskEntry.SuspendLayout()
            Panel1.SuspendLayout()
            groupBoxSummary.SuspendLayout()
            groupBoxFilters.SuspendLayout()
            CType(dataGridViewResults, ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()
            ' 
            ' tabControlManager
            ' 
            tabControlManager.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            tabControlManager.Controls.Add(tabPageTaskLogging)
            tabControlManager.Controls.Add(tabPageReports)
            tabControlManager.Location = New Point(12, 12)
            tabControlManager.Name = "tabControlManager"
            tabControlManager.SelectedIndex = 0
            tabControlManager.Size = New Size(1160, 700)
            tabControlManager.TabIndex = 0
            ' 
            ' tabPageTaskLogging
            ' 
            tabPageTaskLogging.Controls.Add(panelTaskLogging)
            tabPageTaskLogging.Location = New Point(4, 24)
            tabPageTaskLogging.Name = "tabPageTaskLogging"
            tabPageTaskLogging.Padding = New Padding(3)
            tabPageTaskLogging.Size = New Size(1152, 672)
            tabPageTaskLogging.TabIndex = 0
            tabPageTaskLogging.Text = "Task Logging"
            tabPageTaskLogging.UseVisualStyleBackColor = True
            ' 
            ' tabPageReports
            ' 
            tabPageReports.Controls.Add(Panel1)
            tabPageReports.Location = New Point(4, 24)
            tabPageReports.Name = "tabPageReports"
            tabPageReports.Padding = New Padding(3)
            tabPageReports.Size = New Size(1152, 672)
            tabPageReports.TabIndex = 1
            tabPageReports.Text = "Reports & Analytics"
            tabPageReports.UseVisualStyleBackColor = True
            ' 
            ' panelTaskLogging
            ' 
            panelTaskLogging.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            panelTaskLogging.BackColor = Color.White
            panelTaskLogging.Controls.Add(groupBoxTaskEntry)
            panelTaskLogging.Controls.Add(labelTaskLoggingTitle)
            panelTaskLogging.Location = New Point(6, 6)
            panelTaskLogging.Name = "panelTaskLogging"
            panelTaskLogging.Size = New Size(1140, 657)
            panelTaskLogging.TabIndex = 0
            ' 
            ' labelTaskLoggingTitle
            ' 
            labelTaskLoggingTitle.AutoSize = True
            labelTaskLoggingTitle.Font = New Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelTaskLoggingTitle.ForeColor = Color.DarkBlue
            labelTaskLoggingTitle.Location = New Point(20, 20)
            labelTaskLoggingTitle.Name = "labelTaskLoggingTitle"
            labelTaskLoggingTitle.Size = New Size(441, 30)
            labelTaskLoggingTitle.TabIndex = 0
            labelTaskLoggingTitle.Text = "Task Logging - Log Tasks for Staff Members"
            ' 
            ' groupBoxTaskEntry
            ' 
            groupBoxTaskEntry.BackColor = Color.WhiteSmoke
            groupBoxTaskEntry.Controls.Add(buttonResetTaskForm)
            groupBoxTaskEntry.Controls.Add(buttonSubmitTasks)
            groupBoxTaskEntry.Controls.Add(textBoxTaskNotes)
            groupBoxTaskEntry.Controls.Add(labelTaskNotes)
            groupBoxTaskEntry.Controls.Add(checkedListBoxTasks)
            groupBoxTaskEntry.Controls.Add(labelTasks)
            groupBoxTaskEntry.Controls.Add(comboBoxTaskShift)
            groupBoxTaskEntry.Controls.Add(labelTaskShift)
            groupBoxTaskEntry.Controls.Add(comboBoxTaskBranch)
            groupBoxTaskEntry.Controls.Add(labelTaskBranch)
            groupBoxTaskEntry.Controls.Add(textBoxStaffName)
            groupBoxTaskEntry.Controls.Add(labelStaffName)
            groupBoxTaskEntry.Controls.Add(textBoxUserID)
            groupBoxTaskEntry.Controls.Add(labelUserID)
            groupBoxTaskEntry.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            groupBoxTaskEntry.Location = New Point(20, 70)
            groupBoxTaskEntry.Name = "groupBoxTaskEntry"
            groupBoxTaskEntry.Size = New Size(1100, 570)
            groupBoxTaskEntry.TabIndex = 1
            groupBoxTaskEntry.TabStop = False
            groupBoxTaskEntry.Text = "Task Entry Form"
            ' 
            ' labelUserID
            ' 
            labelUserID.AutoSize = True
            labelUserID.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelUserID.Location = New Point(30, 40)
            labelUserID.Name = "labelUserID"
            labelUserID.Size = New Size(90, 17)
            labelUserID.TabIndex = 0
            labelUserID.Text = "Staff User ID:"
            ' 
            ' textBoxUserID
            ' 
            textBoxUserID.CharacterCasing = CharacterCasing.Upper
            textBoxUserID.Font = New Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
            textBoxUserID.Location = New Point(180, 38)
            textBoxUserID.MaxLength = 10
            textBoxUserID.Name = "textBoxUserID"
            textBoxUserID.Size = New Size(200, 27)
            textBoxUserID.TabIndex = 1
            ' 
            ' labelStaffName
            ' 
            labelStaffName.AutoSize = True
            labelStaffName.Location = New Point(420, 40)
            labelStaffName.Name = "labelStaffName"
            labelStaffName.Size = New Size(93, 20)
            labelStaffName.TabIndex = 2
            labelStaffName.Text = "Staff Name:"
            ' 
            ' textBoxStaffName
            ' 
            textBoxStaffName.Location = New Point(550, 38)
            textBoxStaffName.Name = "textBoxStaffName"
            textBoxStaffName.ReadOnly = True
            textBoxStaffName.Size = New Size(250, 27)
            textBoxStaffName.TabIndex = 3
            textBoxStaffName.Text = "Enter User ID to auto-fill name"
            ' 
            ' labelTaskBranch
            ' 
            labelTaskBranch.AutoSize = True
            labelTaskBranch.Location = New Point(30, 90)
            labelTaskBranch.Name = "labelTaskBranch"
            labelTaskBranch.Size = New Size(73, 20)
            labelTaskBranch.TabIndex = 4
            labelTaskBranch.Text = "Location:"
            ' 
            ' comboBoxTaskBranch
            ' 
            comboBoxTaskBranch.DropDownStyle = ComboBoxStyle.DropDownList
            comboBoxTaskBranch.Font = New Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
            comboBoxTaskBranch.FormattingEnabled = True
            comboBoxTaskBranch.Location = New Point(180, 88)
            comboBoxTaskBranch.Name = "comboBoxTaskBranch"
            comboBoxTaskBranch.Size = New Size(250, 28)
            comboBoxTaskBranch.TabIndex = 5
            ' 
            ' labelTaskShift
            ' 
            labelTaskShift.AutoSize = True
            labelTaskShift.Location = New Point(470, 90)
            labelTaskShift.Name = "labelTaskShift"
            labelTaskShift.Size = New Size(88, 20)
            labelTaskShift.TabIndex = 6
            labelTaskShift.Text = "Work Shift:"
            ' 
            ' comboBoxTaskShift
            ' 
            comboBoxTaskShift.DropDownStyle = ComboBoxStyle.DropDownList
            comboBoxTaskShift.FormattingEnabled = True
            comboBoxTaskShift.Location = New Point(600, 88)
            comboBoxTaskShift.Name = "comboBoxTaskShift"
            comboBoxTaskShift.Size = New Size(280, 28)
            comboBoxTaskShift.TabIndex = 7
            ' 
            ' labelTasks
            ' 
            labelTasks.AutoSize = True
            labelTasks.Location = New Point(30, 140)
            labelTasks.Name = "labelTasks"
            labelTasks.Size = New Size(255, 20)
            labelTasks.TabIndex = 8
            labelTasks.Text = "Completed Tasks for Selected Shift:"
            ' 
            ' checkedListBoxTasks
            ' 
            checkedListBoxTasks.BackColor = Color.White
            checkedListBoxTasks.BorderStyle = BorderStyle.FixedSingle
            checkedListBoxTasks.CheckOnClick = True
            checkedListBoxTasks.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
            checkedListBoxTasks.FormattingEnabled = True
            checkedListBoxTasks.Location = New Point(30, 170)
            checkedListBoxTasks.Name = "checkedListBoxTasks"
            checkedListBoxTasks.Size = New Size(1040, 200)
            checkedListBoxTasks.TabIndex = 9
            ' 
            ' labelTaskNotes
            ' 
            labelTaskNotes.AutoSize = True
            labelTaskNotes.Location = New Point(30, 390)
            labelTaskNotes.Name = "labelTaskNotes"
            labelTaskNotes.Size = New Size(132, 20)
            labelTaskNotes.TabIndex = 10
            labelTaskNotes.Text = "Additional Notes:"
            ' 
            ' textBoxTaskNotes
            ' 
            textBoxTaskNotes.BorderStyle = BorderStyle.FixedSingle
            textBoxTaskNotes.Location = New Point(30, 420)
            textBoxTaskNotes.Multiline = True
            textBoxTaskNotes.Name = "textBoxTaskNotes"
            textBoxTaskNotes.ScrollBars = ScrollBars.Vertical
            textBoxTaskNotes.Size = New Size(1040, 70)
            textBoxTaskNotes.TabIndex = 11
            ' 
            ' buttonSubmitTasks
            ' 
            buttonSubmitTasks.BackColor = Color.Green
            buttonSubmitTasks.FlatStyle = FlatStyle.Flat
            buttonSubmitTasks.ForeColor = Color.White
            buttonSubmitTasks.Location = New Point(30, 510)
            buttonSubmitTasks.Name = "buttonSubmitTasks"
            buttonSubmitTasks.Size = New Size(200, 40)
            buttonSubmitTasks.TabIndex = 12
            buttonSubmitTasks.Text = "Submit Tasks"
            buttonSubmitTasks.UseVisualStyleBackColor = False
            ' 
            ' buttonResetTaskForm
            ' 
            buttonResetTaskForm.BackColor = Color.Gray
            buttonResetTaskForm.FlatStyle = FlatStyle.Flat
            buttonResetTaskForm.ForeColor = Color.White
            buttonResetTaskForm.Location = New Point(250, 510)
            buttonResetTaskForm.Name = "buttonResetTaskForm"
            buttonResetTaskForm.Size = New Size(150, 40)
            buttonResetTaskForm.TabIndex = 13
            buttonResetTaskForm.Text = "Reset Form"
            buttonResetTaskForm.UseVisualStyleBackColor = False
            ' 
            ' Panel1
            ' 
            Panel1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            Panel1.Controls.Add(dataGridViewResults)
            Panel1.Controls.Add(labelRecordsCount)
            Panel1.Controls.Add(groupBoxFilters)
            Panel1.Controls.Add(groupBoxSummary)
            Panel1.Controls.Add(labelReportsTitle)
            Panel1.Location = New Point(6, 6)
            Panel1.Name = "Panel1"
            Panel1.Size = New Size(1140, 657)
            Panel1.TabIndex = 0
            ' 
            ' labelReportsTitle
            ' 
            labelReportsTitle.AutoSize = True
            labelReportsTitle.Font = New Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelReportsTitle.Location = New Point(20, 20)
            labelReportsTitle.Name = "labelReportsTitle"
            labelReportsTitle.Size = New Size(308, 30)
            labelReportsTitle.TabIndex = 0
            labelReportsTitle.Text = "Reports &  Analytics Dashboard"
            ' 
            ' groupBoxSummary
            ' 
            groupBoxSummary.BackColor = Color.Gainsboro
            groupBoxSummary.Controls.Add(labelMostActiveBranch)
            groupBoxSummary.Controls.Add(labelBranchesActive)
            groupBoxSummary.Controls.Add(labelUniqueStaff)
            groupBoxSummary.Controls.Add(labelTotalTasks)
            groupBoxSummary.Location = New Point(20, 70)
            groupBoxSummary.Name = "groupBoxSummary"
            groupBoxSummary.Size = New Size(1100, 80)
            groupBoxSummary.TabIndex = 1
            groupBoxSummary.TabStop = False
            groupBoxSummary.Text = "Summary Statistics"
            ' 
            ' labelTotalTasks
            ' 
            labelTotalTasks.AutoSize = True
            labelTotalTasks.BackColor = Color.Purple
            labelTotalTasks.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelTotalTasks.ForeColor = Color.White
            labelTotalTasks.Location = New Point(30, 35)
            labelTotalTasks.Name = "labelTotalTasks"
            labelTotalTasks.Padding = New Padding(10, 5, 10, 5)
            labelTotalTasks.Size = New Size(111, 27)
            labelTotalTasks.TabIndex = 2
            labelTotalTasks.Text = "Total Tasks: 0"
            ' 
            ' labelUniqueStaff
            ' 
            labelUniqueStaff.AutoSize = True
            labelUniqueStaff.BackColor = Color.Green
            labelUniqueStaff.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelUniqueStaff.ForeColor = Color.White
            labelUniqueStaff.Location = New Point(280, 35)
            labelUniqueStaff.Name = "labelUniqueStaff"
            labelUniqueStaff.Padding = New Padding(10, 5, 10, 5)
            labelUniqueStaff.Size = New Size(133, 27)
            labelUniqueStaff.TabIndex = 3
            labelUniqueStaff.Text = "Staff Members: 0"
            ' 
            ' labelBranchesActive
            ' 
            labelBranchesActive.AutoSize = True
            labelBranchesActive.BackColor = Color.Orange
            labelBranchesActive.ForeColor = Color.White
            labelBranchesActive.Location = New Point(550, 35)
            labelBranchesActive.Name = "labelBranchesActive"
            labelBranchesActive.Padding = New Padding(10, 5, 10, 5)
            labelBranchesActive.Size = New Size(123, 25)
            labelBranchesActive.TabIndex = 2
            labelBranchesActive.Text = "Active Branches: 0"
            ' 
            ' labelMostActiveBranch
            ' 
            labelMostActiveBranch.AutoSize = True
            labelMostActiveBranch.BackColor = Color.Red
            labelMostActiveBranch.ForeColor = Color.White
            labelMostActiveBranch.Location = New Point(830, 35)
            labelMostActiveBranch.Name = "labelMostActiveBranch"
            labelMostActiveBranch.Padding = New Padding(10, 5, 10, 5)
            labelMostActiveBranch.Size = New Size(118, 25)
            labelMostActiveBranch.TabIndex = 2
            labelMostActiveBranch.Text = "Most Active: N/A"
            ' 
            ' groupBoxFilters
            ' 
            groupBoxFilters.Controls.Add(buttonExportData)
            groupBoxFilters.Controls.Add(buttonClearFilters)
            groupBoxFilters.Controls.Add(buttonApplyFilters)
            groupBoxFilters.Controls.Add(textBoxFilterStaff)
            groupBoxFilters.Controls.Add(labelFilterStaff)
            groupBoxFilters.Controls.Add(comboBoxFilterShift)
            groupBoxFilters.Controls.Add(labelFilterShift)
            groupBoxFilters.Controls.Add(comboBoxFilterBranch)
            groupBoxFilters.Controls.Add(labelFilterBranch)
            groupBoxFilters.Controls.Add(dateTimePickerTo)
            groupBoxFilters.Controls.Add(labelFilterTo)
            groupBoxFilters.Controls.Add(dateTimePickerFrom)
            groupBoxFilters.Controls.Add(labelFilterFrom)
            groupBoxFilters.Location = New Point(20, 170)
            groupBoxFilters.Name = "groupBoxFilters"
            groupBoxFilters.Size = New Size(1100, 120)
            groupBoxFilters.TabIndex = 2
            groupBoxFilters.TabStop = False
            groupBoxFilters.Text = "Filters & Options"
            ' 
            ' labelFilterFrom
            ' 
            labelFilterFrom.AutoSize = True
            labelFilterFrom.Location = New Point(30, 35)
            labelFilterFrom.Name = "labelFilterFrom"
            labelFilterFrom.Size = New Size(65, 15)
            labelFilterFrom.TabIndex = 0
            labelFilterFrom.Text = "From Date:"
            ' 
            ' dateTimePickerFrom
            ' 
            dateTimePickerFrom.Format = DateTimePickerFormat.Short
            dateTimePickerFrom.Location = New Point(130, 33)
            dateTimePickerFrom.Name = "dateTimePickerFrom"
            dateTimePickerFrom.Size = New Size(150, 23)
            dateTimePickerFrom.TabIndex = 1
            ' 
            ' labelFilterTo
            ' 
            labelFilterTo.AutoSize = True
            labelFilterTo.Location = New Point(300, 35)
            labelFilterTo.Name = "labelFilterTo"
            labelFilterTo.Size = New Size(49, 15)
            labelFilterTo.TabIndex = 2
            labelFilterTo.Text = "To Date:"
            ' 
            ' dateTimePickerTo
            ' 
            dateTimePickerTo.Format = DateTimePickerFormat.Short
            dateTimePickerTo.Location = New Point(380, 33)
            dateTimePickerTo.Name = "dateTimePickerTo"
            dateTimePickerTo.Size = New Size(150, 23)
            dateTimePickerTo.TabIndex = 3
            ' 
            ' labelFilterBranch
            ' 
            labelFilterBranch.AutoSize = True
            labelFilterBranch.Location = New Point(550, 35)
            labelFilterBranch.Name = "labelFilterBranch"
            labelFilterBranch.Size = New Size(47, 15)
            labelFilterBranch.TabIndex = 4
            labelFilterBranch.Text = "Branch:"
            ' 
            ' comboBoxFilterBranch
            ' 
            comboBoxFilterBranch.DropDownStyle = ComboBoxStyle.DropDownList
            comboBoxFilterBranch.FormattingEnabled = True
            comboBoxFilterBranch.Location = New Point(630, 33)
            comboBoxFilterBranch.Name = "comboBoxFilterBranch"
            comboBoxFilterBranch.Size = New Size(180, 23)
            comboBoxFilterBranch.TabIndex = 5
            ' 
            ' labelFilterShift
            ' 
            labelFilterShift.AutoSize = True
            labelFilterShift.Location = New Point(830, 35)
            labelFilterShift.Name = "labelFilterShift"
            labelFilterShift.Size = New Size(34, 15)
            labelFilterShift.TabIndex = 6
            labelFilterShift.Text = "Shift:"
            ' 
            ' comboBoxFilterShift
            ' 
            comboBoxFilterShift.DropDownStyle = ComboBoxStyle.DropDownList
            comboBoxFilterShift.FormattingEnabled = True
            comboBoxFilterShift.Location = New Point(900, 33)
            comboBoxFilterShift.Name = "comboBoxFilterShift"
            comboBoxFilterShift.Size = New Size(150, 23)
            comboBoxFilterShift.TabIndex = 7
            ' 
            ' labelFilterStaff
            ' 
            labelFilterStaff.AutoSize = True
            labelFilterStaff.Location = New Point(30, 75)
            labelFilterStaff.Name = "labelFilterStaff"
            labelFilterStaff.Size = New Size(68, 15)
            labelFilterStaff.TabIndex = 8
            labelFilterStaff.Text = "Staff Initials"
            ' 
            ' textBoxFilterStaff
            ' 
            textBoxFilterStaff.CharacterCasing = CharacterCasing.Upper
            textBoxFilterStaff.Location = New Point(140, 73)
            textBoxFilterStaff.Name = "textBoxFilterStaff"
            textBoxFilterStaff.Size = New Size(150, 23)
            textBoxFilterStaff.TabIndex = 9
            ' 
            ' buttonApplyFilters
            ' 
            buttonApplyFilters.BackColor = Color.Blue
            buttonApplyFilters.ForeColor = Color.White
            buttonApplyFilters.Location = New Point(330, 70)
            buttonApplyFilters.Name = "buttonApplyFilters"
            buttonApplyFilters.Size = New Size(130, 30)
            buttonApplyFilters.TabIndex = 3
            buttonApplyFilters.Text = "Apply Filters"
            buttonApplyFilters.UseVisualStyleBackColor = False
            ' 
            ' buttonClearFilters
            ' 
            buttonClearFilters.BackColor = Color.Gray
            buttonClearFilters.ForeColor = Color.White
            buttonClearFilters.Location = New Point(480, 70)
            buttonClearFilters.Name = "buttonClearFilters"
            buttonClearFilters.Size = New Size(130, 30)
            buttonClearFilters.TabIndex = 10
            buttonClearFilters.Text = "Clear Filter"
            buttonClearFilters.UseVisualStyleBackColor = False
            ' 
            ' buttonExportData
            ' 
            buttonExportData.BackColor = Color.Green
            buttonExportData.ForeColor = Color.White
            buttonExportData.Location = New Point(630, 70)
            buttonExportData.Name = "buttonExportData"
            buttonExportData.Size = New Size(130, 30)
            buttonExportData.TabIndex = 11
            buttonExportData.Text = "Export Data"
            buttonExportData.UseVisualStyleBackColor = False
            ' 
            ' labelRecordsCount
            ' 
            labelRecordsCount.AutoSize = True
            labelRecordsCount.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelRecordsCount.Location = New Point(20, 310)
            labelRecordsCount.Name = "labelRecordsCount"
            labelRecordsCount.Size = New Size(130, 20)
            labelRecordsCount.TabIndex = 3
            labelRecordsCount.Text = "Records Found: 0"
            ' 
            ' dataGridViewResults
            ' 
            dataGridViewResults.AllowUserToAddRows = False
            dataGridViewResults.AllowUserToDeleteRows = False
            dataGridViewResults.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            dataGridViewResults.BackgroundColor = Color.White
            dataGridViewResults.BorderStyle = BorderStyle.Fixed3D
            dataGridViewResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            dataGridViewResults.Location = New Point(20, 340)
            dataGridViewResults.Name = "dataGridViewResults"
            dataGridViewResults.ReadOnly = True
            dataGridViewResults.RowHeadersVisible = False
            dataGridViewResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dataGridViewResults.Size = New Size(1100, 300)
            dataGridViewResults.TabIndex = 4
            ' 
            ' ManagerDashboardForm
            ' 
            BackColor = Color.LightBlue
            ClientSize = New Size(1184, 721)
            Controls.Add(tabControlManager)
            Name = "ManagerDashboardForm"
            StartPosition = FormStartPosition.CenterScreen
            WindowState = FormWindowState.Maximized
            tabControlManager.ResumeLayout(False)
            tabPageTaskLogging.ResumeLayout(False)
            tabPageReports.ResumeLayout(False)
            panelTaskLogging.ResumeLayout(False)
            panelTaskLogging.PerformLayout()
            groupBoxTaskEntry.ResumeLayout(False)
            groupBoxTaskEntry.PerformLayout()
            Panel1.ResumeLayout(False)
            Panel1.PerformLayout()
            groupBoxSummary.ResumeLayout(False)
            groupBoxSummary.PerformLayout()
            groupBoxFilters.ResumeLayout(False)
            groupBoxFilters.PerformLayout()
            CType(dataGridViewResults, ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)

        End Sub

        Friend WithEvents panelTaskLogging As Panel
        Friend WithEvents labelTaskLoggingTitle As Label
        Friend WithEvents groupBoxTaskEntry As GroupBox
        Friend WithEvents labelUserID As Label
        Friend WithEvents textBoxUserID As TextBox
        Friend WithEvents textBoxStaffName As TextBox
        Friend WithEvents labelStaffName As Label
        Friend WithEvents comboBoxTaskBranch As ComboBox
        Friend WithEvents labelTaskBranch As Label
        Friend WithEvents labelTasks As Label
        Friend WithEvents comboBoxTaskShift As ComboBox
        Friend WithEvents labelTaskShift As Label
        Friend WithEvents checkedListBoxTasks As CheckedListBox
        Friend WithEvents labelTaskNotes As Label
        Friend WithEvents buttonSubmitTasks As Button
        Friend WithEvents textBoxTaskNotes As TextBox
        Friend WithEvents buttonResetTaskForm As Button
        Friend WithEvents Panel1 As Panel
        Friend WithEvents groupBoxSummary As GroupBox
        Friend WithEvents labelReportsTitle As Label
        Friend WithEvents labelTotalTasks As Label
        Friend WithEvents labelUniqueStaff As Label
        Friend WithEvents labelMostActiveBranch As Label
        Friend WithEvents labelBranchesActive As Label
        Friend WithEvents groupBoxFilters As GroupBox
        Friend WithEvents dateTimePickerFrom As DateTimePicker
        Friend WithEvents labelFilterFrom As Label
        Friend WithEvents labelFilterBranch As Label
        Friend WithEvents dateTimePickerTo As DateTimePicker
        Friend WithEvents labelFilterTo As Label
        Friend WithEvents comboBoxFilterBranch As ComboBox
        Friend WithEvents comboBoxFilterShift As ComboBox
        Friend WithEvents labelFilterShift As Label
        Friend WithEvents labelFilterStaff As Label
        Friend WithEvents textBoxFilterStaff As TextBox
        Friend WithEvents buttonExportData As Button
        Friend WithEvents buttonClearFilters As Button
        Friend WithEvents buttonApplyFilters As Button
        Friend WithEvents labelRecordsCount As Label
        Friend WithEvents dataGridViewResults As DataGridView

        Private Sub InitializeManagerDashboard()
            comboBoxTaskBranch.Items.Clear()
            comboBoxTaskBranch.Items.AddRange(New String() {"Branch A", "Branch B", "Branch C"})

            comboBoxTaskShift.Items.Clear()
            comboBoxTaskShift.Items.AddRange(New String() {"Morning", "Afternoon", "Night"})

            comboBoxTaskBranch.SelectedIndex = 0
            comboBoxTaskShift.SelectedIndex = 0
        End Sub

    End Class
End Namespace


