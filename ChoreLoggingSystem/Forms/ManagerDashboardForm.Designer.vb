Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class ManagerDashboardForm
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
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
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            tabControlManager = New TabControl()
            tabPageTaskLogging = New TabPage()
            panelTaskLogging = New Panel()
            groupBoxTaskEntry = New GroupBox()
            buttonResetTaskForm = New Button()
            buttonSubmitTasks = New Button()
            textBoxTaskNotes = New TextBox()
            labelTaskNotes = New Label()
            checkedListBoxTasks = New CheckedListBox()
            labelTasks = New Label()
            comboBoxTaskShift = New ComboBox()
            labelTaskShift = New Label()
            comboBoxTaskBranch = New ComboBox()
            labelTaskBranch = New Label()
            textBoxStaffName = New TextBox()
            labelStaffName = New Label()
            textBoxUserID = New TextBox()
            labelUserID = New Label()
            labelTaskLoggingTitle = New Label()
            tabPageReports = New TabPage()
            panelReports = New Panel()
            dataGridViewResults = New DataGridView()
            labelRecordsCount = New Label()
            groupBoxFilters = New GroupBox()
            buttonExportData = New Button()
            buttonClearFilters = New Button()
            buttonApplyFilters = New Button()
            comboBoxFilterStaff = New ComboBox()
            labelFilterStaff = New Label()
            comboBoxFilterShift = New ComboBox()
            labelFilterShift = New Label()
            comboBoxFilterBranch = New ComboBox()
            labelFilterBranch = New Label()
            dateTimePickerTo = New DateTimePicker()
            labelFilterTo = New Label()
            dateTimePickerFrom = New DateTimePicker()
            labelFilterFrom = New Label()
            groupBoxSummary = New GroupBox()
            labelMostActiveBranch = New Label()
            labelBranchesActive = New Label()
            labelUniqueStaff = New Label()
            labelTotalTasks = New Label()
            labelReportsTitle = New Label()
            tabControlManager.SuspendLayout()
            tabPageTaskLogging.SuspendLayout()
            panelTaskLogging.SuspendLayout()
            groupBoxTaskEntry.SuspendLayout()
            tabPageReports.SuspendLayout()
            panelReports.SuspendLayout()
            CType(dataGridViewResults, ComponentModel.ISupportInitialize).BeginInit()
            groupBoxFilters.SuspendLayout()
            groupBoxSummary.SuspendLayout()
            SuspendLayout()
            ' 
            ' tabControlManager
            ' 
            tabControlManager.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            tabControlManager.Controls.Add(tabPageTaskLogging)
            tabControlManager.Controls.Add(tabPageReports)
            tabControlManager.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            tabControlManager.Location = New Point(12, 12)
            tabControlManager.Name = "tabControlManager"
            tabControlManager.SelectedIndex = 0
            tabControlManager.Size = New Size(1160, 700)
            tabControlManager.TabIndex = 0
            ' 
            ' tabPageTaskLogging
            ' 
            tabPageTaskLogging.Controls.Add(panelTaskLogging)
            tabPageTaskLogging.Location = New Point(4, 26)
            tabPageTaskLogging.Name = "tabPageTaskLogging"
            tabPageTaskLogging.Padding = New Padding(3)
            tabPageTaskLogging.Size = New Size(1152, 670)
            tabPageTaskLogging.TabIndex = 0
            tabPageTaskLogging.Text = "Task Logging"
            tabPageTaskLogging.UseVisualStyleBackColor = True
            ' 
            ' panelTaskLogging
            ' 
            panelTaskLogging.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            panelTaskLogging.BackColor = Color.White
            panelTaskLogging.Controls.Add(groupBoxTaskEntry)
            panelTaskLogging.Controls.Add(labelTaskLoggingTitle)
            panelTaskLogging.Location = New Point(6, 6)
            panelTaskLogging.Name = "panelTaskLogging"
            panelTaskLogging.Size = New Size(1140, 657)
            panelTaskLogging.TabIndex = 0
            ' 
            ' groupBoxTaskEntry
            ' 
            groupBoxTaskEntry.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            groupBoxTaskEntry.BackColor = Color.WhiteSmoke
            groupBoxTaskEntry.Controls.Add(buttonResetTaskForm)
            groupBoxTaskEntry.Controls.Add(buttonSubmitTasks)
            groupBoxTaskEntry.Controls.Add(textBoxTaskNotes)
            groupBoxTaskEntry.Controls.Add(labelTaskNotes)
            groupBoxTaskEntry.Controls.Add(checkedListBoxTasks)
            groupBoxTaskEntry.Controls.Add(labelTasks)
            groupBoxTaskEntry.Controls.Add(comboBoxTaskShift)
            groupBoxTaskEntry.Controls.Add(labelTaskShift)
            groupBoxTaskEntry.Controls.Add(comboBoxTaskBranch)
            groupBoxTaskEntry.Controls.Add(labelTaskBranch)
            groupBoxTaskEntry.Controls.Add(textBoxStaffName)
            groupBoxTaskEntry.Controls.Add(labelStaffName)
            groupBoxTaskEntry.Controls.Add(textBoxUserID)
            groupBoxTaskEntry.Controls.Add(labelUserID)
            groupBoxTaskEntry.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            groupBoxTaskEntry.Location = New Point(20, 70)
            groupBoxTaskEntry.Name = "groupBoxTaskEntry"
            groupBoxTaskEntry.Size = New Size(1100, 570)
            groupBoxTaskEntry.TabIndex = 1
            groupBoxTaskEntry.TabStop = False
            groupBoxTaskEntry.Text = "Task Entry Form"
            ' 
            ' buttonResetTaskForm
            ' 
            buttonResetTaskForm.BackColor = Color.Gray
            buttonResetTaskForm.FlatStyle = FlatStyle.Flat
            buttonResetTaskForm.ForeColor = Color.White
            buttonResetTaskForm.Location = New Point(250, 510)
            buttonResetTaskForm.Name = "buttonResetTaskForm"
            buttonResetTaskForm.Size = New Size(150, 40)
            buttonResetTaskForm.TabIndex = 13
            buttonResetTaskForm.Text = "Reset Form"
            buttonResetTaskForm.UseVisualStyleBackColor = False
            ' 
            ' buttonSubmitTasks
            ' 
            buttonSubmitTasks.BackColor = Color.Green
            buttonSubmitTasks.FlatStyle = FlatStyle.Flat
            buttonSubmitTasks.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            buttonSubmitTasks.ForeColor = Color.White
            buttonSubmitTasks.Location = New Point(30, 510)
            buttonSubmitTasks.Name = "buttonSubmitTasks"
            buttonSubmitTasks.Size = New Size(200, 40)
            buttonSubmitTasks.TabIndex = 12
            buttonSubmitTasks.Text = "Submit Tasks"
            buttonSubmitTasks.UseVisualStyleBackColor = False
            ' 
            ' textBoxTaskNotes
            ' 
            textBoxTaskNotes.BorderStyle = BorderStyle.FixedSingle
            textBoxTaskNotes.Location = New Point(30, 420)
            textBoxTaskNotes.Multiline = True
            textBoxTaskNotes.Name = "textBoxTaskNotes"
            textBoxTaskNotes.ScrollBars = ScrollBars.Vertical
            textBoxTaskNotes.Size = New Size(1040, 70)
            textBoxTaskNotes.TabIndex = 11
            ' 
            ' labelTaskNotes
            ' 
            labelTaskNotes.AutoSize = True
            labelTaskNotes.Location = New Point(30, 390)
            labelTaskNotes.Name = "labelTaskNotes"
            labelTaskNotes.Size = New Size(132, 20)
            labelTaskNotes.TabIndex = 10
            labelTaskNotes.Text = "Additional Notes:"
            ' 
            ' checkedListBoxTasks
            ' 
            checkedListBoxTasks.BackColor = Color.White
            checkedListBoxTasks.BorderStyle = BorderStyle.FixedSingle
            checkedListBoxTasks.CheckOnClick = True
            checkedListBoxTasks.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            checkedListBoxTasks.FormattingEnabled = True
            checkedListBoxTasks.Location = New Point(30, 170)
            checkedListBoxTasks.Name = "checkedListBoxTasks"
            checkedListBoxTasks.Size = New Size(1040, 182)
            checkedListBoxTasks.TabIndex = 9
            ' 
            ' labelTasks
            ' 
            labelTasks.AutoSize = True
            labelTasks.Location = New Point(30, 140)
            labelTasks.Name = "labelTasks"
            labelTasks.Size = New Size(255, 20)
            labelTasks.TabIndex = 8
            labelTasks.Text = "Completed Tasks for Selected Shift:"
            ' 
            ' comboBoxTaskShift
            ' 
            comboBoxTaskShift.DropDownStyle = ComboBoxStyle.DropDownList
            comboBoxTaskShift.FormattingEnabled = True
            comboBoxTaskShift.Location = New Point(600, 88)
            comboBoxTaskShift.Name = "comboBoxTaskShift"
            comboBoxTaskShift.Size = New Size(280, 28)
            comboBoxTaskShift.TabIndex = 7
            ' 
            ' labelTaskShift
            ' 
            labelTaskShift.AutoSize = True
            labelTaskShift.Location = New Point(470, 90)
            labelTaskShift.Name = "labelTaskShift"
            labelTaskShift.Size = New Size(88, 20)
            labelTaskShift.TabIndex = 6
            labelTaskShift.Text = "Work Shift:"
            ' 
            ' comboBoxTaskBranch
            ' 
            comboBoxTaskBranch.DropDownStyle = ComboBoxStyle.DropDownList
            comboBoxTaskBranch.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            comboBoxTaskBranch.FormattingEnabled = True
            comboBoxTaskBranch.Location = New Point(180, 88)
            comboBoxTaskBranch.Name = "comboBoxTaskBranch"
            comboBoxTaskBranch.Size = New Size(250, 28)
            comboBoxTaskBranch.TabIndex = 5
            ' 
            ' labelTaskBranch
            ' 
            labelTaskBranch.AutoSize = True
            labelTaskBranch.Location = New Point(30, 90)
            labelTaskBranch.Name = "labelTaskBranch"
            labelTaskBranch.Size = New Size(73, 20)
            labelTaskBranch.TabIndex = 4
            labelTaskBranch.Text = "Location:"
            ' 
            ' textBoxStaffName
            ' 
            textBoxStaffName.Location = New Point(550, 38)
            textBoxStaffName.Name = "textBoxStaffName"
            textBoxStaffName.ReadOnly = True
            textBoxStaffName.Size = New Size(250, 27)
            textBoxStaffName.TabIndex = 3
            textBoxStaffName.Text = "Enter User ID to auto-fill name"
            ' 
            ' labelStaffName
            ' 
            labelStaffName.AutoSize = True
            labelStaffName.Location = New Point(420, 40)
            labelStaffName.Name = "labelStaffName"
            labelStaffName.Size = New Size(93, 20)
            labelStaffName.TabIndex = 2
            labelStaffName.Text = "Staff Name:"
            ' 
            ' textBoxUserID
            ' 
            textBoxUserID.CharacterCasing = CharacterCasing.Upper
            textBoxUserID.Font = New Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            textBoxUserID.Location = New Point(180, 38)
            textBoxUserID.MaxLength = 10
            textBoxUserID.Name = "textBoxUserID"
            textBoxUserID.Size = New Size(200, 27)
            textBoxUserID.TabIndex = 1
            ' 
            ' labelUserID
            ' 
            labelUserID.AutoSize = True
            labelUserID.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelUserID.Location = New Point(30, 40)
            labelUserID.Name = "labelUserID"
            labelUserID.Size = New Size(90, 17)
            labelUserID.TabIndex = 0
            labelUserID.Text = "Staff User ID:"
            ' 
            ' labelTaskLoggingTitle
            ' 
            labelTaskLoggingTitle.AutoSize = True
            labelTaskLoggingTitle.Font = New Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelTaskLoggingTitle.ForeColor = Color.DarkBlue
            labelTaskLoggingTitle.Location = New Point(20, 20)
            labelTaskLoggingTitle.Name = "labelTaskLoggingTitle"
            labelTaskLoggingTitle.Size = New Size(441, 30)
            labelTaskLoggingTitle.TabIndex = 0
            labelTaskLoggingTitle.Text = "Task Logging - Log Tasks for Staff Members"
            ' 
            ' tabPageReports
            ' 
            tabPageReports.Controls.Add(panelReports)
            tabPageReports.Location = New Point(4, 26)
            tabPageReports.Name = "tabPageReports"
            tabPageReports.Padding = New Padding(3)
            tabPageReports.Size = New Size(1152, 670)
            tabPageReports.TabIndex = 1
            tabPageReports.Text = "Reports & Analytics"
            tabPageReports.UseVisualStyleBackColor = True
            ' 
            ' panelReports
            ' 
            panelReports.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            panelReports.BackColor = Color.White
            panelReports.Controls.Add(dataGridViewResults)
            panelReports.Controls.Add(labelRecordsCount)
            panelReports.Controls.Add(groupBoxFilters)
            panelReports.Controls.Add(groupBoxSummary)
            panelReports.Controls.Add(labelReportsTitle)
            panelReports.Location = New Point(6, 6)
            panelReports.Name = "panelReports"
            panelReports.Size = New Size(1140, 657)
            panelReports.TabIndex = 0
            ' 
            ' dataGridViewResults
            ' 
            dataGridViewResults.AllowUserToAddRows = False
            dataGridViewResults.AllowUserToDeleteRows = False
            dataGridViewResults.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            dataGridViewResults.BackgroundColor = Color.White
            dataGridViewResults.BorderStyle = BorderStyle.Fixed3D
            dataGridViewResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            dataGridViewResults.Location = New Point(20, 340)
            dataGridViewResults.Name = "dataGridViewResults"
            dataGridViewResults.ReadOnly = True
            dataGridViewResults.RowHeadersVisible = False
            dataGridViewResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dataGridViewResults.Size = New Size(1100, 300)
            dataGridViewResults.TabIndex = 4
            ' 
            ' labelRecordsCount
            ' 
            labelRecordsCount.AutoSize = True
            labelRecordsCount.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelRecordsCount.Location = New Point(20, 310)
            labelRecordsCount.Name = "labelRecordsCount"
            labelRecordsCount.Size = New Size(130, 20)
            labelRecordsCount.TabIndex = 3
            labelRecordsCount.Text = "Records Found: 0"
            ' 
            ' groupBoxFilters
            ' 
            groupBoxFilters.BackColor = Color.WhiteSmoke
            groupBoxFilters.Controls.Add(buttonExportData)
            groupBoxFilters.Controls.Add(buttonClearFilters)
            groupBoxFilters.Controls.Add(buttonApplyFilters)
            groupBoxFilters.Controls.Add(comboBoxFilterStaff)
            groupBoxFilters.Controls.Add(labelFilterStaff)
            groupBoxFilters.Controls.Add(comboBoxFilterShift)
            groupBoxFilters.Controls.Add(labelFilterShift)
            groupBoxFilters.Controls.Add(comboBoxFilterBranch)
            groupBoxFilters.Controls.Add(labelFilterBranch)
            groupBoxFilters.Controls.Add(dateTimePickerTo)
            groupBoxFilters.Controls.Add(labelFilterTo)
            groupBoxFilters.Controls.Add(dateTimePickerFrom)
            groupBoxFilters.Controls.Add(labelFilterFrom)
            groupBoxFilters.Location = New Point(20, 170)
            groupBoxFilters.Name = "groupBoxFilters"
            groupBoxFilters.Size = New Size(1100, 120)
            groupBoxFilters.TabIndex = 2
            groupBoxFilters.TabStop = False
            groupBoxFilters.Text = "Filters & Options"
            ' 
            ' buttonExportData
            ' 
            buttonExportData.BackColor = Color.Green
            buttonExportData.ForeColor = Color.White
            buttonExportData.Location = New Point(630, 70)
            buttonExportData.Name = "buttonExportData"
            buttonExportData.Size = New Size(130, 30)
            buttonExportData.TabIndex = 11
            buttonExportData.Text = "Export Data"
            buttonExportData.UseVisualStyleBackColor = False
            ' 
            ' buttonClearFilters
            ' 
            buttonClearFilters.BackColor = Color.Gray
            buttonClearFilters.ForeColor = Color.White
            buttonClearFilters.Location = New Point(480, 70)
            buttonClearFilters.Name = "buttonClearFilters"
            buttonClearFilters.Size = New Size(130, 30)
            buttonClearFilters.TabIndex = 10
            buttonClearFilters.Text = "Clear Filters"
            buttonClearFilters.UseVisualStyleBackColor = False
            ' 
            ' buttonApplyFilters
            ' 
            buttonApplyFilters.BackColor = Color.Blue
            buttonApplyFilters.ForeColor = Color.White
            buttonApplyFilters.Location = New Point(330, 70)
            buttonApplyFilters.Name = "buttonApplyFilters"
            buttonApplyFilters.Size = New Size(130, 30)
            buttonApplyFilters.TabIndex = 3
            buttonApplyFilters.Text = "Apply Filters"
            buttonApplyFilters.UseVisualStyleBackColor = False
            ' 
            ' comboBoxFilterStaff
            ' 
            comboBoxFilterStaff.DropDownStyle = ComboBoxStyle.DropDownList
            comboBoxFilterStaff.FormattingEnabled = True
            comboBoxFilterStaff.Location = New Point(140, 73)
            comboBoxFilterStaff.Name = "comboBoxFilterStaff"
            comboBoxFilterStaff.Size = New Size(180, 25)
            comboBoxFilterStaff.TabIndex = 9
            ' 
            ' labelFilterStaff
            ' 
            labelFilterStaff.AutoSize = True
            labelFilterStaff.Location = New Point(30, 75)
            labelFilterStaff.Name = "labelFilterStaff"
            labelFilterStaff.Size = New Size(87, 17)
            labelFilterStaff.TabIndex = 8
            labelFilterStaff.Text = "Staff Initials:"
            ' 
            ' comboBoxFilterShift
            ' 
            comboBoxFilterShift.DropDownStyle = ComboBoxStyle.DropDownList
            comboBoxFilterShift.FormattingEnabled = True
            comboBoxFilterShift.Location = New Point(900, 33)
            comboBoxFilterShift.Name = "comboBoxFilterShift"
            comboBoxFilterShift.Size = New Size(150, 25)
            comboBoxFilterShift.TabIndex = 7
            ' 
            ' labelFilterShift
            ' 
            labelFilterShift.AutoSize = True
            labelFilterShift.Location = New Point(830, 35)
            labelFilterShift.Name = "labelFilterShift"
            labelFilterShift.Size = New Size(41, 17)
            labelFilterShift.TabIndex = 6
            labelFilterShift.Text = "Shift:"
            ' 
            ' comboBoxFilterBranch
            ' 
            comboBoxFilterBranch.DropDownStyle = ComboBoxStyle.DropDownList
            comboBoxFilterBranch.FormattingEnabled = True
            comboBoxFilterBranch.Location = New Point(630, 33)
            comboBoxFilterBranch.Name = "comboBoxFilterBranch"
            comboBoxFilterBranch.Size = New Size(180, 25)
            comboBoxFilterBranch.TabIndex = 5
            ' 
            ' labelFilterBranch
            ' 
            labelFilterBranch.AutoSize = True
            labelFilterBranch.Location = New Point(550, 35)
            labelFilterBranch.Name = "labelFilterBranch"
            labelFilterBranch.Size = New Size(54, 17)
            labelFilterBranch.TabIndex = 4
            labelFilterBranch.Text = "Branch:"
            ' 
            ' dateTimePickerTo
            ' 
            dateTimePickerTo.Format = DateTimePickerFormat.Short
            dateTimePickerTo.Location = New Point(380, 33)
            dateTimePickerTo.Name = "dateTimePickerTo"
            dateTimePickerTo.Size = New Size(150, 25)
            dateTimePickerTo.TabIndex = 3
            ' 
            ' labelFilterTo
            ' 
            labelFilterTo.AutoSize = True
            labelFilterTo.Location = New Point(300, 35)
            labelFilterTo.Name = "labelFilterTo"
            labelFilterTo.Size = New Size(60, 17)
            labelFilterTo.TabIndex = 2
            labelFilterTo.Text = "To Date:"
            ' 
            ' dateTimePickerFrom
            ' 
            dateTimePickerFrom.Format = DateTimePickerFormat.Short
            dateTimePickerFrom.Location = New Point(130, 33)
            dateTimePickerFrom.Name = "dateTimePickerFrom"
            dateTimePickerFrom.Size = New Size(150, 25)
            dateTimePickerFrom.TabIndex = 1
            ' 
            ' labelFilterFrom
            ' 
            labelFilterFrom.AutoSize = True
            labelFilterFrom.Location = New Point(30, 35)
            labelFilterFrom.Name = "labelFilterFrom"
            labelFilterFrom.Size = New Size(77, 17)
            labelFilterFrom.TabIndex = 0
            labelFilterFrom.Text = "From Date:"
            ' 
            ' groupBoxSummary
            ' 
            groupBoxSummary.BackColor = Color.WhiteSmoke
            groupBoxSummary.Controls.Add(labelMostActiveBranch)
            groupBoxSummary.Controls.Add(labelBranchesActive)
            groupBoxSummary.Controls.Add(labelUniqueStaff)
            groupBoxSummary.Controls.Add(labelTotalTasks)
            groupBoxSummary.Location = New Point(20, 70)
            groupBoxSummary.Name = "groupBoxSummary"
            groupBoxSummary.Size = New Size(1100, 80)
            groupBoxSummary.TabIndex = 1
            groupBoxSummary.TabStop = False
            groupBoxSummary.Text = "Summary Statistics"
            ' 
            ' labelMostActiveBranch
            ' 
            labelMostActiveBranch.AutoSize = True
            labelMostActiveBranch.BackColor = Color.Red
            labelMostActiveBranch.ForeColor = Color.White
            labelMostActiveBranch.Location = New Point(800, 35)
            labelMostActiveBranch.Name = "labelMostActiveBranch"
            labelMostActiveBranch.Padding = New Padding(10, 5, 10, 5)
            labelMostActiveBranch.Size = New Size(134, 27)
            labelMostActiveBranch.TabIndex = 3
            labelMostActiveBranch.Text = "Most Active: N/A"
            ' 
            ' labelBranchesActive
            ' 
            labelBranchesActive.AutoSize = True
            labelBranchesActive.BackColor = Color.Orange
            labelBranchesActive.ForeColor = Color.White
            labelBranchesActive.Location = New Point(550, 35)
            labelBranchesActive.Name = "labelBranchesActive"
            labelBranchesActive.Padding = New Padding(10, 5, 10, 5)
            labelBranchesActive.Size = New Size(140, 27)
            labelBranchesActive.TabIndex = 2
            labelBranchesActive.Text = "Active Branches: 0"
            ' 
            ' labelUniqueStaff
            ' 
            labelUniqueStaff.AutoSize = True
            labelUniqueStaff.BackColor = Color.Green
            labelUniqueStaff.ForeColor = Color.White
            labelUniqueStaff.Location = New Point(280, 35)
            labelUniqueStaff.Name = "labelUniqueStaff"
            labelUniqueStaff.Padding = New Padding(10, 5, 10, 5)
            labelUniqueStaff.Size = New Size(133, 27)
            labelUniqueStaff.TabIndex = 1
            labelUniqueStaff.Text = "Staff Members: 0"
            ' 
            ' labelTotalTasks
            ' 
            labelTotalTasks.AutoSize = True
            labelTotalTasks.BackColor = Color.Purple
            labelTotalTasks.ForeColor = Color.White
            labelTotalTasks.Location = New Point(30, 35)
            labelTotalTasks.Name = "labelTotalTasks"
            labelTotalTasks.Padding = New Padding(10, 5, 10, 5)
            labelTotalTasks.Size = New Size(111, 27)
            labelTotalTasks.TabIndex = 0
            labelTotalTasks.Text = "Total Tasks: 0"
            ' 
            ' labelReportsTitle
            ' 
            labelReportsTitle.AutoSize = True
            labelReportsTitle.Font = New Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelReportsTitle.Location = New Point(20, 20)
            labelReportsTitle.Name = "labelReportsTitle"
            labelReportsTitle.Size = New Size(302, 30)
            labelReportsTitle.TabIndex = 0
            labelReportsTitle.Text = "Reports & Analytics Dashboard"
            ' 
            ' ManagerDashboardForm
            ' 
            AutoScaleDimensions = New SizeF(7F, 15F)
            AutoScaleMode = AutoScaleMode.Font
            BackColor = Color.LightBlue
            ClientSize = New Size(1184, 721)
            Controls.Add(tabControlManager)
            Name = "ManagerDashboardForm"
            StartPosition = FormStartPosition.CenterScreen
            Text = "Excellence Care Solutions - Manager Dashboard"
            WindowState = FormWindowState.Maximized
            tabControlManager.ResumeLayout(False)
            tabPageTaskLogging.ResumeLayout(False)
            panelTaskLogging.ResumeLayout(False)
            panelTaskLogging.PerformLayout()
            groupBoxTaskEntry.ResumeLayout(False)
            groupBoxTaskEntry.PerformLayout()
            tabPageReports.ResumeLayout(False)
            panelReports.ResumeLayout(False)
            panelReports.PerformLayout()
            CType(dataGridViewResults, ComponentModel.ISupportInitialize).EndInit()
            groupBoxFilters.ResumeLayout(False)
            groupBoxFilters.PerformLayout()
            groupBoxSummary.ResumeLayout(False)
            groupBoxSummary.PerformLayout()
            ResumeLayout(False)

            '
            ' button show All Staff
            '
            'buttonShowAllStaff.Text = "Show All Staff"
            'buttonShowAllStaff.Location = New Point(300, 325)

        End Sub

        Friend WithEvents tabControlManager As TabControl
        Friend WithEvents tabPageTaskLogging As TabPage
        Friend WithEvents tabPageReports As TabPage
        Friend WithEvents panelTaskLogging As Panel
        Friend WithEvents labelTaskLoggingTitle As Label
        Friend WithEvents groupBoxTaskEntry As GroupBox
        Friend WithEvents labelUserID As Label
        Friend WithEvents textBoxUserID As TextBox
        Friend WithEvents labelStaffName As Label
        Friend WithEvents textBoxStaffName As TextBox
        Friend WithEvents labelTaskBranch As Label
        Friend WithEvents comboBoxTaskBranch As ComboBox
        Friend WithEvents labelTaskShift As Label
        Friend WithEvents comboBoxTaskShift As ComboBox
        Friend WithEvents labelTasks As Label
        Friend WithEvents checkedListBoxTasks As CheckedListBox
        Friend WithEvents labelTaskNotes As Label
        Friend WithEvents textBoxTaskNotes As TextBox
        Friend WithEvents buttonSubmitTasks As Button
        Friend WithEvents buttonResetTaskForm As Button
        Friend WithEvents panelReports As Panel
        Friend WithEvents labelReportsTitle As Label
        Friend WithEvents groupBoxSummary As GroupBox
        Friend WithEvents labelTotalTasks As Label
        Friend WithEvents labelUniqueStaff As Label
        Friend WithEvents labelBranchesActive As Label
        Friend WithEvents labelMostActiveBranch As Label
        Friend WithEvents groupBoxFilters As GroupBox
        Friend WithEvents labelFilterFrom As Label
        Friend WithEvents dateTimePickerFrom As DateTimePicker
        Friend WithEvents labelFilterTo As Label
        Friend WithEvents dateTimePickerTo As DateTimePicker
        Friend WithEvents labelFilterBranch As Label
        Friend WithEvents comboBoxFilterBranch As ComboBox
        Friend WithEvents labelFilterShift As Label
        Friend WithEvents comboBoxFilterShift As ComboBox
        Friend WithEvents labelFilterStaff As Label
        Friend WithEvents buttonApplyFilters As Button
        Friend WithEvents buttonClearFilters As Button
        Friend WithEvents buttonExportData As Button
        Friend WithEvents labelRecordsCount As Label
        Friend WithEvents dataGridViewResults As DataGridView
        Friend WithEvents comboBoxFilterStaff As ComboBox

    End Class
End Namespace