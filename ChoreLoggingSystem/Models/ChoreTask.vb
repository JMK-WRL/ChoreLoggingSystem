Namespace Models
    Public Class ChoreTask
        Public Property TaskID As Integer
        Public Property TaskName As String
        Public Property ShiftID As Integer
        Public Property EstimatedMinutes As Integer
        Public Property IsActive As Boolean
        Public Property CreatedDate As DateTime
        Public Property IsCompleted As Boolean ' For UI tracking

        Public Overrides Function ToString() As String
            Return TaskName
        End Function
    End Class
End Namespace