Imports System
Imports System.Data
Imports MySql.Data.MySqlClient
Imports Microsoft.VisualBasic
Imports System.IO


Imports globaldata
Public Enum MessageType
    Success
    [Error]
    Info
    Warning
End Enum
Public Class GlobalFunctions

    Public conn As New MySqlConnection
    Dim connstr As String
    Public version As String = "Version - 1.0"
    Public comName As String = "Rotech Invoice System"
    Public devCom As String = "Developed By : Crystal Act"
    Public DSN_NAME_CRP As String = "rotech"
    Public srno(32) As String

    Dim btnNameTab1() As String = {"&New", "&Edit", "&Delete", "&Close"}
    Dim btnNameTab2() As String = {"&Save", "C&ancel", "&Close", "&Remove"}
    'Public name As String = "penta-old"
    Public name As String = "rotech"
    Public server As String = "localhost"
    Public user As String = "root"
    Public pwd As String = "root"
    ' Public ico As New System.Drawing.Icon(Application.StartupPath & "\icon\invoice.ico")
    Public Mindate As String
    Public Maxdate As String
    Public invStartNo As String
    Public challanStartNo As String
    Public CurrentYear As String
    Public annxureStartNo As String
    Public subannxureStartNo As String

    Public Title As String = "Rotech ERP"
    Private filePath As String
    Private fileStream As FileStream
    Private streamWriter As StreamWriter
    Public Sub New()
        '   initConfigSettings()
        ' databaseBackup()
    End Sub
   
    'Public Sub databaseBackup()
    '    Try
    '        Dim dbBackUpDirPath As String = Application.StartupPath + "\..\..\dbbackup"
    '        If Directory.Exists(dbBackUpDirPath) Then
    '        Else
    '            Directory.CreateDirectory(dbBackUpDirPath)
    '        End If

    '        dbBackUpDirPath = dbBackUpDirPath + "\" + Format(Now.Date, "dd.MM.yy")
    '        If Directory.Exists(dbBackUpDirPath) Then
    '            If System.IO.File.Exists(dbBackUpDirPath + "\pentafab.sql") Then
    '                Exit Sub
    '            End If
    '        Else
    '            Directory.CreateDirectory(dbBackUpDirPath)
    '        End If
    '        Dim file As String = dbBackUpDirPath + "\pentafab.sql"
    '        mysqlBackUPProcess(file)
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
    'Public Sub mysqlBackUpProcess(ByVal file As String)
    '    Dim myProcess As New Process()
    '    myProcess.StartInfo.FileName = "cmd.exe"
    '    myProcess.StartInfo.UseShellExecute = False
    '    myProcess.StartInfo.WorkingDirectory = GetSingleValue("", False, "Conf", "p_config", "Const = 'MYSQL_PATH'")
    '    If Directory.Exists(myProcess.StartInfo.WorkingDirectory) = False Then
    '        MsgBox("Please Set Mysql Path in Config", MsgBoxStyle.Information, "Database Backup Alear")
    '        Exit Sub
    '    End If
    '    myProcess.StartInfo.RedirectStandardInput = True
    '    myProcess.StartInfo.RedirectStandardOutput = True
    '    myProcess.Start()
    '    Dim myStreamWriter As StreamWriter = myProcess.StandardInput
    '    Dim mystreamreader As StreamReader = myProcess.StandardOutput
    '    myStreamWriter.WriteLine("mysqldump -u " + user + " --password=" + pwd + " -h " + server + " " + name + " > """ + file + """ ")
    '    myStreamWriter.Close()
    '    myProcess.WaitForExit()
    '    myProcess.Close()
    '    MsgBox("Database Backup Created Successfully!", MsgBoxStyle.Information, "Backup")
    'End Sub
    'Public Sub mysqlRestoreProcess(ByVal filepath As String)
    '    Dim myProcess As New Process()
    '    myProcess.StartInfo.FileName = "cmd.exe"
    '    myProcess.StartInfo.UseShellExecute = False
    '    myProcess.StartInfo.WorkingDirectory = GetSingleValue("", False, "Conf", "p_config", "Const = 'MYSQL_PATH'")
    '    myProcess.StartInfo.RedirectStandardInput = True
    '    myProcess.StartInfo.RedirectStandardOutput = True
    '    myProcess.Start()
    '    Dim myStreamWriter As StreamWriter = myProcess.StandardInput
    '    Dim mystreamreader As StreamReader = myProcess.StandardOutput
    '    myStreamWriter.WriteLine("mysql -u " + user + " --password=" + pwd + " -h " + server + " " + name + " < """ + filepath + """ ")
    '    myStreamWriter.Close()
    '    myProcess.WaitForExit()
    '    myProcess.Close()
    '    MsgBox("Database Restoration Successfully!", MsgBoxStyle.Information, "Restore")
    'End Sub

    Public Sub initConfigSettings()
        CurrentYear = GetSingleValue("", False, "Conf", "p_config", "Const = 'CURRENT_YEAR'")
        Mindate = CurrentYear + "-04-01"
        Maxdate = CStr(CInt(CurrentYear) + 1) + "-03-31"
        invStartNo = GetSingleValue("", False, "Conf", "p_config", "Const = 'INVOICE_START_NO'")
        challanStartNo = GetSingleValue("", False, "Conf", "p_config", "Const = 'CHALLAN_START_NO'")
        annxureStartNo = GetSingleValue("", False, "Conf", "p_config", "Const = 'ANNEXURE_START_NO'")
        subannxureStartNo = GetSingleValue("", False, "Conf", "p_config", "Const = 'SUBANNEXURE_START_NO'")
    End Sub

    'Public Function setDefaultPrinterName()
    '    Try
    '        Dim ds As DataTable
    '        Dim compName As String = System.Net.Dns.GetHostName()
    '        ds = GetData(False, "Id,Printername", "Printer", "ComputerName='" & compName & "'")
    '        If ds.Rows.Count > 0 Then
    '            Return Trim$(ds.Rows(0)("Printername"))
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        Return ""
    '    End Try

    'End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Function connection()


        If Not conn Is Nothing Then conn.Close()
        conn.ConnectionString = String.Format("server={0}; user id={1}; password={2}; database={3}; pooling=false", server, user, pwd, name)
        Try
            conn.Open()

        Catch ex As Exception
            MsgBox("cannot connect")
        End Try
        Return 0
    End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="IsDISTINCT"></param>
    ''' <param name="Fields"></param>
    ''' <param name="TableNames"></param>
    ''' <param name="Criteria"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetData(ByVal IsDISTINCT As Boolean, ByVal Fields As String, ByVal TableNames As String, ByVal Criteria As String, Optional ByVal sel As Boolean = False, Optional ByVal SortingOrder As String = "") As DataTable
        Dim ds As New DataTable
        Try
            ' Criteria = Criteria + " AND IS_DELETED <> 1"
            connection()
            Dim dsData = New DataSet()
            Dim Query As String = String.Empty

            If (IsDISTINCT) Then
                Query = "SELECT DISTINCT "
            Else
                Query = "SELECT "
            End If
            Query += Fields + " FROM " + TableNames

            If (Not String.IsNullOrEmpty(Criteria)) Then
                Query += " WHERE " + Criteria
            End If

            If (Not String.IsNullOrEmpty(SortingOrder)) Then
                Query += " Order By " + SortingOrder
            End If
            WriteLog("Error :" & Date.Today.ToString & " " & Query)
            Dim cmd As MySqlCommand = New MySqlCommand(Query, conn)
            Dim sda As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            ds = New DataTable()
            sda.Fill(ds)
            If sel Then
                Dim dsNewRow As DataRow
                dsNewRow = ds.NewRow()
                dsNewRow.Item(0) = 0
                dsNewRow.Item(1) = "Select"
                ds.Rows.InsertAt(dsNewRow, 0)
            End If
            conn.Close()
            'Return ds
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, Title)
            WriteLog("Error :" & Date.Today.ToString & "Above Fail Query : ")
        End Try
        Return ds
    End Function
    Public Function GetSingleValue(ByVal AggregateFunName As String, ByVal IsDISTINCT As Boolean, ByVal FieldName As String, ByVal TableName As String, ByVal Criteria As String) As String
        Try
            connection()
            Dim Query As String = String.Empty
            Dim ReturnValue As String = String.Empty
            Query = "SELECT "
            If (Not String.IsNullOrEmpty(AggregateFunName)) Then
                Query += AggregateFunName
            End If

            If (IsDISTINCT) Then
                Query += "(DISTINCT(" + FieldName + ")) as Value "
            Else
                Query += "(" + FieldName + ") as Value "
            End If
            Query += " FROM " + TableName
            If (Not String.IsNullOrEmpty(Criteria)) Then
                Query += " WHERE " + Criteria
            End If
            WriteLog("Error :" & Date.Today.ToString & " " & Query)
            Dim cmd As New MySqlCommand(Query, conn)
            ReturnValue = Convert.ToString(cmd.ExecuteScalar())
            conn.Close()
            Return ReturnValue
        Catch ex As Exception
            WriteLog("Error :" & Date.Today.ToString & "Above Fail Query : ")
            conn.Close()
            Return ""
        End Try
    End Function
    Public Function Getdate(ByVal IsDISTINCT As Boolean, ByVal FieldName As String, ByVal TableName As String, ByVal Criteria As String) As Date
        Try
            connection()
            Dim Query As String = String.Empty
            Dim ReturnValue As Date
            Query = "SELECT "
            If (IsDISTINCT) Then
                Query += " (DISTINCT(" + FieldName + ")) as value "
            Else
                Query += " (" + FieldName + ") as value "
            End If
            Query += " FROM " + TableName
            If (Not String.IsNullOrEmpty(Criteria)) Then
                Query += " WHERE " + Criteria
            End If
            WriteLog("Error :" & Date.Today.ToString & " " & Query)
            Dim cmd As New MySqlCommand(Query, conn)
            ReturnValue = Convert.ToString(cmd.ExecuteScalar())
            conn.Close()
            Return ReturnValue
        Catch ex As Exception
            WriteLog("Error :" & Date.Today.ToString & "Above Fail Query : ")
            conn.Close()
            Return ""
        End Try

    End Function

    Public Function DeleteData(ByVal TableName As String, ByVal Criteria As String) As String
        Try
            connection()
            Dim Query As String = String.Empty
            Dim ReturnValue As String = String.Empty
            Query = "DELETE "
            Query += " FROM " + TableName

            If (Not String.IsNullOrEmpty(Criteria)) Then
                Query += " WHERE " + Criteria
            End If
            WriteLog("Error :" & Date.Today.ToString & " " & Query)
            Dim cmd As New MySqlCommand(Query, conn)
            ReturnValue = Convert.ToString(cmd.ExecuteScalar())
            Return "Sucess"
            conn.Close()
        Catch ex As Exception
            WriteLog("Error :" & Date.Today.ToString & "Above Fail Query : ")
            conn.Close()
            Return "Fail"
        End Try
    End Function
    Public Function InsertData(ByVal Fields As String, ByVal Values As String, ByVal TableName As String) As String
        Try
            connection()
            Dim Query As String = String.Empty
            Query = "INSERT INTO " + TableName + _
                    " (" + Fields + ")" + _
                    " VALUES " + _
                    " (" + Values + ")"
            WriteLog("Error :" & Date.Today.ToString & " " & Query)
            Dim cmdInsert As MySqlCommand = New MySqlCommand(Query, conn)
            cmdInsert.ExecuteNonQuery()
            Return "Success"
        Catch ex As Exception
            WriteLog("Error :" & Date.Today.ToString & "Above Fail Query : ")
            Return "Fail"
            conn.Close()
        End Try
    End Function
    Public Function UpdateData(ByVal Fields As String, ByVal Values As String, ByVal TableName As String, ByVal Criteria As String) As String
        Try
            connection()
            Dim Query As String = String.Empty
            Dim FieldsData As String() = Split(Fields, "|")
            Dim ValuesData As String() = Split(Values, "|")
            Dim i As Integer = 0
            Query = "UPDATE " + TableName + _
                    " SET "
            For i = 0 To FieldsData.Length - 1
                If (FieldsData.Length = 1 Or i = 0) Then
                    Query += FieldsData(i) + " = " + ValuesData(i)
                Else
                    Query += ", " + FieldsData(i) + " = " + ValuesData(i)
                End If
            Next
            Query = Query + " , MODIFIED_DATE ='" + Format(Today.Date, "yyyy-MM-dd") + "'"
            Query += " WHERE " + Criteria
            Dim cmdUpdate As MySqlCommand = New MySqlCommand(Query, conn)
            cmdUpdate.ExecuteNonQuery()

            Return "Success"
        Catch ex As Exception
            WriteLog("Error :" & Date.Today.ToString & "Above Fail Query : ")
            MsgBox(ex.Message, MsgBoxStyle.Information, "Pentafab")
            Return "Fail"
            conn.Close()
        End Try
    End Function

    Public Function FormattedString(ByVal Text As String) As String
        Try
            If (String.IsNullOrEmpty(Text)) Then
                Return ""
            Else
                Return Text.Replace("'", "''")
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function AutoGenId(ByVal FieldName As String, ByVal TableName As String) As Integer
        '======================
        '   generate new id
        '======================
        Try
            Dim strsql As String
            strsql = GetSingleValue("MAX", False, FieldName, TableName, "")
            If (strsql = "") Then
                AutoGenId = 1
            Else
                AutoGenId = CInt(strsql) + 1
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "Pentafab")
        End Try
    End Function


    Public Sub OpenFile()
        Dim strPath As String
        strPath = HttpContext.Current.Server.MapPath("~/Error.log")
        If System.IO.File.Exists(strPath) Then
            fileStream = New FileStream(strPath, FileMode.Append, FileAccess.Write)
        Else
            fileStream = New FileStream(strPath, FileMode.Create, FileAccess.Write)
        End If
        StreamWriter = New StreamWriter(fileStream)
    End Sub

    Public Sub WriteLog(ByVal strComments As String)
        OpenFile()
        StreamWriter.WriteLine(strComments)
        CloseFile()
    End Sub

    Public Sub CloseFile()
        StreamWriter.Close()
        fileStream.Close()
    End Sub

  
   
End Class
