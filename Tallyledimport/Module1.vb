Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports System.Configuration
Imports System.Collections.Specialized
Imports System.Text

Module Module1
    Public con As SqlConnection
    Public constr1 As String
    Public contally As OdbcConnection
    Public constrauto As String
    Public dbmyservernameauto As String
    Public dbmydbnameauto As String
    Public dbmypwdauto As String
    Public dbuseridauto As String
    Public dbcomp As String
    Public dbreportpathauto As String
    Public dbprovideroledbauto As String
    Public DBINVauto As String
    Public DBFRWauto As String
    Public dbcourauto As String
    Dim arr() As Byte
    Dim tmpp As String
    Dim strp, merr As String
    Dim i As Int32
    Public dbmyservername As String
    Public dbmydbname As String
    Public dbmypwd As String
    Public dbuserid As String
    Public dbreportpath As String
    Public dbGATEPASS As String
    Public dbmGATEPASS As String
    Public dbprovideroledb As String
    Public dbperiod As String
    Public DBINV As String
    Public DBINVPrint As String
    Public DBRINVPrint As String
    Public DBFRW As String
    Public DBllr As String
    Public dbcour As String
    Public dbtripsummary As String
    Public dblrpass As String
    Public dbGPSU As String
    Public dbcard As String
    Public dbobp As String
    Public DBTRANS As String
    Public mcmpid As String
    Public CRNOT, CRnotServ As String
    Public prntername As String
    Public mtallyport As String
    Public tallydsn As String
    Public tallyconstr As String
    Public Tgrpcmpnam As String
    Public Tcollname As String
    Public autorun As String
    Public chktalycon As Boolean
    Public mcompcode As Int16
    Public mtalybit As String
    Public maccdbpath As String
    Public chkaccon As Boolean
    Public Sub MAIN()
        dbmyservername = System.Configuration.ConfigurationManager.AppSettings("myservername")
        dbmydbname = System.Configuration.ConfigurationManager.AppSettings("mydbname")
        dbmypwd = System.Configuration.ConfigurationManager.AppSettings("mypwd")
        'dbmypwd = decodefile(System.Configuration.ConfigurationManager.AppSettings("mypwd"))
        dbcard = (System.Configuration.ConfigurationManager.AppSettings("card"))
        dbuserid = System.Configuration.ConfigurationManager.AppSettings("userid")
        dbreportpath = System.Configuration.ConfigurationManager.AppSettings("reportpath")
        dbprovideroledb = System.Configuration.ConfigurationManager.AppSettings("provideroledb")
        DBINV = System.Configuration.ConfigurationManager.AppSettings("INVPRN2")
        DBINVPrint = System.Configuration.ConfigurationManager.AppSettings("INVPRN")


        mtallyport = System.Configuration.ConfigurationManager.AppSettings("Tallyport")
        Tgrpcmpnam = System.Configuration.ConfigurationManager.AppSettings("CompanyName")
        Tcollname = System.Configuration.ConfigurationManager.AppSettings("TallyCollName")
        mtalybit = System.Configuration.ConfigurationManager.AppSettings("TallyBit")
        maccdbpath = System.Configuration.ConfigurationManager.AppSettings("AccDBPath")

        autorun = System.Configuration.ConfigurationManager.AppSettings("ScheduleRun")


        If mtalybit = "64" Then
            tallydsn = "TallyODBC64_" + Trim(mtallyport)
        ElseIf mtalybit = "32" Then
            tallydsn = "TallyODBC_" + Trim(mtallyport)
        End If




        'constr = "Provider= " & Trim(dbprovideroledb) & ";Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";Persist Security Info=True;User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
        'constr = "Provider= " & Trim(dbprovideroledb) & ";Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";Network Library=DBMSSOCN;User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
        'con = New OleDb.OleDbConnection(constr)

        constr1 = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";Persist Security Info=true;User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd)

        'Data Source=accmdub100es1\sa;Initial Catalog=ominventry21;Persist Security Info=True;User ID=sa;Password=Sa@536


        'constr1 = "Password=" & Trim(dbmypwd) & ";Persist Security Info=True;User ID=" & Trim(dbuserid) & ";Initial Catalog=" & Trim(dbmydbname) & ";Data Source=" & Trim(dbmyservername) & ""
        con = New SqlConnection(constr1)

        tallyconstr = "DSN=" & Trim(tallydsn) & ";PORT=" & Trim(mtallyport) & ";DRIVER=Tally ODBC Driver;SERVER={(local)}"

        'contally = New OdbcConnection("DSN=TallyODBC_9000;PORT=9000;DRIVER=Tally ODBC Driver;SERVER={(local)}")

        contally = New OdbcConnection(tallyconstr)
        Try
            contally.Open()
            chktalycon = True
        Catch ex As Exception
            chktalycon = False
        End Try
        'Try
        '    If con1.State = ConnectionState.Closed Then con1.Open()
        '    Dim da As New SqlDataAdapter, ds As New DataSet
        '    da.SelectCommand = New SqlCommand
        '    da.SelectCommand.Connection = con1
        '    da.SelectCommand.CommandType = CommandType.Text
        '    da.SelectCommand.CommandText = "SELECT TOP 1 [FY] FROM [SAP_FY_CRY]"
        '    da.Fill(ds, "tbl2")
        '    Dim dt As DataTable = ds.Tables("tbl2")
        '    dbperiod = dt.Rows(0)("FY")
        '    If con1.State = ConnectionState.Closed Then con1.Open()

        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'Finally
        '    If con1.State = ConnectionState.Open Then con1.Close()
        'End Try



    End Sub



    Public Function getDataReader(ByVal SQL As String) As SqlDataReader
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim cmd As New SqlCommand(SQL, con)
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader
        Return dr

    End Function

    Public Function getDataTable(ByVal SQL As String) As DataTable
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim cmd As New SqlCommand(SQL, con)
        Dim table As New DataTable
        cmd.CommandTimeout = 600
        Dim da As New SqlDataAdapter(cmd)
        'cmd.CommandTimeout = 600

        da.Fill(table)
        Return table


    End Function

    Public Sub executeQuery(ByVal SQL As String)
        merr = ""

        If con.State = ConnectionState.Closed Then
            con.Open()
        End If


        'trans = con.BeginTransaction

        'Dim cmd As New OleDbCommand(SQL, con, trans)
        Dim cmd As New SqlCommand(SQL, con)

        Try
            cmd.ExecuteNonQuery()
            'trans.Commit()
            ' merr2 = "Saved!"
        Catch ex As Exception
            'If InStr(merr, "PRIMARY KEY") > 0 Then

            'End If
            merr = Trim(ex.Message)
            '   trans.Rollback()
            MsgBox(ex.Message)
        End Try

    End Sub
End Module
