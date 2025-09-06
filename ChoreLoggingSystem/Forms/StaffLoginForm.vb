Imports ChoreLoggingSystem.Models
Imports ChoreLoggingSystem.Services

Public Class StaffLoginForm
    Private ReadOnly _databaseService As DatabaseService
    Private _authenticatedStaff As Staff

    ' This property is what the Program.vb is looking for
    Public ReadOnly Property AuthenticatedStaff As Staff
        Get
            Return _authenticatedStaff
        End Get
    End Property

    Public Sub New()
        ' This call is required by the designer
        InitializeComponent()

        ' Initialize the database service
        _databaseService = New DatabaseService()

        ' Initialize the form
        InitializeLoginForm()
    End Sub

    Private Sub InitializeLoginForm()
        ' Set form properties (you may have already set these in designer)
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
                ' Authentication successful
                Return staff
            End If

            Return Nothing
        Catch ex As Exception
            ' Log error but don't expose to user
            Return Nothing
        End Try
    End Function

    Private Sub buttonCancel_Click(sender As Object, e As EventArgs) Handles buttonCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub buttonManagerLogin_Click(sender As Object, e As EventArgs) Handles buttonManagerLogin.Click
        ' Switch to manager login
        Me.DialogResult = DialogResult.Retry ' Use Retry to indicate manager login request
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

End Class