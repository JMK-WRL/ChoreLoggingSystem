Imports Microsoft.VisualBasic.ApplicationServices
Imports ChoreLoggingSystem.Forms

Namespace My
    Partial Friend Class MyApplication
        Protected Overrides Function OnStartup(eventArgs As ApplicationServices.StartupEventArgs) As Boolean
            ' For testing - directly start MainForm
            Dim mainForm As New MainForm()
            Me.MainForm = mainForm
            Return True
        End Function
    End Class
End Namespace