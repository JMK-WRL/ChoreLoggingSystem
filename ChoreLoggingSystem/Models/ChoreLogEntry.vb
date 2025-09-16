Namespace Models
    Public Class ChoreLogEntry
        Public Property LogID As Integer
        Public Property UserID As String           ' Changed from StaffInitials
        Public Property StaffName As String
        Public Property AuthenticatedStaffID As Integer
        Public Property BranchID As Integer
        Public Property BranchName As String
        Public Property ShiftID As Integer
        Public Property ShiftName As String
        Public Property TaskID As Integer
        Public Property TaskName As String
        Public Property CompletedDateTime As DateTime
        Public Property Status As String
        Public Property Notes As String
    End Class
End Namespace