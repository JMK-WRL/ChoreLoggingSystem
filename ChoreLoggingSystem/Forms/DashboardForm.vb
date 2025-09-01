Imports ChoreLoggingSystem.Models
Imports ChoreLoggingSystem.Services

Namespace Forms
    Partial Public Class DashboardForm
        Inherits Form

        Private ReadOnly _databaseService As DatabaseService

        Public Sub New()
            InitializeComponent()
            _databaseService = New DatabaseService()
            LoadInitialData()
        End Sub

        Private Sub LoadInitialData()
            Try
                ' Load filter dropdowns
                Dim branches As New List(Of Branch) From {New Branch With {.BranchID = -1, .BranchName = "All Branches"}}
                branches.AddRange(_databaseService.GetBranches())
                comboBoxFilterBranch.DataSource = branches
                comboBoxFilterBranch.DisplayMember = "BranchName"
                comboBoxFilterBranch.ValueMember = "BranchID"

                Dim shifts As New List(Of Shift) From {New Shift With {.ShiftID = -1, .ShiftName = "All Shifts"}}
                shifts.AddRange(_databaseService.GetShifts())
                comboBoxFilterBranch.DataSource = shifts
                comboBoxFilterBranch.DisplayMember = "ShiftName"
                comboBoxFilterBranch.ValueMember = "ShiftID"

                ' Set default date range (last 30 days)
                dateTimePickerFrom.Value = DateTime.Now.AddDays(-30)
                dateTimePickerTo.Value = DateTime.Now

                ' Load initial data
                LoadDashboardData()
            Catch ex As Exception
                MessageBox.Show($"Error loading dashboard data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub buttonApplyFilters_Click(sender As Object, e As EventArgs) Handles buttonApplyFilters.Click
            LoadDashboardData()
        End Sub

        Private Sub LoadDashboardData()
            Try
                Dim fromDate As DateTime = dateTimePickerFrom.Value.Date
                Dim toDate As DateTime = dateTimePickerTo.Value.Date
                Dim branchId As Integer? = If(CInt(comboBoxFilterBranch.SelectedValue) = -1, Nothing, CInt(comboBoxFilterBranch.SelectedValue))
                Dim shiftId As Integer? = If(CInt(comboBoxFilterBranch.SelectedValue) = -1, Nothing, CInt(comboBoxFilterBranch.SelectedValue))
                Dim staffInitials As String = If(String.IsNullOrWhiteSpace(textBoxFilterStaff.Text), Nothing, textBoxFilterStaff.Text.Trim())

                Dim entries As List(Of ChoreLogEntry) = _databaseService.GetChoreLogEntries(fromDate, toDate, branchId, shiftId, staffInitials)

                dataGridViewResults.DataSource = entries
                FormatDataGrid()

                labelRecordCount.Text = $"Records Found: {entries.Count}"
            Catch ex As Exception
                MessageBox.Show($"Error loading data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub FormatDataGrid()
            If dataGridViewResults.Columns.Count > 0 Then
                ' Format visible columns
                dataGridViewResults.Columns("LogID").HeaderText = "Log ID"
                dataGridViewResults.Columns("LogID").Width = 80
                dataGridViewResults.Columns("StaffInitials").HeaderText = "Staff"
                dataGridViewResults.Columns("StaffInitials").Width = 80
                dataGridViewResults.Columns("BranchName").HeaderText = "Branch"
                dataGridViewResults.Columns("BranchName").Width = 120
                dataGridViewResults.Columns("ShiftName").HeaderText = "Shift"
                dataGridViewResults.Columns("ShiftName").Width = 100
                dataGridViewResults.Columns("TaskName").HeaderText = "Task"
                dataGridViewResults.Columns("TaskName").Width = 250
                dataGridViewResults.Columns("CompletedDateTime").HeaderText = "Completed"
                dataGridViewResults.Columns("CompletedDateTime").Width = 150
                dataGridViewResults.Columns("Status").HeaderText = "Status"
                dataGridViewResults.Columns("Status").Width = 80
                dataGridViewResults.Columns("Notes").HeaderText = "Notes"
                dataGridViewResults.Columns("Notes").Width = 200

                ' Hide ID columns (not useful for users)
                dataGridViewResults.Columns("BranchID").Visible = False
                dataGridViewResults.Columns("ShiftID").Visible = False
                dataGridViewResults.Columns("TaskID").Visible = False

                ' Format datetime column
                dataGridViewResults.Columns("CompletedDateTime").DefaultCellStyle.Format = "MM/dd/yyyy HH:mm"
            End If
        End Sub

        Private Sub buttonExport_Click(sender As Object, e As EventArgs) Handles buttonExport.Click
            Try
                Dim saveFileDialog As New SaveFileDialog With {
                    .Filter = "Excel Files|*.xlsx|CSV Files|*.csv",
                    .Title = "Export Dashboard Data",
                    .FileName = $"ChoreLog_Export_{DateTime.Now:yyyyMMdd_HHmm}"
                }

                If saveFileDialog.ShowDialog() = DialogResult.OK Then
                    ' For now, show message - actual export implementation would go here
                    MessageBox.Show($"Export functionality would save to: {saveFileDialog.FileName}" & vbCrLf &
                                  $"Records to export: {dataGridViewResults.Rows.Count}",
                                  "Export Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Exception
                MessageBox.Show($"Error exporting data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub buttonClearFilters_Click(sender As Object, e As EventArgs) Handles buttonClearFilters.Click
            Try
                ' Reset all filters to default values
                comboBoxFilterBranch.SelectedIndex = 0 ' "All Branches"
                comboBoxFilterBranch.SelectedIndex = 0   ' "All Shifts" 
                textBoxFilterStaff.Clear()
                dateTimePickerFrom.Value = DateTime.Now.AddDays(-30)
                dateTimePickerTo.Value = DateTime.Now

                ' Reload data with cleared filters
                LoadDashboardData()

                MessageBox.Show("Filters cleared and data refreshed.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show($"Error clearing filters: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub InitializeComponent()
            labelTitle = New Label()
            labelFilters = New Label()
            labelFilterBranch = New Label()
            comboBoxFilterBranch = New ComboBox()
            labelFilterShift = New Label()
            comboBoxFilter = New ComboBox()
            labelFilterFrom = New Label()
            dateTimePickerFrom = New DateTimePicker()
            labelFilterTo = New Label()
            dateTimePickerTo = New DateTimePicker()
            labelFilterStaff = New Label()
            textBoxFilterStaff = New TextBox()
            buttonApplyFilters = New Button()
            buttonExport = New Button()
            buttonClearFilters = New Button()
            labelRecordCount = New Label()
            dataGridViewResults = New DataGridView()
            CType(dataGridViewResults, ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()
            ' 
            ' labelTitle
            ' 
            labelTitle.AutoSize = True
            labelTitle.Location = New Point(20, 20)
            labelTitle.Name = "labelTitle"
            labelTitle.Size = New Size(173, 15)
            labelTitle.TabIndex = 0
            labelTitle.Text = "CHORE LOGGING DASHBOARD"
            ' 
            ' labelFilters
            ' 
            labelFilters.AutoSize = True
            labelFilters.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelFilters.Location = New Point(20, 70)
            labelFilters.Name = "labelFilters"
            labelFilters.Size = New Size(44, 15)
            labelFilters.TabIndex = 1
            labelFilters.Text = "Filters:"
            ' 
            ' labelFilterBranch
            ' 
            labelFilterBranch.AutoSize = True
            labelFilterBranch.Location = New Point(20, 100)
            labelFilterBranch.Name = "labelFilterBranch"
            labelFilterBranch.Size = New Size(47, 15)
            labelFilterBranch.TabIndex = 2
            labelFilterBranch.Text = "Branch:"
            ' 
            ' comboBoxFilterBranch
            ' 
            comboBoxFilterBranch.DropDownStyle = ComboBoxStyle.DropDownList
            comboBoxFilterBranch.FormattingEnabled = True
            comboBoxFilterBranch.Location = New Point(90, 100)
            comboBoxFilterBranch.Name = "comboBoxFilterBranch"
            comboBoxFilterBranch.Size = New Size(150, 23)
            comboBoxFilterBranch.TabIndex = 3
            ' 
            ' labelFilterShift
            ' 
            labelFilterShift.AutoSize = True
            labelFilterShift.Location = New Point(260, 100)
            labelFilterShift.Name = "labelFilterShift"
            labelFilterShift.Size = New Size(34, 15)
            labelFilterShift.TabIndex = 4
            labelFilterShift.Text = "Shift:"
            ' 
            ' comboBoxFilter
            ' 
            comboBoxFilter.DropDownStyle = ComboBoxStyle.DropDownList
            comboBoxFilter.FormattingEnabled = True
            comboBoxFilter.Location = New Point(320, 100)
            comboBoxFilter.Name = "comboBoxFilter"
            comboBoxFilter.Size = New Size(120, 23)
            comboBoxFilter.TabIndex = 5
            ' 
            ' labelFilterFrom
            ' 
            labelFilterFrom.AutoSize = True
            labelFilterFrom.Location = New Point(460, 100)
            labelFilterFrom.Name = "labelFilterFrom"
            labelFilterFrom.Size = New Size(38, 15)
            labelFilterFrom.TabIndex = 6
            labelFilterFrom.Text = "From:"
            ' 
            ' dateTimePickerFrom
            ' 
            dateTimePickerFrom.Location = New Point(520, 100)
            dateTimePickerFrom.Name = "dateTimePickerFrom"
            dateTimePickerFrom.Size = New Size(120, 23)
            dateTimePickerFrom.TabIndex = 7
            ' 
            ' labelFilterTo
            ' 
            labelFilterTo.AutoSize = True
            labelFilterTo.Location = New Point(660, 100)
            labelFilterTo.Name = "labelFilterTo"
            labelFilterTo.Size = New Size(22, 15)
            labelFilterTo.TabIndex = 8
            labelFilterTo.Text = "To:"
            ' 
            ' dateTimePickerTo
            ' 
            dateTimePickerTo.Location = New Point(700, 100)
            dateTimePickerTo.Name = "dateTimePickerTo"
            dateTimePickerTo.Size = New Size(120, 23)
            dateTimePickerTo.TabIndex = 9
            ' 
            ' labelFilterStaff
            ' 
            labelFilterStaff.AutoSize = True
            labelFilterStaff.Location = New Point(20, 140)
            labelFilterStaff.Name = "labelFilterStaff"
            labelFilterStaff.Size = New Size(71, 15)
            labelFilterStaff.TabIndex = 10
            labelFilterStaff.Text = "Staff Initials:"
            ' 
            ' textBoxFilterStaff
            ' 
            textBoxFilterStaff.Location = New Point(130, 140)
            textBoxFilterStaff.Name = "textBoxFilterStaff"
            textBoxFilterStaff.Size = New Size(100, 23)
            textBoxFilterStaff.TabIndex = 11
            ' 
            ' buttonApplyFilters
            ' 
            buttonApplyFilters.Location = New Point(20, 180)
            buttonApplyFilters.Name = "buttonApplyFilters"
            buttonApplyFilters.Size = New Size(100, 35)
            buttonApplyFilters.TabIndex = 12
            buttonApplyFilters.Text = "Apply Filters"
            buttonApplyFilters.UseVisualStyleBackColor = True
            ' 
            ' buttonExport
            ' 
            buttonExport.Location = New Point(140, 180)
            buttonExport.Name = "buttonExport"
            buttonExport.Size = New Size(80, 35)
            buttonExport.TabIndex = 13
            buttonExport.Text = "Export"
            buttonExport.UseVisualStyleBackColor = True
            ' 
            ' buttonClearFilters
            ' 
            buttonClearFilters.Location = New Point(240, 180)
            buttonClearFilters.Name = "buttonClearFilters"
            buttonClearFilters.Size = New Size(100, 35)
            buttonClearFilters.TabIndex = 14
            buttonClearFilters.Text = "Clear Filters"
            buttonClearFilters.UseVisualStyleBackColor = True
            ' 
            ' labelRecordCount
            ' 
            labelRecordCount.AutoSize = True
            labelRecordCount.Location = New Point(20, 230)
            labelRecordCount.Name = "labelRecordCount"
            labelRecordCount.Size = New Size(98, 15)
            labelRecordCount.TabIndex = 15
            labelRecordCount.Text = "Records Found: 0"
            ' 
            ' dataGridViewResults
            ' 
            dataGridViewResults.AllowUserToAddRows = False
            dataGridViewResults.AllowUserToDeleteRows = False
            dataGridViewResults.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            dataGridViewResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            dataGridViewResults.Location = New Point(20, 260)
            dataGridViewResults.Name = "dataGridViewResults"
            dataGridViewResults.ReadOnly = True
            dataGridViewResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dataGridViewResults.Size = New Size(940, 380)
            dataGridViewResults.TabIndex = 16
            ' 
            ' DashboardForm
            ' 
            ClientSize = New Size(984, 661)
            Controls.Add(dataGridViewResults)
            Controls.Add(labelRecordCount)
            Controls.Add(buttonClearFilters)
            Controls.Add(buttonExport)
            Controls.Add(buttonApplyFilters)
            Controls.Add(textBoxFilterStaff)
            Controls.Add(labelFilterStaff)
            Controls.Add(dateTimePickerTo)
            Controls.Add(labelFilterTo)
            Controls.Add(dateTimePickerFrom)
            Controls.Add(labelFilterFrom)
            Controls.Add(comboBoxFilter)
            Controls.Add(labelFilterShift)
            Controls.Add(comboBoxFilterBranch)
            Controls.Add(labelFilterBranch)
            Controls.Add(labelFilters)
            Controls.Add(labelTitle)
            Name = "DashboardForm"
            StartPosition = FormStartPosition.CenterScreen
            Text = "Chore Logging Dashboard"
            CType(dataGridViewResults, ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)
            PerformLayout()

        End Sub

        Friend WithEvents labelTitle As Label
        Friend WithEvents labelFilters As Label
        Friend WithEvents labelFilterBranch As Label
        Friend WithEvents comboBoxFilterBranch As ComboBox
        Friend WithEvents labelFilterShift As Label
        Friend WithEvents comboBoxFilter As ComboBox
        Friend WithEvents labelFilterFrom As Label
        Friend WithEvents dateTimePickerFrom As DateTimePicker
        Friend WithEvents labelFilterTo As Label
        Friend WithEvents dateTimePickerTo As DateTimePicker
        Friend WithEvents labelFilterStaff As Label
        Friend WithEvents textBoxFilterStaff As TextBox
        Friend WithEvents buttonApplyFilters As Button
        Friend WithEvents buttonExport As Button
        Friend WithEvents buttonClearFilters As Button
        Friend WithEvents labelRecordCount As Label
        Friend WithEvents dataGridViewResults As DataGridView
    End Class
End Namespace