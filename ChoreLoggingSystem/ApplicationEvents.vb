Imports Microsoft.VisualBasic.ApplicationServices
Imports ChoreLoggingSystem.Forms

Namespace My
    Partial Friend Class MyApplication
        Protected Overrides Function OnStartup(eventArgs As ApplicationServices.StartupEventArgs) As Boolean
            Try
                ' Show manager login form first
                Dim loginForm As New ManagerLoginForm()

                Dim loginResult As DialogResult = loginForm.ShowDialog()

                If loginResult = DialogResult.OK AndAlso loginForm.AuthenticatedManager IsNot Nothing Then
                    ' Authentication successful - open manager dashboard
                    Dim managerDashboard As New ManagerDashboardForm(loginForm.AuthenticatedManager)
                    Me.MainForm = managerDashboard

                    ' Clean up login form
                    loginForm.Dispose()

                    Return True ' Continue with application startup
                Else
                    ' Authentication failed or cancelled - exit application
                    If loginForm IsNot Nothing Then
                        loginForm.Dispose()
                    End If
                    Return False ' Exit application
                End If

            Catch ex As Exception
                MessageBox.Show($"Application startup error: {ex.Message}" & vbCrLf & vbCrLf &
                              "Please check your database connection and try again.",
                              "Startup Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False ' Exit application on error
            End Try
        End Function
    End Class
End Namespace