Imports ChoreLoggingSystem.Models
Imports ChoreLoggingSystem.Services

Namespace Forms
    Partial Public Class ManagerDashboardForm
        Inherits Form

        Private ReadOnly _databaseService As DatabaseService
        Private ReadOnly _currentManager As Staff

        Public Sub New(loggedInManager As Staff)
            InitializeComponent()
            _currentManager = loggedInManager
            _databaseService = New DatabaseService()

            InitializeManagerDashboard()
            LoadInitialData()
        End Sub

        Private Sub InitializeManagerDashboard()
            ' Set window title
            Me.Text = $"Manager Dashboard - {_currentManager.FullName}"

            ' Initialize tab control for different sections
            SetupTabControl()

            ' Apply manager-specific styling
            ApplyManagerStyling()
        End Sub

        Private Sub SetupTabControl()
            ' Create tab control with two main sections
            tabControlManager.TabPages.Clear()

            ' Task Logging Tab
            Dim taskLoggingTab As New TabPage("Task Logging")
            taskLoggingTab.Controls.Add(panelTaskLogging)
            tabControlManager.TabPages.Add(taskLoggingTab)

            ' Reports & Analytics Tab  
            Dim reportsTab As New TabPage("Reports & Analytics")
            reportsTab.Controls.Add(panelReports)
            tabControlManager.TabPages.Add(reportsTab)
        End Sub

        Private Sub LoadInitialData()
            Try
                ' Load all branches (managers see all locations)
                LoadAllBranches()

                ' Load all shifts
                LoadAllShifts()

                ' Set default date range for reports
                dateTimePickerFrom.Value = DateTime.Now.AddDays(-30)
                dateTimePickerTo.Value = DateTime.Now

                ' Load initial dashboard data
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

        Private Sub ApplyManagerStyling()
            ' Manager-specific color scheme
            Me.BackColor = Color.FromArgb(240, 248, 255) ' Light blue

            ' Tab styling
            tabControlManager.BackColor = Color.FromArgb(230, 240, 250)

            ' Panel styling
            panelTaskLogging.BackColor = Color.White
            panelReports.BackColor = Color.White

            ' Button styling
            buttonSubmitTasks.BackColor = Color.FromArgb(0, 120, 70)
            buttonSubmitTasks.ForeColor = Color.White

            buttonApplyFilters.BackColor = Color.FromArgb(70, 130, 180)
            buttonApplyFilters.ForeColor = Color.White
        End Sub

        Friend WithEvents tabControlManager As TabControl
        Friend WithEvents panelTaskLogging As TabPage
        Friend WithEvents TabPage2 As TabPage

        Private Sub InitializeComponent()
            tabControlManager = New TabControl()
            panelTaskLogging = New TabPage()
            TabPage2 = New TabPage()
            Panel1 = New Panel()
            labelBranch = New Label()
            comboBoxTaskBranch = New ComboBox()
            labelShift = New Label()
            comboBoxTaskShift = New ComboBox()
            labelUserID = New Label()
            textBoxUserID = New TextBox()
            labelStaffName = New Label()
            textBoxStaffName = New TextBox()
            labelTasks = New Label()
            checkListBoxTasks = New CheckedListBox()
            labelTaskNotes = New Label()
            textBoxTaskNotes = New TextBox()
            buttonSubmitTasks = New Button()
            tabControlManager.SuspendLayout()
            panelTaskLogging.SuspendLayout()
            Panel1.SuspendLayout()
            SuspendLayout()
            ' 
            ' tabControlManager
            ' 
            tabControlManager.Controls.Add(panelTaskLogging)
            tabControlManager.Controls.Add(TabPage2)
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
            ' TabPage2
            ' 
            TabPage2.Location = New Point(4, 24)
            TabPage2.Name = "TabPage2"
            TabPage2.Padding = New Padding(3)
            TabPage2.Size = New Size(952, 552)
            TabPage2.TabIndex = 1
            TabPage2.Text = "TabPage2"
            TabPage2.UseVisualStyleBackColor = True
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
            ' labelBranch
            ' 
            labelBranch.AutoSize = True
            labelBranch.Location = New Point(20, 20)
            labelBranch.Name = "labelBranch"
            labelBranch.Size = New Size(47, 15)
            labelBranch.TabIndex = 0
            labelBranch.Text = "Branch:"
            ' 
            ' comboBoxTaskBranch
            ' 
            comboBoxTaskBranch.FormattingEnabled = True
            comboBoxTaskBranch.Location = New Point(120, 15)
            comboBoxTaskBranch.Name = "comboBoxTaskBranch"
            comboBoxTaskBranch.Size = New Size(250, 23)
            comboBoxTaskBranch.TabIndex = 1
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
            ' comboBoxTaskShift
            ' 
            comboBoxTaskShift.FormattingEnabled = True
            comboBoxTaskShift.Location = New Point(120, 55)
            comboBoxTaskShift.Name = "comboBoxTaskShift"
            comboBoxTaskShift.Size = New Size(250, 23)
            comboBoxTaskShift.TabIndex = 3
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
            ' textBoxUserID
            ' 
            textBoxUserID.Location = New Point(120, 95)
            textBoxUserID.Name = "textBoxUserID"
            textBoxUserID.Size = New Size(250, 23)
            textBoxUserID.TabIndex = 5
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
            ' textBoxStaffName
            ' 
            textBoxStaffName.Location = New Point(120, 135)
            textBoxStaffName.Name = "textBoxStaffName"
            textBoxStaffName.ReadOnly = True
            textBoxStaffName.Size = New Size(250, 23)
            textBoxStaffName.TabIndex = 7
            ' 
            ' labelTasks
            ' 
            labelTasks.AutoSize = True
            labelTasks.Location = New Point(20, 180)
            labelTasks.Name = "labelTasks"
            labelTasks.Size = New Size(37, 15)
            labelTasks.TabIndex = 8
            labelTasks.Text = "Tasks:"
            ' 
            ' checkListBoxTasks
            ' 
            checkListBoxTasks.FormattingEnabled = True
            checkListBoxTasks.Location = New Point(120, 180)
            checkListBoxTasks.Name = "checkListBoxTasks"
            checkListBoxTasks.Size = New Size(350, 148)
            checkListBoxTasks.TabIndex = 9
            ' 
            ' labelTaskNotes
            ' 
            labelTaskNotes.AutoSize = True
            labelTaskNotes.Location = New Point(20, 350)
            labelTaskNotes.Name = "labelTaskNotes"
            labelTaskNotes.Size = New Size(66, 15)
            labelTaskNotes.TabIndex = 10
            labelTaskNotes.Text = "Task Notes:"
            ' 
            ' textBoxTaskNotes
            ' 
            textBoxTaskNotes.Location = New Point(120, 345)
            textBoxTaskNotes.Multiline = True
            textBoxTaskNotes.Name = "textBoxTaskNotes"
            textBoxTaskNotes.Size = New Size(500, 60)
            textBoxTaskNotes.TabIndex = 11
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
    End Class
End Namespace