Namespace Models
    Public Class Staff
        Public Property StaffID As Integer
        Public Property UserID As String
        Public Property FullName As String
        Public Property EmployeeID As String
        Public Property PIN As String
        Public Property IsActive As Boolean
        Public Property CreatedDate As DateTime
        Public Property LastLoginDate As DateTime?
        Public Property BranchID As Integer?

        Public Overrides Function ToString() As String
            Return $"{FullName} ({UserID})"
        End Function
    End Class
End Namespace