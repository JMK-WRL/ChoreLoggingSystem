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

                ' Set default date range
                DateTimePickerFrom.Value = DateTime.Now.AddDays(-30)
                DateTimePickerTo.Value = DateTime.Now

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

                ComboBoxFilterBranch.DataSource = filterBranches
                ComboBoxFilterBranch.DisplayMember = "BranchName"
                ComboBoxFilterBranch.ValueMember = "BranchID"
                ComboBoxFilterBranch.SelectedIndex = 0
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

                ComboBoxFilterShift.DataSource = filterShifts
                ComboBoxFilterShift.DisplayMember = "ShiftName"
                ComboBoxFilterShift.ValueMember = "ShiftID"
                ComboBoxFilterShift.SelectedIndex = 0
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
                Dim fromDate As DateTime = DateTimePickerFrom.Value.Date
                Dim toDate As DateTime = DateTimePickerTo.Value.Date
                Dim branchId As Integer? = If(CInt(ComboBoxFilterBranch.SelectedValue) = -1, Nothing, CInt(ComboBoxFilterBranch.SelectedValue))
                Dim shiftId As Integer? = If(CInt(ComboBoxFilterShift.SelectedValue) = -1, Nothing, CInt(ComboBoxFilterShift.SelectedValue))
                Dim staffSearch As String = If(String.IsNullOrWhiteSpace(TextBoxFilterStaff.Text), Nothing, TextBoxFilterStaff.Text.Trim())

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
            ComboBoxFilterBranch.SelectedIndex = 0 ' "All Branches"
            ComboBoxFilterShift.SelectedIndex = 0 ' "All Shifts"
            TextBoxFilterStaff.Clear()
            DateTimePickerFrom.Value = DateTime.Now.AddDays(-30)
            DateTimePickerTo.Value = DateTime.Now

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

        Friend WithEvents tabControlManager As TabControl
        Friend WithEvents panelTaskLogging As TabPage
        Friend WithEvents panelReports As TabPage

        Private Sub InitializeComponent()
            tabControlManager = New TabControl()
            panelTaskLogging = New TabPage()
            Panel1 = New Panel()
            buttonSubmitTasks = New Button()
            textBoxTaskNotes = New TextBox()
            labelTaskNotes = New Label()
            checkListBoxTasks = New CheckedListBox()
            labelTasks = New Label()
            textBoxStaffName = New TextBox()
            labelStaffName = New Label()
            textBoxUserID = New TextBox()
            labelUserID = New Label()
            comboBoxTaskShift = New ComboBox()
            labelShift = New Label()
            comboBoxTaskBranch = New ComboBox()
            labelBranch = New Label()
            panelReports = New TabPage()
            Panel2 = New Panel()
            Label1 = New Label()
            ComboBoxFilterBranch = New ComboBox()
            Label2 = New Label()
            ComboBoxFilterShift = New ComboBox()
            Label3 = New Label()
            DateTimePickerFrom = New DateTimePicker()
            Label4 = New Label()
            DateTimePickerTo = New DateTimePicker()
            Label5 = New Label()
            TextBoxFilterStaff = New TextBox()
            buttonApplyFilters = New Button()
            dataGridViewResults = New DataGridView()
            labelRecordsCount = New Label()
            labelTotalTasks = New Label()
            labelUniqueStaff = New Label()
            labelBrancesActive = New Label()
            labelMostActiveBranch = New Label()
            tabControlManager.SuspendLayout()
            panelTaskLogging.SuspendLayout()
            Panel1.SuspendLayout()
            panelReports.SuspendLayout()
            Panel2.SuspendLayout()
            CType(dataGridViewResults, ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()
            ' 
            ' tabControlManager
            ' 
            tabControlManager.Controls.Add(panelTaskLogging)
            tabControlManager.Controls.Add(panelReports)
            tabControlManager.Location = New Point(10, 10)
            tabControlManager.Name = "tabControlManager"
            tabControlManager.SelectedIndex = 0
            tabControlManager.Size = New Size(960, 580)
            tabControlManager.TabIndex = 0
            ' 
            ' panelTaskLogging
            ' 
            panelTaskLogging.Controls.Add(Panel1)
            panelTaskLogging.Location = New Point(4, 24)
            panelTaskLogging.Name = "panelTaskLogging"
            panelTaskLogging.Padding = New Padding(3)
            panelTaskLogging.Size = New Size(952, 552)
            panelTaskLogging.TabIndex = 0
            panelTaskLogging.Text = "Task Logging"
            panelTaskLogging.UseVisualStyleBackColor = True
            ' 
            ' Panel1
            ' 
            Panel1.Controls.Add(buttonSubmitTasks)
            Panel1.Controls.Add(textBoxTaskNotes)
            Panel1.Controls.Add(labelTaskNotes)
            Panel1.Controls.Add(checkListBoxTasks)
            Panel1.Controls.Add(labelTasks)
            Panel1.Controls.Add(textBoxStaffName)
            Panel1.Controls.Add(labelStaffName)
            Panel1.Controls.Add(textBoxUserID)
            Panel1.Controls.Add(labelUserID)
            Panel1.Controls.Add(comboBoxTaskShift)
            Panel1.Controls.Add(labelShift)
            Panel1.Controls.Add(comboBoxTaskBranch)
            Panel1.Controls.Add(labelBranch)
            Panel1.Location = New Point(10, 10)
            Panel1.Name = "Panel1"
            Panel1.Size = New Size(910, 530)
            Panel1.TabIndex = 0
            ' 
            ' buttonSubmitTasks
            ' 
            buttonSubmitTasks.Location = New Point(120, 420)
            buttonSubmitTasks.Name = "buttonSubmitTasks"
            buttonSubmitTasks.Size = New Size(120, 40)
            buttonSubmitTasks.TabIndex = 12
            buttonSubmitTasks.Text = "Submit"
            buttonSubmitTasks.UseVisualStyleBackColor = True
            ' 
            ' textBoxTaskNotes
            ' 
            textBoxTaskNotes.Location = New Point(120, 345)
            textBoxTaskNotes.Multiline = True
            textBoxTaskNotes.Name = "textBoxTaskNotes"
            textBoxTaskNotes.Size = New Size(500, 60)
            textBoxTaskNotes.TabIndex = 11
            ' 
            ' labelTaskNotes
            ' 
            labelTaskNotes.AutoSize = True
            labelTaskNotes.Location = New Point(20, 350)
            labelTaskNotes.Name = "labelTaskNotes"
            labelTaskNotes.Size = New Size(67, 15)
            labelTaskNotes.TabIndex = 10
            labelTaskNotes.Text = "Task Notes:"
            ' 
            ' checkListBoxTasks
            ' 
            checkListBoxTasks.FormattingEnabled = True
            checkListBoxTasks.Location = New Point(120, 180)
            checkListBoxTasks.Name = "checkListBoxTasks"
            checkListBoxTasks.Size = New Size(350, 148)
            checkListBoxTasks.TabIndex = 9
            ' 
            ' labelTasks
            ' 
            labelTasks.AutoSize = True
            labelTasks.Location = New Point(20, 180)
            labelTasks.Name = "labelTasks"
            labelTasks.Size = New Size(38, 15)
            labelTasks.TabIndex = 8
            labelTasks.Text = "Tasks:"
            ' 
            ' textBoxStaffName
            ' 
            textBoxStaffName.Location = New Point(120, 135)
            textBoxStaffName.Name = "textBoxStaffName"
            textBoxStaffName.ReadOnly = True
            textBoxStaffName.Size = New Size(250, 23)
            textBoxStaffName.TabIndex = 7
            ' 
            ' labelStaffName
            ' 
            labelStaffName.AutoSize = True
            labelStaffName.Location = New Point(20, 140)
            labelStaffName.Name = "labelStaffName"
            labelStaffName.Size = New Size(69, 15)
            labelStaffName.TabIndex = 6
            labelStaffName.Text = "Staff Name:"
            ' 
            ' textBoxUserID
            ' 
            textBoxUserID.Location = New Point(120, 95)
            textBoxUserID.Name = "textBoxUserID"
            textBoxUserID.Size = New Size(250, 23)
            textBoxUserID.TabIndex = 5
            ' 
            ' labelUserID
            ' 
            labelUserID.AutoSize = True
            labelUserID.Location = New Point(20, 100)
            labelUserID.Name = "labelUserID"
            labelUserID.Size = New Size(47, 15)
            labelUserID.TabIndex = 4
            labelUserID.Text = "User ID:"
            ' 
            ' comboBoxTaskShift
            ' 
            comboBoxTaskShift.FormattingEnabled = True
            comboBoxTaskShift.Location = New Point(120, 55)
            comboBoxTaskShift.Name = "comboBoxTaskShift"
            comboBoxTaskShift.Size = New Size(250, 23)
            comboBoxTaskShift.TabIndex = 3
            ' 
            ' labelShift
            ' 
            labelShift.AutoSize = True
            labelShift.Location = New Point(20, 60)
            labelShift.Name = "labelShift"
            labelShift.Size = New Size(34, 15)
            labelShift.TabIndex = 2
            labelShift.Text = "Shift:"
            ' 
            ' comboBoxTaskBranch
            ' 
            comboBoxTaskBranch.FormattingEnabled = True
            comboBoxTaskBranch.Location = New Point(120, 15)
            comboBoxTaskBranch.Name = "comboBoxTaskBranch"
            comboBoxTaskBranch.Size = New Size(250, 23)
            comboBoxTaskBranch.TabIndex = 1
            ' 
            ' labelBranch
            ' 
            labelBranch.AutoSize = True
            labelBranch.Location = New Point(20, 20)
            labelBranch.Name = "labelBranch"
            labelBranch.Size = New Size(47, 15)
            labelBranch.TabIndex = 0
            labelBranch.Text = "Branch:"
            ' 
            ' panelReports
            ' 
            panelReports.Controls.Add(Panel2)
            panelReports.Location = New Point(4, 24)
            panelReports.Name = "panelReports"
            panelReports.Padding = New Padding(3)
            panelReports.Size = New Size(952, 552)
            panelReports.TabIndex = 1
            panelReports.Text = "Reports & Analytics"
            panelReports.UseVisualStyleBackColor = True
            ' 
            ' Panel2
            ' 
            Panel2.Controls.Add(labelMostActiveBranch)
            Panel2.Controls.Add(labelBrancesActive)
            Panel2.Controls.Add(labelUniqueStaff)
            Panel2.Controls.Add(labelTotalTasks)
            Panel2.Controls.Add(labelRecordsCount)
            Panel2.Controls.Add(dataGridViewResults)
            Panel2.Controls.Add(buttonApplyFilters)
            Panel2.Controls.Add(TextBoxFilterStaff)
            Panel2.Controls.Add(Label5)
            Panel2.Controls.Add(DateTimePickerTo)
            Panel2.Controls.Add(Label4)
            Panel2.Controls.Add(DateTimePickerFrom)
            Panel2.Controls.Add(Label3)
            Panel2.Controls.Add(ComboBoxFilterShift)
            Panel2.Controls.Add(Label2)
            Panel2.Controls.Add(ComboBoxFilterBranch)
            Panel2.Controls.Add(Label1)
            Panel2.Location = New Point(10, 10)
            Panel2.Name = "Panel2"
            Panel2.Size = New Size(910, 530)
            Panel2.TabIndex = 0
            ' 
            ' Label1
            ' 
            Label1.AutoSize = True
            Label1.Location = New Point(20, 20)
            Label1.Name = "Label1"
            Label1.Size = New Size(47, 15)
            Label1.TabIndex = 0
            Label1.Text = "Branch:"
            AddHandler Label1.Click, New EventHandler(Me.Label1_Click)
            ' 
            ' ComboBoxFilterBranch
            ' 
            ComboBoxFilterBranch.FormattingEnabled = True
            ComboBoxFilterBranch.Location = New Point(120, 15)
            ComboBoxFilterBranch.Name = "ComboBoxFilterBranch"
            ComboBoxFilterBranch.Size = New Size(250, 23)
            ComboBoxFilterBranch.TabIndex = 1
            ' 
            ' Label2
            ' 
            Label2.AutoSize = True
            Label2.Location = New Point(400, 20)
            Label2.Name = "Label2"
            Label2.Size = New Size(34, 15)
            Label2.TabIndex = 2
            Label2.Text = "Shift:"
            ' 
            ' ComboBoxFilterShift
            ' 
            ComboBoxFilterShift.FormattingEnabled = True
            ComboBoxFilterShift.Location = New Point(480, 15)
            ComboBoxFilterShift.Name = "ComboBoxFilterShift"
            ComboBoxFilterShift.Size = New Size(250, 23)
            ComboBoxFilterShift.TabIndex = 3
            ' 
            ' Label3
            ' 
            Label3.AutoSize = True
            Label3.Location = New Point(20, 60)
            Label3.Name = "Label3"
            Label3.Size = New Size(38, 15)
            Label3.TabIndex = 4
            Label3.Text = "From:"
            ' 
            ' DateTimePickerFrom
            ' 
            DateTimePickerFrom.Location = New Point(120, 55)
            DateTimePickerFrom.Name = "DateTimePickerFrom"
            DateTimePickerFrom.Size = New Size(250, 23)
            DateTimePickerFrom.TabIndex = 5
            ' 
            ' Label4
            ' 
            Label4.AutoSize = True
            Label4.Location = New Point(400, 60)
            Label4.Name = "Label4"
            Label4.Size = New Size(23, 15)
            Label4.TabIndex = 6
            Label4.Text = "To:"
            ' 
            ' DateTimePickerTo
            ' 
            DateTimePickerTo.Location = New Point(480, 55)
            DateTimePickerTo.Name = "DateTimePickerTo"
            DateTimePickerTo.Size = New Size(250, 23)
            DateTimePickerTo.TabIndex = 7
            ' 
            ' Label5
            ' 
            Label5.AutoSize = True
            Label5.Location = New Point(20, 100)
            Label5.Name = "Label5"
            Label5.Size = New Size(31, 15)
            Label5.TabIndex = 8
            Label5.Text = "Staff"
            ' 
            ' TextBoxFilterStaff
            ' 
            TextBoxFilterStaff.Location = New Point(120, 95)
            TextBoxFilterStaff.Name = "TextBoxFilterStaff"
            TextBoxFilterStaff.Size = New Size(250, 23)
            TextBoxFilterStaff.TabIndex = 9
            ' 
            ' buttonApplyFilters
            ' 
            buttonApplyFilters.BackColor = SystemColors.AppWorkspace
            buttonApplyFilters.Location = New Point(400, 95)
            buttonApplyFilters.Name = "buttonApplyFilters"
            buttonApplyFilters.Size = New Size(150, 35)
            buttonApplyFilters.TabIndex = 10
            buttonApplyFilters.Text = "Apply Filters"
            buttonApplyFilters.UseVisualStyleBackColor = False
            ' 
            ' dataGridViewResults
            ' 
            dataGridViewResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            dataGridViewResults.Location = New Point(20, 140)
            dataGridViewResults.Name = "dataGridViewResults"
            dataGridViewResults.Size = New Size(870, 300)
            dataGridViewResults.TabIndex = 11
            ' 
            ' labelRecordsCount
            ' 
            labelRecordsCount.AutoSize = True
            labelRecordsCount.Location = New Point(20, 450)
            labelRecordsCount.Name = "labelRecordsCount"
            labelRecordsCount.Size = New Size(98, 15)
            labelRecordsCount.TabIndex = 12
            labelRecordsCount.Text = "Records Found: 0"
            ' 
            ' labelTotalTasks
            ' 
            labelTotalTasks.AutoSize = True
            labelTotalTasks.Location = New Point(200, 450)
            labelTotalTasks.Name = "labelTotalTasks"
            labelTotalTasks.Size = New Size(0, 15)
            labelTotalTasks.TabIndex = 13
            ' 
            ' labelUniqueStaff
            ' 
            labelUniqueStaff.AutoSize = True
            labelUniqueStaff.Location = New Point(400, 450)
            labelUniqueStaff.Name = "labelUniqueStaff"
            labelUniqueStaff.Size = New Size(41, 15)
            labelUniqueStaff.TabIndex = 15
            labelUniqueStaff.Text = "Label7"
            ' 
            ' labelBrancesActive
            ' 
            labelBrancesActive.AutoSize = True
            labelBrancesActive.Location = New Point(600, 450)
            labelBrancesActive.Name = "labelBrancesActive"
            labelBrancesActive.Size = New Size(41, 15)
            labelBrancesActive.TabIndex = 16
            labelBrancesActive.Text = "Label7"
            ' 
            ' labelMostActiveBranch
            ' 
            labelMostActiveBranch.AutoSize = True
            labelMostActiveBranch.Location = New Point(20, 480)
            labelMostActiveBranch.Name = "labelMostActiveBranch"
            labelMostActiveBranch.Size = New Size(41, 15)
            labelMostActiveBranch.TabIndex = 17
            labelMostActiveBranch.Text = "Label7"
            ' 
            ' ManagerDashboardForm
            ' 
            BackColor = Color.LightBlue
            ClientSize = New Size(984, 611)
            Controls.Add(tabControlManager)
            Name = "ManagerDashboardForm"
            Text = "Manager Dashboard - [FullName]"
            tabControlManager.ResumeLayout(False)
            panelTaskLogging.ResumeLayout(False)
            Panel1.ResumeLayout(False)
            Panel1.PerformLayout()
            panelReports.ResumeLayout(False)
            Panel2.ResumeLayout(False)
            Panel2.PerformLayout()
            CType(dataGridViewResults, ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)

        End Sub

        Friend WithEvents Panel1 As Panel
        Friend WithEvents comboBoxTaskShift As ComboBox
        Friend WithEvents labelShift As Label
        Friend WithEvents comboBoxTaskBranch As ComboBox
        Friend WithEvents labelBranch As Label
        Friend WithEvents textBoxUserID As TextBox
        Friend WithEvents labelUserID As Label
        Friend WithEvents checkListBoxTasks As CheckedListBox
        Friend WithEvents labelTasks As Label
        Friend WithEvents textBoxStaffName As TextBox
        Friend WithEvents labelStaffName As Label
        Friend WithEvents textBoxTaskNotes As TextBox
        Friend WithEvents labelTaskNotes As Label
        Friend WithEvents buttonSubmitTasks As Button
        Friend WithEvents Panel2 As Panel
        Friend WithEvents Label1 As Label

        Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

        End Sub

        Friend WithEvents ComboBoxFilterBranch As ComboBox
        Friend WithEvents DateTimePickerFrom As DateTimePicker
        Friend WithEvents Label3 As Label
        Friend WithEvents ComboBoxFilterShift As ComboBox
        Friend WithEvents Label2 As Label
        Friend WithEvents buttonApplyFilters As Button
        Friend WithEvents TextBoxFilterStaff As TextBox
        Friend WithEvents Label5 As Label
        Friend WithEvents DateTimePickerTo As DateTimePicker
        Friend WithEvents Label4 As Label
        Friend WithEvents dataGridViewResults As DataGridView
        Friend WithEvents labelTotalTasks As Label
        Friend WithEvents labelRecordsCount As Label
        Friend WithEvents labelBrancesActive As Label
        Friend WithEvents labelUniqueStaff As Label
        Friend WithEvents labelMostActiveBranch As Label
    End Class
End Namespace