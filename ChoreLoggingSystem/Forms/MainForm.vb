Imports ChoreLoggingSystem.Models
Imports ChoreLoggingSystem.Services
Imports Microsoft.Data.SqlClient


Namespace Forms
    Partial Public Class MainForm
        Inherits Form

        Private ReadOnly _databaseService As DatabaseService

        Public Sub New()
            MessageBox.Show("MainForm constructor started", "Debug")
            InitializeComponent()
            _databaseService = New DatabaseService()
            LoadInitialData()
        End Sub

        Private Sub LoadInitialData()
            Try
                MessageBox.Show("LoadInitialData started", "Debug")

                ' Load branches
                Dim branches As List(Of Branch) = _databaseService.GetBranches()
                MessageBox.Show($"DatabaseService found {branches.Count} branches", "Debug")

                comboBoxBranch.DataSource = branches
                comboBoxBranch.DisplayMember = "BranchName"
                comboBoxBranch.ValueMember = "BranchID"
                comboBoxBranch.SelectedIndex = -1

                ' Load shifts
                Dim shifts As List(Of Shift) = _databaseService.GetShifts()
                MessageBox.Show($"DatabaseService found {shifts.Count} shifts", "Debug")

                comboBoxShift.DataSource = shifts
                comboBoxShift.DisplayMember = "ShiftName"
                comboBoxShift.ValueMember = "ShiftID"
                comboBoxShift.SelectedIndex = -1

                MessageBox.Show("LoadInitialData completed successfully", "Debug")

            Catch ex As Exception
                MessageBox.Show($"Error in LoadInitialData: {ex.Message}", "Database Error")
            End Try
        End Sub

        Private Sub comboBoxShift_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboBoxShift.SelectedIndexChanged
            If comboBoxShift.SelectedValue IsNot Nothing AndAlso TypeOf comboBoxShift.SelectedValue Is Integer Then
                Dim shiftId As Integer = CInt(comboBoxShift.SelectedValue)
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

        Private Sub buttonSubmit_Click(sender As Object, e As EventArgs) Handles buttonSubmit.Click
            Try
                ' Validation
                If String.IsNullOrWhiteSpace(textBoxStaffInitials.Text) Then
                    MessageBox.Show("Please enter your initials.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                If comboBoxBranch.SelectedValue Is Nothing Then
                    MessageBox.Show("Please select a branch.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                If comboBoxShift.SelectedValue Is Nothing Then
                    MessageBox.Show("Please select a shift.", "Validation Error",
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

                ' Submit to database
                Dim success As Boolean = _databaseService.LogCompletedTasks(
                    textBoxStaffInitials.Text.Trim(),
                    CInt(comboBoxBranch.SelectedValue),
                    CInt(comboBoxShift.SelectedValue),
                    completedTaskIds,
                    textBoxNotes.Text.Trim()
                )

                If success Then
                    MessageBox.Show($"{completedTaskIds.Count} task(s) logged successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ResetForm()
                Else
                    MessageBox.Show("Error logging tasks. Please try again.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Catch ex As Exception
                MessageBox.Show($"Error submitting tasks: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub buttonReset_Click(sender As Object, e As EventArgs) Handles buttonReset.Click
            ResetForm()
        End Sub

        Private Sub ResetForm()
            textBoxStaffInitials.Clear()
            textBoxNotes.Clear()
            comboBoxBranch.SelectedIndex = -1
            comboBoxShift.SelectedIndex = -1
            checkedListBoxTasks.Items.Clear()
        End Sub

        Private Sub buttonDashboard_Click(sender As Object, e As EventArgs) Handles buttonDashboard.Click
            Dim dashboardForm As New DashboardForm()
            dashboardForm.Show()
        End Sub

        Private Sub InitializeComponent()
            labelTitle = New Label()
            labelStaffInitials = New Label()
            labelBranch = New Label()
            labelShift = New Label()
            labelTasks = New Label()
            labelNotes = New Label()
            textBoxStaffInitials = New TextBox()
            comboBoxBranch = New ComboBox()
            comboBoxShift = New ComboBox()
            checkedListBoxTasks = New CheckedListBox()
            textBoxNotes = New TextBox()
            buttonSubmit = New Button()
            buttonReset = New Button()
            buttonDashboard = New Button()
            SuspendLayout()
            ' 
            ' labelTitle
            ' 
            labelTitle.AutoSize = True
            labelTitle.Font = New Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelTitle.Location = New Point(60, 9)
            labelTitle.Name = "labelTitle"
            labelTitle.Size = New Size(270, 30)
            labelTitle.TabIndex = 0
            labelTitle.Text = "CHORE LOGGING SYSTEM"
            ' 
            ' labelStaffInitials
            ' 
            labelStaffInitials.AutoSize = True
            labelStaffInitials.Location = New Point(50, 80)
            labelStaffInitials.Name = "labelStaffInitials"
            labelStaffInitials.Size = New Size(71, 15)
            labelStaffInitials.TabIndex = 1
            labelStaffInitials.Text = "Staff Initials:"
            ' 
            ' labelBranch
            ' 
            labelBranch.AutoSize = True
            labelBranch.Location = New Point(50, 120)
            labelBranch.Name = "labelBranch"
            labelBranch.Size = New Size(47, 15)
            labelBranch.TabIndex = 2
            labelBranch.Text = "Branch:"
            ' 
            ' labelShift
            ' 
            labelShift.AutoSize = True
            labelShift.Location = New Point(50, 160)
            labelShift.Name = "labelShift"
            labelShift.Size = New Size(34, 15)
            labelShift.TabIndex = 3
            labelShift.Text = "Shift:"
            ' 
            ' labelTasks
            ' 
            labelTasks.AutoSize = True
            labelTasks.Location = New Point(50, 200)
            labelTasks.Name = "labelTasks"
            labelTasks.Size = New Size(37, 15)
            labelTasks.TabIndex = 4
            labelTasks.Text = "Tasks:"
            ' 
            ' labelNotes
            ' 
            labelNotes.AutoSize = True
            labelNotes.Location = New Point(50, 450)
            labelNotes.Name = "labelNotes"
            labelNotes.Size = New Size(41, 15)
            labelNotes.TabIndex = 5
            labelNotes.Text = "Notes:"
            ' 
            ' textBoxStaffInitials
            ' 
            textBoxStaffInitials.Location = New Point(150, 80)
            textBoxStaffInitials.Name = "textBoxStaffInitials"
            textBoxStaffInitials.Size = New Size(200, 23)
            textBoxStaffInitials.TabIndex = 6
            ' 
            ' comboBoxBranch
            ' 
            comboBoxBranch.DropDownStyle = ComboBoxStyle.DropDownList
            comboBoxBranch.FormattingEnabled = True
            comboBoxBranch.Location = New Point(150, 120)
            comboBoxBranch.Name = "comboBoxBranch"
            comboBoxBranch.Size = New Size(200, 23)
            comboBoxBranch.TabIndex = 7
            ' 
            ' comboBoxShift
            ' 
            comboBoxShift.DropDownStyle = ComboBoxStyle.DropDownList
            comboBoxShift.FormattingEnabled = True
            comboBoxShift.Location = New Point(150, 160)
            comboBoxShift.Name = "comboBoxShift"
            comboBoxShift.Size = New Size(200, 23)
            comboBoxShift.TabIndex = 8
            ' 
            ' checkedListBoxTasks
            ' 
            checkedListBoxTasks.FormattingEnabled = True
            checkedListBoxTasks.Location = New Point(50, 230)
            checkedListBoxTasks.Name = "checkedListBoxTasks"
            checkedListBoxTasks.Size = New Size(500, 184)
            checkedListBoxTasks.TabIndex = 9
            ' 
            ' textBoxNotes
            ' 
            textBoxNotes.Location = New Point(50, 470)
            textBoxNotes.Multiline = True
            textBoxNotes.Name = "textBoxNotes"
            textBoxNotes.ScrollBars = ScrollBars.Vertical
            textBoxNotes.Size = New Size(500, 60)
            textBoxNotes.TabIndex = 10
            ' 
            ' buttonSubmit
            ' 
            buttonSubmit.Location = New Point(50, 550)
            buttonSubmit.Name = "buttonSubmit"
            buttonSubmit.Size = New Size(100, 30)
            buttonSubmit.TabIndex = 11
            buttonSubmit.Text = "Submit"
            buttonSubmit.UseVisualStyleBackColor = True
            ' 
            ' buttonReset
            ' 
            buttonReset.Location = New Point(170, 550)
            buttonReset.Name = "buttonReset"
            buttonReset.Size = New Size(100, 30)
            buttonReset.TabIndex = 12
            buttonReset.Text = "Dashboard"
            buttonReset.UseVisualStyleBackColor = True
            ' 
            ' buttonDashboard
            ' 
            buttonDashboard.Location = New Point(290, 550)
            buttonDashboard.Name = "buttonDashboard"
            buttonDashboard.Size = New Size(100, 30)
            buttonDashboard.TabIndex = 13
            buttonDashboard.Text = "Dashboard"
            buttonDashboard.UseVisualStyleBackColor = True
            ' 
            ' MainForm
            ' 
            ClientSize = New Size(784, 611)
            Controls.Add(buttonDashboard)
            Controls.Add(buttonReset)
            Controls.Add(buttonSubmit)
            Controls.Add(textBoxNotes)
            Controls.Add(checkedListBoxTasks)
            Controls.Add(comboBoxShift)
            Controls.Add(comboBoxBranch)
            Controls.Add(textBoxStaffInitials)
            Controls.Add(labelNotes)
            Controls.Add(labelTasks)
            Controls.Add(labelShift)
            Controls.Add(labelBranch)
            Controls.Add(labelStaffInitials)
            Controls.Add(labelTitle)
            Name = "MainForm"
            ResumeLayout(False)
            PerformLayout()

        End Sub

        Friend WithEvents labelTitle As Label
        Friend WithEvents labelStaffInitials As Label
        Friend WithEvents labelBranch As Label
        Friend WithEvents labelShift As Label
        Friend WithEvents labelTasks As Label
        Friend WithEvents labelNotes As Label
        Friend WithEvents textBoxStaffInitials As TextBox
        Friend WithEvents comboBoxBranch As ComboBox
        Friend WithEvents comboBoxShift As ComboBox
        Friend WithEvents checkedListBoxTasks As CheckedListBox
        Friend WithEvents textBoxNotes As TextBox
        Friend WithEvents buttonSubmit As Button
        Friend WithEvents buttonReset As Button
        Friend WithEvents buttonDashboard As Button
    End Class
End Namespace