Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class MainForm
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
            labelMainTitle = New Label()
            labelUserID = New Label()
            labelBranch = New Label()
            labelShift = New Label()
            labelTasks = New Label()
            labelNotes = New Label()
            textBoxUserID = New TextBox()
            comboBoxBranch = New ComboBox()
            comboBoxShift = New ComboBox()
            checkedListBoxTasks = New CheckedListBox()
            textBoxNotes = New TextBox()
            buttonSubmit = New Button()
            buttonReset = New Button()
            labelCompanyTitle = New Label()
            labelSubtitle = New Label()
            pictureBoxLogo = New PictureBox()
            CType(pictureBoxLogo, ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()

            ' pictureBoxLogo
            pictureBoxLogo.Location = New Point(20, 15)
            pictureBoxLogo.Name = "pictureBoxLogo"
            pictureBoxLogo.Size = New Size(80, 80)
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.StretchImage
            pictureBoxLogo.TabIndex = 16
            pictureBoxLogo.TabStop = False

            ' labelCompanyTitle
            labelCompanyTitle.AutoSize = True
            labelCompanyTitle.Font = New Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelCompanyTitle.Location = New Point(280, 25)
            labelCompanyTitle.Name = "labelCompanyTitle"
            labelCompanyTitle.Size = New Size(366, 25)
            labelCompanyTitle.TabIndex = 14
            labelCompanyTitle.Text = "EXCELLENCE CARE SOLUTIONS"
            labelCompanyTitle.TextAlign = ContentAlignment.MiddleCenter

            ' labelSubtitle
            labelSubtitle.AutoSize = True
            labelSubtitle.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Italic, GraphicsUnit.Point, CByte(0))
            labelSubtitle.Location = New Point(300, 55)
            labelSubtitle.Name = "labelSubtitle"
            labelSubtitle.Size = New Size(242, 16)
            labelSubtitle.TabIndex = 15
            labelSubtitle.Text = "Professional Task Management System"
            labelSubtitle.TextAlign = ContentAlignment.MiddleCenter

            ' labelMainTitle
            labelMainTitle.AutoSize = True
            labelMainTitle.Font = New Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelMainTitle.Location = New Point(350, 90)
            labelMainTitle.Name = "labelMainTitle"
            labelMainTitle.Size = New Size(161, 24)
            labelMainTitle.TabIndex = 0
            labelMainTitle.Text = "TASK LOGGING"
            labelMainTitle.TextAlign = ContentAlignment.MiddleCenter

            ' labelUserID
            labelUserID.AutoSize = True
            labelUserID.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelUserID.Location = New Point(50, 140)
            labelUserID.Name = "labelUserID"
            labelUserID.Size = New Size(63, 16)
            labelUserID.TabIndex = 1
            labelUserID.Text = "User ID:"

            ' textBoxUserID
            textBoxUserID.Location = New Point(160, 138)
            textBoxUserID.Name = "textBoxUserID"
            textBoxUserID.ReadOnly = True
            textBoxUserID.Size = New Size(200, 23)
            textBoxUserID.TabIndex = 6
            textBoxUserID.BackColor = Color.FromArgb(230, 230, 230)

            ' labelBranch
            labelBranch.AutoSize = True
            labelBranch.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelBranch.Location = New Point(50, 180)
            labelBranch.Name = "labelBranch"
            labelBranch.Size = New Size(59, 16)
            labelBranch.TabIndex = 2
            labelBranch.Text = "Location:"

            ' comboBoxBranch
            comboBoxBranch.DropDownStyle = ComboBoxStyle.DropDownList
            comboBoxBranch.FormattingEnabled = True
            comboBoxBranch.Location = New Point(160, 178)
            comboBoxBranch.Name = "comboBoxBranch"
            comboBoxBranch.Size = New Size(250, 23)
            comboBoxBranch.TabIndex = 7

            ' labelShift
            labelShift.AutoSize = True
            labelShift.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelShift.Location = New Point(50, 220)
            labelShift.Name = "labelShift"
            labelShift.Size = New Size(81, 16)
            labelShift.TabIndex = 3
            labelShift.Text = "Work Shift:"

            ' comboBoxShift
            comboBoxShift.DropDownStyle = ComboBoxStyle.DropDownList
            comboBoxShift.FormattingEnabled = True
            comboBoxShift.Location = New Point(160, 218)
            comboBoxShift.Name = "comboBoxShift"
            comboBoxShift.Size = New Size(250, 23)
            comboBoxShift.TabIndex = 8

            ' labelTasks
            labelTasks.AutoSize = True
            labelTasks.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelTasks.Location = New Point(50, 260)
            labelTasks.Name = "labelTasks"
            labelTasks.Size = New Size(200, 16)
            labelTasks.TabIndex = 4
            labelTasks.Text = "Tasks for Selected Shift:"

            ' checkedListBoxTasks
            checkedListBoxTasks.CheckOnClick = True
            checkedListBoxTasks.FormattingEnabled = True
            checkedListBoxTasks.Location = New Point(50, 285)
            checkedListBoxTasks.Name = "checkedListBoxTasks"
            checkedListBoxTasks.Size = New Size(600, 180)
            checkedListBoxTasks.TabIndex = 9
            checkedListBoxTasks.BorderStyle = BorderStyle.Fixed3D

            ' labelNotes
            labelNotes.AutoSize = True
            labelNotes.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            labelNotes.Location = New Point(50, 485)
            labelNotes.Name = "labelNotes"
            labelNotes.Size = New Size(150, 16)
            labelNotes.TabIndex = 5
            labelNotes.Text = "Additional Notes:"

            ' textBoxNotes
            textBoxNotes.Location = New Point(50, 510)
            textBoxNotes.Multiline = True
            textBoxNotes.Name = "textBoxNotes"
            textBoxNotes.ScrollBars = ScrollBars.Vertical
            textBoxNotes.Size = New Size(600, 60)
            textBoxNotes.TabIndex = 10
            textBoxNotes.BorderStyle = BorderStyle.Fixed3D

            ' buttonSubmit
            buttonSubmit.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            buttonSubmit.Location = New Point(50, 585)
            buttonSubmit.Name = "buttonSubmit"
            buttonSubmit.Size = New Size(120, 35)
            buttonSubmit.TabIndex = 11
            buttonSubmit.Text = "Submit Tasks"
            buttonSubmit.UseVisualStyleBackColor = True

            ' buttonReset
            buttonReset.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
            buttonReset.Location = New Point(190, 585)
            buttonReset.Name = "buttonReset"
            buttonReset.Size = New Size(120, 35)
            buttonReset.TabIndex = 12
            buttonReset.Text = "Reset Form"
            buttonReset.UseVisualStyleBackColor = True

            ' MainForm
            AutoScaleDimensions = New SizeF(7.0F, 15.0F)
            AutoScaleMode = AutoScaleMode.Font
            ClientSize = New Size(900, 650)
            Controls.Add(pictureBoxLogo)
            Controls.Add(labelSubtitle)
            Controls.Add(labelCompanyTitle)
            Controls.Add(buttonReset)
            Controls.Add(buttonSubmit)
            Controls.Add(textBoxNotes)
            Controls.Add(checkedListBoxTasks)
            Controls.Add(comboBoxShift)
            Controls.Add(comboBoxBranch)
            Controls.Add(textBoxUserID)
            Controls.Add(labelNotes)
            Controls.Add(labelTasks)
            Controls.Add(labelShift)
            Controls.Add(labelBranch)
            Controls.Add(labelUserID)
            Controls.Add(labelMainTitle)
            FormBorderStyle = FormBorderStyle.FixedSingle
            MaximizeBox = False
            Name = "MainForm"
            StartPosition = FormStartPosition.CenterScreen
            Text = "Excellence Care Solutions - Task Management System"
            CType(pictureBoxLogo, ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)
            PerformLayout()

        End Sub

        Friend WithEvents labelMainTitle As Label
        Friend WithEvents labelUserID As Label
        Friend WithEvents labelBranch As Label
        Friend WithEvents labelShift As Label
        Friend WithEvents labelTasks As Label
        Friend WithEvents labelNotes As Label
        Friend WithEvents textBoxUserID As TextBox
        Friend WithEvents comboBoxBranch As ComboBox
        Friend WithEvents comboBoxShift As ComboBox
        Friend WithEvents checkedListBoxTasks As CheckedListBox
        Friend WithEvents textBoxNotes As TextBox
        Friend WithEvents buttonSubmit As Button
        Friend WithEvents buttonReset As Button
        Friend WithEvents labelCompanyTitle As Label
        Friend WithEvents labelSubtitle As Label
        Friend WithEvents pictureBoxLogo As PictureBox

    End Class
End Namespace