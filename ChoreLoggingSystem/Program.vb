Imports System
Imports System.Windows.Forms
Imports ChoreLoggingSystem.Forms

Module Program
    <STAThread>
    Sub Main()
        ' Enable visual styles for modern Windows appearance
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        Try
            ' Start with staff login (most common users)
            ShowStaffLogin()

        Catch ex As Exception
            ' Handle any startup errors
            MessageBox.Show("Application startup error: " & ex.Message & vbCrLf & vbCrLf &
                          "Please check your database connection and try again." & vbCrLf &
                          "Error Details: " & ex.GetType().Name,
                          "Startup Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ShowStaffLogin()
        Dim staffLoginForm As StaffLoginForm = Nothing

        Try
            staffLoginForm = New StaffLoginForm()
            Dim loginResult As DialogResult = staffLoginForm.ShowDialog()

            Select Case loginResult
                Case DialogResult.OK
                    ' Staff authentication successful - open task logging form
                    If staffLoginForm.AuthenticatedStaff IsNot Nothing Then
                        Dim mainForm As New MainForm(staffLoginForm.AuthenticatedStaff)
                        Application.Run(mainForm)
                    End If

                Case DialogResult.Retry
                    ' User clicked "Manager Login" button - switch to manager login
                    staffLoginForm.Dispose()
                    ShowManagerLogin()

                Case Else
                    ' Login cancelled - exit application
                    MessageBox.Show("Login cancelled. Application will exit.",
                                  "Application Startup",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information)
            End Select

        Finally
            If staffLoginForm IsNot Nothing Then
                staffLoginForm.Dispose()
            End If
        End Try
    End Sub

    Private Sub ShowManagerLogin()
        Dim managerLoginForm As ManagerLoginForm = Nothing
        Dim managerDashboard As ManagerDashboardForm = Nothing

        Try
            managerLoginForm = New ManagerLoginForm()
            Dim loginResult As DialogResult = managerLoginForm.ShowDialog()

            If loginResult = DialogResult.OK AndAlso managerLoginForm.AuthenticatedManager IsNot Nothing Then
                ' Manager authentication successful - open manager dashboard
                managerDashboard = New ManagerDashboardForm(managerLoginForm.AuthenticatedManager)
                Application.Run(managerDashboard)
            Else
                ' Manager login failed or cancelled - go back to staff login
                MessageBox.Show("Manager login cancelled. Returning to staff login.",
                              "Login",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information)
                ShowStaffLogin()
            End If

        Finally
            If managerLoginForm IsNot Nothing Then
                managerLoginForm.Dispose()
            End If
            If managerDashboard IsNot Nothing Then
                managerDashboard.Dispose()
            End If
        End Try
    End Sub
End Module