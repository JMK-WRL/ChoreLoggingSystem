<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        labelStafffInitials = New Label()
        labelBranch = New Label()
        textBoxStaffInitials = New TextBox()
        comboBoxBranch = New ComboBox()
        labelShift = New Label()
        comboBoxShift = New ComboBox()
        labelTasks = New Label()
        checkedListBoxTasks = New CheckedListBox()
        labelNotes = New Label()
        textBoxNotes = New TextBox()
        buttonSubmit = New Button()
        buttonReset = New Button()
        buttonDashboard = New Button()
        SuspendLayout()
        ' 
        ' labelTitle
        ' 
        labelTitle.AutoSize = True
        labelTitle.Font = New Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        labelTitle.ImageAlign = ContentAlignment.MiddleLeft
        labelTitle.Location = New Point(233, 9)
        labelTitle.Name = "labelTitle"
        labelTitle.Size = New Size(270, 30)
        labelTitle.TabIndex = 0
        labelTitle.Text = "CHORE LOGGING SYSTEM"
        ' 
        ' labelStafffInitials
        ' 
        labelStafffInitials.AutoSize = True
        labelStafffInitials.Location = New Point(50, 80)
        labelStafffInitials.Name = "labelStafffInitials"
        labelStafffInitials.Size = New Size(71, 15)
        labelStafffInitials.TabIndex = 1
        labelStafffInitials.Text = "Staff Initials:"
        ' 
        ' labelBranch
        ' 
        labelBranch.AutoSize = True
        labelBranch.Location = New Point(50, 120)
        labelBranch.Name = "labelBranch"
        labelBranch.Size = New Size(47, 15)
        labelBranch.TabIndex = 2
        labelBranch.Text = "Branch:"
        ' 
        ' textBoxStaffInitials
        ' 
        textBoxStaffInitials.Location = New Point(150, 80)
        textBoxStaffInitials.Name = "textBoxStaffInitials"
        textBoxStaffInitials.Size = New Size(200, 23)
        textBoxStaffInitials.TabIndex = 3
        ' 
        ' comboBoxBranch
        ' 
        comboBoxBranch.DropDownStyle = ComboBoxStyle.DropDownList
        comboBoxBranch.FormattingEnabled = True
        comboBoxBranch.Location = New Point(150, 120)
        comboBoxBranch.Name = "comboBoxBranch"
        comboBoxBranch.Size = New Size(200, 23)
        comboBoxBranch.TabIndex = 4
        ' 
        ' labelShift
        ' 
        labelShift.AutoSize = True
        labelShift.Location = New Point(50, 160)
        labelShift.Name = "labelShift"
        labelShift.Size = New Size(34, 15)
        labelShift.TabIndex = 5
        labelShift.Text = "Shift:"
        ' 
        ' comboBoxShift
        ' 
        comboBoxShift.DropDownStyle = ComboBoxStyle.DropDownList
        comboBoxShift.FormattingEnabled = True
        comboBoxShift.Location = New Point(150, 160)
        comboBoxShift.Name = "comboBoxShift"
        comboBoxShift.Size = New Size(200, 23)
        comboBoxShift.TabIndex = 6
        ' 
        ' labelTasks
        ' 
        labelTasks.AutoSize = True
        labelTasks.Location = New Point(50, 200)
        labelTasks.Name = "labelTasks"
        labelTasks.Size = New Size(129, 15)
        labelTasks.TabIndex = 7
        labelTasks.Text = "Tasks for Selected Shift:"
        ' 
        ' checkedListBoxTasks
        ' 
        checkedListBoxTasks.FormattingEnabled = True
        checkedListBoxTasks.Location = New Point(50, 230)
        checkedListBoxTasks.Name = "checkedListBoxTasks"
        checkedListBoxTasks.Size = New Size(500, 184)
        checkedListBoxTasks.TabIndex = 8
        ' 
        ' labelNotes
        ' 
        labelNotes.AutoSize = True
        labelNotes.Location = New Point(50, 450)
        labelNotes.Name = "labelNotes"
        labelNotes.Size = New Size(41, 15)
        labelNotes.TabIndex = 9
        labelNotes.Text = "Notes:"
        ' 
        ' textBoxNotes
        ' 
        textBoxNotes.Location = New Point(50, 470)
        textBoxNotes.Multiline = True
        textBoxNotes.Name = "textBoxNotes"
        textBoxNotes.ScrollBars = ScrollBars.Vertical
        textBoxNotes.Size = New Size(500, 60)
        textBoxNotes.TabIndex = 10
        ' 
        ' buttonSubmit
        ' 
        buttonSubmit.Location = New Point(50, 550)
        buttonSubmit.Name = "buttonSubmit"
        buttonSubmit.Size = New Size(100, 30)
        buttonSubmit.TabIndex = 11
        buttonSubmit.Text = "Submit"
        buttonSubmit.UseVisualStyleBackColor = True
        ' 
        ' buttonReset
        ' 
        buttonReset.Location = New Point(170, 550)
        buttonReset.Name = "buttonReset"
        buttonReset.Size = New Size(100, 30)
        buttonReset.TabIndex = 12
        buttonReset.Text = "Reset"
        buttonReset.UseVisualStyleBackColor = True
        ' 
        ' buttonDashboard
        ' 
        buttonDashboard.Location = New Point(290, 550)
        buttonDashboard.Name = "buttonDashboard"
        buttonDashboard.Size = New Size(100, 30)
        buttonDashboard.TabIndex = 13
        buttonDashboard.Text = "Dashboard"
        buttonDashboard.UseVisualStyleBackColor = True
        ' 
        ' MainForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(784, 611)
        Controls.Add(buttonDashboard)
        Controls.Add(buttonReset)
        Controls.Add(buttonSubmit)
        Controls.Add(textBoxNotes)
        Controls.Add(labelNotes)
        Controls.Add(checkedListBoxTasks)
        Controls.Add(labelTasks)
        Controls.Add(comboBoxShift)
        Controls.Add(labelShift)
        Controls.Add(comboBoxBranch)
        Controls.Add(textBoxStaffInitials)
        Controls.Add(labelBranch)
        Controls.Add(labelStafffInitials)
        Controls.Add(labelTitle)
        Name = "MainForm"
        Text = "Chore Logging System"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents labelTitle As Label
    Friend WithEvents labelStafffInitials As Label
    Friend WithEvents labelBranch As Label
    Friend WithEvents textBoxStaffInitials As TextBox
    Friend WithEvents comboBoxBranch As ComboBox
    Friend WithEvents labelShift As Label
    Friend WithEvents comboBoxShift As ComboBox
    Friend WithEvents labelTasks As Label
    Friend WithEvents checkedListBoxTasks As CheckedListBox
    Friend WithEvents labelNotes As Label
    Friend WithEvents textBoxNotes As TextBox
    Friend WithEvents buttonSubmit As Button
    Friend WithEvents buttonReset As Button
    Friend WithEvents buttonDashboard As Button
End Class
