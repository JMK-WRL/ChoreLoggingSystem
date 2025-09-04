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
        Me.tabControlManager = New System.Windows.Forms.TabControl()
        Me.tabPageTaskLogging = New System.Windows.Forms.TabPage()
        Me.panelTaskLogging = New System.Windows.Forms.Panel()
        Me.labelTaskLoggingTitle = New System.Windows.Forms.Label()
        Me.groupBoxTaskEntry = New System.Windows.Forms.GroupBox()
        Me.labelUserID = New System.Windows.Forms.Label()
        Me.textBoxUserID = New System.Windows.Forms.TextBox()
        Me.labelStaffName = New System.Windows.Forms.Label()
        Me.textBoxStaffName = New System.Windows.Forms.TextBox()
        Me.labelTaskBranch = New System.Windows.Forms.Label()
        Me.comboBoxTaskBranch = New System.Windows.Forms.ComboBox()
        Me.labelTaskShift = New System.Windows.Forms.Label()
        Me.comboBoxTaskShift = New System.Windows.Forms.ComboBox()
        Me.labelTasks = New System.Windows.Forms.Label()
        Me.checkedListBoxTasks = New System.Windows.Forms.CheckedListBox()
        Me.labelTaskNotes = New System.Windows.Forms.Label()
        Me.textBoxTaskNotes = New System.Windows.Forms.TextBox()
        Me.buttonSubmitTasks = New System.Windows.Forms.Button()
        Me.buttonResetTaskForm = New System.Windows.Forms.Button()
        Me.tabPageReports = New System.Windows.Forms.TabPage()
        Me.panelReports = New System.Windows.Forms.Panel()
        Me.labelReportsTitle = New System.Windows.Forms.Label()
        Me.groupBoxSummary = New System.Windows.Forms.GroupBox()
        Me.labelTotalTasks = New System.Windows.Forms.Label()
        Me.labelUniqueStaff = New System.Windows.Forms.Label()
        Me.labelBranchesActive = New System.Windows.Forms.Label()
        Me.labelMostActiveBranch = New System.Windows.Forms.Label()
        Me.groupBoxFilters = New System.Windows.Forms.GroupBox()
        Me.labelFilterFrom = New System.Windows.Forms.Label()
        Me.dateTimePickerFrom = New System.Windows.Forms.DateTimePicker()
        Me.labelFilterTo = New System.Windows.Forms.Label()
        Me.dateTimePickerTo = New System.Windows.Forms.DateTimePicker()
        Me.labelFilterBranch = New System.Windows.Forms.Label()
        Me.comboBoxFilterBranch = New System.Windows.Forms.ComboBox()
        Me.labelFilterShift = New System.Windows.Forms.Label()
        Me.comboBoxFilterShift = New System.Windows.Forms.ComboBox()
        Me.labelFilterStaff = New System.Windows.Forms.Label()
        Me.textBoxFilterStaff = New System.Windows.Forms.TextBox()
        Me.buttonApplyFilters = New System.Windows.Forms.Button()
        Me.buttonClearFilters = New System.Windows.Forms.Button()
        Me.buttonExportData = New System.Windows.Forms.Button()
        Me.labelRecordsCount = New System.Windows.Forms.Label()
        Me.dataGridViewResults = New System.Windows.Forms.DataGridView()
        Me.tabControlManager.SuspendLayout()
        Me.tabPageTaskLogging.SuspendLayout()
        Me.panelTaskLogging.SuspendLayout()
        Me.groupBoxTaskEntry.SuspendLayout()
        Me.tabPageReports.SuspendLayout()
        Me.panelReports.SuspendLayout()
        Me.groupBoxSummary.SuspendLayout()
        Me.groupBoxFilters.SuspendLayout()
        CType(Me.dataGridViewResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tabControlManager
        '
        Me.tabControlManager.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabControlManager.Controls.Add(Me.tabPageTaskLogging)
        Me.tabControlManager.Controls.Add(Me.tabPageReports)
        Me.tabControlManager.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.tabControlManager.Location = New System.Drawing.Point(12, 12)
        Me.tabControlManager.Name = "tabControlManager"
        Me.tabControlManager.SelectedIndex = 0
        Me.tabControlManager.Size = New System.Drawing.Size(1160, 700)
        Me.tabControlManager.TabIndex = 0
        '
        'tabPageTaskLogging
        '
        Me.tabPageTaskLogging.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tabPageTaskLogging.Controls.Add(Me.panelTaskLogging)
        Me.tabPageTaskLogging.Location = New System.Drawing.Point(4, 27)
        Me.tabPageTaskLogging.Name = "tabPageTaskLogging"
        Me.tabPageTaskLogging.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPageTaskLogging.Size = New System.Drawing.Size(1152, 669)
        Me.tabPageTaskLogging.TabIndex = 0
        Me.tabPageTaskLogging.Text = "Task Logging"
        Me.tabPageTaskLogging.UseVisualStyleBackColor = False
        '
        'panelTaskLogging
        '
        Me.panelTaskLogging.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panelTaskLogging.BackColor = System.Drawing.Color.White
        Me.panelTaskLogging.Controls.Add(Me.labelTaskLoggingTitle)
        Me.panelTaskLogging.Controls.Add(Me.groupBoxTaskEntry)
        Me.panelTaskLogging.Location = New System.Drawing.Point(6, 6)
        Me.panelTaskLogging.Name = "panelTaskLogging"
        Me.panelTaskLogging.Size = New System.Drawing.Size(1140, 657)
        Me.panelTaskLogging.TabIndex = 0
        '
        'labelTaskLoggingTitle
        '
        Me.labelTaskLoggingTitle.AutoSize = True
        Me.labelTaskLoggingTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.labelTaskLoggingTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.labelTaskLoggingTitle.Location = New System.Drawing.Point(20, 20)
        Me.labelTaskLoggingTitle.Name = "labelTaskLoggingTitle"
        Me.labelTaskLoggingTitle.Size = New System.Drawing.Size(380, 30)
        Me.labelTaskLoggingTitle.TabIndex = 0
        Me.labelTaskLoggingTitle.Text = "Task Logging - Log Tasks for Staff Members"
        '
        'groupBoxTaskEntry
        '
        Me.groupBoxTaskEntry.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.groupBoxTaskEntry.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.groupBoxTaskEntry.Controls.Add(Me.labelUserID)
        Me.groupBoxTaskEntry.Controls.Add(Me.textBoxUserID)
        Me.groupBoxTaskEntry.Controls.Add(Me.labelStaffName)
        Me.groupBoxTaskEntry.Controls.Add(Me.textBoxStaffName)
        Me.groupBoxTaskEntry.Controls.Add(Me.labelTaskBranch)
        Me.groupBoxTaskEntry.Controls.Add(Me.comboBoxTaskBranch)
        Me.groupBoxTaskEntry.Controls.Add(Me.labelTaskShift)
        Me.groupBoxTaskEntry.Controls.Add(Me.comboBoxTaskShift)
        Me.groupBoxTaskEntry.Controls.Add(Me.labelTasks)
        Me.groupBoxTaskEntry.Controls.Add(Me.checkedListBoxTasks)
        Me.groupBoxTaskEntry.Controls.Add(Me.labelTaskNotes)
        Me.groupBoxTaskEntry.Controls.Add(Me.textBoxTaskNotes)
        Me.groupBoxTaskEntry.Controls.Add(Me.buttonSubmitTasks)
        Me.groupBoxTaskEntry.Controls.Add(Me.buttonResetTaskForm)
        Me.groupBoxTaskEntry.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.groupBoxTaskEntry.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.groupBoxTaskEntry.Location = New System.Drawing.Point(20, 70)
        Me.groupBoxTaskEntry.Name = "groupBoxTaskEntry"
        Me.groupBoxTaskEntry.Size = New System.Drawing.Size(1100, 570)
        Me.groupBoxTaskEntry.TabIndex = 1
        Me.groupBoxTaskEntry.TabStop = False
        Me.groupBoxTaskEntry.Text = "Task Entry Form"
        '
        'labelUserID
        '
        Me.labelUserID.AutoSize = True
        Me.labelUserID.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.labelUserID.ForeColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(81, Byte), Integer))
        Me.labelUserID.Location = New System.Drawing.Point(30, 40)
        Me.labelUserID.Name = "labelUserID"
        Me.labelUserID.Size = New System.Drawing.Size(120, 19)
        Me.labelUserID.TabIndex = 0
        Me.labelUserID.Text = "Staff User ID:"
        '
        'textBoxUserID
        '
        Me.textBoxUserID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.textBoxUserID.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.textBoxUserID.Location = New System.Drawing.Point(180, 38)
        Me.textBoxUserID.MaxLength = 10
        Me.textBoxUserID.Name = "textBoxUserID"
        Me.textBoxUserID.Size = New System.Drawing.Size(200, 27)
        Me.textBoxUserID.TabIndex = 1
        '
        'labelStaffName
        '
        Me.labelStaffName.AutoSize = True
        Me.labelStaffName.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.labelStaffName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(81, Byte), Integer))
        Me.labelStaffName.Location = New System.Drawing.Point(420, 40)
        Me.labelStaffName.Name = "labelStaffName"
        Me.labelStaffName.Size = New System.Drawing.Size(108, 19)
        Me.labelStaffName.TabIndex = 2
        Me.labelStaffName.Text = "Staff Name:"
        '
        'textBoxStaffName
        '
        Me.textBoxStaffName.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.textBoxStaffName.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.textBoxStaffName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.textBoxStaffName.Location = New System.Drawing.Point(550, 38)
        Me.textBoxStaffName.Name = "textBoxStaffName"
        Me.textBoxStaffName.ReadOnly = True
        Me.textBoxStaffName.Size = New System.Drawing.Size(250, 27)
        Me.textBoxStaffName.TabIndex = 3
        Me.textBoxStaffName.Text = "Enter User ID to auto-fill name"
        '
        'labelTaskBranch
        '
        Me.labelTaskBranch.AutoSize = True
        Me.labelTaskBranch.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.labelTaskBranch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(81, Byte), Integer))
        Me.labelTaskBranch.Location = New System.Drawing.Point(30, 90)
        Me.labelTaskBranch.Name = "labelTaskBranch"
        Me.labelTaskBranch.Size = New System.Drawing.Size(97, 19)
        Me.labelTaskBranch.TabIndex = 4
        Me.labelTaskBranch.Text = "Location:"
        '
        'comboBoxTaskBranch
        '
        Me.comboBoxTaskBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBoxTaskBranch.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.comboBoxTaskBranch.FormattingEnabled = True
        Me.comboBoxTaskBranch.Location = New System.Drawing.Point(180, 88)
        Me.comboBoxTaskBranch.Name = "comboBoxTaskBranch"
        Me.comboBoxTaskBranch.Size = New System.Drawing.Size(250, 28)
        Me.comboBoxTaskBranch.TabIndex = 5
        '
        'labelTaskShift
        '
        Me.labelTaskShift.AutoSize = True
        Me.labelTaskShift.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.labelTaskShift.ForeColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(81, Byte), Integer))
        Me.labelTaskShift.Location = New System.Drawing.Point(470, 90)
        Me.labelTaskShift.Name = "labelTaskShift"
        Me.labelTaskShift.Size = New System.Drawing.Size(102, 19)
        Me.labelTaskShift.TabIndex = 6
        Me.labelTaskShift.Text = "Work Shift:"
        '
        'comboBoxTaskShift
        '
        Me.comboBoxTaskShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBoxTaskShift.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.comboBoxTaskShift.FormattingEnabled = True
        Me.comboBoxTaskShift.Location = New System.Drawing.Point(600, 88)
        Me.comboBoxTaskShift.Name = "comboBoxTaskShift"
        Me.comboBoxTaskShift.Size = New System.Drawing.Size(280, 28)
        Me.comboBoxTaskShift.TabIndex = 7
        '
        'labelTasks
        '
        Me.labelTasks.AutoSize = True
        Me.labelTasks.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.labelTasks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(81, Byte), Integer))
        Me.labelTasks.Location = New System.Drawing.Point(30, 140)
        Me.labelTasks.Name = "labelTasks"
        Me.labelTasks.Size = New System.Drawing.Size(230, 19)
        Me.labelTasks.TabIndex = 8
        Me.labelTasks.Text = "Completed Tasks for Selected Shift:"
        '
        'checkedListBoxTasks
        '
        Me.checkedListBoxTasks.BackColor = System.Drawing.Color.White
        Me.checkedListBoxTasks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.checkedListBoxTasks.CheckOnClick = True
        Me.checkedListBoxTasks.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.checkedListBoxTasks.FormattingEnabled = True
        Me.checkedListBoxTasks.Location = New System.Drawing.Point(30, 170)
        Me.checkedListBoxTasks.Name = "checkedListBoxTasks"
        Me.checkedListBoxTasks.Size = New System.Drawing.Size(1040, 200)
        Me.checkedListBoxTasks.TabIndex = 9
        '
        'labelTaskNotes
        '
        Me.labelTaskNotes.AutoSize = True
        Me.labelTaskNotes.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.labelTaskNotes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(81, Byte), Integer))
        Me.labelTaskNotes.Location = New System.Drawing.Point(30, 390)
        Me.labelTaskNotes.Name = "labelTaskNotes"
        Me.labelTaskNotes.Size = New System.Drawing.Size(138, 19)
        Me.labelTaskNotes.TabIndex = 10
        Me.labelTaskNotes.Text = "Additional Notes:"
        '
        'textBoxTaskNotes
        '
        Me.textBoxTaskNotes.BackColor = System.Drawing.Color.White
        Me.textBoxTaskNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.textBoxTaskNotes.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.textBoxTaskNotes.Location = New System.Drawing.Point(30, 420)
        Me.textBoxTaskNotes.Multiline = True
        Me.textBoxTaskNotes.Name = "textBoxTaskNotes"
        Me.textBoxTaskNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.textBoxTaskNotes.Size = New System.Drawing.Size(1040, 70)
        Me.textBoxTaskNotes.TabIndex = 11
        '
        'buttonSubmitTasks
        '
        Me.buttonSubmitTasks.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.buttonSubmitTasks.FlatAppearance.BorderSize = 0
        Me.buttonSubmitTasks.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonSubmitTasks.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.buttonSubmitTasks.ForeColor = System.Drawing.Color.White
        Me.buttonSubmitTasks.Location = New System.Drawing.Point(30, 510)
        Me.buttonSubmitTasks.Name = "buttonSubmitTasks"
        Me.buttonSubmitTasks.Size = New System.Drawing.Size(200, 40)
        Me.buttonSubmitTasks.TabIndex = 12
        Me.buttonSubmitTasks.Text = "Submit Tasks"
        Me.buttonSubmitTasks.UseVisualStyleBackColor = False
        '
        'buttonResetTaskForm
        '
        Me.buttonResetTaskForm.BackColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.buttonResetTaskForm.FlatAppearance.BorderSize = 0
        Me.buttonResetTaskForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonResetTaskForm.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.buttonResetTaskForm.ForeColor = System.Drawing.Color.White
        Me.buttonResetTaskForm.Location = New System.Drawing.Point(250, 510)
        Me.buttonResetTaskForm.Name = "buttonResetTaskForm"
        Me.buttonResetTaskForm.Size = New System.Drawing.Size(150, 40)
        Me.buttonResetTaskForm.TabIndex = 13
        Me.buttonResetTaskForm.Text = "Reset Form"
        Me.buttonResetTaskForm.UseVisualStyleBackColor = False
        '
        'tabPageReports
        '
        Me.tabPageReports.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tabPageReports.Controls.Add(Me.panelReports)
        Me.tabPageReports.Location = New System.Drawing.Point(4, 27)
        Me.tabPageReports.Name = "tabPageReports"
        Me.tabPageReports.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPageReports.Size = New System.Drawing.Size(1152, 669)
        Me.tabPageReports.TabIndex = 1
        Me.tabPageReports.Text = "Reports & Analytics"
        Me.tabPageReports.UseVisualStyleBackColor = False
        '
        'panelReports
        '
        Me.panelReports.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panelReports.BackColor = System.Drawing.Color.White
        Me.panelReports.Controls.Add(Me.labelReportsTitle)
        Me.panelReports.Controls.Add(Me.groupBoxSummary)
        Me.panelReports.Controls.Add(Me.groupBoxFilters)
        Me.panelReports.Controls.Add(Me.labelRecordsCount)
        Me.panelReports.Controls.Add(Me.dataGridViewResults)
        Me.panelReports.Location = New System.Drawing.Point(6, 6)
        Me.panelReports.Name = "panelReports"
        Me.panelReports.Size = New System.Drawing.Size(1140, 657)
        Me.panelReports.TabIndex = 0
        '
        'labelReportsTitle
        '
        Me.labelReportsTitle.AutoSize = True
        Me.labelReportsTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.labelReportsTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.labelReportsTitle.Location = New System.Drawing.Point(20, 20)
        Me.labelReportsTitle.Name = "labelReportsTitle"
        Me.labelReportsTitle.Size = New System.Drawing.Size(280, 30)
        Me.labelReportsTitle.TabIndex = 0
        Me.labelReportsTitle.Text = "Reports & Analytics Dashboard"
        '
        'groupBoxSummary
        '
        Me.groupBoxSummary.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.groupBoxSummary.Controls.Add(Me.labelTotalTasks)
        Me.groupBoxSummary.Controls.Add(Me.labelUniqueStaff)
        Me.groupBoxSummary.Controls.Add(Me.labelBranchesActive)
        Me.groupBoxSummary.Controls.Add(Me.labelMostActiveBranch)
        Me.groupBoxSummary.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.groupBoxSummary.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.groupBoxSummary.Location = New System.Drawing.Point(20, 70)
        Me.groupBoxSummary.Name = "groupBoxSummary"
        Me.groupBoxSummary.Size = New System.Drawing.Size(1100, 80)
        Me.groupBoxSummary.TabIndex = 1
        Me.groupBoxSummary.TabStop = False
        Me.groupBoxSummary.Text = "Summary Statistics"
        '
        'labelTotalTasks
        '
        Me.labelTotalTasks.AutoSize = True
        Me.labelTotalTasks.BackColor = System.Drawing.Color.FromArgb(CType(CType(99, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.labelTotalTasks.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.labelTotalTasks.ForeColor = System.Drawing.Color.White
        Me.labelTotalTasks.Location = New System.Drawing.Point(30, 35)
        Me.labelTotalTasks.Name = "labelTotalTasks"
        Me.labelTotalTasks.Padding = New System.Windows.Forms.Padding(10, 5, 10, 5)
        Me.labelTotalTasks.Size = New System.Drawing.Size(118, 29)
        Me.labelTotalTasks.TabIndex = 0
        Me.labelTotalTasks.Text = "Total Tasks: 0"
        '
        'labelUniqueStaff
        '
        Me.labelUniqueStaff.AutoSize = True
        Me.labelUniqueStaff.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.labelUniqueStaff.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.labelUniqueStaff.ForeColor = System.Drawing.Color.White
        Me.labelUniqueStaff.Location = New System.Drawing.Point(280, 35)
        Me.labelUniqueStaff.Name = "labelUniqueStaff"
        Me.labelUniqueStaff.Padding = New System.Windows.Forms.Padding(10, 5, 10, 5)
        Me.labelUniqueStaff.Size = New System.Drawing.Size(137, 29)
        Me.labelUniqueStaff.TabIndex = 1
        Me.labelUniqueStaff.Text = "Staff Members: 0"
        '
        'labelBranchesActive
        '
        Me.labelBranchesActive.AutoSize = True
        Me.labelBranchesActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(11, Byte), Integer))
        Me.labelBranchesActive.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.labelBranchesActive.ForeColor = System.Drawing.Color.White
        Me.labelBranchesActive.Location = New System.Drawing.Point(550, 35)
        Me.labelBranchesActive.Name = "labelBranchesActive"
        Me.labelBranchesActive.Padding = New System.Windows.Forms.Padding(10, 5, 10, 5)
        Me.labelBranchesActive.Size = New System.Drawing.Size(148, 29)
        Me.labelBranchesActive.TabIndex = 2
        Me.labelBranchesActive.Text = "Active Branches: 0"
        '
        'labelMostActiveBranch
        '
        Me.labelMostActiveBranch.AutoSize = True
        Me.labelMostActiveBranch.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.labelMostActiveBranch.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.labelMostActiveBranch.ForeColor = System.Drawing.Color.White
        Me.labelMostActiveBranch.Location = New System.Drawing.Point(830, 35)
        Me.labelMostActiveBranch.Name = "labelMostActiveBranch"
        Me.labelMostActiveBranch.Padding = New System.Windows.Forms.Padding(10, 5, 10, 5)
        Me.labelMostActiveBranch.Size = New System.Drawing.Size(139, 29)
        Me.labelMostActiveBranch.TabIndex = 3
        Me.labelMostActiveBranch.Text = "Most Active: N/A"
        '
        'groupBoxFilters
        '
        Me.groupBoxFilters.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.groupBoxFilters.Controls.Add(Me.labelFilterFrom)
        Me.groupBoxFilters.Controls.Add(Me.dateTimePickerFrom)
        Me.groupBoxFilters.Controls.Add(Me.labelFilterTo)
        Me.groupBoxFilters.Controls.Add(Me.dateTimePickerTo)
        Me.groupBoxFilters.Controls.Add(Me.labelFilterBranch)
        Me.groupBoxFilters.Controls.Add(Me.comboBoxFilterBranch)
        Me.groupBoxFilters.Controls.Add(Me.labelFilterShift)
        Me.groupBoxFilters.Controls.Add(Me.comboBoxFilterShift)
        Me.groupBoxFilters.Controls.Add(Me.labelFilterStaff)
        Me.groupBoxFilters.Controls.Add(Me.textBoxFilterStaff)
        Me.groupBoxFilters.Controls.Add(Me.buttonApplyFilters)
        Me.groupBoxFilters.Controls.Add(Me.buttonClearFilters)
        Me.groupBoxFilters.Controls.Add(Me.buttonExportData)
        Me.groupBoxFilters.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.groupBoxFilters.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.groupBoxFilters.Location = New System.Drawing.Point(20, 170)
        Me.groupBoxFilters.Name = "groupBoxFilters"
        Me.groupBoxFilters.Size = New System.Drawing.Size(1100, 120)
        Me.groupBoxFilters.TabIndex = 2
        Me.groupBoxFilters.TabStop = False
        Me.groupBoxFilters.Text = "Filters & Options"
        '
        'labelFilterFrom
        '
        Me.labelFilterFrom.AutoSize = True
        Me.labelFilterFrom.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.labelFilterFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(81, Byte), Integer))
        Me.labelFilterFrom.Location = New System.Drawing.Point(30, 35)
        Me.labelFilterFrom.Name = "labelFilterFrom"
        Me.labelFilterFrom.Size = New System.Drawing.Size(88, 19)
        Me.labelFilterFrom.TabIndex = 0
        Me.labelFilterFrom.Text = "From Date:"
        '
        'dateTimePickerFrom
        '
        Me.dateTimePickerFrom.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.dateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateTimePickerFrom.Location = New System.Drawing.Point(130, 33)
        Me.dateTimePickerFrom.Name = "dateTimePickerFrom"
        Me.dateTimePickerFrom.Size = New System.Drawing.Size(150, 25)
        Me.dateTimePickerFrom.TabIndex = 1
        '
        'labelFilterTo
        '
        Me.labelFilterTo.AutoSize = True
        Me.labelFilterTo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.labelFilterTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(81, Byte), Integer))
        Me.labelFilterTo.Location = New System.Drawing.Point(300, 35)
        Me.labelFilterTo.Name = "labelFilterTo"
        Me.labelFilterTo.Size = New System.Drawing.Size(70, 19)
        Me.labelFilterTo.TabIndex = 2
        Me.labelFilterTo.Text = "To Date:"
        '
        'dateTimePickerTo
        '
        Me.dateTimePickerTo.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.dateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateTimePickerTo.Location = New System.Drawing.Point(380, 33)
        Me.dateTimePickerTo.Name = "dateTimePickerTo"
        Me.dateTimePickerTo.Size = New System.Drawing.Size(150, 25)
        Me.dateTimePickerTo.TabIndex = 3
        '
        'labelFilterBranch
        '
        Me.labelFilterBranch.AutoSize = True
        Me.labelFilterBranch.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.labelFilterBranch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(81, Byte), Integer))
        Me.labelFilterBranch.Location = New System.Drawing.Point(550, 35)
        Me.labelFilterBranch.Name = "labelFilterBranch"
        Me.labelFilterBranch.Size = New System.Drawing.Size(75, 19)
        Me.labelFilterBranch.TabIndex = 4
        Me.labelFilterBranch.Text = "Branch:"
        '
        'comboBoxFilterBranch
        '
        Me.comboBoxFilterBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBoxFilterBranch.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.comboBoxFilterBranch.FormattingEnabled = True
        Me.comboBoxFilterBranch.Location = New System.Drawing.Point(630, 33)
        Me.comboBoxFilterBranch.Name = "comboBoxFilterBranch"
        Me.comboBoxFilterBranch.Size = New System.Drawing.Size(180, 25)
        Me.comboBoxFilterBranch.TabIndex = 5
        '
        'labelFilterShift
        '
        Me.labelFilterShift.AutoSize = True
        Me.labelFilterShift.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.labelFilterShift.ForeColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(81, Byte), Integer))
        Me.labelFilterShift.Location = New System.Drawing.Point(830, 35)
        Me.labelFilterShift.Name = "labelFilterShift"
        Me.labelFilterShift.Size = New System.Drawing.Size(60, 19)
        Me.labelFilterShift.TabIndex = 6
        Me.labelFilterShift.Text = "Shift:"
        '
        'comboBoxFilterShift
        '
        Me.comboBoxFilterShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBoxFilterShift.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.comboBoxFilterShift.FormattingEnabled = True
        Me.comboBoxFilterShift.Location = New System.Drawing.Point(900, 33)
        Me.comboBoxFilterShift.Name = "comboBoxFilterShift"
        Me.comboBoxFilterShift.Size = New System.Drawing.Size(150, 25)
        Me.comboBoxFilterShift.TabIndex = 7
        '
        'labelFilterStaff
        '
        Me.labelFilterStaff.AutoSize = True
        Me.labelFilterStaff.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.labelFilterStaff.ForeColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(81, Byte), Integer))
        Me.labelFilterStaff.Location = New System.Drawing.Point(30, 75)
        Me.labelFilterStaff.Name = "labelFilterStaff"
        Me.labelFilterStaff.Size = New System.Drawing.Size(101, 19)
        Me.labelFilterStaff.TabIndex = 8
        Me.labelFilterStaff.Text = "Staff Initials:"
        '
        'textBoxFilterStaff
        '
        Me.textBoxFilterStaff.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.textBoxFilterStaff.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.textBoxFilterStaff.Location = New System.Drawing.Point(140, 73)
        Me.textBoxFilterStaff.Name = "textBoxFilterStaff"
        Me.textBoxFilterStaff.Size = New System.Drawing.Size(150, 25)
        Me.textBoxFilterStaff.TabIndex = 9
        '
        'buttonApplyFilters
        '
        Me.buttonApplyFilters.BackColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.buttonApplyFilters.FlatAppearance.BorderSize = 0
        Me.buttonApplyFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonApplyFilters.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.buttonApplyFilters.ForeColor = System.Drawing.Color.White
        Me.buttonApplyFilters.Location = New System.Drawing.Point(330, 70)
        Me.buttonApplyFilters.Name = "buttonApplyFilters"
        Me.buttonApplyFilters.Size = New System.Drawing.Size(130, 30)
        Me.buttonApplyFilters.TabIndex = 10
        Me.buttonApplyFilters.Text = "Apply Filters"
        Me.buttonApplyFilters.UseVisualStyleBackColor = False
        '
        'buttonClearFilters
        '
        Me.buttonClearFilters.BackColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.buttonClearFilters.FlatAppearance.BorderSize = 0
        Me.buttonClearFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonClearFilters.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.buttonClearFilters.ForeColor = System.Drawing.Color.White
        Me.buttonClearFilters.Location = New System.Drawing.Point(480, 70)
        Me.buttonClearFilters.Name = "buttonClearFilters"
        Me.buttonClearFilters.Size = New System.Drawing.Size(130, 30)
        Me.buttonClearFilters.TabIndex = 11
        Me.buttonClearFilters.Text = "Clear Filters"
        Me.buttonClearFilters.UseVisualStyleBackColor = False
        '
        'buttonExportData
        '
        Me.buttonExportData.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.buttonExportData.FlatAppearance.BorderSize = 0
        Me.buttonExportData.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonExportData.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.buttonExportData.ForeColor = System.Drawing.Color.White
        Me.buttonExportData.Location = New System.Drawing.Point(630, 70)
        Me.buttonExportData.Name = "buttonExportData"
        Me.buttonExportData.Size = New System.Drawing.Size(130, 30)
        Me.buttonExportData.TabIndex = 12
        Me.buttonExportData.Text = "Export Data"
        Me.buttonExportData.UseVisualStyleBackColor = False
        '
        'labelRecordsCount
        '
        Me.labelRecordsCount.AutoSize = True
        Me.labelRecordsCount.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.labelRecordsCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(81, Byte), Integer))
        Me.labelRecordsCount.Location = New System.Drawing.Point(20, 310)
        Me.labelRecordsCount.Name = "labelRecordsCount"
        Me.labelRecordsCount.Size = New System.Drawing.Size(130, 20)
        Me.labelRecordsCount.TabIndex = 3
        Me.labelRecordsCount.Text = "Records Found: 0"
        '
        'dataGridViewResults
        '
        Me.dataGridViewResults.AllowUserToAddRows = False
        Me.dataGridViewResults.AllowUserToDeleteRows = False
        Me.dataGridViewResults.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dataGridViewResults.BackgroundColor = System.Drawing.Color.White
        Me.dataGridViewResults.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dataGridViewResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataGridViewResults.GridColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.dataGridViewResults.Location = New System.Drawing.Point(20, 340)
        Me.dataGridViewResults.Name = "dataGridViewResults"
        Me.dataGridViewResults.ReadOnly = True
        Me.dataGridViewResults.RowHeadersVisible = False
        Me.dataGridViewResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dataGridViewResults.Size = New System.Drawing.Size(1100, 300)
        Me.dataGridViewResults.TabIndex = 4
        '
        'ManagerDashboardForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1184, 724)
        Me.Controls.Add(Me.tabControlManager)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MinimumSize = New System.Drawing.Size(1200, 760)
        Me.Name = "ManagerDashboardForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Manager Dashboard - Excellence Care Solutions"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tabControlManager.ResumeLayout(False)
        Me.tabPageTaskLogging.ResumeLayout(False)
        Me.panelTaskLogging.ResumeLayout(False)
        Me.panelTaskLogging.PerformLayout()
        Me.groupBoxTaskEntry.ResumeLayout(False)
        Me.groupBoxTaskEntry.PerformLayout()
        Me.tabPageReports.ResumeLayout(False)
        Me.panelReports.ResumeLayout(False)
        Me.panelReports.PerformLayout()
        Me.groupBoxSummary.ResumeLayout(False)
        Me.groupBoxSummary.PerformLayout()
        Me.groupBoxFilters.ResumeLayout(False)
        Me.groupBoxFilters.PerformLayout()
        CType(Me.dataGridViewResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    ' ===== CONTROL DECLARATIONS =====
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
    Friend WithEvents textBoxFilterStaff As TextBox
    Friend WithEvents buttonApplyFilters As Button
    Friend WithEvents buttonClearFilters As Button
    Friend WithEvents buttonExportData As Button
    Friend WithEvents labelRecordsCount As Label
    Friend WithEvents dataGridViewResults As DataGridView

End Class