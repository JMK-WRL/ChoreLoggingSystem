Imports ChoreLoggingSystem.Models
Imports ChoreLoggingSystem.Services
Imports System.Drawing.Text
Imports Microsoft.Data.SqlClient

Namespace Forms
    Partial Public Class MainForm
        Inherits Form

        Private ReadOnly _databaseService As DatabaseService
        Private ReadOnly _currentStaff As Staff

        Public Sub New(loggedInStaff As Staff)
            InitializeComponent()
            _currentStaff = loggedInStaff
            _databaseService = New DatabaseService()

            ' Initialize company branding
            InitializeCompanyBranding()

            ' Pre-populate authenticated staff data
            textBoxUserID.Text = _currentStaff.FullName & " (" & _currentStaff.UserID & ")"
            textBoxUserID.ReadOnly = True

            ' Set window title with staff name
            Me.Text = $"Excellence Care Solutions - Task Management - {_currentStaff.FullName}"

            LoadInitialData()
        End Sub

        ' Temporary constructor for testing (remove when login is implemented)
        Public Sub New()
            InitializeComponent()
            _databaseService = New DatabaseService()

            ' Create a temporary test staff member
            _currentStaff = New Staff With {
                .StaffID = 1,
                .UserID = "TEST",
                .FullName = "Test User"
            }

            ' Initialize company branding
            InitializeCompanyBranding()

            ' Pre-populate test user data
            textBoxUserID.Text = _currentStaff.FullName & " (" & _currentStaff.UserID & ")"
            textBoxUserID.ReadOnly = True

            ' Set window title
            Me.Text = $"Excellence Care Solutions - Task Management - {_currentStaff.FullName}"

            LoadInitialData()
        End Sub

        Private Sub InitializeCompanyBranding()
            Try
                ' Set form colors
                Me.BackColor = Color.FromArgb(245, 245, 245)

                ' Set company title styling
                labelCompanyTitle.ForeColor = Color.FromArgb(0, 51, 102)
                labelSubtitle.ForeColor = Color.FromArgb(100, 100, 100)
                labelMainTitle.ForeColor = Color.FromArgb(0, 51, 102)

                ' Load company logo
                LoadCompanyLogo()

                ' Style buttons
                StyleButtons()

            Catch ex As Exception
                ' Continue without styling if there are issues
            End Try
        End Sub

        Private Sub LoadCompanyLogo()
            Try
                ' Create a professional sample logo
                Dim logoBitmap As New Bitmap(80, 80)
                Using g As Graphics = Graphics.FromImage(logoBitmap)
                    ' Set high quality rendering
                    g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                    g.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit

                    ' Clear background
                    g.Clear(Color.White)

                    ' Draw outer circle (company brand color)
                    g.FillEllipse(New SolidBrush(Color.FromArgb(0, 51, 102)), 5, 5, 70, 70)

                    ' Draw inner circle
                    g.FillEllipse(Brushes.White, 15, 15, 50, 50)

                    ' Draw company initials
                    Using font As New Font("Arial", 14, FontStyle.Bold)
                        Dim textBrush As New SolidBrush(Color.FromArgb(0, 51, 102))
                        g.DrawString("ECS", font, textBrush, 25, 28)
                    End Using

                    ' Draw border
                    g.DrawEllipse(New Pen(Color.FromArgb(0, 51, 102), 2), 5, 5, 70, 70)
                End Using

                pictureBoxLogo.Image = logoBitmap

            Catch ex As Exception
                ' If logo creation fails, hide the picture box
                pictureBoxLogo.Visible = False
            End Try
        End Sub

        Private Sub StyleButtons()
            Try
                ' Style Submit button
                buttonSubmit.BackColor = Color.FromArgb(0, 102, 51)
                buttonSubmit.ForeColor = Color.White
                buttonSubmit.FlatStyle = FlatStyle.Flat
                buttonSubmit.FlatAppearance.BorderSize = 0

                ' Style Reset button
                buttonReset.BackColor = Color.FromArgb(204, 102, 0)
                buttonReset.ForeColor = Color.White
                buttonReset.FlatStyle = FlatStyle.Flat
                buttonReset.FlatAppearance.BorderSize = 0

            Catch ex As Exception
                ' Continue without button styling if there are issues
            End Try
        End Sub

        Private Sub LoadInitialData()
            Try
                ' Load branches
                Dim branches As List(Of Branch) = _databaseService.GetBranches()

                comboBoxBranch.DataSource = branches
                comboBoxBranch.DisplayMember = "BranchName"
                comboBoxBranch.ValueMember = "BranchID"
                comboBoxBranch.SelectedIndex = -1

                ' Load shifts
                Dim shifts As List(Of Shift) = _databaseService.GetShifts()

                comboBoxShift.DataSource = shifts
                comboBoxShift.DisplayMember = "ShiftName"
                comboBoxShift.ValueMember = "ShiftID"
                comboBoxShift.SelectedIndex = -1

            Catch ex As Exception
                MessageBox.Show($"Error loading initial data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                If comboBoxBranch.SelectedValue Is Nothing Then
                    MessageBox.Show("Please select a location.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                If comboBoxShift.SelectedValue Is Nothing Then
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

                ' Submit to database with authenticated staff ID
                Dim success As Boolean = _databaseService.LogCompletedTasks(
                    _currentStaff.UserID,
                    CInt(comboBoxBranch.SelectedValue),
                    CInt(comboBoxShift.SelectedValue),
                    completedTaskIds,
                    textBoxNotes.Text.Trim(),
                    _currentStaff.StaffID
                )

                If success Then
                    MessageBox.Show($"{completedTaskIds.Count} task(s) logged successfully for {_currentStaff.FullName}!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            textBoxNotes.Clear()
            comboBoxBranch.SelectedIndex = -1
            comboBoxShift.SelectedIndex = -1
            checkedListBoxTasks.Items.Clear()
            ' Note: User ID stays populated and read-only
        End Sub

    End Class
End Namespace