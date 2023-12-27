<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmimport
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frmimport))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.PB = New System.Windows.Forms.ProgressBar()
        Me.Btnimport = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkop = New System.Windows.Forms.CheckBox()
        Me.optcred = New System.Windows.Forms.RadioButton()
        Me.optdebt = New System.Windows.Forms.RadioButton()
        Me.optpend = New System.Windows.Forms.RadioButton()
        Me.optgrp = New System.Windows.Forms.RadioButton()
        Me.optled = New System.Windows.Forms.RadioButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Lbldoctype = New System.Windows.Forms.Label()
        Me.lbltotamt = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkpend = New System.Windows.Forms.CheckBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.optstktfr = New System.Windows.Forms.RadioButton()
        Me.optDN = New System.Windows.Forms.RadioButton()
        Me.optpurc = New System.Windows.Forms.RadioButton()
        Me.optCN = New System.Windows.Forms.RadioButton()
        Me.optSales = New System.Windows.Forms.RadioButton()
        Me.chkall = New System.Windows.Forms.CheckBox()
        Me.Btnexport = New System.Windows.Forms.Button()
        Me.Dg = New System.Windows.Forms.DataGridView()
        Me.Sel = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnload = New System.Windows.Forms.Button()
        Me.Mskdateto = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Mskdatefr = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ChkAccdb = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.Dg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.PaleTurquoise
        Me.Panel1.Controls.Add(Me.ChkAccdb)
        Me.Panel1.Controls.Add(Me.BtnExit)
        Me.Panel1.Controls.Add(Me.PB)
        Me.Panel1.Controls.Add(Me.Btnimport)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.optpend)
        Me.Panel1.Controls.Add(Me.optgrp)
        Me.Panel1.Controls.Add(Me.optled)
        Me.Panel1.Location = New System.Drawing.Point(45, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1087, 130)
        Me.Panel1.TabIndex = 0
        '
        'BtnExit
        '
        Me.BtnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnExit.Location = New System.Drawing.Point(1009, 3)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(75, 23)
        Me.BtnExit.TabIndex = 6
        Me.BtnExit.Text = "Exit"
        Me.BtnExit.UseVisualStyleBackColor = True
        '
        'PB
        '
        Me.PB.Location = New System.Drawing.Point(13, 92)
        Me.PB.Name = "PB"
        Me.PB.Size = New System.Drawing.Size(304, 23)
        Me.PB.TabIndex = 6
        '
        'Btnimport
        '
        Me.Btnimport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btnimport.Location = New System.Drawing.Point(133, 46)
        Me.Btnimport.Name = "Btnimport"
        Me.Btnimport.Size = New System.Drawing.Size(75, 23)
        Me.Btnimport.TabIndex = 4
        Me.Btnimport.Text = "Import"
        Me.Btnimport.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.LightGray
        Me.Panel2.Controls.Add(Me.chkop)
        Me.Panel2.Controls.Add(Me.optcred)
        Me.Panel2.Controls.Add(Me.optdebt)
        Me.Panel2.Location = New System.Drawing.Point(326, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(313, 29)
        Me.Panel2.TabIndex = 3
        '
        'chkop
        '
        Me.chkop.AutoSize = True
        Me.chkop.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkop.Location = New System.Drawing.Point(183, 6)
        Me.chkop.Name = "chkop"
        Me.chkop.Size = New System.Drawing.Size(116, 17)
        Me.chkop.TabIndex = 5
        Me.chkop.Text = "Opening(GRPO)"
        Me.chkop.UseVisualStyleBackColor = True
        '
        'optcred
        '
        Me.optcred.AutoSize = True
        Me.optcred.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optcred.Location = New System.Drawing.Point(84, 3)
        Me.optcred.Name = "optcred"
        Me.optcred.Size = New System.Drawing.Size(83, 19)
        Me.optcred.TabIndex = 4
        Me.optcred.TabStop = True
        Me.optcred.Text = "Creditors"
        Me.optcred.UseVisualStyleBackColor = True
        '
        'optdebt
        '
        Me.optdebt.AutoSize = True
        Me.optdebt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optdebt.Location = New System.Drawing.Point(3, 3)
        Me.optdebt.Name = "optdebt"
        Me.optdebt.Size = New System.Drawing.Size(75, 19)
        Me.optdebt.TabIndex = 3
        Me.optdebt.TabStop = True
        Me.optdebt.Text = "Debtors"
        Me.optdebt.UseVisualStyleBackColor = True
        '
        'optpend
        '
        Me.optpend.AutoSize = True
        Me.optpend.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optpend.Location = New System.Drawing.Point(209, 5)
        Me.optpend.Name = "optpend"
        Me.optpend.Size = New System.Drawing.Size(102, 19)
        Me.optpend.TabIndex = 2
        Me.optpend.TabStop = True
        Me.optpend.Text = "Outstanding"
        Me.optpend.UseVisualStyleBackColor = True
        '
        'optgrp
        '
        Me.optgrp.AutoSize = True
        Me.optgrp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optgrp.Location = New System.Drawing.Point(106, 5)
        Me.optgrp.Name = "optgrp"
        Me.optgrp.Size = New System.Drawing.Size(64, 19)
        Me.optgrp.TabIndex = 1
        Me.optgrp.TabStop = True
        Me.optgrp.Text = "Group"
        Me.optgrp.UseVisualStyleBackColor = True
        '
        'optled
        '
        Me.optled.AutoSize = True
        Me.optled.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optled.Location = New System.Drawing.Point(13, 5)
        Me.optled.Name = "optled"
        Me.optled.Size = New System.Drawing.Size(70, 19)
        Me.optled.TabIndex = 0
        Me.optled.TabStop = True
        Me.optled.Text = "Ledger"
        Me.optled.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.MistyRose
        Me.Panel3.Controls.Add(Me.Lbldoctype)
        Me.Panel3.Controls.Add(Me.lbltotamt)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.chkpend)
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Controls.Add(Me.chkall)
        Me.Panel3.Controls.Add(Me.Btnexport)
        Me.Panel3.Controls.Add(Me.Dg)
        Me.Panel3.Controls.Add(Me.btnload)
        Me.Panel3.Controls.Add(Me.Mskdateto)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.Mskdatefr)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Location = New System.Drawing.Point(45, 148)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1087, 498)
        Me.Panel3.TabIndex = 5
        '
        'Lbldoctype
        '
        Me.Lbldoctype.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbldoctype.ForeColor = System.Drawing.Color.Maroon
        Me.Lbldoctype.Location = New System.Drawing.Point(910, 10)
        Me.Lbldoctype.Name = "Lbldoctype"
        Me.Lbldoctype.Size = New System.Drawing.Size(100, 23)
        Me.Lbldoctype.TabIndex = 17
        Me.Lbldoctype.Text = "Label4"
        '
        'lbltotamt
        '
        Me.lbltotamt.AutoSize = True
        Me.lbltotamt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotamt.ForeColor = System.Drawing.Color.Maroon
        Me.lbltotamt.Location = New System.Drawing.Point(891, 462)
        Me.lbltotamt.Name = "lbltotamt"
        Me.lbltotamt.Size = New System.Drawing.Size(16, 16)
        Me.lbltotamt.TabIndex = 16
        Me.lbltotamt.Text = "0"
        Me.lbltotamt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(727, 462)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 16)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Total"
        '
        'chkpend
        '
        Me.chkpend.AutoSize = True
        Me.chkpend.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkpend.Location = New System.Drawing.Point(345, 32)
        Me.chkpend.Name = "chkpend"
        Me.chkpend.Size = New System.Drawing.Size(130, 17)
        Me.chkpend.TabIndex = 14
        Me.chkpend.Text = "Pending for Import"
        Me.chkpend.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel4.Controls.Add(Me.optstktfr)
        Me.Panel4.Controls.Add(Me.optDN)
        Me.Panel4.Controls.Add(Me.optpurc)
        Me.Panel4.Controls.Add(Me.optCN)
        Me.Panel4.Controls.Add(Me.optSales)
        Me.Panel4.Location = New System.Drawing.Point(5, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(354, 24)
        Me.Panel4.TabIndex = 13
        '
        'optstktfr
        '
        Me.optstktfr.AutoSize = True
        Me.optstktfr.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optstktfr.Location = New System.Drawing.Point(263, 3)
        Me.optstktfr.Name = "optstktfr"
        Me.optstktfr.Size = New System.Drawing.Size(64, 17)
        Me.optstktfr.TabIndex = 4
        Me.optstktfr.TabStop = True
        Me.optstktfr.Text = "Stk Tfr"
        Me.optstktfr.UseVisualStyleBackColor = True
        '
        'optDN
        '
        Me.optDN.AutoSize = True
        Me.optDN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDN.Location = New System.Drawing.Point(208, 3)
        Me.optDN.Name = "optDN"
        Me.optDN.Size = New System.Drawing.Size(43, 17)
        Me.optDN.TabIndex = 3
        Me.optDN.TabStop = True
        Me.optDN.Text = "DN"
        Me.optDN.UseVisualStyleBackColor = True
        '
        'optpurc
        '
        Me.optpurc.AutoSize = True
        Me.optpurc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optpurc.Location = New System.Drawing.Point(121, 3)
        Me.optpurc.Name = "optpurc"
        Me.optpurc.Size = New System.Drawing.Size(78, 17)
        Me.optpurc.TabIndex = 2
        Me.optpurc.TabStop = True
        Me.optpurc.Text = "Purchase"
        Me.optpurc.UseVisualStyleBackColor = True
        '
        'optCN
        '
        Me.optCN.AutoSize = True
        Me.optCN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCN.Location = New System.Drawing.Point(72, 3)
        Me.optCN.Name = "optCN"
        Me.optCN.Size = New System.Drawing.Size(42, 17)
        Me.optCN.TabIndex = 1
        Me.optCN.TabStop = True
        Me.optCN.Text = "CN"
        Me.optCN.UseVisualStyleBackColor = True
        '
        'optSales
        '
        Me.optSales.AutoSize = True
        Me.optSales.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSales.Location = New System.Drawing.Point(8, 3)
        Me.optSales.Name = "optSales"
        Me.optSales.Size = New System.Drawing.Size(56, 17)
        Me.optSales.TabIndex = 0
        Me.optSales.TabStop = True
        Me.optSales.Text = "Sales"
        Me.optSales.UseVisualStyleBackColor = True
        '
        'chkall
        '
        Me.chkall.AutoSize = True
        Me.chkall.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkall.Location = New System.Drawing.Point(9, 32)
        Me.chkall.Name = "chkall"
        Me.chkall.Size = New System.Drawing.Size(80, 17)
        Me.chkall.TabIndex = 12
        Me.chkall.Text = "Select All"
        Me.chkall.UseVisualStyleBackColor = True
        '
        'Btnexport
        '
        Me.Btnexport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btnexport.Location = New System.Drawing.Point(697, 6)
        Me.Btnexport.Name = "Btnexport"
        Me.Btnexport.Size = New System.Drawing.Size(122, 23)
        Me.Btnexport.TabIndex = 11
        Me.Btnexport.Text = "Export To Tally"
        Me.Btnexport.UseVisualStyleBackColor = True
        '
        'Dg
        '
        Me.Dg.AllowUserToAddRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dg.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.Dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Dg.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Sel, Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7, Me.Column8, Me.Column9, Me.Column10})
        Me.Dg.Location = New System.Drawing.Point(8, 50)
        Me.Dg.Name = "Dg"
        Me.Dg.RowHeadersVisible = False
        Me.Dg.Size = New System.Drawing.Size(1002, 409)
        Me.Dg.TabIndex = 10
        '
        'Sel
        '
        Me.Sel.HeaderText = "Sel"
        Me.Sel.Name = "Sel"
        Me.Sel.Width = 60
        '
        'Column1
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column1.HeaderText = "InvNo"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column1.Width = 70
        '
        'Column2
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.Column2.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column2.HeaderText = "Date"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column2.Width = 80
        '
        'Column3
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.Column3.DefaultCellStyle = DataGridViewCellStyle4
        Me.Column3.HeaderText = "VoucherNo"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column3.Width = 80
        '
        'Column4
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.Column4.DefaultCellStyle = DataGridViewCellStyle5
        Me.Column4.HeaderText = "Party Name"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column4.Width = 200
        '
        'Column5
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column5.DefaultCellStyle = DataGridViewCellStyle6
        Me.Column5.HeaderText = "Amount"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column5.Width = 80
        '
        'Column6
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column6.DefaultCellStyle = DataGridViewCellStyle7
        Me.Column6.HeaderText = "Discount Amt"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column6.Width = 80
        '
        'Column7
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column7.DefaultCellStyle = DataGridViewCellStyle8
        Me.Column7.HeaderText = "Courier"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column7.Width = 70
        '
        'Column8
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column8.DefaultCellStyle = DataGridViewCellStyle9
        Me.Column8.HeaderText = "Forward"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column8.Width = 70
        '
        'Column9
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column9.DefaultCellStyle = DataGridViewCellStyle10
        Me.Column9.HeaderText = "Roundoff"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column9.Width = 70
        '
        'Column10
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column10.DefaultCellStyle = DataGridViewCellStyle11
        Me.Column10.HeaderText = "Totamt"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        Me.Column10.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'btnload
        '
        Me.btnload.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnload.Location = New System.Drawing.Point(627, 6)
        Me.btnload.Name = "btnload"
        Me.btnload.Size = New System.Drawing.Size(64, 23)
        Me.btnload.TabIndex = 9
        Me.btnload.Text = "Load"
        Me.btnload.UseVisualStyleBackColor = True
        '
        'Mskdateto
        '
        Me.Mskdateto.Location = New System.Drawing.Point(551, 7)
        Me.Mskdateto.Mask = "##-##-####"
        Me.Mskdateto.Name = "Mskdateto"
        Me.Mskdateto.Size = New System.Drawing.Size(70, 20)
        Me.Mskdateto.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(525, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(22, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "To"
        '
        'Mskdatefr
        '
        Me.Mskdatefr.Location = New System.Drawing.Point(449, 7)
        Me.Mskdatefr.Mask = "##-##-####"
        Me.Mskdatefr.Name = "Mskdatefr"
        Me.Mskdatefr.Size = New System.Drawing.Size(70, 20)
        Me.Mskdatefr.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(382, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Date From"
        '
        'ChkAccdb
        '
        Me.ChkAccdb.AutoSize = True
        Me.ChkAccdb.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAccdb.Location = New System.Drawing.Point(14, 30)
        Me.ChkAccdb.Name = "ChkAccdb"
        Me.ChkAccdb.Size = New System.Drawing.Size(88, 17)
        Me.ChkAccdb.TabIndex = 7
        Me.ChkAccdb.Text = "Access DB"
        Me.ChkAccdb.UseVisualStyleBackColor = True
        '
        'Frmimport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1174, 658)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Frmimport"
        Me.Text = "Tally Data Import"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.Dg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents optcred As RadioButton
    Friend WithEvents optdebt As RadioButton
    Friend WithEvents optpend As RadioButton
    Friend WithEvents optgrp As RadioButton
    Friend WithEvents optled As RadioButton
    Friend WithEvents Btnimport As Button
    Friend WithEvents chkop As CheckBox
    Friend WithEvents PB As ProgressBar
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Dg As DataGridView
    Friend WithEvents btnload As Button
    Friend WithEvents Mskdateto As MaskedTextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Mskdatefr As MaskedTextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Sel As DataGridViewCheckBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
    Friend WithEvents Column9 As DataGridViewTextBoxColumn
    Friend WithEvents Column10 As DataGridViewTextBoxColumn
    Friend WithEvents Btnexport As Button
    Friend WithEvents chkall As CheckBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents optDN As RadioButton
    Friend WithEvents optpurc As RadioButton
    Friend WithEvents optCN As RadioButton
    Friend WithEvents optSales As RadioButton
    Friend WithEvents chkpend As CheckBox
    Friend WithEvents lbltotamt As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents BtnExit As Button
    Friend WithEvents optstktfr As RadioButton
    Friend WithEvents Lbldoctype As Label
    Friend WithEvents ChkAccdb As CheckBox
End Class
