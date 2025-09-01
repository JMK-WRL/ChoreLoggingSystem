Namespace Models
    Public Class Shift
        Public Property ShiftID As Integer
        Public Property ShiftName As String
        Public Property StartTime As TimeSpan
        Public Property EndTime As TimeSpan
        Public Property IsActive As Boolean
        Public Property CreatedDate As DateTime

        Public Overrides Function ToString() As String
            Return ShiftName
        End Function
    End Class
End Namespace