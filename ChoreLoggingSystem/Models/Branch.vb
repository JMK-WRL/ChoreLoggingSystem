Namespace Models
    Public Class Branch
        Public Property BranchID As Integer
        Public Property BranchName As String
        Public Property IsActive As Boolean
        Public Property CreatedDate As DateTime

        Public Overrides Function ToString() As String
            Return BranchName
        End Function
    End Class
End Namespace