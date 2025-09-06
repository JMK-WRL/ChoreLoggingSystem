Imports ChoreLoggingSystem.Models
Imports ChoreLoggingSystem.Services

Namespace Forms
    Public Class StaffLoginForm
        Inherits Form

        Private ReadOnly _databaseService As DatabaseService
        Private _authenticatedStaff As Staff

        Public ReadOnly Property AuthenticatedStaff As Staff
            Get
                Return _authenticatedStaff
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            _databaseService = New DatabaseService()
            InitializeLoginForm()
        End Sub

        Private Sub InitializeLoginForm()
            ' Set form properties
            Me.FormBorderStyle = FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.StartPosition = FormStartPosition.CenterScreen
            Me.BackColor = Color.FromArgb(245, 248, 255)

            ' Set focus to UserID textbox
            textBoxUserID.Focus()
        End Sub

        Private Sub buttonLogin_Click(sender As Object, e As EventArgs) Handles buttonLogin.Click
            Try
                ' Clear any previous error messages
                labelErrorMessage.Text = ""
                labelErrorMessage.Visible = False

                ' Validation
                If String.IsNullOrWhiteSpace(textBoxUserID.Text) Then
                    ShowError("Please enter your User ID.")
                    textBoxUserID.Focus()
                    Return
                End If

                If String.IsNullOrWhiteSpace(textBoxPIN.Text) Then
                    ShowError("Please enter your PIN.")
                    textBoxPIN.Focus()
                    Return
                End If

                ' Show loading indicator
                buttonLogin.Text = "Authenticating..."
                buttonLogin.Enabled = False
                Me.Cursor = Cursors.WaitCursor

                ' Authenticate user from database
                _authenticatedStaff = AuthenticateStaff(textBoxUserID.Text.Trim().ToUpper(), textBoxPIN.Text.Trim())

                If _authenticatedStaff IsNot Nothing Then
                    ' Authentication successful
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    ' Authentication failed
                    ShowError("Invalid credentials. Please check your User ID and PIN.")
                    textBoxPIN.Clear()
                    textBoxUserID.Focus()
                End If

            Catch ex As Exception
                ShowError($"Login error: {ex.Message}")
            Finally
                ' Reset button state
                buttonLogin.Text = "Login"
                buttonLogin.Enabled = True
                Me.Cursor = Cursors.Default
            End Try
        End Sub

        Private Function AuthenticateStaff(userID As String, pin As String) As Staff
            Try
                ' Get staff from database
                Dim staff As Staff = _databaseService.GetStaffByUserID(userID)

                If staff IsNot Nothing AndAlso staff.PIN = pin Then
                    ' Update last login date
                    UpdateLastLogin(staff.StaffID)
                    Return staff
                End If

                Return Nothing
            Catch ex As Exception
                ' Log error but don't expose to user
                Return Nothing
            End Try
        End Function

        Private Sub UpdateLastLogin(staffID As Integer)
            Try
                ' You can implement this to track last login times
                ' For now, we'll skip this functionality
            Catch ex As Exception
                ' Continue even if login tracking fails
            End Try
        End Sub

        Private Sub buttonCancel_Click(sender As Object, e As EventArgs) Handles buttonCancel.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub ShowError(message As String)
            labelErrorMessage.Text = message
            labelErrorMessage.ForeColor = Color.Red
            labelErrorMessage.Visible = True
        End Sub

        ' Allow Enter key to trigger login
        Private Sub textBoxPIN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBoxPIN.KeyPress
            If e.KeyChar = Convert.ToChar(Keys.Enter) Then
                buttonLogin_Click(sender, e)
            End If
        End Sub

        Private Sub textBoxUserID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBoxUserID.KeyPress
            If e.KeyChar = Convert.ToChar(Keys.Enter) Then
                textBoxPIN.Focus()
            End If
        End Sub

        Private Sub InitializeComponent()
            labelCompanyTitle = New Label()
            labelLoginTitle = New Label()
            labelUserID = New Label()
            labelPIN = New Label()
            textBoxUserID = New TextBox()
            textBoxPIN = New TextBox()
            buttonLogin = New Button()
            buttonCancel = New Button()
            labelErrorMessage = New Label()
            labelInstructions = New Label()
            panelLogin = New Panel()
            buttonManagerLogin = New Button()
            panelLogin.SuspendLayout()
            SuspendLayout()

            ' panelLogin
            panelLogin.BackColor = Color.White
            panelLogin.BorderStyle = BorderStyle.FixedSingle
            panelLogin.Controls.Add(labelCompanyTitle)
            panelLogin.Controls.Add(labelLoginTitle)
            panelLogin.Controls.Add(labelInstructions)
            panelLogin.Controls.Add(labelUserID)
            panelLogin.Controls.Add(textBoxUserID)
            panelLogin.Controls.Add(labelPIN)
            panelLogin.Controls.Add(textBoxPIN)
            panelLogin.Controls.Add(labelErrorMessage)
            panelLogin.Controls.Add(buttonLogin)
            panelLogin.Controls.Add(buttonCancel)
            panelLogin.Controls.Add(buttonManagerLogin)
            panelLogin.Location = New Point(20, 20)
            panelLogin.Size = New Size(360, 320)

            ' labelCompanyTitle
            labelCompanyTitle.AutoSize = True
            labelCompanyTitle.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
            labelCompanyTitle.ForeColor = Color.FromArgb(0, 51, 102)
            labelCompanyTitle.Location = New Point(80, 20)
            labelCompanyTitle.Size = New Size(200, 21)
            labelCompanyTitle.Text = "EXCELLENCE CARE SOLUTIONS"

            ' labelLoginTitle
            labelLoginTitle.AutoSize = True
            labelLoginTitle.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
            labelLoginTitle.ForeColor = Color.FromArgb(0, 51, 102)
            labelLoginTitle.Location = New Point(130, 50)
            labelLoginTitle.Size = New Size(100, 19)
            labelLoginTitle.Text = "STAFF LOGIN"

            ' labelInstructions
            labelInstructions.AutoSize = True
            labelInstructions.Font = New Font("Segoe UI", 8.25F, FontStyle.Italic)
            labelInstructions.ForeColor = Color.FromArgb(100, 100, 100)
            labelInstructions.Location = New Point(115, 75)
            labelInstructions.Size = New Size(130, 13)
            labelInstructions.Text = "Enter your credentials"

            ' labelUserID
            labelUserID.AutoSize = True
            labelUserID.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            labelUserID.Location = New Point(30, 110)
            labelUserID.Size = New Size(55, 15)
            labelUserID.Text = "User ID:"

            ' textBoxUserID
            textBoxUserID.Font = New Font("Segoe UI", 10.0F)
            textBoxUserID.Location = New Point(100, 108)
            textBoxUserID.Size = New Size(180, 25)
            textBoxUserID.CharacterCasing = CharacterCasing.Upper
            textBoxUserID.MaxLength = 10

            ' labelPIN
            labelPIN.AutoSize = True
            labelPIN.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            labelPIN.Location = New Point(30, 150)
            labelPIN.Size = New Size(30, 15)
            labelPIN.Text = "PIN:"

            ' textBoxPIN
            textBoxPIN.Font = New Font("Segoe UI", 10.0F)
            textBoxPIN.Location = New Point(100, 148)
            textBoxPIN.Size = New Size(180, 25)
            textBoxPIN.MaxLength = 4
            textBoxPIN.UseSystemPasswordChar = True

            ' labelErrorMessage
            labelErrorMessage.AutoSize = True
            labelErrorMessage.Font = New Font("Segoe UI", 8.25F)
            labelErrorMessage.ForeColor = Color.Red
            labelErrorMessage.Location = New Point(30, 180)
            labelErrorMessage.MaximumSize = New Size(300, 0)
            labelErrorMessage.Text = ""
            labelErrorMessage.Visible = False

            ' buttonLogin
            buttonLogin.BackColor = Color.FromArgb(0, 120, 70)
            buttonLogin.FlatStyle = FlatStyle.Flat
            buttonLogin.FlatAppearance.BorderSize = 0
            buttonLogin.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            buttonLogin.ForeColor = Color.White
            buttonLogin.Location = New Point(70, 210)
            buttonLogin.Size = New Size(80, 30)
            buttonLogin.Text = "Login"

            ' buttonCancel
            buttonCancel.BackColor = Color.FromArgb(180, 180, 180)
            buttonCancel.FlatStyle = FlatStyle.Flat
            buttonCancel.FlatAppearance.BorderSize = 0
            buttonCancel.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            buttonCancel.ForeColor = Color.White
            buttonCancel.Location = New Point(160, 210)
            buttonCancel.Size = New Size(80, 30)
            buttonCancel.Text = "Cancel"

            ' buttonManagerLogin
            buttonManagerLogin.BackColor = Color.FromArgb(70, 130, 180)
            buttonManagerLogin.FlatStyle = FlatStyle.Flat
            buttonManagerLogin.FlatAppearance.BorderSize = 0
            buttonManagerLogin.Font = New Font("Segoe UI", 8.0F, FontStyle.Bold)
            buttonManagerLogin.ForeColor = Color.White
            buttonManagerLogin.Location = New Point(100, 250)
            buttonManagerLogin.Size = New Size(120, 25)
            buttonManagerLogin.Text = "Manager Login"

            ' StaffLoginForm
            ClientSize = New Size(400, 360)
            Controls.Add(panelLogin)
            FormBorderStyle = FormBorderStyle.FixedDialog
            MaximizeBox = False
            MinimizeBox = False
            Name = "StaffLoginForm"
            StartPosition = FormStartPosition.CenterScreen
            Text = "Staff Login - Excellence Care Solutions"
            panelLogin.ResumeLayout(False)
            panelLogin.PerformLayout()
            ResumeLayout(False)
        End Sub

        Private Sub buttonManagerLogin_Click(sender As Object, e As EventArgs) Handles buttonManagerLogin.Click
            ' Switch to manager login
            Me.DialogResult = DialogResult.Retry ' Use Retry to indicate manager login request
            Me.Close()
        End Sub

        Friend WithEvents labelCompanyTitle As Label
        Friend WithEvents labelLoginTitle As Label
        Friend WithEvents labelUserID As Label
        Friend WithEvents labelPIN As Label
        Friend WithEvents textBoxUserID As TextBox
        Friend WithEvents textBoxPIN As TextBox
        Friend WithEvents buttonLogin As Button
        Friend WithEvents buttonCancel As Button
        Friend WithEvents labelErrorMessage As Label
        Friend WithEvents labelInstructions As Label
        Friend WithEvents panelLogin As Panel
        Friend WithEvents buttonManagerLogin As Button

    End Class
End Namespace