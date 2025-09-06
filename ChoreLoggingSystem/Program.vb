Imports System
Imports System.Windows.Forms
Imports ChoreLoggingSystem.Forms

Module Program
    <STAThread>
    Sub Main()
        ' Enable visual styles for modern Windows appearance
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        Dim loginForm As ManagerLoginForm = Nothing

        Try
            ' Show manager login form first
            loginForm = New ManagerLoginForm()

            ' Show login dialog and check result
            Dim loginResult As DialogResult = loginForm.ShowDialog()

            If loginResult = DialogResult.OK AndAlso loginForm.AuthenticatedManager IsNot Nothing Then
                ' Authentication successful - open manager dashboard
                Dim managerDashboard As New ManagerDashboardForm(loginForm.AuthenticatedManager)


                ' Run the main application with the dashboard form
                Application.Run(ManagerDashboardForm)
            Else
                ' Authentication failed or cancelled - exit application
                MessageBox.Show("Login cancelled or failed. Application will exit.",
                              "Application Startup",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            ' Handle any startup errors
            MessageBox.Show("Application startup error: " & ex.Message & vbCrLf & vbCrLf &
                          "Please check your database connection and try again." & vbCrLf &
                          "Error Details: " & ex.GetType().Name,
                          "Startup Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Clean up login form
            If loginForm IsNot Nothing Then
                loginForm.Dispose()
            End If
        End Try
    End Sub
End Module