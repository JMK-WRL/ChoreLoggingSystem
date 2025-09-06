<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StaffLoginForm
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
        panelLogin = New Panel()
        labelCompanyTitle = New Label()
        labelLoginTitle = New Label()
        labelInstructions = New Label()
        laberUserID = New Label()
        textBoxUserID = New TextBox()
        labelPIN = New Label()
        textBoxPIN = New TextBox()
        labelErrorMessage = New Label()
        buttonLogin = New Button()
        buttonCancel = New Button()
        buttonManagerLogin = New Button()
        panelLogin.SuspendLayout()
        SuspendLayout()
        ' 
        ' panelLogin
        ' 
        panelLogin.BorderStyle = BorderStyle.FixedSingle
        panelLogin.Controls.Add(buttonManagerLogin)
        panelLogin.Controls.Add(buttonCancel)
        panelLogin.Controls.Add(buttonLogin)
        panelLogin.Controls.Add(labelErrorMessage)
        panelLogin.Controls.Add(textBoxPIN)
        panelLogin.Controls.Add(labelPIN)
        panelLogin.Controls.Add(textBoxUserID)
        panelLogin.Controls.Add(laberUserID)
        panelLogin.Controls.Add(labelInstructions)
        panelLogin.Controls.Add(labelLoginTitle)
        panelLogin.Controls.Add(labelCompanyTitle)
        panelLogin.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        panelLogin.ForeColor = SystemColors.ActiveCaptionText
        panelLogin.Location = New Point(20, 20)
        panelLogin.Name = "panelLogin"
        panelLogin.Size = New Size(360, 320)
        panelLogin.TabIndex = 0
        ' 
        ' labelCompanyTitle
        ' 
        labelCompanyTitle.AutoSize = True
        labelCompanyTitle.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        labelCompanyTitle.ForeColor = Color.MidnightBlue
        labelCompanyTitle.Location = New Point(80, 20)
        labelCompanyTitle.Name = "labelCompanyTitle"
        labelCompanyTitle.Size = New Size(242, 21)
        labelCompanyTitle.TabIndex = 0
        labelCompanyTitle.Text = "EXCELLENCE CARE SOLUTIONS"
        ' 
        ' labelLoginTitle
        ' 
        labelLoginTitle.AutoSize = True
        labelLoginTitle.ForeColor = Color.MidnightBlue
        labelLoginTitle.Location = New Point(130, 50)
        labelLoginTitle.Name = "labelLoginTitle"
        labelLoginTitle.Size = New Size(89, 17)
        labelLoginTitle.TabIndex = 1
        labelLoginTitle.Text = "STAFF LOGIN"
        ' 
        ' labelInstructions
        ' 
        labelInstructions.AutoSize = True
        labelInstructions.Font = New Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        labelInstructions.ForeColor = Color.Gray
        labelInstructions.Location = New Point(115, 75)
        labelInstructions.Name = "labelInstructions"
        labelInstructions.Size = New Size(111, 13)
        labelInstructions.TabIndex = 2
        labelInstructions.Text = "Enter your Credentials"
        ' 
        ' laberUserID
        ' 
        laberUserID.AutoSize = True
        laberUserID.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        laberUserID.Location = New Point(30, 110)
        laberUserID.Name = "laberUserID"
        laberUserID.Size = New Size(52, 15)
        laberUserID.TabIndex = 3
        laberUserID.Text = "User ID:"
        ' 
        ' textBoxUserID
        ' 
        textBoxUserID.CharacterCasing = CharacterCasing.Upper
        textBoxUserID.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        textBoxUserID.Location = New Point(100, 108)
        textBoxUserID.MaxLength = 10
        textBoxUserID.Name = "textBoxUserID"
        textBoxUserID.Size = New Size(180, 25)
        textBoxUserID.TabIndex = 4
        ' 
        ' labelPIN
        ' 
        labelPIN.AutoSize = True
        labelPIN.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        labelPIN.Location = New Point(30, 150)
        labelPIN.Name = "labelPIN"
        labelPIN.Size = New Size(27, 15)
        labelPIN.TabIndex = 5
        labelPIN.Text = "PIN"
        ' 
        ' textBoxPIN
        ' 
        textBoxPIN.Font = New Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        textBoxPIN.Location = New Point(100, 148)
        textBoxPIN.MaxLength = 4
        textBoxPIN.Name = "textBoxPIN"
        textBoxPIN.Size = New Size(180, 25)
        textBoxPIN.TabIndex = 6
        textBoxPIN.UseSystemPasswordChar = True
        ' 
        ' labelErrorMessage
        ' 
        labelErrorMessage.AutoSize = True
        labelErrorMessage.Location = New Point(30, 180)
        labelErrorMessage.MaximumSize = New Size(300, 0)
        labelErrorMessage.Name = "labelErrorMessage"
        labelErrorMessage.Size = New Size(0, 17)
        labelErrorMessage.TabIndex = 7
        labelErrorMessage.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' buttonLogin
        ' 
        buttonLogin.BackColor = Color.Green
        buttonLogin.FlatStyle = FlatStyle.Flat
        buttonLogin.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        buttonLogin.ForeColor = Color.White
        buttonLogin.Location = New Point(70, 210)
        buttonLogin.Name = "buttonLogin"
        buttonLogin.Size = New Size(80, 30)
        buttonLogin.TabIndex = 8
        buttonLogin.Text = "Login"
        buttonLogin.UseVisualStyleBackColor = False
        ' 
        ' buttonCancel
        ' 
        buttonCancel.BackColor = Color.Gray
        buttonCancel.FlatStyle = FlatStyle.Flat
        buttonCancel.ForeColor = Color.White
        buttonCancel.Location = New Point(160, 210)
        buttonCancel.Name = "buttonCancel"
        buttonCancel.Size = New Size(80, 30)
        buttonCancel.TabIndex = 9
        buttonCancel.Text = "Cancel"
        buttonCancel.UseVisualStyleBackColor = False
        ' 
        ' buttonManagerLogin
        ' 
        buttonManagerLogin.BackColor = Color.RoyalBlue
        buttonManagerLogin.FlatStyle = FlatStyle.Flat
        buttonManagerLogin.Font = New Font("Segoe UI", 8.0F, FontStyle.Bold)
        buttonManagerLogin.ForeColor = Color.White
        buttonManagerLogin.Location = New Point(100, 250)
        buttonManagerLogin.Name = "buttonManagerLogin"
        buttonManagerLogin.Size = New Size(120, 25)
        buttonManagerLogin.TabIndex = 10
        buttonManagerLogin.Text = "Manager Login"
        buttonManagerLogin.UseVisualStyleBackColor = False
        ' 
        ' StaffLoginForm
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(384, 321)
        Controls.Add(panelLogin)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "StaffLoginForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Staff Login - Excellence Care Solutions"
        panelLogin.ResumeLayout(False)
        panelLogin.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents panelLogin As Panel
    Friend WithEvents labelCompanyTitle As Label
    Friend WithEvents labelInstructions As Label
    Friend WithEvents labelLoginTitle As Label
    Friend WithEvents laberUserID As Label
    Friend WithEvents labelPIN As Label
    Friend WithEvents textBoxUserID As TextBox
    Friend WithEvents textBoxPIN As TextBox
    Friend WithEvents labelErrorMessage As Label
    Friend WithEvents buttonLogin As Button
    Friend WithEvents buttonCancel As Button
    Friend WithEvents buttonManagerLogin As Button
End Class
