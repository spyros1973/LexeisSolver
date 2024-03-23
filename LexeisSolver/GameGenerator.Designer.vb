<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GameGenerator
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
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkRequireVowelNeighbors = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.numMinWordTimes = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbLanguage = New System.Windows.Forms.ComboBox()
        Me.numMinLongestWordLength = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.numWordThreshold = New System.Windows.Forms.NumericUpDown()
        Me.numGames = New System.Windows.Forms.NumericUpDown()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtOutputPath = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtOut = New System.Windows.Forms.TextBox()
        Me.prg = New System.Windows.Forms.ProgressBar()
        Me.GroupBox1.SuspendLayout()
        CType(Me.numMinWordTimes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numMinLongestWordLength, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numWordThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numGames, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnGenerate
        '
        Me.btnGenerate.Location = New System.Drawing.Point(454, 161)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(85, 34)
        Me.btnGenerate.TabIndex = 0
        Me.btnGenerate.Text = "Generate"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkRequireVowelNeighbors)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.numMinWordTimes)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmbLanguage)
        Me.GroupBox1.Controls.Add(Me.numMinLongestWordLength)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.numWordThreshold)
        Me.GroupBox1.Controls.Add(Me.numGames)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.txtOutputPath)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(527, 143)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Generation parameters"
        '
        'chkRequireVowelNeighbors
        '
        Me.chkRequireVowelNeighbors.AutoSize = True
        Me.chkRequireVowelNeighbors.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkRequireVowelNeighbors.Location = New System.Drawing.Point(263, 122)
        Me.chkRequireVowelNeighbors.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.chkRequireVowelNeighbors.Name = "chkRequireVowelNeighbors"
        Me.chkRequireVowelNeighbors.Size = New System.Drawing.Size(259, 17)
        Me.chkRequireVowelNeighbors.TabIndex = 14
        Me.chkRequireVowelNeighbors.Text = "Require consonnants half neighbors to be vowels"
        Me.chkRequireVowelNeighbors.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(468, 93)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(12, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "x"
        '
        'numMinWordTimes
        '
        Me.numMinWordTimes.Location = New System.Drawing.Point(485, 90)
        Me.numMinWordTimes.Maximum = New Decimal(New Integer() {15, 0, 0, 0})
        Me.numMinWordTimes.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numMinWordTimes.Name = "numMinWordTimes"
        Me.numMinWordTimes.Size = New System.Drawing.Size(35, 20)
        Me.numMinWordTimes.TabIndex = 11
        Me.numMinWordTimes.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(262, 59)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Language:"
        '
        'cmbLanguage
        '
        Me.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLanguage.FormattingEnabled = True
        Me.cmbLanguage.Location = New System.Drawing.Point(358, 56)
        Me.cmbLanguage.Name = "cmbLanguage"
        Me.cmbLanguage.Size = New System.Drawing.Size(163, 21)
        Me.cmbLanguage.TabIndex = 9
        '
        'numMinLongestWordLength
        '
        Me.numMinLongestWordLength.Location = New System.Drawing.Point(415, 90)
        Me.numMinLongestWordLength.Maximum = New Decimal(New Integer() {15, 0, 0, 0})
        Me.numMinLongestWordLength.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numMinLongestWordLength.Name = "numMinLongestWordLength"
        Me.numMinLongestWordLength.Size = New System.Drawing.Size(47, 20)
        Me.numMinLongestWordLength.TabIndex = 8
        Me.numMinLongestWordLength.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(262, 92)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(143, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Minimum longest word length"
        '
        'numWordThreshold
        '
        Me.numWordThreshold.Location = New System.Drawing.Point(135, 90)
        Me.numWordThreshold.Maximum = New Decimal(New Integer() {40, 0, 0, 0})
        Me.numWordThreshold.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numWordThreshold.Name = "numWordThreshold"
        Me.numWordThreshold.Size = New System.Drawing.Size(64, 20)
        Me.numWordThreshold.TabIndex = 6
        Me.numWordThreshold.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'numGames
        '
        Me.numGames.Location = New System.Drawing.Point(135, 57)
        Me.numGames.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.numGames.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numGames.Name = "numGames"
        Me.numGames.Size = New System.Drawing.Size(64, 20)
        Me.numGames.TabIndex = 5
        Me.numGames.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Location = New System.Drawing.Point(493, 27)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(28, 20)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtOutputPath
        '
        Me.txtOutputPath.Location = New System.Drawing.Point(135, 27)
        Me.txtOutputPath.Name = "txtOutputPath"
        Me.txtOutputPath.Size = New System.Drawing.Size(352, 20)
        Me.txtOutputPath.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 93)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Word threshold"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Number of games"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Output path"
        '
        'txtOut
        '
        Me.txtOut.BackColor = System.Drawing.SystemColors.Info
        Me.txtOut.Location = New System.Drawing.Point(12, 221)
        Me.txtOut.Multiline = True
        Me.txtOut.Name = "txtOut"
        Me.txtOut.ReadOnly = True
        Me.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOut.Size = New System.Drawing.Size(527, 335)
        Me.txtOut.TabIndex = 2
        '
        'prg
        '
        Me.prg.Location = New System.Drawing.Point(12, 161)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(348, 34)
        Me.prg.TabIndex = 3
        Me.prg.Visible = False
        '
        'GameGenerator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(552, 568)
        Me.Controls.Add(Me.prg)
        Me.Controls.Add(Me.txtOut)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnGenerate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "GameGenerator"
        Me.Text = "Game Generator"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.numMinWordTimes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numMinLongestWordLength, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numWordThreshold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numGames, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnGenerate As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents numWordThreshold As NumericUpDown
    Friend WithEvents numGames As NumericUpDown
    Friend WithEvents Button1 As Button
    Friend WithEvents txtOutputPath As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtOut As TextBox
    Friend WithEvents prg As ProgressBar
    Friend WithEvents numMinLongestWordLength As NumericUpDown
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents cmbLanguage As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents numMinWordTimes As NumericUpDown
    Friend WithEvents chkRequireVowelNeighbors As CheckBox
End Class
