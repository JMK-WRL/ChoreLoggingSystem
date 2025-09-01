<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DashboardForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        labelTitle = New Label()
        labelFilters = New Label()
        labelFiltersBranch = New Label()
        comboBoxFilterBranch = New ComboBox()
        labelFiltersShift = New Label()
        comboBoxFilterShift = New ComboBox()
        labelFiltersFrom = New Label()
        dateTimePickerFrom = New DateTimePicker()
        labelFilterTo = New Label()
        dateTimePickerTo = New DateTimePicker()
        labelFilterStaff = New Label()
        textBoxFilterStaff = New TextBox()
        buttonApplyFilters = New Button()
        buttonExport = New Button()
        buttonClearFilters = New Button()
        labelRecordsCount = New Label()
        dataGridViewResults = New DataGridView()
        CType(dataGridViewResults, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' labelTitle
        ' 
        labelTitle.AutoSize = True
        labelTitle.Font = New Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        labelTitle.Location = New Point(20, 20)
        labelTitle.Name = "labelTitle"
        labelTitle.Size = New Size(321, 30)
        labelTitle.TabIndex = 0
        labelTitle.Text = "CHORE LOGGING DASHBOARD"
        ' 
        ' labelFilters
        ' 
        labelFilters.AutoSize = True
        labelFilters.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        labelFilters.Location = New Point(20, 70)
        labelFilters.Name = "labelFilters"
        labelFilters.Size = New Size(60, 21)
        labelFilters.TabIndex = 1
        labelFilters.Text = "Filters:"
        ' 
        ' labelFiltersBranch
        ' 
        labelFiltersBranch.AutoSize = True
        labelFiltersBranch.Location = New Point(20, 100)
        labelFiltersBranch.Name = "labelFiltersBranch"
        labelFiltersBranch.Size = New Size(47, 15)
        labelFiltersBranch.TabIndex = 2
        labelFiltersBranch.Text = "Branch:"
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
        ' labelFiltersShift
        ' 
        labelFiltersShift.AutoSize = True
        labelFiltersShift.Location = New Point(260, 100)
        labelFiltersShift.Name = "labelFiltersShift"
        labelFiltersShift.Size = New Size(34, 15)
        labelFiltersShift.TabIndex = 4
        labelFiltersShift.Text = "Shift:"
        ' 
        ' comboBoxFilterShift
        ' 
        comboBoxFilterShift.DropDownStyle = ComboBoxStyle.DropDownList
        comboBoxFilterShift.FormattingEnabled = True
        comboBoxFilterShift.Location = New Point(320, 100)
        comboBoxFilterShift.Name = "comboBoxFilterShift"
        comboBoxFilterShift.Size = New Size(120, 23)
        comboBoxFilterShift.TabIndex = 5
        ' 
        ' labelFiltersFrom
        ' 
        labelFiltersFrom.AutoSize = True
        labelFiltersFrom.Location = New Point(460, 100)
        labelFiltersFrom.Name = "labelFiltersFrom"
        labelFiltersFrom.Size = New Size(38, 15)
        labelFiltersFrom.TabIndex = 6
        labelFiltersFrom.Text = "From:"
        ' 
        ' dateTimePickerFrom
        ' 
        dateTimePickerFrom.Location = New Point(520, 100)
        dateTimePickerFrom.Name = "dateTimePickerFrom"
        dateTimePickerFrom.Size = New Size(100, 23)
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
        ' labelRecordsCount
        ' 
        labelRecordsCount.AutoSize = True
        labelRecordsCount.Location = New Point(20, 230)
        labelRecordsCount.Name = "labelRecordsCount"
        labelRecordsCount.Size = New Size(98, 15)
        labelRecordsCount.TabIndex = 15
        labelRecordsCount.Text = "Records Found: 0"
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
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(984, 661)
        Controls.Add(dataGridViewResults)
        Controls.Add(labelRecordsCount)
        Controls.Add(buttonClearFilters)
        Controls.Add(buttonExport)
        Controls.Add(buttonApplyFilters)
        Controls.Add(textBoxFilterStaff)
        Controls.Add(labelFilterStaff)
        Controls.Add(dateTimePickerTo)
        Controls.Add(labelFilterTo)
        Controls.Add(dateTimePickerFrom)
        Controls.Add(labelFiltersFrom)
        Controls.Add(comboBoxFilterShift)
        Controls.Add(labelFiltersShift)
        Controls.Add(comboBoxFilterBranch)
        Controls.Add(labelFiltersBranch)
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
    Friend WithEvents labelFiltersBranch As Label
    Friend WithEvents comboBoxFilterBranch As ComboBox
    Friend WithEvents labelFiltersShift As Label
    Friend WithEvents comboBoxFilterShift As ComboBox
    Friend WithEvents labelFiltersFrom As Label
    Friend WithEvents dateTimePickerFrom As DateTimePicker
    Friend WithEvents labelFilterTo As Label
    Friend WithEvents dateTimePickerTo As DateTimePicker
    Friend WithEvents labelFilterStaff As Label
    Friend WithEvents textBoxFilterStaff As TextBox
    Friend WithEvents buttonApplyFilters As Button
    Friend WithEvents buttonExport As Button
    Friend WithEvents buttonClearFilters As Button
    Friend WithEvents labelRecordsCount As Label
    Friend WithEvents dataGridViewResults As DataGridView
End Class
