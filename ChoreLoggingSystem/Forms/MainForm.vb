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
                ' Debug: Check if we get here
                MessageBox.Show("LoadInitialData started", "Debug")

                ' Load branches
                Dim branches As List(Of Branch) = _databaseService.GetBranches()
                MessageBox.Show($"Found {branches.Count} branches", "Debug")

                comboBoxBranch.DataSource = branches
                comboBoxBranch.DisplayMember = "BranchName"
                comboBoxBranch.ValueMember = "BranchID"
                comboBoxBranch.SelectedIndex = -1

                ' Load shifts
                Dim shifts As List(Of Shift) = _databaseService.GetShifts()
                MessageBox.Show($"Found {shifts.Count} shifts", "Debug")

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

        Private Sub InitializeComponent()
            labelMainTitle = New Label()
            labelUserID = New Label()
            labelBranch = New Label()
            labelShift = New Label()
            labelTasks = New Label()
            labelNotes = New Label()
            textBoxUserID = New TextBox()
            comboBoxBranch = New ComboBox()
            comboBoxShift = New ComboBox()
            checkedListBoxTasks = New CheckedListBox()
            textBoxNotes = New TextBox()
            buttonSubmit = New Button()
            buttonReset = New Button()
            labelCompanyTitle = New Label()
            labelSubtitle = New Label()
            pictureBoxLogo = New PictureBox()
            SuspendLayout()

            ' pictureBoxLogo
            pictureBoxLogo.Location = New Point(20, 15)
            pictureBoxLogo.Name = "pictureBoxLogo"
            pictureBoxLogo.Size = New Size(80, 80)
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.StretchImage
            pictureBoxLogo.TabIndex = 16
            pictureBoxLogo.TabStop = False

            ' labelCompanyTitle
            labelCompanyTitle.AutoSize = True
            labelCompanyTitle.Font = New Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelCompanyTitle.Location = New Point(280, 25)
            labelCompanyTitle.Name = "labelCompanyTitle"
            labelCompanyTitle.Size = New Size(366, 25)
            labelCompanyTitle.TabIndex = 14
            labelCompanyTitle.Text = "EXCELLENCE CARE SOLUTIONS"
            labelCompanyTitle.TextAlign = ContentAlignment.MiddleCenter

            ' labelSubtitle
            labelSubtitle.AutoSize = True
            labelSubtitle.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Italic, GraphicsUnit.Point, CByte(0))
            labelSubtitle.Location = New Point(300, 55)
            labelSubtitle.Name = "labelSubtitle"
            labelSubtitle.Size = New Size(242, 16)
            labelSubtitle.TabIndex = 15
            labelSubtitle.Text = "Professional Task Management System"
            labelSubtitle.TextAlign = ContentAlignment.MiddleCenter

            ' labelMainTitle
            labelMainTitle.AutoSize = True
            labelMainTitle.Font = New Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelMainTitle.Location = New Point(350, 90)
            labelMainTitle.Name = "labelMainTitle"
            labelMainTitle.Size = New Size(161, 24)
            labelMainTitle.TabIndex = 0
            labelMainTitle.Text = "TASK LOGGING"
            labelMainTitle.TextAlign = ContentAlignment.MiddleCenter

            ' labelUserID
            labelUserID.AutoSize = True
            labelUserID.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelUserID.Location = New Point(50, 140)
            labelUserID.Name = "labelUserID"
            labelUserID.Size = New Size(63, 16)
            labelUserID.TabIndex = 1
            labelUserID.Text = "User ID:"

            ' textBoxUserID
            textBoxUserID.Location = New Point(160, 138)
            textBoxUserID.Name = "textBoxUserID"
            textBoxUserID.ReadOnly = True
            textBoxUserID.Size = New Size(200, 23)
            textBoxUserID.TabIndex = 6
            textBoxUserID.BackColor = Color.FromArgb(230, 230, 230)

            ' labelBranch
            labelBranch.AutoSize = True
            labelBranch.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelBranch.Location = New Point(50, 180)
            labelBranch.Name = "labelBranch"
            labelBranch.Size = New Size(59, 16)
            labelBranch.TabIndex = 2
            labelBranch.Text = "Location:"

            ' comboBoxBranch
            comboBoxBranch.DropDownStyle = ComboBoxStyle.DropDownList
            comboBoxBranch.FormattingEnabled = True
            comboBoxBranch.Location = New Point(160, 178)
            comboBoxBranch.Name = "comboBoxBranch"
            comboBoxBranch.Size = New Size(250, 23)
            comboBoxBranch.TabIndex = 7

            ' labelShift
            labelShift.AutoSize = True
            labelShift.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelShift.Location = New Point(50, 220)
            labelShift.Name = "labelShift"
            labelShift.Size = New Size(81, 16)
            labelShift.TabIndex = 3
            labelShift.Text = "Work Shift:"

            ' comboBoxShift
            comboBoxShift.DropDownStyle = ComboBoxStyle.DropDownList
            comboBoxShift.FormattingEnabled = True
            comboBoxShift.Location = New Point(160, 218)
            comboBoxShift.Name = "comboBoxShift"
            comboBoxShift.Size = New Size(250, 23)
            comboBoxShift.TabIndex = 8

            ' labelTasks
            labelTasks.AutoSize = True
            labelTasks.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelTasks.Location = New Point(50, 260)
            labelTasks.Name = "labelTasks"
            labelTasks.Size = New Size(200, 16)
            labelTasks.TabIndex = 4
            labelTasks.Text = "Tasks for Selected Shift:"

            ' checkedListBoxTasks
            checkedListBoxTasks.CheckOnClick = True
            checkedListBoxTasks.FormattingEnabled = True
            checkedListBoxTasks.Location = New Point(50, 285)
            checkedListBoxTasks.Name = "checkedListBoxTasks"
            checkedListBoxTasks.Size = New Size(600, 180)
            checkedListBoxTasks.TabIndex = 9
            checkedListBoxTasks.BorderStyle = BorderStyle.Fixed3D

            ' labelNotes
            labelNotes.AutoSize = True
            labelNotes.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelNotes.Location = New Point(50, 485)
            labelNotes.Name = "labelNotes"
            labelNotes.Size = New Size(150, 16)
            labelNotes.TabIndex = 5
            labelNotes.Text = "Additional Notes:"

            ' textBoxNotes
            textBoxNotes.Location = New Point(50, 510)
            textBoxNotes.Multiline = True
            textBoxNotes.Name = "textBoxNotes"
            textBoxNotes.ScrollBars = ScrollBars.Vertical
            textBoxNotes.Size = New Size(600, 60)
            textBoxNotes.TabIndex = 10
            textBoxNotes.BorderStyle = BorderStyle.Fixed3D

            ' buttonSubmit
            buttonSubmit.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            buttonSubmit.Location = New Point(50, 585)
            buttonSubmit.Name = "buttonSubmit"
            buttonSubmit.Size = New Size(120, 35)
            buttonSubmit.TabIndex = 11
            buttonSubmit.Text = "Submit Tasks"
            buttonSubmit.UseVisualStyleBackColor = True

            ' buttonReset
            buttonReset.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            buttonReset.Location = New Point(190, 585)
            buttonReset.Name = "buttonReset"
            buttonReset.Size = New Size(120, 35)
            buttonReset.TabIndex = 12
            buttonReset.Text = "Reset Form"
            buttonReset.UseVisualStyleBackColor = True

            ' MainForm
            ClientSize = New Size(900, 650)
            Controls.Add(pictureBoxLogo)
            Controls.Add(labelSubtitle)
            Controls.Add(labelCompanyTitle)
            Controls.Add(buttonReset)
            Controls.Add(buttonSubmit)
            Controls.Add(textBoxNotes)
            Controls.Add(checkedListBoxTasks)
            Controls.Add(comboBoxShift)
            Controls.Add(comboBoxBranch)
            Controls.Add(textBoxUserID)
            Controls.Add(labelNotes)
            Controls.Add(labelTasks)
            Controls.Add(labelShift)
            Controls.Add(labelBranch)
            Controls.Add(labelUserID)
            Controls.Add(labelMainTitle)
            FormBorderStyle = FormBorderStyle.FixedSingle
            MaximizeBox = False
            Name = "MainForm"
            StartPosition = FormStartPosition.CenterScreen
            Text = "Excellence Care Solutions - Task Management System"
            ResumeLayout(False)
            PerformLayout()

        End Sub

        Friend WithEvents labelMainTitle As Label
        Friend WithEvents labelUserID As Label
        Friend WithEvents labelBranch As Label
        Friend WithEvents labelShift As Label
        Friend WithEvents labelTasks As Label
        Friend WithEvents labelNotes As Label
        Friend WithEvents textBoxUserID As TextBox
        Friend WithEvents comboBoxBranch As ComboBox
        Friend WithEvents comboBoxShift As ComboBox
        Friend WithEvents checkedListBoxTasks As CheckedListBox
        Friend WithEvents textBoxNotes As TextBox
        Friend WithEvents buttonSubmit As Button
        Friend WithEvents buttonReset As Button
        Friend WithEvents labelCompanyTitle As Label
        Friend WithEvents labelSubtitle As Label
        Friend WithEvents pictureBoxLogo As PictureBox

    End Class
End Namespace