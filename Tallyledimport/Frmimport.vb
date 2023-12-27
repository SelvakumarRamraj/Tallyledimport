Imports System.IO
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Threading.Thread
Imports System.Globalization
Imports System.Xml
'Imports MSXML2
Public Class Frmimport
    Dim conacc As OleDb.OleDbConnection
    Dim msql, msql2, msql3, merrname, msql4, msql5 As String
    Dim mtrans As SqlTransaction
    Dim matrans As OleDb.OleDbTransaction
    Dim mhgt, nn, np, n, minvno, l, lno As Int32
    Dim mgrp, mcourierchrg, mforwardchrg, mvchno, mdoctype, mroundled As String
    Dim mdate, mparty, mnarr As String
    Dim mamt, mtotamt, mdiscamt, mktotamt As Double
    Dim mcourier, mforward, mroundoff As Single
    Dim mupdt As Boolean
    'Private ServerHTTP As New MSXML2.ServerXMLHTTP30
    'Private XMLDOM As New MSXML2.DOMDocument30
    Dim xmlstc As String
    Dim tallyDataString, mmst As String

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
        Application.Exit()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call MAIN()
        mupdt = False
        PB.Minimum = 0
        optSales.Checked = True
        If Mid(Trim(Tgrpcmpnam), 1, 6) = "THARUN" Then
            optpurc.Visible = True
            optstktfr.Visible = True
        Else
            optpurc.Visible = False
            optstktfr.Visible = False
        End If

        optCN.Visible = False
        optDN.Visible = False
        'Me.WindowState = FormWindowState.Maximized
        'mhgt = Me.Height
        'Me.Width = My.Computer.Screen.Bounds.Width - 25
        'GroupBox1.Width = My.Computer.Screen.Bounds.Width - 50
        'GroupBox1.Height = 800
        'Dg.Width = GroupBox1.Width - 40
        'Dg.Height = GroupBox1.Height - 60
        'Panel1.Top = (Me.Height / 2) - (Panel1.Height / 2) - 30
        Panel1.Top = 100
        Panel1.Left = (Me.Width / 2) - (Panel1.Width / 2)



        Panel2.Visible = False
        chkop.Visible = False

        Panel3.Top = 100 + Panel1.Height + 10
        Panel3.Left = (Me.Width / 2) - (Panel3.Width / 2)

        mgrp = "SUNDRY DEBTORS"
        'mgrp = "SUNDRY CREDITORS"

        If chktalycon = True Then
            ' Cursor = Cursors.WaitCursor
            'Call saveledrun()
            'Call saverecrun()
            'Call saveoustand()
            'Cursor = Cursors.Default
            'Call killproc()
            'Application.Exit()
            mcompcode = getcmpcod()
            'MsgBox(mcompcode)
        Else
            MsgBox("Tally not Open")
            mcompcode = 0
            'Call killproc()
            'Application.Exit()
        End If



    End Sub
    Private Function getcmpcod() As Int16
        Dim mcode As Int16
        msql2 = "select id from companymast where compname='" & Trim(Tgrpcmpnam) & "'"
        Dim dtt As DataTable = getDataTable(msql2)
        If dtt.Rows.Count > 0 Then
            For Each rww As DataRow In dtt.Rows
                mcode = rww(0)
            Next
        End If
        Return mcode
    End Function

    Private Sub chkall_CheckedChanged(sender As Object, e As EventArgs) Handles chkall.CheckedChanged
        If chkall.Checked = True Then
            For j As Integer = 0 To Dg.Rows.Count - 1
                Dg.Rows(j).Cells(0).Value = True
            Next
        Else
            For j As Integer = 0 To Dg.Rows.Count - 1
                Dg.Rows(j).Cells(0).Value = False
            Next
        End If
    End Sub

    Private Sub killproc()
        Dim processes As Process() = Process.GetProcessesByName("Tallyledimport")

        For Each process As Process In processes
            process.Kill()
        Next
    End Sub

    Private Sub Btnimport_Click(sender As Object, e As EventArgs) Handles Btnimport.Click
        If chktalycon = True Then
            Cursor = Cursors.WaitCursor
            If optled.Checked = True Then
                If ChkAccdb.Checked = True Then
                    Call accessdb()
                Else

                    Call saveledrun()
                End If

            ElseIf optgrp.Checked = True Then
                    Call loadgrp()
                ElseIf optdebt.Checked = True Then
                    Call loadoutstanding()
                ElseIf optcred.Checked = True Then
                    Call loadoutstanding()
            End If
            Cursor = Cursors.Default
        Else
            MsgBox("Tally Not Open!")
        End If



    End Sub



    Private Sub btnload_Click(sender As Object, e As EventArgs) Handles btnload.Click
        If optSales.Checked = True Or optpurc.Checked = True Or optstktfr.Checked = True Or optCN.Checked = True Or optDN.Checked = True Then
            Call loadinvoice()
        Else
            MsgBox("Select Voucher Type!")
        End If

    End Sub

    Private Sub Btnexport_Click(sender As Object, e As EventArgs) Handles Btnexport.Click
        Call exportinv()
    End Sub

    Private Sub loadinvoice()
        If optSales.Checked = True Then
            mdoctype = "SALES"
            If chkpend.Checked = True Then
                msql2 = "select invno,date,convert(nvarchar(10),invno)+'/'+rtrim(finyr) vchno, party,Amount,cdiscamt,isnull(courier,0) courier,forward,[round] roundoff,totamt from inv where date>='" & Format(CDate(Mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and isnull(updt,0)=0"
                'msql2 = "select invno,date,convert(nvarchar(10),invno)+'/'+rtrim(finyr) vchno, party,Amount,cdiscamt,courier,forward,[round] roundoff,totamt from inv where date>='" & Format(CDate(Mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'"
            Else
                msql2 = "select invno,date,convert(nvarchar(10),invno)+'/'+rtrim(finyr) vchno, party,Amount,cdiscamt,isnull(courier,0) courier,forward,[round] roundoff,totamt from inv where date>='" & Format(CDate(Mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'"
            End If
        ElseIf optstktfr.Checked = True Then
            mdoctype = "STKTFR"

            If chkpend.Checked = True Then
                msql2 = "select invno,date,convert(nvarchar(10),invno)+'/'+rtrim(finyr) vchno, party,Amount,cdiscamt,0 courier,forward,[round] roundoff,totamt from stktfrinv where date>='" & Format(CDate(Mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and isnull(updt,0)=0"
                'msql2 = "select invno,date,convert(nvarchar(10),invno)+'/'+rtrim(finyr) vchno, party,Amount,cdiscamt,courier,forward,[round] roundoff,totamt from inv where date>='" & Format(CDate(Mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'"
            Else
                msql2 = "select invno,date,convert(nvarchar(10),invno)+'/'+rtrim(finyr) vchno, party,Amount,cdiscamt,0 courier,forward,[round] roundoff,totamt from stktfrinv where date>='" & Format(CDate(Mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'"
            End If

        ElseIf optpurc.Checked = True Then
            mdoctype = "PURC"
            If chkpend.Checked = True Then
                msql2 = "select rno invno,rdate Date,convert(nvarchar(10),invdcno)+'/'+rtrim(finyr) vchno, party,Amount,0 cdiscamt,0 Courier,forward,roundoff,totamt,q194amt Tds from rcpt where rdate>='" & Format(CDate(Mskdatefr.Text), "yyyy-MM-dd") & "' and rdate<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and isnull(updt,0)=0 "
            Else
                msql2 = "select rno invno,rdate Date,convert(nvarchar(10),invdcno)+'/'+rtrim(finyr) vchno, party,Amount,0 cdiscamt,0 Courier,forward,roundoff,totamt,q194amt Tds from rcpt where rdate>='" & Format(CDate(Mskdatefr.Text), "yyyy-MM-dd") & "' and rdate<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'"
            End If
        Else
            mdoctype = "SALES"
            If chkpend.Checked = True Then
                msql2 = "select invno,date,convert(nvarchar(10),invno)+'/'+rtrim(finyr) vchno, party,Amount,cdiscamt,courier,forward,[round] roundoff,totamt from inv where date>='" & Format(CDate(Mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and isnull(updt,0)=0"
                'msql2 = "select invno,date,convert(nvarchar(10),invno)+'/'+rtrim(finyr) vchno, party,Amount,cdiscamt,courier,forward,[round] roundoff,totamt from inv where date>='" & Format(CDate(Mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'"
            Else
                msql2 = "select invno,date,convert(nvarchar(10),invno)+'/'+rtrim(finyr) vchno, party,Amount,cdiscamt,courier,forward,[round] roundoff,totamt from inv where date>='" & Format(CDate(Mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'"
            End If
        End If


        Lbldoctype.Text = mdoctype


        mktotamt = 0
        Dg.Rows.Clear()
        Dim dtg As DataTable = getDataTable(msql2)
        If dtg.Rows.Count > 0 Then
            For Each rrw As DataRow In dtg.Rows
                n = Dg.Rows.Add
                Dg.Rows(n).Cells(1).Value = rrw("invno")
                Dg.Rows(n).Cells(2).Value = rrw("date")
                Dg.Rows(n).Cells(3).Value = rrw("vchno")
                Dg.Rows(n).Cells(4).Value = rrw("party")
                Dg.Rows(n).Cells(5).Value = Format(rrw("Amount"), "###########0.00")
                Dg.Rows(n).Cells(6).Value = rrw("cdiscamt")
                Dg.Rows(n).Cells(7).Value = rrw("courier")
                Dg.Rows(n).Cells(8).Value = rrw("forward")
                Dg.Rows(n).Cells(9).Value = rrw("roundoff")
                Dg.Rows(n).Cells(10).Value = Format(rrw("Totamt"), "###########0.00")
                mktotamt = Format(mktotamt + Val(rrw("Totamt")), "###########0.00")
            Next
        End If
        lbltotamt.Text = Format(mktotamt, "###########0.00")
        'dtg.Dispose()
    End Sub
    Private Sub optpend_CheckedChanged(sender As Object, e As EventArgs) Handles optpend.CheckedChanged
        PB.Minimum = 0
        If optpend.Checked = True Then
            If Panel2.Visible = False Then Panel2.Visible = True
        Else
            If Panel2.Visible = True Then Panel2.Visible = False
        End If
    End Sub

    Private Sub optled_CheckedChanged(sender As Object, e As EventArgs) Handles optled.CheckedChanged
        PB.Maximum = 0
    End Sub

    Private Sub optgrp_CheckedChanged(sender As Object, e As EventArgs) Handles optgrp.CheckedChanged
        PB.Maximum = 0
    End Sub

    Private Sub optdebt_CheckedChanged(sender As Object, e As EventArgs) Handles optdebt.CheckedChanged
        PB.Value = 0
        If optdebt.Checked = True Then
            If chkop.Visible = False Then chkop.Visible = True
            'omkar
            'mgrp = "SUNDRY DEBTORS"
            mgrp = "Sundry Debtors"
        Else
            If chkop.Visible = True Then chkop.Visible = False
            mgrp = ""
        End If
    End Sub

    Private Sub optcred_CheckedChanged(sender As Object, e As EventArgs) Handles optcred.CheckedChanged
        PB.Value = 0
        If optcred.Checked = True Then
            'mgrp = "SUNDRY CREDITORS"
            mgrp = "Sundry Creditors"
        Else
            mgrp = ""
        End If
    End Sub

    Private Sub saveledrun()


        'msql = "SELECT LEDGER.`$name`,LEDGER.`$ADDITIONALname`,LEDGER.`$_PRIMARYGROUP`," _
        '   & "LEDGER.`$_ADDRESS1`,LEDGER.`$_ADDRESS2`,LEDGER.`$_ADDRESS3`," _
        '   & "LEDGER.`$_ADDRESS4`,LEDGER.`$_ADDRESS5`,LEDGER.`$PARENT`," _
        '   & "LEDGER.`$IncomeTaxNumber`,LEDGER.`$SALESTAXNUMBER`,LEDGER.`$INTERSTATESTNUMBER`,LEDGER.`$VATTINNUMBER`," _
        '   & "LEDGER.`$Narration`,LEDGER.`$_PERFORMANCE`,LEDGER.`$mdisc`,LEDGER.`$mlorry`,LEDGER.`$mbrand`," _
        '   & "LEDGER.`$mGRADE`,LEDGER.`$mDEST`,LEDGER.`$mDOCU`,LEDGER.`$mKEYPER`,LEDGER.`$mHOLIDAY`,LEDGER.`$mDISTRICT`,LEDGER.`$ledgercontact`,LEDGER.`$ledgerphone`, " _
        '   & "LEDGER.`$mPROP`,LEDGER.`$mSTD`,Ledger.`$_ClosingBalance`,Ledger.`$mbill`,Ledger.`$EMail`,Ledger.`$CreditLimit`,Ledger.`$LedgerMobile`, " _
        '   & "LEDGER.`$partygstin`,LEDGER.`$mdadd1`,LEDGER.`$mdadd2`,LEDGER.`$mdadd3`,LEDGER.`$mdadd4`,LEDGER.`$mdcity`, " _
        '   & "LEDGER.`$mdpincode`,LEDGER.`$mdstate`,LEDGER.`$mdgstin`,Ledger.`$mcity`,Ledger.`$PINCode`,Ledger.`$mdistance`,Ledger.`$mddistance`,Ledger.`$statename` FROM  Ledger"


        msql = "SELECT LEDGER.`$name`,LEDGER.`$Mailingname`,LEDGER.`$_PRIMARYGROUP`," _
           & "LEDGER.`$_ADDRESS1`,LEDGER.`$_ADDRESS2`,LEDGER.`$_ADDRESS3`," _
           & "LEDGER.`$_ADDRESS4`,LEDGER.`$_ADDRESS5`,LEDGER.`$PARENT`," _
           & "LEDGER.`$IncomeTaxNumber`,LEDGER.`$SALESTAXNUMBER`,LEDGER.`$INTERSTATESTNUMBER`,LEDGER.`$VATTINNUMBER`," _
           & "LEDGER.`$Narration`,LEDGER.`$_PERFORMANCE`,LEDGER.`$mdisc`,LEDGER.`$mlorry`,LEDGER.`$mbrand`," _
           & "LEDGER.`$mGRADE`,LEDGER.`$mDEST`,LEDGER.`$mDOCU`,LEDGER.`$mKEYPER`,LEDGER.`$mHOLIDAY`,LEDGER.`$mDISTRICT`,LEDGER.`$ledgercontact`,LEDGER.`$ledgerphone`, " _
           & "LEDGER.`$mPROP`,LEDGER.`$mSTD`,Ledger.`$LedgerMobile`,Ledger.`$_ClosingBalance`,Ledger.`$mbill`,Ledger.`$EMail`,Ledger.`$OpeningBalance`, " _
           & "LEDGER.`$PartyGSTIN`,LEDGER.`$mdadd1`,LEDGER.`$mdadd2`,LEDGER.`$mdadd3`,LEDGER.`$mdadd4`,LEDGER.`$mdcity`, " _
           & "LEDGER.`$mdpincode`,LEDGER.`$mdstate`,LEDGER.`$mdgstin`,Ledger.`$mddistance`,Ledger.`$mcity`,Ledger.`$PINCode`,Ledger.`$mdistance`,Ledger.`$statename`,Ledger.`$_PartyGSTIN`,LEDGER.`$Mcardcode` FROM  Ledger"




        '                  0       1         2            3         4          5         6         7         8          9                      10                   11            12
        ' msql = "SELECT @$name,@ADDlname,@PRIMARYGROUP,@ADDRESS1,@ADDRESS2,@ADDRESS3,@ADDRESS4,@ADDRESS5,@PARENT`,@$IncomeTaxNumber,@SALESTAXNUMBER,@INTERSTATESTNUMBER,@VATTINNUMBER`,"
        '       13             14     15      16      17      18      19     20     21        22        23          24            25
        ' & "@Narration,@PERFORMANCE,@mdisc,@mlorry,@mbrand,@mGRADE,@mDEST,@mDOCU,@mKEYPER,@mHOLIDAY,@mDISTRICT,@ledgercontact,@ledgerphone, " _
        '        26    27     28             29     30     31          32               33         34       35      36     37      38      
        '  & "@mPROP,@mSTD,@ClosingBalance,@mbill,@EMail,@CreditLimit,@LedgerMobile, @partygstin,@mdadd1,@mdadd2,@mdadd3,@mdadd4,@mdcity, " _
        '       39         40         41    42      43        44        45         46
        '  & "@mdpincode,@mdstate,@mdgstin,@mcity,@PINCode,@mdistance,@mddistance,@statename"




        'msql = "Select AllCompLedger3.`$LedgerName`,AllCompLedger3.`$OpeningBalance` , AllCompLedger3.`$ClosingBalance`,AllCompLedger3.`$Parent`, AllCompLedger3.`$PrimaryGroup`,AllCompLedger3.`$CompanyName`,  " _
        '      & " AllCompLedger3.`$Mname`,AllCompLedger3.`$Add1`,AllCompLedger3.`$Add2`,AllCompLedger3.`$Add3`,AllCompLedger3.`$Add4`,AllCompLedger3.`$Add5`,AllCompLedger3.`$Pincode`, " _
        '      & " AllCompLedger3.`$State`,AllCompLedger3.`$Country`,AllCompLedger3.`$Mail`,AllCompLedger3.`$Phone`,AllCompLedger3.`$Mobile`,AllCompLedger3.`$Fax`, " _
        '      & " AllCompLedger3.`$Contact`,AllCompLedger3.`$ITNumber`,AllCompLedger3.`$GSTIN`,AllCompLedger3.`$Creditlimit`,AllCompLedger3.`$salestaxnumber`,AllCompLedger3.`$Tinno`,AllCompLedger3.`$Gp`,AllCompLedger3.`$GpName` from AllCompLedger3 WHERE AllCompLedger3.`$CompanyName` <> '" & Trim(Tgrpcmpnam) & "'"



        'Tcollname'

        If contally.State = ConnectionState.Closed Then
            contally.Open()
        End If

        Dim cmd As New Odbc.OdbcCommand(msql, contally)
        Dim dtl As New DataTable
        cmd.CommandTimeout = 600
        Dim da As New Odbc.OdbcDataAdapter(cmd)
        'cmd.CommandTimeout = 600

        da.Fill(dtl)
        np = 0
        PB.Maximum = dtl.Rows.Count
        PB.Step = 1
        'For Each rww As DataRow In dtl.Rows
        '    MsgBox(rww(0).ToString)
        'Next


        Dim cmdel As New Data.SqlClient.SqlCommand
        cmdel.CommandText = "delete from ledmas"
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        mtrans = con.BeginTransaction
        cmdel.Transaction = mtrans
        cmdel.Connection = con

        'Try
        '    cmdel.ExecuteNonQuery()

        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

        '**omkar
        If Mid(Trim(Tgrpcmpnam), 1, 6) = "OOMKAR" Then
            mmst = "insert into ledmas(name,mname,dum1,add1,add2,add3,add4,add5,parent,tax,tngst,Narr,perf,disc,lorry,brand,grade,destination,document,keyperson,holiday,district,contact,telephone," _
                  & "prop,std,mobile,clbal,bill,email, opbal,gstin,dadd1,dadd2,dadd3,dadd4,dcity,dpincode,dstate,dgstin,ddistance,city,pincode,distance,statename,code) " _
                  & " VALUES (@name,@ADDlname,@PRIMARYGROUP,@ADDRESS1,@ADDRESS2,@ADDRESS3,@ADDRESS4,@ADDRESS5,@PARENT,@IncomeTaxNumber,@VATTINNUMBER," _
                  & "@Narration,@PERFORMANCE,@mdisc,@mlorry,@mbrand,@mGRADE,@mDEST,@mDOCU,@mKEYPER,@mHOLIDAY,@mDISTRICT,@ledgercontact,@ledgerphone, " _
                  & "@mPROP,@mSTD,@LedgerMobile,@ClosingBalance,@mbill,@EMail,@CreditLimit, @partygstin,@mdadd1,@mdadd2,@mdadd3,@mdadd4,@mdcity, " _
                  & "@mdpincode,@mdstate,@mdgstin,@mddistance,@mcity,@PINCode,@mdistance,@statename,@cmpcode)"
        ElseIf Mid(Trim(Tgrpcmpnam), 1, 6) = "THARUN" Then
            '**tharun
            mmst = "insert into ledmas(name,mname,dum1,add1,add2,add3,add4,add5,parent,tax,tngst,Narr,perf,disc,lorry,brand,grade,destination,document,keyperson,holiday,district,contact,telephone," _
              & "prop,std,mobile,clbal,bill,email, opbal,gstin,dadd1,dadd2,dadd3,dadd4,dcity,dpincode,dstate,dgstin,ddistance,city,pincode,distance,statename,code,partycode) " _
              & " VALUES (@name,@ADDlname,@PRIMARYGROUP,@ADDRESS1,@ADDRESS2,@ADDRESS3,@ADDRESS4,@ADDRESS5,@PARENT,@IncomeTaxNumber,@VATTINNUMBER," _
              & "@Narration,@PERFORMANCE,@mdisc,@mlorry,@mbrand,@mGRADE,@mDEST,@mDOCU,@mKEYPER,@mHOLIDAY,@mDISTRICT,@ledgercontact,@ledgerphone, " _
              & "@mPROP,@mSTD,@LedgerMobile,@ClosingBalance,@mbill,@EMail,@CreditLimit, @partygstin,@mdadd1,@mdadd2,@mdadd3,@mdadd4,@mdcity, " _
              & "@mdpincode,@mdstate,@mdgstin,@mddistance,@mcity,@PINCode,@mdistance,@statename,@cmpcode,@partycode)"
        End If

        'Dim cmd2 As New Data.SqlClient.SqlCommand
        Dim cmd2 As New Data.SqlClient.SqlCommand
        cmd2.CommandTimeout = 600
        'cmd2.CommandText = "INSERT INTO TallyLedger(LedgerName,OpeningBalance,ClosingBalance,Parent,PrimaryGroup,Companyname,mname,add1,add2,add3,add4,add5,pincode,state,country,mail,phone,mobile,fax,contact,Itnumber,gstin,CreditLimit,Salestaxnumber,Tinno,Gp,GpName) " _
        '                  & " VALUES (@Ledgername,@opbal,@clbal,@Pgrp,@PrimaryGroup,@companyname,@mname,@add1,@add2,@add3,@add4,@add5,@Pincode,@State,@country,@mail,@phone,@mobile,@fax,@contact,@Itnumber,@GSTIN,@creditLimit,@salestaxnumber,@Tinno,@Gp,@Gpname )"
        cmd2.CommandText = mmst
        'cmd2 = New SqlCommand(mmst, con)
        'cmd2.CommandTimeout = 600
        'If Mid(Trim(Tgrpcmpnam), 1, 6) = "THARUN" Then
        '    cmd2.Parameters.Add("@name", SqlDbType.NVarChar) '0
        '    cmd2.Parameters.Add("@ADDlname", SqlDbType.NVarChar) '1
        '    cmd2.Parameters.Add("@PRIMARYGROUP", SqlDbType.NVarChar)   '2
        '    cmd2.Parameters.Add("@ADDRESS1", SqlDbType.NVarChar)       '3
        '    cmd2.Parameters.Add("@ADDRESS2", SqlDbType.NVarChar)       '4
        '    cmd2.Parameters.Add("@ADDRESS3", SqlDbType.NVarChar)    '5
        '    cmd2.Parameters.Add("@ADDRESS4", SqlDbType.NVarChar)    '6
        '    cmd2.Parameters.Add("@ADDRESS5", SqlDbType.NVarChar)    '7
        '    cmd2.Parameters.Add("@PARENT", SqlDbType.NVarChar)  '8
        '    cmd2.Parameters.Add("@IncomeTaxNumber", SqlDbType.NVarChar)     '9
        '    cmd2.Parameters.Add("@VATTINNUMBER", SqlDbType.NVarChar)        '10
        '    cmd2.Parameters.Add("@Narration", SqlDbType.NVarChar)       '11  13
        '    cmd2.Parameters.Add("@PERFORMANCE", SqlDbType.Real)     '12  14
        '    cmd2.Parameters.Add("@mdisc", SqlDbType.Real)       '13  15
        '    cmd2.Parameters.Add("@mlorry", SqlDbType.NVarChar)      '14  16
        '    cmd2.Parameters.Add("@mbrand", SqlDbType.NVarChar)      '15  17
        '    cmd2.Parameters.Add("@mGRADE", SqlDbType.NVarChar)      '16  18
        '    cmd2.Parameters.Add("@mDEST", SqlDbType.NVarChar)       '17  19
        '    cmd2.Parameters.Add("@mDOCU", SqlDbType.NVarChar)       '18  20
        '    cmd2.Parameters.Add("@mKEYPER", SqlDbType.NVarChar)     '19  21
        '    cmd2.Parameters.Add("@mHOLIDAY", SqlDbType.NVarChar)    '20  22
        '    cmd2.Parameters.Add("@mDISTRICT", SqlDbType.NVarChar)   '21  23
        '    cmd2.Parameters.Add("@ledgercontact", SqlDbType.NVarChar)       '22  24
        '    cmd2.Parameters.Add("@ledgerphone", SqlDbType.NVarChar)     '23  25
        '    cmd2.Parameters.Add("@mPROP", SqlDbType.NVarChar)       '24  26
        '    cmd2.Parameters.Add("@mSTD", SqlDbType.NVarChar)        '25  27
        '    cmd2.Parameters.Add("@LedgerMobile", SqlDbType.NVarChar)    '26  28 
        '    cmd2.Parameters.Add("@ClosingBalance", SqlDbType.Decimal)   '27  29
        '    cmd2.Parameters.Add("@mbill", SqlDbType.NVarChar)       '28  30
        '    cmd2.Parameters.Add("@EMail", SqlDbType.NVarChar)       '29  31
        '    cmd2.Parameters.Add("@CreditLimit", SqlDbType.Decimal)      '30  32
        '    cmd2.Parameters.Add("@partygstin", SqlDbType.NVarChar)      '31  33
        '    cmd2.Parameters.Add("@mdadd1", SqlDbType.NVarChar)      '32  34
        '    cmd2.Parameters.Add("@mdadd2", SqlDbType.NVarChar)      '33  35
        '    cmd2.Parameters.Add("@mdadd3", SqlDbType.NVarChar)      '34  36
        '    cmd2.Parameters.Add("@mdadd4", SqlDbType.NVarChar)      '35  37
        '    cmd2.Parameters.Add("@mdcity", SqlDbType.NVarChar)      '36  38
        '    cmd2.Parameters.Add("@mdpincode", SqlDbType.NVarChar)   '37  39
        '    cmd2.Parameters.Add("@mdstate", SqlDbType.NVarChar)     '38  40
        '    cmd2.Parameters.Add("@mdgstin", SqlDbType.NVarChar)     '39  41
        '    cmd2.Parameters.Add("@mddistance", SqlDbType.NVarChar)  '40  42
        '    cmd2.Parameters.Add("@mcity", SqlDbType.NVarChar)       '41  43
        '    cmd2.Parameters.Add("@PINCode", SqlDbType.NVarChar)     '42  44
        '    cmd2.Parameters.Add("@mdistance", SqlDbType.NVarChar)   '43  45
        '    cmd2.Parameters.Add("@statename", SqlDbType.NVarChar)   '44  46
        '    cmd2.Parameters.Add("@cmpcode", SqlDbType.SmallInt)   '  45  47
        '    cmd2.Parameters.Add("@partycode", SqlDbType.NVarChar) '  46  48 'Tharun
        'Else
        cmd2.Parameters.Add("@name", SqlDbType.NVarChar) '0
        cmd2.Parameters.Add("@ADDlname", SqlDbType.NVarChar) '1
        cmd2.Parameters.Add("@PRIMARYGROUP", SqlDbType.NVarChar)   '2
        cmd2.Parameters.Add("@ADDRESS1", SqlDbType.NVarChar)       '3
        cmd2.Parameters.Add("@ADDRESS2", SqlDbType.NVarChar)       '4
        cmd2.Parameters.Add("@ADDRESS3", SqlDbType.NVarChar)    '5
        cmd2.Parameters.Add("@ADDRESS4", SqlDbType.NVarChar)    '6
        cmd2.Parameters.Add("@ADDRESS5", SqlDbType.NVarChar)    '7
        cmd2.Parameters.Add("@PARENT", SqlDbType.NVarChar)  '8
        cmd2.Parameters.Add("@IncomeTaxNumber", SqlDbType.NVarChar)     '9
        cmd2.Parameters.Add("@VATTINNUMBER", SqlDbType.NVarChar)        '10
        cmd2.Parameters.Add("@Narration", SqlDbType.NVarChar)       '11  13
        cmd2.Parameters.Add("@PERFORMANCE", SqlDbType.Real)     '12  14
        cmd2.Parameters.Add("@mdisc", SqlDbType.Real)       '13  15
        cmd2.Parameters.Add("@mlorry", SqlDbType.NVarChar)      '14  16
        cmd2.Parameters.Add("@mbrand", SqlDbType.NVarChar)      '15  17
        cmd2.Parameters.Add("@mGRADE", SqlDbType.NVarChar)      '16  18
        cmd2.Parameters.Add("@mDEST", SqlDbType.NVarChar)       '17  19
        cmd2.Parameters.Add("@mDOCU", SqlDbType.NVarChar)       '18  20
        cmd2.Parameters.Add("@mKEYPER", SqlDbType.NVarChar)     '19  21
        cmd2.Parameters.Add("@mHOLIDAY", SqlDbType.NVarChar)    '20  22
        cmd2.Parameters.Add("@mDISTRICT", SqlDbType.NVarChar)   '21  23
        cmd2.Parameters.Add("@ledgercontact", SqlDbType.NVarChar)       '22  24
        cmd2.Parameters.Add("@ledgerphone", SqlDbType.NVarChar)     '23  25
        cmd2.Parameters.Add("@mPROP", SqlDbType.NVarChar)       '24  26
        cmd2.Parameters.Add("@mSTD", SqlDbType.NVarChar)        '25  27
        cmd2.Parameters.Add("@LedgerMobile", SqlDbType.NVarChar)    '26  28 
        cmd2.Parameters.Add("@ClosingBalance", SqlDbType.Decimal)   '27  29
        cmd2.Parameters.Add("@mbill", SqlDbType.NVarChar)       '28  30
        cmd2.Parameters.Add("@EMail", SqlDbType.NVarChar)       '29  31
        cmd2.Parameters.Add("@CreditLimit", SqlDbType.Decimal)      '30  32
        cmd2.Parameters.Add("@partygstin", SqlDbType.NVarChar)      '31  33
        cmd2.Parameters.Add("@mdadd1", SqlDbType.NVarChar)      '32  34
        cmd2.Parameters.Add("@mdadd2", SqlDbType.NVarChar)      '33  35
        cmd2.Parameters.Add("@mdadd3", SqlDbType.NVarChar)      '34  36
        cmd2.Parameters.Add("@mdadd4", SqlDbType.NVarChar)      '35  37
        cmd2.Parameters.Add("@mdcity", SqlDbType.NVarChar)      '36  38
        cmd2.Parameters.Add("@mdpincode", SqlDbType.NVarChar)   '37  39
        cmd2.Parameters.Add("@mdstate", SqlDbType.NVarChar)     '38  40
        cmd2.Parameters.Add("@mdgstin", SqlDbType.NVarChar)     '39  41
        cmd2.Parameters.Add("@mddistance", SqlDbType.NVarChar)  '40  42
        cmd2.Parameters.Add("@mcity", SqlDbType.NVarChar)       '41  43
        cmd2.Parameters.Add("@PINCode", SqlDbType.NVarChar)     '42  44
        cmd2.Parameters.Add("@mdistance", SqlDbType.NVarChar)   '43  45
        cmd2.Parameters.Add("@statename", SqlDbType.NVarChar)   '44  46
        cmd2.Parameters.Add("@cmpcode", SqlDbType.SmallInt)   '  45  47
        If Mid(Trim(Tgrpcmpnam), 1, 6) = "THARUN" Then
            cmd2.Parameters.Add("@partycode", SqlDbType.NVarChar) '  46  48 'Tharun
        End If


        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        cmd2.Connection = con

        cmd2.Transaction = mtrans

        Try
            cmdel.ExecuteNonQuery()

            For Each rw As DataRow In dtl.Rows
                'cmd2.Parameters(0).Value = rw(0).Replace(vbCr, "").Replace(vbLf, "") & vbNullString
                'merrname = Replace(rw(2), "'", "`") & vbNullString
                merrname = If(IsDBNull(rw(2)) = False, rw(2) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                'If merrname = "K.R.JANARTHANAM CHETTI,TIR" Then
                '    MsgBox(merrname)
                'End If
                'If merrname = "SUNDRY DEBTORS" Then
                '    MsgBox(merrname)
                'End If
                cmd2.Parameters(0).Value = Replace(rw(0), "'", "`") & vbNullString
                'cmd2.Parameters(1).Value = IIf(IsDBNull(rw(1)) = False, Replace(rw(1), "'", "`") & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd2.Parameters(1).Value = If(IsDBNull(rw(1)) = False, Replace(rw(1), "'", "`") & vbNullString, "")
                cmd2.Parameters(2).Value = If(IsDBNull(rw(2)) = False, rw(2) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd2.Parameters(3).Value = If(IsDBNull(rw(3)) = False, Replace(rw(3), "'", "`") & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd2.Parameters(4).Value = If(IsDBNull(rw(4)) = False, Replace(rw(4), "'", "`") & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd2.Parameters(5).Value = If(IsDBNull(rw(5)) = False, Replace(rw(5), "'", "`") & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd2.Parameters(6).Value = If(IsDBNull(rw(6)) = False, Replace(rw(6), "'", "`") & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd2.Parameters(7).Value = If(IsDBNull(rw(7)) = False, Replace(rw(7), "'", "`") & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd2.Parameters(8).Value = If(IsDBNull(rw(8)) = False, rw(8) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd2.Parameters(9).Value = If(IsDBNull(rw(9)) = False, rw(9) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                If IsDBNull(rw(10)) = False Then
                    If Len(Trim(rw(10))) > 0 Then
                        cmd2.Parameters(10).Value = Replace(rw(10), "'", "`") & vbNullString
                    Else
                        cmd2.Parameters(10).Value = If(IsDBNull(rw(12)) = False, Replace(rw(12), "'", "`") & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                    End If
                Else
                    cmd2.Parameters(10).Value = If(IsDBNull(rw(12)) = False, Replace(rw(12), "'", "`") & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                End If

                cmd2.Parameters(11).Value = Mid(IIf(IsDBNull(rw(13)) = False, rw(13) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 125)
                If IsDBNull(rw(14)) = False Then
                    cmd2.Parameters(12).Value = Val(rw(14))
                Else
                    cmd2.Parameters(12).Value = 0
                End If

                If IsDBNull(rw(15)) = False Then
                    cmd2.Parameters(13).Value = Val(rw(15))
                Else
                    cmd2.Parameters(13).Value = 0
                End If
                cmd2.Parameters(14).Value = IIf(IsDBNull(rw(16)) = False, rw(16) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd2.Parameters(15).Value = IIf(IsDBNull(rw(17)) = False, rw(17) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd2.Parameters(16).Value = IIf(IsDBNull(rw(18)) = False, rw(18) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd2.Parameters(17).Value = IIf(IsDBNull(rw(19)) = False, rw(19) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd2.Parameters(18).Value = IIf(IsDBNull(rw(20)) = False, rw(20) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd2.Parameters(19).Value = IIf(IsDBNull(rw(21)) = False, rw(21) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd2.Parameters(20).Value = Mid(IIf(IsDBNull(rw(22)) = False, rw(22) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 10)
                cmd2.Parameters(21).Value = IIf(IsDBNull(rw(23)) = False, rw(23) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd2.Parameters(22).Value = Mid(IIf(IsDBNull(rw(24)) = False, rw(24) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 30)
                cmd2.Parameters(23).Value = Mid(IIf(IsDBNull(rw(25)) = False, rw(25) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 30)
                cmd2.Parameters(24).Value = Mid(IIf(IsDBNull(rw(26)) = False, rw(26) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 30)
                cmd2.Parameters(25).Value = Mid(IIf(IsDBNull(rw(27)) = False, rw(27) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 15)
                cmd2.Parameters(26).Value = Mid(IIf(IsDBNull(rw(28)) = False, rw(28) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 30)

                If IsDBNull(rw(29)) = False Then
                    cmd2.Parameters(27).Value = Val(rw(29)) * -1
                Else
                    cmd2.Parameters(27).Value = 0
                End If


                'cmd2.Parameters(27).Value = Replace(rw(27), "'", "`") & vbNullString
                cmd2.Parameters(28).Value = Mid(IIf(IsDBNull(rw(30)) = False, rw(30) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 4)
                cmd2.Parameters(29).Value = Mid(IIf(IsDBNull(rw(31)) = False, rw(31) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 50)

                If IsDBNull(rw(32)) = False Then
                    cmd2.Parameters(30).Value = Val(rw(32)) * -1
                Else
                    cmd2.Parameters(30).Value = 0
                End If


                'If IsNull(RSLED.Fields("LEDGER.`$SALESTAXNUMBER`")) = False Then '10
                '    If Len(Trim(RSLED.Fields("LEDGER.`$SALESTAXNUMBER`"))) > 0 Then  '10
                '        rs!TNGST = RSLED.Fields("LEDGER.`$SALESTAXNUMBER`") & vbNullString '10
                '    Else
                '        rs!TNGST = RSLED.Fields("LEDGER.`$VATTINNUMBER`") & vbNullString '12
                '    End If
                'Else
                '    rs!TNGST = RSLED.Fields("LEDGER.`$VATTINNUMBER`") & vbNullString '12
                'End If

                If IsDBNull(rw(33)) = False Then
                    If Len(Trim(rw(33))) > 0 Then
                        cmd2.Parameters(31).Value = Mid(IIf(IsDBNull(rw(33)) = False, rw(33) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 30)
                    Else
                        cmd2.Parameters(31).Value = Mid(IIf(IsDBNull(rw(47)) = False, rw(47) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 30)
                    End If

                Else
                    cmd2.Parameters(31).Value = Mid(IIf(IsDBNull(rw(47)) = False, rw(47) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 30)
                End If
                'cmd2.Parameters(30).Value = Replace(rw(30), "'", "`") & vbNullString
                ' cmd2.Parameters(31).Value = Mid(IIf(IsDBNull(rw(33)) = False, rw(33) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 30)

                cmd2.Parameters(32).Value = Mid(IIf(IsDBNull(rw(34)) = False, rw(34) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 30)
                cmd2.Parameters(33).Value = Mid(IIf(IsDBNull(rw(35)) = False, rw(35) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 30)
                cmd2.Parameters(34).Value = Mid(IIf(IsDBNull(rw(36)) = False, rw(36) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 30)
                cmd2.Parameters(35).Value = Mid(IIf(IsDBNull(rw(37)) = False, rw(37) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 30)
                cmd2.Parameters(36).Value = Mid(IIf(IsDBNull(rw(38)) = False, rw(38) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 30)
                cmd2.Parameters(37).Value = Mid(IIf(IsDBNull(rw(39)) = False, rw(39) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 30)
                cmd2.Parameters(38).Value = Mid(IIf(IsDBNull(rw(40)) = False, rw(40) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 30)
                cmd2.Parameters(39).Value = Mid(IIf(IsDBNull(rw(41)) = False, rw(41) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 30)
                cmd2.Parameters(40).Value = If(IsDBNull(rw(42)) = False, Trim(rw(42)) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd2.Parameters(41).Value = If(IsDBNull(rw(43)) = False, Trim(rw(43)) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd2.Parameters(42).Value = If(IsDBNull(rw(44)) = False, Trim(rw(44)) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd2.Parameters(43).Value = If(IsDBNull(rw(45)) = False, Trim(rw(45)) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd2.Parameters(44).Value = If(IsDBNull(rw(46)) = False, Trim(rw(46)) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd2.Parameters(45).Value = mcompcode

                If Mid(Trim(Tgrpcmpnam), 1, 6) = "THARUN" Then
                    cmd2.Parameters(46).Value = If(IsDBNull(rw(48)) = False, Trim(rw(48)) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "") 'Tharun
                End If


                cmd2.ExecuteNonQuery()
                'PB.Value = np + 1
                PB.PerformStep()
            Next
            mtrans.Commit()
            MsgBox("Ledger Imported Successfully! - " & Now())
            PB.Value = 0
        Catch ex As Exception
            'mtrans.Rollback()
            MsgBox("Error:  " & merrname & " - " & vbNullString & ex.Message)
            mtrans.Rollback()
            np = 0
            PB.Value = 0
        End Try

        dtl.Dispose()



    End Sub
    Private Sub loadgrp()
        msql = "Select groups.`$name`,groups.`$parent`,groups.`$_primarygroup`,groups.`$Grpdistrict` FROM  GROUPS"


        'Tcollname'

        If contally.State = ConnectionState.Closed Then
            contally.Open()
        End If

        Dim cmd1 As New Odbc.OdbcCommand(msql, contally)
        Dim dtl1 As New DataTable
        cmd1.CommandTimeout = 600
        Dim da As New Odbc.OdbcDataAdapter(cmd1)
        'cmd.CommandTimeout = 600

        da.Fill(dtl1)
        np = 0
        PB.Maximum = dtl1.Rows.Count
        PB.Step = 1

        Dim cmdel1 As New Data.SqlClient.SqlCommand
        cmdel1.CommandText = "delete from grplist"
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        mtrans = con.BeginTransaction
        cmdel1.Transaction = mtrans
        cmdel1.Connection = con


        Dim mmst2 As String = "insert into grplist(name,parent,dum1,alopar,code)" _
              & " VALUES (@name,@parent,@PRIMARYGROUP,@grpdistrict,@mcode)"

        Dim cmd3 As New Data.SqlClient.SqlCommand
        cmd3.CommandTimeout = 600
        'cmd2.CommandText = "INSERT INTO TallyLedger(LedgerName,OpeningBalance,ClosingBalance,Parent,PrimaryGroup,Companyname,mname,add1,add2,add3,add4,add5,pincode,state,country,mail,phone,mobile,fax,contact,Itnumber,gstin,CreditLimit,Salestaxnumber,Tinno,Gp,GpName) " _
        '                  & " VALUES (@Ledgername,@opbal,@clbal,@Pgrp,@PrimaryGroup,@companyname,@mname,@add1,@add2,@add3,@add4,@add5,@Pincode,@State,@country,@mail,@phone,@mobile,@fax,@contact,@Itnumber,@GSTIN,@creditLimit,@salestaxnumber,@Tinno,@Gp,@Gpname )"
        cmd3.CommandText = mmst2


        cmd3.Parameters.Add("@name", SqlDbType.NVarChar) '0
        cmd3.Parameters.Add("@parent", SqlDbType.NVarChar) '1
        cmd3.Parameters.Add("@PRIMARYGROUP", SqlDbType.NVarChar)   '2
        cmd3.Parameters.Add("@grpdistrict", SqlDbType.NVarChar)       '3
        cmd3.Parameters.Add("@mcode", SqlDbType.SmallInt)

        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        cmd3.Connection = con

        cmd3.Transaction = mtrans

        Try
            cmdel1.ExecuteNonQuery()

            For Each rw As DataRow In dtl1.Rows
                'cmd2.Parameters(0).Value = rw(0).Replace(vbCr, "").Replace(vbLf, "") & vbNullString
                merrname = Replace(rw(0), "'", "`") & vbNullString
                cmd3.Parameters(0).Value = Replace(rw(0), "'", "`") & vbNullString
                'cmd2.Parameters(1).Value = IIf(IsDBNull(rw(1)) = False, Replace(rw(1), "'", "`") & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd3.Parameters(1).Value = If(IsDBNull(rw(1)) = False, Replace(rw(1), "'", "`") & vbNullString, "")
                cmd3.Parameters(2).Value = If(IsDBNull(rw(2)) = False, rw(2) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd3.Parameters(3).Value = If(IsDBNull(rw(3)) = False, Replace(rw(3), "'", "`") & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd3.Parameters(4).Value = mcompcode
                cmd3.ExecuteNonQuery()
                'PB.Value = np + 1
                PB.PerformStep()
            Next
            mtrans.Commit()
            MsgBox("Groups Imported Successfully! - " & Now())
            PB.Value = 0
        Catch ex As Exception
            'mtrans.Rollback()
            MsgBox("Error:  " & merrname & " - " & vbNullString & ex.Message)
            mtrans.Rollback()
            np = 0
            PB.Value = 0
        End Try

        dtl1.Dispose()



    End Sub
    Private Sub loadoutstanding()

        Dim mdebt As String
        'msql = "select TDLPendingBills.`$Party`,TDLPendingBills.`$Name`,TDLPendingBills.`$BillRefNo`,TDLPendingBills.`$BillDate`,TDLPendingBills.`$BillCreditPeriod`,TDLPendingBills.`$OpeningBalance`,TDLPendingBills.`$FinalBalance`,TDLPendingBills.`$DueDate`,TDLPendingBills.`$Curdate`,TDLPendingBills.`$Dueon`,TDLPendingBills.`$Group`,TDLPendingBills.`$Primarygrp` from TDLPendingBills where TDLPendingBills.`$Primarygrp`=" '& grp

        'omkar tally prime
        'msql = "select TDLPendingBills.`$BillDate`,TDLPendingBills.`$Party`,TDLPendingBills.`$Name`,TDLPendingBills.`$OpeningBalance`,TDLPendingBills.`$FinalBalance`,TDLPendingBills.`$DueDate`, TDLPendingBills.`$Dueon`,TDLPendingBills.`$BillRefNo`,TDLPendingBills.`$BillCreditPeriod`,TDLPendingBills.`$Curdate`,TDLPendingBills.`$Group`,TDLPendingBills.`$PrimaryGroup` from TDLPendingBills where TDLPendingBills.`$PrimaryGroup`=" & Trim(mgrp)
        'tharu tally erp9
        If Mid(Trim(Tgrpcmpnam), 1, 6) = "OOMKAR" Then
            msql = "select TDLPendingBills.`$BillDate`,TDLPendingBills.`$Party`,TDLPendingBills.`$Name`,TDLPendingBills.`$OpeningBalance`,TDLPendingBills.`$FinalBalance`,TDLPendingBills.`$DueDate`, TDLPendingBills.`$Dueon`,TDLPendingBills.`$BillRefNo`,TDLPendingBills.`$BillCreditPeriod`,TDLPendingBills.`$Curdate`,TDLPendingBills.`$Group`,TDLPendingBills.`$PrimaryGroup` from TDLPendingBills where TDLPendingBills.`$PrimaryGroup`=" & Trim(mgrp)
        ElseIf Mid(Trim(Tgrpcmpnam), 1, 6) = "THARUN" Then
            msql = "select TDLPendingBills.`$BillDate`,TDLPendingBills.`$Party`,TDLPendingBills.`$Name`,TDLPendingBills.`$OpeningBalance`,TDLPendingBills.`$FinalBalance`,TDLPendingBills.`$DueDate`, TDLPendingBills.`$Dueon`,TDLPendingBills.`$BillRefNo`,TDLPendingBills.`$BillCreditPeriod`,TDLPendingBills.`$Curdate`,TDLPendingBills.`$Group`,TDLPendingBills.`$PrimaryGroup` from TDLPendingBills where TDLPendingBills.`$PrimaryGrp`=" & Trim(mgrp)
        End If

        ' msql = "select TDLPendingBills.`$BillDate`,TDLPendingBills.`$Party`,TDLPendingBills.`$Name`,TDLPendingBills.`$OpeningBalance`,TDLPendingBills.`$FinalBalance`,TDLPendingBills.`$DueDate`, TDLPendingBills.`$Dueon`,TDLPendingBills.`$BillRefNo`,TDLPendingBills.`$BillCreditPeriod`,TDLPendingBills.`$Curdate`,TDLPendingBills.`$Group`,TDLPendingBills.`$PrimaryGroup` from TDLPendingBills"



        'msql = "Select groups.`$name`,groups.`$parent`,groups.`$_primarygroup`,groups.`$Grpdistrict` FROM  GROUPS"


        'Tcollname'

        If contally.State = ConnectionState.Closed Then
            contally.Open()
        End If

        Dim cmd1 As New Odbc.OdbcCommand(msql, contally)
        Dim dtl1 As New DataTable
        cmd1.CommandTimeout = 600
        Dim da As New Odbc.OdbcDataAdapter(cmd1)
        'cmd.CommandTimeout = 600

        da.Fill(dtl1)
        np = 0
        PB.Maximum = dtl1.Rows.Count
        PB.Step = 1


        Dim cmdel1 As New Data.SqlClient.SqlCommand
        If optdebt.Checked = True And chkop.Checked = False Then
            msql3 = "delete from grpp"
        ElseIf optdebt.Checked = True And chkop.Checked = True Then
            msql3 = "delete from grpo"
        ElseIf optcred.Checked = True Then
            msql3 = "delete from crpp"
        End If

        'cmdel1.CommandText = "delete from grplist"
        cmdel1.CommandText = msql3
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        mtrans = con.BeginTransaction
        cmdel1.Transaction = mtrans
        cmdel1.Connection = con


        Dim mmst2 As String

        If optdebt.Checked = True And chkop.Checked = False Then
            mdebt = "Sundry Debtors"
            mmst2 = "insert into grpp (date,name,bno,amt,amt2,duedate,dueday,code)" _
              & " VALUES (@date,@party,@bno,@amt,@amt2,@duedate,@dueday,@mcode)"
        ElseIf optdebt.Checked = True And chkop.Checked = True Then
            mdebt = "Sundry Debtors"
            mmst2 = "insert into grpo (date,name,bno,amt,amt2,duedate,dueday,code)" _
             & " VALUES (@date,@party,@bno,@amt,@amt2,@duedate,@dueday,@mcode)"
        ElseIf optcred.Checked = True Then
            mdebt = "Sundry Creditors"
            mmst2 = "insert into crpp (date,name,bno,amt,amt2,duedate,dueday,code)" _
             & " VALUES (@date,@party,@bno,@amt,@amt2,@duedate,@dueday,@mcode)"
        Else
            mdebt = "Sundry Debtors"
            mmst2 = "insert into grpp (date,name,bno,amt,amt2,duedate,dueday,code)" _
              & " VALUES (@date,@party,@bno,@amt,@amt2,@duedate,@dueday,@mcode)"
        End If

        'nn = DateDiff("d", DateValue(RSLED.Fields("TDLPendingBills.`$BillDate`")), DateValue(Now()))

        Dim cmd3 As New Data.SqlClient.SqlCommand
        cmd3.CommandTimeout = 600
        'cmd2.CommandText = "INSERT INTO TallyLedger(LedgerName,OpeningBalance,ClosingBalance,Parent,PrimaryGroup,Companyname,mname,add1,add2,add3,add4,add5,pincode,state,country,mail,phone,mobile,fax,contact,Itnumber,gstin,CreditLimit,Salestaxnumber,Tinno,Gp,GpName) " _
        '                  & " VALUES (@Ledgername,@opbal,@clbal,@Pgrp,@PrimaryGroup,@companyname,@mname,@add1,@add2,@add3,@add4,@add5,@Pincode,@State,@country,@mail,@phone,@mobile,@fax,@contact,@Itnumber,@GSTIN,@creditLimit,@salestaxnumber,@Tinno,@Gp,@Gpname )"
        cmd3.CommandText = mmst2

        cmd3.Parameters.Add("@date", SqlDbType.DateTime)
        cmd3.Parameters.Add("@party", SqlDbType.NVarChar) '0
        cmd3.Parameters.Add("@bno", SqlDbType.NVarChar) '1
        cmd3.Parameters.Add("@amt", SqlDbType.Decimal)   '2
        cmd3.Parameters.Add("@amt2", SqlDbType.Decimal)       '3
        cmd3.Parameters.Add("@duedate", SqlDbType.DateTime)       '3
        cmd3.Parameters.Add("@dueday", SqlDbType.SmallInt)       '3
        cmd3.Parameters.Add("@mcode", SqlDbType.SmallInt)

        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        cmd3.Connection = con

        cmd3.Transaction = mtrans

        Try
            cmdel1.ExecuteNonQuery()

            For Each rw As DataRow In dtl1.Rows
                'cmd2.Parameters(0).Value = rw(0).Replace(vbCr, "").Replace(vbLf, "") & vbNullString
                merrname = Replace(rw(1), "'", "`") & vbNullString & " - Bno :" & If(IsDBNull(rw(2)) = False, Replace(rw(2), "'", "`") & vbNullString, "")
                'If merrname = "CHEQUE COLLECTION" Then
                '        MsgBox(merrname)
                '    End If
                cmd3.Parameters(0).Value = rw(0)
                cmd3.Parameters(1).Value = Replace(rw(1), "'", "`") & vbNullString
                'cmd2.Parameters(1).Value = IIf(IsDBNull(rw(1)) = False, Replace(rw(1), "'", "`") & vbNullString, "").Replace(vbCr, "").Replace(vbLf, "")
                cmd3.Parameters(2).Value = If(IsDBNull(rw(2)) = False, Replace(rw(2), "'", "`") & vbNullString, "")

                If optdebt.Checked = True Then
                    cmd3.Parameters(3).Value = If(IsDBNull(rw(3)) = False, Val(rw(3)) & vbNullString, 0).Replace(vbCr, "").Replace(vbLf, "") * -1
                    cmd3.Parameters(4).Value = If(IsDBNull(rw(4)) = False, Val(rw(4)) & vbNullString, 0).Replace(vbCr, "").Replace(vbLf, "") * -1
                Else
                    cmd3.Parameters(3).Value = If(IsDBNull(rw(3)) = False, Val(rw(3)) & vbNullString, 0).Replace(vbCr, "").Replace(vbLf, "")
                    cmd3.Parameters(4).Value = If(IsDBNull(rw(4)) = False, Val(rw(4)) & vbNullString, 0).Replace(vbCr, "").Replace(vbLf, "")
                End If


                cmd3.Parameters(5).Value = rw(5)
                nn = DateDiff("d", DateValue(rw(0)), DateValue(Now()))
                cmd3.Parameters(6).Value = nn
                cmd3.Parameters(7).Value = mcompcode
                cmd3.ExecuteNonQuery()
                'PB.Value = np + 1
                PB.PerformStep()
            Next
            mtrans.Commit()
            MsgBox(Trim(mdebt) & " OutStanding Imported Successfully! - " & Now())
            PB.Value = 0
        Catch ex As Exception
            'mtrans.Rollback()
            MsgBox("Error:  " & merrname & " - " & vbNullString & ex.Message)
            mtrans.Rollback()
            np = 0
            PB.Value = 0
        End Try

        dtl1.Dispose()



    End Sub

    '    ALTER TABLE dbo.grpp ALTER COLUMN [bno] nvarchar(32) Not NULL 

    'ALTER TABLE dbo.crpp ALTER COLUMN [bno] nvarchar(32) Not NULL 

    'ALTER TABLE dbo.grpo ALTER COLUMN [bno] nvarchar(32) Not NULL 
    Private Sub exportinv()
        Cursor = Cursors.WaitCursor
        'Dim tallyDataString As String
        l = 0
        For k As Integer = 0 To Dg.Rows.Count - 1
            If Dg.Rows(k).Cells(0).Value = True Then
                l = l + 1
            End If
        Next
        PB.Maximum = l
        PB.Step = 1

        lno = 0
        For i As Int32 = 0 To Dg.Rows.Count - 1

            If Dg.Rows(i).Cells(0).Value = True Then
                minvno = Val(Dg.Rows(i).Cells(1).Value)
                mdate = Format(CDate(Dg.Rows(i).Cells(2).Value), "yyyy-MM-dd")
                mvchno = Trim(Dg.Rows(i).Cells(3).Value)

                mparty = Trim(Dg.Rows(i).Cells(4).Value)
                mamt = Val(Dg.Rows(i).Cells(5).Value)
                mdiscamt = Val(Dg.Rows(i).Cells(6).Value)
                mcourier = Val(Dg.Rows(i).Cells(7).Value)
                mforward = Val(Dg.Rows(i).Cells(8).Value)
                mroundoff = Val(Dg.Rows(i).Cells(9).Value)
                mtotamt = Val(Dg.Rows(i).Cells(10).Value)
                mnarr = "Inv.No :" & mvchno
                If InStr(mparty, "&") > 0 Then
                    mparty = Replace(mparty, "&", "&amp;")
                End If
                'mamt, mtotamt, mdiscamt, mcourier, mforward, mroundoff
                'tallyDataString = GenerateTallyDataString(minvno, mdate, mparty, mvchno, mamt, mdiscamt, mcourier, mforward, mroundoff, mtotamt, mnarr)
                If mdoctype = "SALES" Then
                    tallyDataString = genxml(minvno, mdate, mparty, mvchno, mamt, mdiscamt, mcourier, mforward, mroundoff, mtotamt, mnarr)
                ElseIf mdoctype = "STKTFR" Then
                    tallyDataString = genxmltfr(minvno, mdate, mparty, mvchno, mamt, mdiscamt, mcourier, mforward, mroundoff, mtotamt, mnarr)
                ElseIf mdoctype = "PURC" Then
                    tallyDataString = genxmlpurc(minvno, mdate, mparty, mvchno, mamt, mdiscamt, mcourier, mforward, mroundoff, mtotamt, mnarr)
                End If


                Call WRequest("http://localhost:" + mtallyport, "POST", tallyDataString)
                If mupdt = True Then
                    If mdoctype = "SALES" Then
                        msql5 = "UPDATE INV SET UPDT=1 WHERE INVNO=" & minvno
                    ElseIf mdoctype = "STKTFR" Then
                        msql5 = "UPDATE stktfrINV SET UPDT=1 WHERE INVNO=" & minvno
                    ElseIf mdoctype = "PURC" Then
                        msql5 = "UPDATE rcpt SET UPDT=1 WHERE rNO=" & minvno
                    End If
                    executeQuery(msql5)
                    lno = lno + 1
                    PB.PerformStep()
                    'Call sendtallydata(tallyDataString)
                End If
            End If

            'Dim tallyDataString As String = GenerateTallyDataString()

            ' Save XML data string to a file
            'Dim xmlFilePath As String = "d:\TallyData.xml"
            'System.IO.File.WriteAllText(xmlFilePath, tallyDataString
        Next
        If lno > 0 Then
            MsgBox("Exported " & lno & " Vouchers  Sucessfully!")
            PB.Value = 0
        End If
        Cursor = Cursors.Default
    End Sub

    'Private Sub sendtallydata(ByVal xmlstr As String)

    '    Dim srv As ServerXMLHTTP = New ServerXMLHTTP()
    '    srv.open("POST", "http://localhost:" + mtallyport, False, Nothing, Nothing)
    '    srv.send(xmlstr)
    '    Dim responsstr As String = srv.responseText







    '    'ServerHTTP.open "POST", "http://localhost:" + mtallynumber
    '    'ServerHTTP.send xmlstc
    '    ''MM = ServerHTTP.ReadyState

    '    'responsstr = ServerHTTP.responseText
    '    'newstring = InStrRev(responsstr, "<LINEERROR>")

    '    'If newstring = 0 Then
    '    '    MUPT = True
    '    '    'XMLDOM.loadXML (responsstr)
    '    '    'MsgBox "Response String " + responsstr
    '    '    'Set CHILDNODE = XMLDOM.selectNodes("ENVELOPE/BODY/DATA/IMPORTRESULT/LASTVCHID")
    '    '    'MsgBox "Voucher Created with MASTER ID " + CHILDNODE(0).text, , "Voucher Creation"
    '    '    ' MsgBox "ABC Save Successful", vbOKOnly, "Voucher : "
    '    'Else
    '    '    MUPT = False
    '    '    MsgBox "Failed to POST B.No." & VNO
    '    'End If

    '    'responsestr = ServerHTTP.responseText
    'End Sub

    Function GenerateTallyDataString(ByVal mkinvno As Int32, ByVal kdate As String, ByVal kparty As String, ByVal kvchno As String, ByVal kamt As Double, ByVal kdiscamt As Double, ByVal kcourier As Single, ByVal kforward As Single, ByVal kroundoff As Single, ByVal ktotamt As Double, ByVal knarr As String) As String
        ' Create XML structure for Tally Prime data
        Dim xml As New Xml.XmlDocument()


        xml.AppendChild(xml.CreateXmlDeclaration("1.0", Nothing, Nothing))
        ' Create root element
        Dim rootElement As XmlNode = xml.CreateElement("ENVELOPE")
        xml.AppendChild(rootElement)

        ' Create header element
        Dim headerElement As XmlNode = xml.CreateElement("HEADER")
        rootElement.AppendChild(headerElement)

        ' Add TALLYREQUEST element
        Dim tallyRequestElement As XmlNode = xml.CreateElement("TALLYREQUEST")
        tallyRequestElement.InnerText = "Import Data"
        headerElement.AppendChild(tallyRequestElement)

        ' Create body element
        Dim bodyElement As XmlNode = xml.CreateElement("BODY")
        rootElement.AppendChild(bodyElement)


        ' Add VOUCHER element
        Dim voucherElement As XmlNode = xml.CreateElement("VOUCHER")
        bodyElement.AppendChild(voucherElement)

        ' Add voucher details
        Dim dateElement As XmlNode = xml.CreateElement("DATE")
        dateElement.InnerText = kdate
        voucherElement.AppendChild(dateElement)

        Dim narrElement As XmlNode = xml.CreateElement("NARRATION")
        narrElement.InnerText = knarr
        voucherElement.AppendChild(narrElement)

        Dim voucherTypeNameElement As XmlNode = xml.CreateElement("VOUCHERTYPENAME")
        voucherTypeNameElement.InnerText = "Sales"
        voucherElement.AppendChild(voucherTypeNameElement)

        '' Add ledger details
        'Dim partyLedgerNameElement As XmlNode = xml.CreateElement("PARTYLEDGERNAME")
        'partyLedgerNameElement.InnerText = "CustomerName"
        'voucherElement.AppendChild(partyLedgerNameElement)


        Dim voucherNumberElement As XmlNode = xml.CreateElement("VOUCHERNUMBER")
        voucherNumberElement.InnerText = kvchno
        voucherElement.AppendChild(voucherNumberElement)

        Dim referenceNumberElement As XmlNode = xml.CreateElement("REFERENCE")
        referenceNumberElement.InnerText = kvchno
        voucherElement.AppendChild(referenceNumberElement)

        ' Add ledger details
        Dim partyLedgerNameElement As XmlNode = xml.CreateElement("PARTYLEDGERNAME")
        partyLedgerNameElement.InnerText = kparty
        voucherElement.AppendChild(partyLedgerNameElement)

        Dim Effectivedate As XmlNode = xml.CreateElement("EFFECTIVEDATE")
        Effectivedate.InnerText = kdate
        voucherElement.AppendChild(Effectivedate)



        Dim allLedgerEntriesListElement1 As XmlNode = xml.CreateElement("ALLLEDGERENTRIES.LIST")
        voucherElement.AppendChild(allLedgerEntriesListElement1)


        Dim ledgerNameElement1 As XmlNode = xml.CreateElement("LEDGERNAME")
        ledgerNameElement1.InnerText = kparty
        allLedgerEntriesListElement1.AppendChild(ledgerNameElement1)

        Dim isDeemedPositiveElement1 As XmlNode = xml.CreateElement("ISDEEMEDPOSITIVE")
        isDeemedPositiveElement1.InnerText = "Yes"
        allLedgerEntriesListElement1.AppendChild(isDeemedPositiveElement1)

        Dim amountElement1 As XmlNode = xml.CreateElement("AMOUNT")
        amountElement1.InnerText = Format(mtotamt * -1, "#########0.00") ' Including 18% GST on the base amount
        allLedgerEntriesListElement1.AppendChild(amountElement1)




        msql2 = "select  rtrim(ltrim(CONVERT(nchar(10),taxrate))) +'% '+ case when ISNULL(igst,0)>0 then 'IGST SALES' else 'GST SALES' end talyname,  SUM(amount) amt,taxrate from binv where invno=" & Val(mkinvno) & vbCrLf _
               & "group by taxrate, case when ISNULL(igst,0)>0 then 'IGST SALES' else 'GST SALES' end"

        Dim dtt As DataTable = getDataTable(msql2)

        If dtt.Rows.Count > 0 Then
            For Each rrw As DataRow In dtt.Rows

                Dim allLedgerEntriesListElement As XmlNode = xml.CreateElement("ALLLEDGERENTRIES.LIST")
                voucherElement.AppendChild(allLedgerEntriesListElement)

                Dim ledgerNameElement As XmlNode = xml.CreateElement("LEDGERNAME")
                ledgerNameElement.InnerText = rrw("talyname")
                allLedgerEntriesListElement.AppendChild(ledgerNameElement)

                Dim isDeemedPositiveElement As XmlNode = xml.CreateElement("ISDEEMEDPOSITIVE")
                isDeemedPositiveElement.InnerText = "No"
                allLedgerEntriesListElement.AppendChild(isDeemedPositiveElement)

                Dim amountElement As XmlNode = xml.CreateElement("AMOUNT")
                amountElement.InnerText = Format(rrw("amt"), "########0.00")  ' Including 18% GST on the base amount
                allLedgerEntriesListElement.AppendChild(amountElement)


            Next
        End If
        dtt.Dispose()


        msql4 = " select kk.invno,kk.mtaxrate,kk.talyname, kk.taxrate,kk.taxcode,sum(kk.taxamt) taxamt from " & vbCrLf _
              & "(select k.invno,k.mtaxrate,'OUTPUT TAX '+RTRIM(ltrim(upper(k.taxname)))+' '+rtrim(ltrim(convert(nchar(10),k.staxrate)))+'%' as talyname, k.staxrate taxrate,upper(k.taxname) taxcode,sum(k.taxamt) taxamt from " & vbCrLf _
              & "(select invno,taxrate mtaxrate, staxrate,taxname,taxamt from " & vbCrLf _
              & "(select invno,taxrate,staxrate,amount,cgst,sgst,igst from binv) s " & vbCrLf _
              & " unpivot " & vbCrLf _
              & "( taxamt for taxname in (cgst,sgst,igst)) n " & vbCrLf _
              & "where invno=" & Val(mkinvno) & ") k " & vbCrLf _
              & "group by k.invno,k.staxrate,k.taxname,k.mtaxrate Having Sum(k.taxamt) > 0 " & vbCrLf _
              & "Union All " & vbCrLf _
              & "select l.invno,l.frtaxper mtaxrate,l.talyname,l.taxrate,l.taxcode,l.taxamt from " & vbCrLf _
              & "(select invno,frtaxper,'OUTPUT TAX IGST '+ case when ISNULL(igst,0)>0 then rtrim(ltrim(convert(nchar(10),frtaxper))) else '0' end+'%' talyname, " & vbCrLf _
              & " case when ISNULL(igst,0)>0 then frtaxper else 0 end taxrate, 'IGST' taxcode,case when ISNULL(igst,0)>0 then frtaxamt else 0 end taxamt from inv where invno=" & Val(mkinvno) & vbCrLf _
              & "Union All " & vbCrLf _
              & "select invno,frtaxper,'OUTPUT TAX CGST '+ case when ISNULL(cgst,0)>0 then rtrim(ltrim(convert(nchar(10),round(frtaxper/2,2)))) else '0' end+'%' talyname, " & vbCrLf _
              & "case when ISNULL(cgst,0)>0 then round(frtaxper/2,2) else 0 end taxrate, 'CGST' taxcode,case when ISNULL(cgst,0)>0 then round(frtaxamt/2,2) else 0 end taxamt from inv where invno=" & Val(mkinvno) & vbCrLf _
              & "Union All " & vbCrLf _
              & "select invno,frtaxper,'OUTPUT TAX SGST '+ case when ISNULL(sgst,0)>0 then rtrim(ltrim(convert(nchar(10),round(frtaxper/2,2)))) else '0' end +'%' talyname, " & vbCrLf _
              & " case when ISNULL(sgst,0)>0 then round(frtaxper/2,2) else 0 end taxrate, 'SGST' taxcode,case when ISNULL(sgst,0)>0 then round(frtaxamt/2,2) else 0 end taxamt from inv where invno=" & Val(mkinvno) & ") l " & vbCrLf _
              & " where l.taxamt>0) kk group by kk.invno,kk.mtaxrate,kk.talyname, kk.taxrate,kk.taxcode    order by kk.taxrate "

        Dim dt1 As DataTable = getDataTable(msql4)
        If dt1.Rows.Count > 0 Then
            For Each rw1 As DataRow In dt1.Rows
                Dim allLedgerEntriesListElement As XmlNode = xml.CreateElement("ALLLEDGERENTRIES.LIST")
                voucherElement.AppendChild(allLedgerEntriesListElement)

                Dim ledgerNameElementtax As XmlNode = xml.CreateElement("LEDGERNAME")
                ledgerNameElementtax.InnerText = rw1("talyname")
                allLedgerEntriesListElement.AppendChild(ledgerNameElementtax)

                Dim isDeemedPositiveElement As XmlNode = xml.CreateElement("ISDEEMEDPOSITIVE")
                isDeemedPositiveElement.InnerText = "No"
                allLedgerEntriesListElement.AppendChild(isDeemedPositiveElement)

                Dim amountElement As XmlNode = xml.CreateElement("AMOUNT")
                amountElement.InnerText = rw1("taxamt") ' Including 18% GST on the base amount
                allLedgerEntriesListElement.AppendChild(amountElement)
            Next
        End If
        dt1.Dispose()

        ' Add GST tax details
        'Dim allLedgerEntriesListElement As XmlNode = xml.CreateElement("ALLLEDGERENTRIES.LIST")
        'voucherElement.AppendChild(allLedgerEntriesListElement)

        'Dim ledgerNameElement As XmlNode = xml.CreateElement("LEDGERNAME")
        'ledgerNameElement.InnerText = "Sales - GST"
        'allLedgerEntriesListElement.AppendChild(ledgerNameElement)

        'Dim isDeemedPositiveElement As XmlNode = xml.CreateElement("ISDEEMEDPOSITIVE")
        'isDeemedPositiveElement.InnerText = "Yes"
        'allLedgerEntriesListElement.AppendChild(isDeemedPositiveElement)

        'Dim amountElement As XmlNode = xml.CreateElement("AMOUNT")
        'amountElement.InnerText = "1180.00" ' Including 18% GST on the base amount
        'allLedgerEntriesListElement.AppendChild(amountElement)

        If kcourier > 0 Then
            Dim ledgercourAllocationListElement As XmlNode = xml.CreateElement("LEDGERALLOCATIONS.LIST")
            voucherElement.AppendChild(ledgercourAllocationListElement)

            Dim ledgerNameElementcour As XmlNode = xml.CreateElement("LEDGERNAME")
            ledgerNameElementcour.InnerText = "COURIER CHARGES"
            ledgercourAllocationListElement.AppendChild(ledgerNameElementcour)

            Dim isDeemedPositiveElement As XmlNode = xml.CreateElement("ISDEEMEDPOSITIVE")
            isDeemedPositiveElement.InnerText = "No"
            ledgercourAllocationListElement.AppendChild(isDeemedPositiveElement)


            Dim amountElementcour As XmlNode = xml.CreateElement("AMOUNT")
            amountElementcour.InnerText = Format(kcourier, "#####0.00") ' Adjust the round-off amount as needed
            ledgercourAllocationListElement.AppendChild(amountElementcour)
        End If


        If kforward > 0 Then
            Dim ledgerforwAllocationListElement As XmlNode = xml.CreateElement("LEDGERALLOCATIONS.LIST")
            voucherElement.AppendChild(ledgerforwAllocationListElement)

            Dim ledgerNameElementforward As XmlNode = xml.CreateElement("LEDGERNAME")
            ledgerNameElementforward.InnerText = "FORWARDING CHARGES"
            ledgerforwAllocationListElement.AppendChild(ledgerNameElementforward)

            Dim isDeemedPositiveElement As XmlNode = xml.CreateElement("ISDEEMEDPOSITIVE")
            isDeemedPositiveElement.InnerText = "No"
            ledgerforwAllocationListElement.AppendChild(isDeemedPositiveElement)

            Dim amountElementcour As XmlNode = xml.CreateElement("AMOUNT")
            amountElementcour.InnerText = Format(kforward, "#####0.00") ' Adjust the round-off amount as needed
            ledgerforwAllocationListElement.AppendChild(amountElementcour)
        End If



        If kroundoff <> 0 Then
            Dim ledgerAllocationListElement As XmlNode = xml.CreateElement("LEDGERALLOCATIONS.LIST")
            voucherElement.AppendChild(ledgerAllocationListElement)
            Dim ledgerNameElementRoundOff As XmlNode = xml.CreateElement("LEDGERNAME")
            ledgerNameElementRoundOff.InnerText = "CHARITY"
            ledgerAllocationListElement.AppendChild(ledgerNameElementRoundOff)

            If kroundoff > 0 Then
                Dim isDeemedPositiveElement As XmlNode = xml.CreateElement("ISDEEMEDPOSITIVE")
                isDeemedPositiveElement.InnerText = "No"
                ledgerAllocationListElement.AppendChild(isDeemedPositiveElement)

                Dim amountElementRoundOff As XmlNode = xml.CreateElement("AMOUNT")
                amountElementRoundOff.InnerText = Format(kroundoff, "###0.00") ' Adjust the round-off amount as needed
                ledgerAllocationListElement.AppendChild(amountElementRoundOff)
            Else
                Dim isDeemedPositiveElement As XmlNode = xml.CreateElement("ISDEEMEDPOSITIVE")
                isDeemedPositiveElement.InnerText = "Yes"
                ledgerAllocationListElement.AppendChild(isDeemedPositiveElement)

                Dim amountElementRoundOff As XmlNode = xml.CreateElement("AMOUNT")
                amountElementRoundOff.InnerText = Format(kroundoff, "###0.00") ' Adjust the round-off amount as needed
                ledgerAllocationListElement.AppendChild(amountElementRoundOff)
            End If
        End If


        ' Add round-off details
        'Dim ledgerAllocationListElement As XmlNode = xml.CreateElement("LEDGERALLOCATIONS.LIST")
        'voucherElement.AppendChild(ledgerAllocationListElement)

        'Dim ledgerNameElementRoundOff As XmlNode = xml.CreateElement("LEDGERNAME")
        'ledgerNameElementRoundOff.InnerText = "Round Off"
        'ledgerAllocationListElement.AppendChild(ledgerNameElementRoundOff)

        'Dim amountElementRoundOff As XmlNode = xml.CreateElement("AMOUNT")
        'amountElementRoundOff.InnerText = "-0.80" ' Adjust the round-off amount as needed
        'ledgerAllocationListElement.AppendChild(amountElementRoundOff)

        ' Add more voucher details, taxes, and round-off as needed

        ' Save XML as string
        Dim stringWriter As New StringWriter()
        Dim xmlWriter As New XmlTextWriter(stringWriter)
        xml.WriteTo(xmlWriter)

        Return stringWriter.ToString()
    End Function



    Function WRequest(ByVal URL As String, ByVal method As String, ByVal POSTdata As String) As String

        'URL="http://localhost:" + portnumber
        'method = "POST"
        'POSTdata = xmlstring

        Dim responseData As String = ""

        Try
            Dim hwrequest As Net.HttpWebRequest = Net.WebRequest.Create(URL)
            hwrequest.Accept = "*/*"
            hwrequest.AllowAutoRedirect = True
            hwrequest.UserAgent = "http_requester/0.1"
            hwrequest.Timeout = 60000
            hwrequest.Method = method

            If hwrequest.Method = "POST" Then
                hwrequest.ContentType = "application/x-www-form-urlencoded"

                Dim encoding As New Text.ASCIIEncoding() 'Use UTF8Encoding for XML requests
                Dim postByteArray() As Byte = encoding.GetBytes(POSTdata)
                hwrequest.ContentLength = postByteArray.Length

                Dim postStream As IO.Stream = hwrequest.GetRequestStream()
                postStream.Write(postByteArray, 0, postByteArray.Length)
                postStream.Close()

            End If

            Dim hwresponse As Net.HttpWebResponse = hwrequest.GetResponse()
            If hwresponse.StatusCode = Net.HttpStatusCode.OK Then
                Dim responseStream As IO.StreamReader = New IO.StreamReader(hwresponse.GetResponseStream())
                responseData = responseStream.ReadToEnd()
                mupdt = True
            End If

            hwresponse.Close()
        Catch e As Exception
            responseData = "An error occurred: " & e.Message
        End Try
        Return responseData

    End Function
    Private Function genxml(ByVal mkinvno As Int32, ByVal kdate As String, ByVal kparty As String, ByVal kvchno As String, ByVal kamt As Double, ByVal kdiscamt As Double, ByVal kcourier As Single, ByVal kforward As Single, ByVal kroundoff As Single, ByVal ktotamt As Double, ByVal knarr As String) As String
        'Dim DATE2 As String = Format(CDate("01-04-2023"), "dd/MM/yyyy")
        Dim DATE2 As String = Format(CDate(kdate), "dd/MM/yyyy")
        ' DATE2 = Format(vodat, "yyyymmdd")

        xmlstc = "<ENVELOPE>" + vbCrLf &
        "<HEADER>" + vbCrLf &
        "<VERSION>1</VERSION>" + vbCrLf &
        "<TALLYREQUEST>Import</TALLYREQUEST>" + vbCrLf &
        "<TYPE>Data</TYPE>" + vbCrLf &
        "<ID>Vouchers</ID>" + vbCrLf &
        "</HEADER>" + vbCrLf &
        "<BODY>" + vbCrLf &
        "<DESC>" + vbCrLf &
        "</DESC>" + vbCrLf &
        "<DATA>" + vbCrLf &
        "<TALLYMESSAGE >" + vbCrLf &
        "<VOUCHER VCHTYPE=""Sales"" ACTION=""Create"">" + vbCrLf &
        "<DATE>" + DATE2 + "</DATE>" + vbCrLf &
        "<NARRATION>" + knarr + "</NARRATION>" + vbCrLf &
        "<VOUCHERTYPENAME>Sales</VOUCHERTYPENAME>" + vbCrLf &
        "<VOUCHERNUMBER>" + kvchno + "</VOUCHERNUMBER>" + vbCrLf &
        "<REFERENCE>" + Trim(kvchno) + "</REFERENCE>" + vbCrLf &
        "<PARTYLEDGERNAME>" + Trim(kparty) + "</PARTYLEDGERNAME>" + vbCrLf &
        "<EFFECTIVEDATE>" + DATE2 + "</EFFECTIVEDATE>" + vbCrLf &
        "<ALLLEDGERENTRIES.LIST>" + vbCrLf &
        "<LEDGERNAME>" + Trim(kparty) + "</LEDGERNAME>" + vbCrLf &
        "<GSTCLASS />" + vbCrLf &
        "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>" + vbCrLf &
        "<LEDGERFROMITEM>No</LEDGERFROMITEM>" + vbCrLf
        xmlstc = xmlstc + "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>" + vbCrLf &
         "<ISPARTYLEDGER>Yes</ISPARTYLEDGER>" + vbCrLf &
         "<AMOUNT>" + LTrim(ktotamt * -1) + "</AMOUNT>" + vbCrLf &
          "<BILLALLOCATIONS.LIST>" + vbCrLf &
            "<NAME>" + Trim(kvchno) + "</NAME>" + vbCrLf &
            "<BILLTYPE>New Ref</BILLTYPE>" + vbCrLf &
            "<AMOUNT>" + LTrim(ktotamt * -1) + "</AMOUNT>" + vbCrLf &
            "</BILLALLOCATIONS.LIST>" + vbCrLf &
            "</ALLLEDGERENTRIES.LIST>" + vbCrLf




        msql2 = "select  rtrim(ltrim(CONVERT(nchar(10),taxrate))) +'% '+ case when ISNULL(igst,0)>0 then 'IGST SALES' else 'GST SALES' end talyname,  SUM(amount) amt,taxrate from binv where invno=" & Val(mkinvno) & vbCrLf _
               & "group by taxrate, case when ISNULL(igst,0)>0 then 'IGST SALES' else 'GST SALES' end"

        Dim dtt As DataTable = getDataTable(msql2)

        If dtt.Rows.Count > 0 Then
            For Each rrw As DataRow In dtt.Rows
                xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf &
                   "<LEDGERNAME>" + Trim(rrw("talyname")) + "</LEDGERNAME>" + vbCrLf &
                   "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf &
                   "<AMOUNT>" + LTrim(rrw("amt")) + "</AMOUNT>" + vbCrLf &
                   "</ALLLEDGERENTRIES.LIST>" + vbCrLf
            Next
        End If






        msql4 = " select kk.invno,kk.mtaxrate,kk.talyname, kk.taxrate,kk.taxcode,sum(kk.taxamt) taxamt from " & vbCrLf _
              & "(select k.invno,k.mtaxrate,'OUTPUT TAX '+RTRIM(ltrim(upper(k.taxname)))+' '+rtrim(ltrim(convert(nchar(10),k.staxrate)))+'%' as talyname, k.staxrate taxrate,upper(k.taxname) taxcode,sum(k.taxamt) taxamt from " & vbCrLf _
              & "(select invno,taxrate mtaxrate, staxrate,taxname,taxamt from " & vbCrLf _
              & "(select invno,taxrate,staxrate,amount,cgst,sgst,igst from binv) s " & vbCrLf _
              & " unpivot " & vbCrLf _
              & "( taxamt for taxname in (cgst,sgst,igst)) n " & vbCrLf _
              & "where invno=" & Val(mkinvno) & ") k " & vbCrLf _
              & "group by k.invno,k.staxrate,k.taxname,k.mtaxrate Having Sum(k.taxamt) > 0 " & vbCrLf _
              & "Union All " & vbCrLf _
              & "select l.invno,l.frtaxper mtaxrate,l.talyname,l.taxrate,l.taxcode,l.taxamt from " & vbCrLf _
              & "(select invno,frtaxper,'OUTPUT TAX IGST '+ case when ISNULL(igst,0)>0 then rtrim(ltrim(convert(nchar(10),frtaxper))) else '0' end+'%' talyname, " & vbCrLf _
              & " case when ISNULL(igst,0)>0 then frtaxper else 0 end taxrate, 'IGST' taxcode,case when ISNULL(igst,0)>0 then frtaxamt else 0 end taxamt from inv where invno=" & Val(mkinvno) & vbCrLf _
              & "Union All " & vbCrLf _
              & "select invno,frtaxper,'OUTPUT TAX CGST '+ case when ISNULL(cgst,0)>0 then rtrim(ltrim(convert(nchar(10),round(frtaxper/2,2)))) else '0' end+'%' talyname, " & vbCrLf _
              & "case when ISNULL(cgst,0)>0 then round(frtaxper/2,2) else 0 end taxrate, 'CGST' taxcode,case when ISNULL(cgst,0)>0 then round(frtaxamt/2,2) else 0 end taxamt from inv where invno=" & Val(mkinvno) & vbCrLf _
              & "Union All " & vbCrLf _
              & "select invno,frtaxper,'OUTPUT TAX SGST '+ case when ISNULL(sgst,0)>0 then rtrim(ltrim(convert(nchar(10),round(frtaxper/2,2)))) else '0' end +'%' talyname, " & vbCrLf _
              & " case when ISNULL(sgst,0)>0 then round(frtaxper/2,2) else 0 end taxrate, 'SGST' taxcode,case when ISNULL(sgst,0)>0 then round(frtaxamt/2,2) else 0 end taxamt from inv where invno=" & Val(mkinvno) & ") l " & vbCrLf _
              & " where l.taxamt>0) kk group by kk.invno,kk.mtaxrate,kk.talyname, kk.taxrate,kk.taxcode    order by kk.taxrate "

        Dim dt1 As DataTable = getDataTable(msql4)
        If dt1.Rows.Count > 0 Then
            For Each rw1 As DataRow In dt1.Rows
                xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf &
               "<LEDGERNAME>" + Trim(rw1("talyname")) + "</LEDGERNAME>" + vbCrLf &
               "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf &
               "<AMOUNT>" + LTrim(rw1("TAXAMT")) + "</AMOUNT>" + vbCrLf &
               "</ALLLEDGERENTRIES.LIST>" + vbCrLf
            Next
        End If




        '            If Len(Trim(Vatname)) > 0 Then
        '             xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf & _
        '             "<LEDGERNAME>" + MCREDIT + "</LEDGERNAME>" + vbCrLf & _
        '             "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf & _
        '             "<AMOUNT>" + LTrim(MKTAMT) + "</AMOUNT>" + vbCrLf & _
        '             "</ALLLEDGERENTRIES.LIST>" + vbCrLf
        '             If VTAXAMT > 0 Then
        '              xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf & _
        '              "<LEDGERNAME>" + Vatname + "</LEDGERNAME>" + vbCrLf & _
        '              "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf & _
        '              "<AMOUNT>" + LTrim(VTAXAMT) + "</AMOUNT>" + vbCrLf & _
        '              "</ALLLEDGERENTRIES.LIST>" + vbCrLf
        '             End If
        '             If VSTAXAMT > 0 Then
        '              xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf & _
        '              "<LEDGERNAME>" + "SURCHARGES" + "</LEDGERNAME>" + vbCrLf & _
        '              "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf & _
        '              "<AMOUNT>" + LTrim(VSTAXAMT) + "</AMOUNT>" + vbCrLf & _
        '              "</ALLLEDGERENTRIES.LIST>" + vbCrLf
        '             End If
        '
        If kcourier > 0 Then
            xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf &
            "<LEDGERNAME>" + "COURIER CHARGES" + "</LEDGERNAME>" + vbCrLf &
            "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf &
            "<AMOUNT>" + LTrim(kcourier) + "</AMOUNT>" + vbCrLf &
            "</ALLLEDGERENTRIES.LIST>" + vbCrLf
        End If
        If kforward > 0 Then
            xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf &
              "<LEDGERNAME>" + "FORWARDING CHARGES" + "</LEDGERNAME>" + vbCrLf &
              "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf &
              "<AMOUNT>" + LTrim(kforward) + "</AMOUNT>" + vbCrLf &
              "</ALLLEDGERENTRIES.LIST>" + vbCrLf
        End If
        '"ROUNDED OFF" tharun
        '"CHARITY" omkar
        If Mid(Trim(Tgrpcmpnam), 1, 6) = "THARUN" Then
            mroundled = "ROUNDED OFF"
        Else
            mroundled = "CHARITY"
        End If

        If kroundoff <> 0 Then
            xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf &
              "<LEDGERNAME>" + Trim(mroundled) + "</LEDGERNAME>" + vbCrLf
            If kroundoff > 0 Then
                xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf &
               "<AMOUNT>" + LTrim(kroundoff) + "</AMOUNT>" + vbCrLf
            Else
                xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>" + vbCrLf &
               "<AMOUNT>" + LTrim(kroundoff) + "</AMOUNT>" + vbCrLf
            End If
            xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>" + vbCrLf
        End If




        xmlstc = xmlstc + "</VOUCHER>" + vbCrLf &
        "</TALLYMESSAGE>" + vbCrLf &
        "</DATA>" + vbCrLf &
        "</BODY>" + vbCrLf & "</ENVELOPE>"

        Dim xmlFilePath As String = "d:\TallyData.xml"
        System.IO.File.WriteAllText(xmlFilePath, xmlstc)


        Return xmlstc

    End Function

    Private Function genxmltfr(ByVal mkinvno As Int32, ByVal kdate As String, ByVal kparty As String, ByVal kvchno As String, ByVal kamt As Double, ByVal kdiscamt As Double, ByVal kcourier As Single, ByVal kforward As Single, ByVal kroundoff As Single, ByVal ktotamt As Double, ByVal knarr As String) As String
        'Dim DATE2 As String = Format(CDate("01-04-2023"), "dd/MM/yyyy")
        Dim DATE2 As String = Format(CDate(kdate), "dd/MM/yyyy")
        ' DATE2 = Format(vodat, "yyyymmdd")

        xmlstc = "<ENVELOPE>" + vbCrLf &
        "<HEADER>" + vbCrLf &
        "<VERSION>1</VERSION>" + vbCrLf &
        "<TALLYREQUEST>Import</TALLYREQUEST>" + vbCrLf &
        "<TYPE>Data</TYPE>" + vbCrLf &
        "<ID>Vouchers</ID>" + vbCrLf &
        "</HEADER>" + vbCrLf &
        "<BODY>" + vbCrLf &
        "<DESC>" + vbCrLf &
        "</DESC>" + vbCrLf &
        "<DATA>" + vbCrLf &
        "<TALLYMESSAGE >" + vbCrLf &
        "<VOUCHER VCHTYPE=""Sales"" ACTION=""Create"">" + vbCrLf &
        "<DATE>" + DATE2 + "</DATE>" + vbCrLf &
        "<NARRATION>" + knarr + "</NARRATION>" + vbCrLf &
        "<VOUCHERTYPENAME>Sales</VOUCHERTYPENAME>" + vbCrLf &
        "<VOUCHERNUMBER>" + kvchno + "</VOUCHERNUMBER>" + vbCrLf &
        "<REFERENCE>" + Trim(kvchno) + "</REFERENCE>" + vbCrLf &
        "<PARTYLEDGERNAME>" + Trim(kparty) + "</PARTYLEDGERNAME>" + vbCrLf &
        "<EFFECTIVEDATE>" + DATE2 + "</EFFECTIVEDATE>" + vbCrLf &
        "<ALLLEDGERENTRIES.LIST>" + vbCrLf &
        "<LEDGERNAME>" + Trim(kparty) + "</LEDGERNAME>" + vbCrLf &
        "<GSTCLASS />" + vbCrLf &
        "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>" + vbCrLf &
        "<LEDGERFROMITEM>No</LEDGERFROMITEM>" + vbCrLf
        xmlstc = xmlstc + "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>" + vbCrLf &
         "<ISPARTYLEDGER>Yes</ISPARTYLEDGER>" + vbCrLf &
         "<AMOUNT>" + LTrim(ktotamt * -1) + "</AMOUNT>" + vbCrLf &
          "<BILLALLOCATIONS.LIST>" + vbCrLf &
            "<NAME>" + Trim(kvchno) + "</NAME>" + vbCrLf &
            "<BILLTYPE>New Ref</BILLTYPE>" + vbCrLf &
            "<AMOUNT>" + LTrim(ktotamt * -1) + "</AMOUNT>" + vbCrLf &
            "</BILLALLOCATIONS.LIST>" + vbCrLf &
            "</ALLLEDGERENTRIES.LIST>" + vbCrLf




        'msql2 = "select  rtrim(ltrim(CONVERT(nchar(10),taxrate))) +'% '+ case when ISNULL(igst,0)>0 then 'IGST SALES' else 'GST SALES' end talyname,  SUM(amount) amt,taxrate from binv where invno=" & Val(mkinvno) & vbCrLf _
        '       & "group by taxrate, case when ISNULL(igst,0)>0 then 'IGST SALES' else 'GST SALES' end"
        msql2 = "select  'STOCK TRANSFER OUTWARD' talyname,  SUM(amount) amt,taxrate from bstktfrinv where invno=" & mkinvno & " group by taxrate "

        Dim dtt As DataTable = getDataTable(msql2)

        If dtt.Rows.Count > 0 Then
            For Each rrw As DataRow In dtt.Rows
                xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf &
                   "<LEDGERNAME>" + Trim(rrw("talyname")) + "</LEDGERNAME>" + vbCrLf &
                   "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf &
                   "<AMOUNT>" + LTrim(rrw("amt")) + "</AMOUNT>" + vbCrLf &
                   "</ALLLEDGERENTRIES.LIST>" + vbCrLf
            Next
        End If






        msql4 = " select kk.invno,kk.mtaxrate,kk.talyname, kk.taxrate,kk.taxcode,sum(kk.taxamt) taxamt from " & vbCrLf _
              & "(select k.invno,k.mtaxrate,'OUTPUT TAX '+RTRIM(ltrim(upper(k.taxname)))+' '+rtrim(ltrim(convert(nchar(10),k.staxrate)))+'%' as talyname, k.staxrate taxrate,upper(k.taxname) taxcode,sum(k.taxamt) taxamt from " & vbCrLf _
              & "(select invno,taxrate mtaxrate, staxrate,taxname,taxamt from " & vbCrLf _
              & "(select invno,taxrate,staxrate,amount,cgst,sgst,igst from bstktfrinv) s " & vbCrLf _
              & " unpivot " & vbCrLf _
              & "( taxamt for taxname in (cgst,sgst,igst)) n " & vbCrLf _
              & "where invno=" & Val(mkinvno) & ") k " & vbCrLf _
              & "group by k.invno,k.staxrate,k.taxname,k.mtaxrate Having Sum(k.taxamt) > 0 " & vbCrLf _
              & "Union All " & vbCrLf _
              & "select l.invno,l.frtaxper mtaxrate,l.talyname,l.taxrate,l.taxcode,l.taxamt from " & vbCrLf _
              & "(select invno,frtaxper,'OUTPUT TAX IGST '+ case when ISNULL(igst,0)>0 then rtrim(ltrim(convert(nchar(10),frtaxper))) else '0' end+'%' talyname, " & vbCrLf _
              & " case when ISNULL(igst,0)>0 then frtaxper else 0 end taxrate, 'IGST' taxcode,case when ISNULL(igst,0)>0 then frtaxamt else 0 end taxamt from stktfrinv where invno=" & Val(mkinvno) & vbCrLf _
              & "Union All " & vbCrLf _
              & "select invno,frtaxper,'OUTPUT TAX CGST '+ case when ISNULL(cgst,0)>0 then rtrim(ltrim(convert(nchar(10),round(frtaxper/2,2)))) else '0' end+'%' talyname, " & vbCrLf _
              & "case when ISNULL(cgst,0)>0 then round(frtaxper/2,2) else 0 end taxrate, 'CGST' taxcode,case when ISNULL(cgst,0)>0 then round(frtaxamt/2,2) else 0 end taxamt from stktfrinv where invno=" & Val(mkinvno) & vbCrLf _
              & "Union All " & vbCrLf _
              & "select invno,frtaxper,'OUTPUT TAX SGST '+ case when ISNULL(sgst,0)>0 then rtrim(ltrim(convert(nchar(10),round(frtaxper/2,2)))) else '0' end +'%' talyname, " & vbCrLf _
              & " case when ISNULL(sgst,0)>0 then round(frtaxper/2,2) else 0 end taxrate, 'SGST' taxcode,case when ISNULL(sgst,0)>0 then round(frtaxamt/2,2) else 0 end taxamt from stktfrinv where invno=" & Val(mkinvno) & ") l " & vbCrLf _
              & " where l.taxamt>0) kk group by kk.invno,kk.mtaxrate,kk.talyname, kk.taxrate,kk.taxcode    order by kk.taxrate "

        Dim dt1 As DataTable = getDataTable(msql4)
        If dt1.Rows.Count > 0 Then
            For Each rw1 As DataRow In dt1.Rows
                xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf &
               "<LEDGERNAME>" + Trim(rw1("talyname")) + "</LEDGERNAME>" + vbCrLf &
               "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf &
               "<AMOUNT>" + LTrim(rw1("TAXAMT")) + "</AMOUNT>" + vbCrLf &
               "</ALLLEDGERENTRIES.LIST>" + vbCrLf
            Next
        End If




        '            If Len(Trim(Vatname)) > 0 Then
        '             xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf & _
        '             "<LEDGERNAME>" + MCREDIT + "</LEDGERNAME>" + vbCrLf & _
        '             "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf & _
        '             "<AMOUNT>" + LTrim(MKTAMT) + "</AMOUNT>" + vbCrLf & _
        '             "</ALLLEDGERENTRIES.LIST>" + vbCrLf
        '             If VTAXAMT > 0 Then
        '              xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf & _
        '              "<LEDGERNAME>" + Vatname + "</LEDGERNAME>" + vbCrLf & _
        '              "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf & _
        '              "<AMOUNT>" + LTrim(VTAXAMT) + "</AMOUNT>" + vbCrLf & _
        '              "</ALLLEDGERENTRIES.LIST>" + vbCrLf
        '             End If
        '             If VSTAXAMT > 0 Then
        '              xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf & _
        '              "<LEDGERNAME>" + "SURCHARGES" + "</LEDGERNAME>" + vbCrLf & _
        '              "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf & _
        '              "<AMOUNT>" + LTrim(VSTAXAMT) + "</AMOUNT>" + vbCrLf & _
        '              "</ALLLEDGERENTRIES.LIST>" + vbCrLf
        '             End If
        '
        If kcourier > 0 Then
            xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf &
            "<LEDGERNAME>" + "COURIER CHARGES" + "</LEDGERNAME>" + vbCrLf &
            "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf &
            "<AMOUNT>" + LTrim(kcourier) + "</AMOUNT>" + vbCrLf &
            "</ALLLEDGERENTRIES.LIST>" + vbCrLf
        End If
        If kforward > 0 Then
            xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf &
              "<LEDGERNAME>" + "FORWARDING CHARGES" + "</LEDGERNAME>" + vbCrLf &
              "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf &
              "<AMOUNT>" + LTrim(kforward) + "</AMOUNT>" + vbCrLf &
              "</ALLLEDGERENTRIES.LIST>" + vbCrLf
        End If

        If Mid(Trim(Tgrpcmpnam), 1, 6) = "THARUN" Then
            mroundled = "ROUNDED OFF"
        Else
            mroundled = "CHARITY"
        End If


        If kroundoff <> 0 Then
            xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf &
              "<LEDGERNAME>" + Trim(mroundled) + "</LEDGERNAME>" + vbCrLf
            If kroundoff > 0 Then
                xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf &
               "<AMOUNT>" + LTrim(kroundoff) + "</AMOUNT>" + vbCrLf
            Else
                xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>" + vbCrLf &
               "<AMOUNT>" + LTrim(kroundoff) + "</AMOUNT>" + vbCrLf
            End If
            xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>" + vbCrLf
        End If




        xmlstc = xmlstc + "</VOUCHER>" + vbCrLf &
        "</TALLYMESSAGE>" + vbCrLf &
        "</DATA>" + vbCrLf &
        "</BODY>" + vbCrLf & "</ENVELOPE>"

        Dim xmlFilePath As String = "d:\TallyData.xml"
        System.IO.File.WriteAllText(xmlFilePath, xmlstc)


        Return xmlstc

    End Function


    Private Function genxmlpurc(ByVal mkinvno As Int32, ByVal kdate As String, ByVal kparty As String, ByVal kvchno As String, ByVal kamt As Double, ByVal kdiscamt As Double, ByVal kcourier As Single, ByVal kforward As Single, ByVal kroundoff As Single, ByVal ktotamt As Double, ByVal knarr As String) As String
        'Dim DATE2 As String = Format(CDate("01-04-2023"), "dd/MM/yyyy")
        Dim DATE2 As String = Format(CDate(kdate), "dd/MM/yyyy")
        ' DATE2 = Format(vodat, "yyyymmdd")

        xmlstc = "<ENVELOPE>" + vbCrLf &
        "<HEADER>" + vbCrLf &
        "<VERSION>1</VERSION>" + vbCrLf &
        "<TALLYREQUEST>Import</TALLYREQUEST>" + vbCrLf &
        "<TYPE>Data</TYPE>" + vbCrLf &
        "<ID>Vouchers</ID>" + vbCrLf &
        "</HEADER>" + vbCrLf &
        "<BODY>" + vbCrLf &
        "<DESC>" + vbCrLf &
        "</DESC>" + vbCrLf &
        "<DATA>" + vbCrLf &
        "<TALLYMESSAGE >" + vbCrLf &
        "<VOUCHER VCHTYPE=""Purchase"" ACTION=""Create"">" + vbCrLf &
        "<DATE>" + DATE2 + "</DATE>" + vbCrLf &
        "<NARRATION>" + knarr + "</NARRATION>" + vbCrLf &
        "<VOUCHERTYPENAME>Sales</VOUCHERTYPENAME>" + vbCrLf &
        "<VOUCHERNUMBER>" + LTrim(mkinvno) + "</VOUCHERNUMBER>" + vbCrLf &
        "<REFERENCE>" + Trim(kvchno) + "</REFERENCE>" + vbCrLf &
        "<PARTYLEDGERNAME>" + Trim(kparty) + "</PARTYLEDGERNAME>" + vbCrLf &
        "<EFFECTIVEDATE>" + DATE2 + "</EFFECTIVEDATE>" + vbCrLf &
        "<ALLLEDGERENTRIES.LIST>" + vbCrLf &
        "<LEDGERNAME>" + Trim(kparty) + "</LEDGERNAME>" + vbCrLf &
        "<GSTCLASS />" + vbCrLf &
        "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf &
        "<LEDGERFROMITEM>No</LEDGERFROMITEM>" + vbCrLf
        xmlstc = xmlstc + "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>" + vbCrLf &
         "<ISPARTYLEDGER>Yes</ISPARTYLEDGER>" + vbCrLf &
         "<AMOUNT>" + LTrim(ktotamt) + "</AMOUNT>" + vbCrLf &
          "<BILLALLOCATIONS.LIST>" + vbCrLf &
            "<NAME>" + Trim(kvchno) + "</NAME>" + vbCrLf &
            "<BILLTYPE>New Ref</BILLTYPE>" + vbCrLf &
            "<AMOUNT>" + LTrim(ktotamt) + "</AMOUNT>" + vbCrLf &
            "</BILLALLOCATIONS.LIST>" + vbCrLf &
            "</ALLLEDGERENTRIES.LIST>" + vbCrLf




        'msql2 = "select  rtrim(ltrim(CONVERT(nchar(10),taxrate))) +'% '+ case when ISNULL(igst,0)>0 then 'IGST SALES' else 'GST SALES' end talyname,  SUM(amount) amt,taxrate from binv where invno=" & Val(mkinvno) & vbCrLf _
        '       & "group by taxrate, case when ISNULL(igst,0)>0 then 'IGST SALES' else 'GST SALES' end"
        'msql2 = "select  'STOCK TRANSFER OUTWARD' talyname,  SUM(amount) amt,taxrate from bstktfrinv where invno=" & mkinvno & " group by taxrate "

        msql2 = "select  rtrim(ltrim(CONVERT(nchar(10),taxrate))) +'% '+ case when ISNULL(igst,0)>0 then 'IGST PURCHASE' else 'GST PURCHASE' end talyname,  SUM(amount) amt,taxrate from brcpt where rno=" & Val(mkinvno) & vbCrLf _
               & "group by taxrate, case when ISNULL(igst, 0)>0 then 'IGST PURCHASE' else 'GST PURCHASE' end "


        Dim dtt As DataTable = getDataTable(msql2)

        If dtt.Rows.Count > 0 Then
            For Each rrw As DataRow In dtt.Rows
                xmlstc = xmlstc + " <ALLLEDGERENTRIES.LIST> " + vbCrLf &
                   " <LEDGERNAME> " + Trim(rrw("talyname")) + "</LEDGERNAME>" + vbCrLf &
                   "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>" + vbCrLf &
                   "<AMOUNT>" + LTrim(rrw("amt") * -1) + "</AMOUNT>" + vbCrLf &
                   "</ALLLEDGERENTRIES.LIST>" + vbCrLf
            Next
        End If






        'msql4 = " Select kk.invno,kk.mtaxrate,kk.talyname, kk.taxrate,kk.taxcode,sum(kk.taxamt) taxamt from " & vbCrLf _
        '      & "(Select k.invno,k.mtaxrate,'OUTPUT TAX '+RTRIM(ltrim(upper(k.taxname)))+' '+rtrim(ltrim(convert(nchar(10),k.staxrate)))+'%' as talyname, k.staxrate taxrate,upper(k.taxname) taxcode,sum(k.taxamt) taxamt from " & vbCrLf _
        '      & "(select invno,taxrate mtaxrate, staxrate,taxname,taxamt from " & vbCrLf _
        '      & "(select invno,taxrate,staxrate,amount,cgst,sgst,igst from bstktfrinv) s " & vbCrLf _
        '      & " unpivot " & vbCrLf _
        '      & "( taxamt for taxname in (cgst,sgst,igst)) n " & vbCrLf _
        '      & "where invno=" & Val(mkinvno) & ") k " & vbCrLf _
        '      & "group by k.invno,k.staxrate,k.taxname,k.mtaxrate Having Sum(k.taxamt) > 0 " & vbCrLf _
        '      & "Union All " & vbCrLf _
        '      & "select l.invno,l.frtaxper mtaxrate,l.talyname,l.taxrate,l.taxcode,l.taxamt from " & vbCrLf _
        '      & "(select invno,frtaxper,'OUTPUT TAX IGST '+ case when ISNULL(igst,0)>0 then rtrim(ltrim(convert(nchar(10),frtaxper))) else '0' end+'%' talyname, " & vbCrLf _
        '      & " case when ISNULL(igst,0)>0 then frtaxper else 0 end taxrate, 'IGST' taxcode,case when ISNULL(igst,0)>0 then frtaxamt else 0 end taxamt from stktfrinv where invno=" & Val(mkinvno) & vbCrLf _
        '      & "Union All " & vbCrLf _
        '      & "select invno,frtaxper,'OUTPUT TAX CGST '+ case when ISNULL(cgst,0)>0 then rtrim(ltrim(convert(nchar(10),round(frtaxper/2,2)))) else '0' end+'%' talyname, " & vbCrLf _
        '      & "case when ISNULL(cgst,0)>0 then round(frtaxper/2,2) else 0 end taxrate, 'CGST' taxcode,case when ISNULL(cgst,0)>0 then round(frtaxamt/2,2) else 0 end taxamt from stktfrinv where invno=" & Val(mkinvno) & vbCrLf _
        '      & "Union All " & vbCrLf _
        '      & "select invno,frtaxper,'OUTPUT TAX SGST '+ case when ISNULL(sgst,0)>0 then rtrim(ltrim(convert(nchar(10),round(frtaxper/2,2)))) else '0' end +'%' talyname, " & vbCrLf _
        '      & " case when ISNULL(sgst,0)>0 then round(frtaxper/2,2) else 0 end taxrate, 'SGST' taxcode,case when ISNULL(sgst,0)>0 then round(frtaxamt/2,2) else 0 end taxamt from stktfrinv where invno=" & Val(mkinvno) & ") l " & vbCrLf _
        '      & " where l.taxamt>0) kk group by kk.invno,kk.mtaxrate,kk.talyname, kk.taxrate,kk.taxcode    order by kk.taxrate "


        msql4 = "select kk.invno,kk.mtaxrate,kk.talyname, kk.taxrate,kk.taxcode,sum(kk.taxamt) taxamt from  " & vbCrLf _
               & "(select k.invno,k.mtaxrate,'INPUT TAX '+RTRIM(ltrim(upper(k.taxname)))+' '+rtrim(ltrim(convert(nchar(10),k.staxrate)))+'%' as talyname, k.staxrate taxrate,upper(k.taxname) taxcode,sum(k.taxamt) taxamt from   " & vbCrLf _
               & "(select invno,taxrate mtaxrate, staxrate,taxname,taxamt from   " & vbCrLf _
               & "(select rno invno,taxrate,staxrate,amount,cgst,sgst,igst from brcpt) s  " & vbCrLf _
               & " unpivot  " & vbCrLf _
               & "( taxamt for taxname in (cgst,sgst,igst)) n   " & vbCrLf _
               & " where invno=" & Val(mkinvno) & ") k  " & vbCrLf _
               & " group by k.invno,k.staxrate,k.taxname,k.mtaxrate Having Sum(k.taxamt) > 0  " & vbCrLf _
               & " Union All  " & vbCrLf _
               & " select l.invno,l.frtaxper mtaxrate,l.talyname,l.taxrate,l.taxcode,l.taxamt from  " & vbCrLf _
               & "(select rno invno,frtaxper,'INPUT TAX IGST '+ case when ISNULL(igst,0)>0 then rtrim(ltrim(convert(nchar(10),frtaxper))) else '0' end+'%' talyname,  " & vbCrLf _
               & " Case When ISNULL(igst,0)>0 Then frtaxper Else 0 End taxrate, 'IGST' taxcode,case when ISNULL(igst,0)>0 then frtaxamt else 0 end taxamt from rcpt where rno=" & Val(mkinvno) & vbCrLf _
               & " Union All  " & vbCrLf _
               & " Select rno invno,frtaxper,'INPUT TAX CGST '+ case when ISNULL(cgst,0)>0 then rtrim(ltrim(convert(nchar(10),round(frtaxper/2,2)))) else '0' end+'%' talyname,  " & vbCrLf _
               & " Case When ISNULL(cgst,0)>0 Then round(frtaxper/2,2) Else 0 End taxrate, 'CGST' taxcode,case when ISNULL(cgst,0)>0 then round(frtaxamt/2,2) else 0 end taxamt from rcpt where rno=" & Val(mkinvno) & vbCrLf _
               & " Union All  " & vbCrLf _
               & " Select rno invno,frtaxper,'INPUT TAX SGST '+ case when ISNULL(sgst,0)>0 then rtrim(ltrim(convert(nchar(10),round(frtaxper/2,2)))) else '0' end +'%' talyname,  " & vbCrLf _
               & " Case When ISNULL(sgst,0)>0 Then round(frtaxper/2,2) Else 0 End taxrate, 'SGST' taxcode,case when ISNULL(sgst,0)>0 then round(frtaxamt/2,2) else 0 end taxamt from rcpt where rno=" & Val(mkinvno) & vbCrLf _
               & " union all " & vbCrLf _
               & " select rno invno,q194per mtaxrate,case when isnull(q194per,0)>0 and q194per<0.75 then 'TDS '+convert(varchar(5),q194per)+'% ON PURCHASE 194Q'  " & vbCrLf _
               & "  When  isnull(q194per,0)>0.75 And q194per<=2 Then 'TDS ' + convert(varchar(5),q194per)+'% ON CONTRACT 194C'  " & vbCrLf _
               & "  When isnull(q194per,0)=3.75 Then  'TDS '+ convert(varchar(5),q194per)+'%  ON CONTRACT 194H'  else '' end talynam, " & vbCrLf _
               & "  round(q194per,1) taxrate,'TDS' Taxcode,q194amt taxamt from rcpt where rno=" & Val(mkinvno) & vbCrLf _
               & " union all " & vbCrLf _
               & " select rno invno,disc mtaxrate,case when isnull(discamt,0)>0 then 'DISCOUNT ON PURCHASE' else '' end talyname,isnull(disc,0) taxrate,'DISC' taxcode,isnull(discamt,0) taxamt " & vbCrLf _
               & " from rcpt where rno=" & Val(mkinvno) & vbCrLf _
               & " union all " & vbCrLf _
               & " select rno invno,0 mtaxrate,case when isnull(forward,0)>0 then  'FREIGHT CHARGES-INWARD' else '' end   talyname,0 taxrate,'FORWARDCHRG' taxcode,isnull(forward,0) taxamt " & vbCrLf _
               & " from rcpt where rno=" & Val(mkinvno) & " ) l  " & vbCrLf _
               & " where l.taxamt>0) kk group by kk.invno,kk.mtaxrate,kk.talyname, kk.taxrate,kk.taxcode  " & vbCrLf _
               & " order by case when taxcode='CGST' then 1 when taxcode='SGST' then 2 when taxcode='IGST' then 3 when taxcode='FORWARDCHRG' then 4 when taxcode='DISC' then 5 when taxcode='TDS' then 6 end "

        Dim dt1 As DataTable = getDataTable(msql4)
        If dt1.Rows.Count > 0 Then
            For Each rw1 As DataRow In dt1.Rows
                If rw1("Taxcode") = "TDS" Or rw1("Taxcode") = "DISC" Then
                    xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf &
                    "<LEDGERNAME>" + Trim(rw1("talyname")) + "</LEDGERNAME>" + vbCrLf &
                    "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf &
                    "<AMOUNT>" + LTrim(rw1("TAXAMT")) + "</AMOUNT>" + vbCrLf &
                    "</ALLLEDGERENTRIES.LIST>" + vbCrLf
                Else
                    xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf &
                    "<LEDGERNAME>" + Trim(rw1("talyname")) + "</LEDGERNAME>" + vbCrLf &
                    "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>" + vbCrLf &
                    "<AMOUNT>" + LTrim(rw1("TAXAMT") * -1) + "</AMOUNT>" + vbCrLf &
                    "</ALLLEDGERENTRIES.LIST>" + vbCrLf
                End If

            Next
        End If




        '            If Len(Trim(Vatname)) > 0 Then
        '             xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf & _
        '             "<LEDGERNAME>" + MCREDIT + "</LEDGERNAME>" + vbCrLf & _
        '             "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf & _
        '             "<AMOUNT>" + LTrim(MKTAMT) + "</AMOUNT>" + vbCrLf & _
        '             "</ALLLEDGERENTRIES.LIST>" + vbCrLf
        '             If VTAXAMT > 0 Then
        '              xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf & _
        '              "<LEDGERNAME>" + Vatname + "</LEDGERNAME>" + vbCrLf & _
        '              "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf & _
        '              "<AMOUNT>" + LTrim(VTAXAMT) + "</AMOUNT>" + vbCrLf & _
        '              "</ALLLEDGERENTRIES.LIST>" + vbCrLf
        '             End If
        '             If VSTAXAMT > 0 Then
        '              xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf & _
        '              "<LEDGERNAME>" + "SURCHARGES" + "</LEDGERNAME>" + vbCrLf & _
        '              "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf & _
        '              "<AMOUNT>" + LTrim(VSTAXAMT) + "</AMOUNT>" + vbCrLf & _
        '              "</ALLLEDGERENTRIES.LIST>" + vbCrLf
        '             End If
        '
        'If kcourier > 0 Then
        '    xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf &
        '    "<LEDGERNAME>" + "COURIER CHARGES" + "</LEDGERNAME>" + vbCrLf &
        '    "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf &
        '    "<AMOUNT>" + LTrim(kcourier) + "</AMOUNT>" + vbCrLf &
        '    "</ALLLEDGERENTRIES.LIST>" + vbCrLf
        'End If
        'If kforward > 0 Then
        '    xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf &
        '      "<LEDGERNAME>" + "FORWARDING CHARGES" + "</LEDGERNAME>" + vbCrLf &
        '      "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf &
        '      "<AMOUNT>" + LTrim(kforward) + "</AMOUNT>" + vbCrLf &
        '      "</ALLLEDGERENTRIES.LIST>" + vbCrLf
        'End If

        If Mid(Trim(Tgrpcmpnam), 1, 6) = "THARUN" Then
            mroundled = "ROUNDED OFF"
        Else
            mroundled = "CHARITY"
        End If

        If kroundoff <> 0 Then
            xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + vbCrLf &
              "<LEDGERNAME>" + Trim(mroundled) + "</LEDGERNAME>" + vbCrLf
            If kroundoff > 0 Then
                xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + vbCrLf &
               "<AMOUNT>" + LTrim(kroundoff) + "</AMOUNT>" + vbCrLf
            Else
                xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>" + vbCrLf &
               "<AMOUNT>" + LTrim(kroundoff) + "</AMOUNT>" + vbCrLf
            End If
            xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>" + vbCrLf
        End If




        xmlstc = xmlstc + "</VOUCHER>" + vbCrLf &
        "</TALLYMESSAGE>" + vbCrLf &
        "</DATA>" + vbCrLf &
        "</BODY>" + vbCrLf & "</ENVELOPE>"

        Dim xmlFilePath As String = "d:\TallyData.xml"
        System.IO.File.WriteAllText(xmlFilePath, xmlstc)


        Return xmlstc

    End Function



    Private Sub accessdb()
        'Dim acon As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & maccdbpath & "\atinv2007.accdb" & ";persist security info=False;"
        Dim acon As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & maccdbpath & ";persist security info=False;"
        'Dim acon As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & maccdbpath

        conacc = New OleDb.OleDbConnection(acon)
        'Try
        conacc.Open()
            'chkaccon = True


            msql = "SELECT LEDGER.`$name`,LEDGER.`$Mailingname`,LEDGER.`$_PRIMARYGROUP`," _
          & "LEDGER.`$_ADDRESS1`,LEDGER.`$_ADDRESS2`,LEDGER.`$_ADDRESS3`," _
          & "LEDGER.`$_ADDRESS4`,LEDGER.`$_ADDRESS5`,LEDGER.`$PARENT`," _
          & "LEDGER.`$IncomeTaxNumber`,LEDGER.`$SALESTAXNUMBER`,LEDGER.`$INTERSTATESTNUMBER`,LEDGER.`$VATTINNUMBER`," _
          & "LEDGER.`$Narration`,LEDGER.`$_PERFORMANCE`,LEDGER.`$mdisc`,LEDGER.`$mlorry`,LEDGER.`$mbrand`," _
          & "LEDGER.`$mGRADE`,LEDGER.`$mDEST`,LEDGER.`$mDOCU`,LEDGER.`$mKEYPER`,LEDGER.`$mHOLIDAY`,LEDGER.`$mDISTRICT`,LEDGER.`$ledgercontact`,LEDGER.`$ledgerphone`, " _
          & "LEDGER.`$mPROP`,LEDGER.`$mSTD`,Ledger.`$LedgerMobile`,Ledger.`$_ClosingBalance`,Ledger.`$mbill`,Ledger.`$EMail`,Ledger.`$OpeningBalance`, " _
          & "LEDGER.`$PartyGSTIN`,LEDGER.`$mdadd1`,LEDGER.`$mdadd2`,LEDGER.`$mdadd3`,LEDGER.`$mdadd4`,LEDGER.`$mdcity`, " _
          & "LEDGER.`$mdpincode`,LEDGER.`$mdstate`,LEDGER.`$mdgstin`,Ledger.`$mddistance`,Ledger.`$mcity`,Ledger.`$PINCode`,Ledger.`$mdistance`,Ledger.`$statename`,Ledger.`$_PartyGSTIN`,LEDGER.`$Mcardcode` FROM  Ledger"


            'Tcollname'

            If contally.State = ConnectionState.Closed Then
                contally.Open()
            End If

            Dim cmd As New Odbc.OdbcCommand(msql, contally)
            Dim dtl As New DataTable
            cmd.CommandTimeout = 600
            Dim da As New Odbc.OdbcDataAdapter(cmd)
            'cmd.CommandTimeout = 600

            da.Fill(dtl)
            np = 0
            PB.Maximum = dtl.Rows.Count
            PB.Step = 1
            'For Each rww As DataRow In dtl.Rows
            '    MsgBox(rww(0).ToString)
            'Next

            Dim cmdela As New Data.OleDb.OleDbCommand
            'Dim cmdel As New Data.SqlClient.SqlCommand
            cmdela.CommandText = "delete from ledmas"
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            'mtrans = conacc.BeginTransaction
            'cmdela.Transaction = mtrans
            cmdela.Connection = conacc



        mmst = "insert into ledmas(name,mname,parent,dum1) " _
                      & " VALUES (@name,@ADDlname,@parent, @PRIMARYGROUP)"

        'Dim cmd2 As New Data.SqlClient.SqlCommand
        Dim cmda2 As New Data.OleDb.OleDbCommand
            cmda2.CommandTimeout = 600

            cmda2.CommandText = mmst

            cmda2.Parameters.Add("@name", OleDbType.VarChar) '0
            cmda2.Parameters.Add("@ADDlname", OleDbType.VarChar) '1
            cmda2.Parameters.Add("@PARENT", OleDbType.VarChar)  '8
            cmda2.Parameters.Add("@PRIMARYGROUP", OleDbType.VarChar)   '2


            If conacc.State = ConnectionState.Closed Then
                conacc.Open()
            End If

            cmda2.Connection = conacc

            'cmd2.Transaction = mtrans

            Try
                cmdela.ExecuteNonQuery()

                For Each rw As DataRow In dtl.Rows

                'merrname = Replace(rw(2), "'", "`") & vbNullString
                merrname = Mid(If(IsDBNull(rw(2)) = False, rw(2) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 32)

                cmda2.Parameters(0).Value = Mid(Replace(rw(0), "'", "`") & vbNullString, 1, 30)
                cmda2.Parameters(1).Value = Mid(If(IsDBNull(rw(1)) = False, Replace(rw(1), "'", "`") & vbNullString, ""), 1, 30)
                cmda2.Parameters(2).Value = Mid(If(IsDBNull(rw(8)) = False, rw(8) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 30)
                cmda2.Parameters(3).Value = Mid(If(IsDBNull(rw(2)) = False, rw(2) & vbNullString, "").Replace(vbCr, "").Replace(vbLf, ""), 1, 30)

                cmda2.ExecuteNonQuery()
                    'PB.Value = np + 1
                    PB.PerformStep()
                Next
                'mtrans.Commit()
                MsgBox("Ledger Imported Successfully! - " & Now())
                PB.Value = 0


            Catch ex As Exception
            ' chktalycon = False
            MsgBox("Error : " & ex.Message)
        End Try
        'acon = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" & maccdbpath & "\atinv2007.accdb" & ";persist security info=False;"
    End Sub

End Class
