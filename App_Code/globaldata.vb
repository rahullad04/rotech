Imports System.Data.OleDb
Imports Microsoft.VisualBasic
Imports System.IO
Imports GlobalFunctions

Module globaldata

    ' Public FSystem As New FileSystemObject
    Public FSystem As New System.Object
    Public fileSource As String
    Public fileDestination As String
    Public pathforreport As String
    Public dbpath As String
    'Public strpath As String = connectionPath()
    Public ddl As Boolean = False
    Dim GF As New GlobalFunctions()
    Public Class comboClass
        Private _text As String
        Private _data As Integer
        Public Overrides Function tostring() As String
            Return _text
        End Function
        Public ReadOnly Property text() As String
            Get
                Return _text
            End Get
        End Property
        Public ReadOnly Property data() As Integer
            Get
                Return _data
            End Get
        End Property
        Sub New(Optional ByRef t As String = "", Optional ByVal n As Integer = Nothing)
            _text = t
            _data = n
        End Sub
    End Class
    'Public Sub popCombo(ByRef ds As DataTable, ByRef cmb As ComboBox, ByVal name As String, ByVal data As String)

    '    '==========================================
    '    '   fill combo box according to criteria
    '    '==========================================


    '    Try
    '        Dim obj As comboClass
    '        cmb.Items.Clear()
    '        If (ds.Rows.Count > 0) Then

    '            For i As Integer = 0 To ds.Rows.Count - 1
    '                obj = New comboClass(ds.Rows(i)(name).ToString, ds.Rows(i)(data).ToString)
    '                cmb.Items.Add(obj)
    '            Next

    '        End If

    '        'While objdr.Read
    '        '    obj = New comboClass(objdr(name), objdr(data))
    '        '    cmb.Items.Add(obj)
    '        'End While
    '    Catch ex As Exception
    '        MsgBox("Error in PopCombo -->> " & ex.ToString, MsgBoxStyle.Critical)
    '    End Try
    'End Sub

    'Public Sub Bindlist(ByRef ds As DataTable, ByRef lst As ListBox, ByVal name As String, ByVal data As String)
    '    Try
    '        Dim obj As comboClass
    '        lst.Items.Clear()
    '        If (ds.Rows.Count > 0) Then

    '            For i As Integer = 0 To ds.Rows.Count - 1
    '                obj = New comboClass(ds.Rows(i)(name), ds.Rows(i)(data))
    '                lst.Items.Add(obj)
    '            Next

    '        End If

    '    Catch ex As Exception
    '        MsgBox("Error in Bind List -->> " & ex.ToString, MsgBoxStyle.Critical)
    '    End Try
    'End Sub
    '''

    Public Function numbernull(ByVal num As Object) As Object
        Return Val(num & "")
    End Function
    Public Function IsNum(ByVal Key As Long) As Long

        '=====================================
        '   don't allow to enter same digit
        '=====================================

        Dim Num As String
        Num = "0123456789"
        If InStr(1, Num, Chr(Key)) Then
            IsNum = Key
        Else
            IsNum = 0
        End If

    End Function
    'Public Sub bakeupdatabase()
    '    Try
    '        Dim strs, str1, str2 As String
    '        str1 = Application.StartupPath & "\BackUp"
    '        If Directory.Exists(str1) Then
    '        Else
    '            Directory.CreateDirectory(str1)
    '        End If
    '        str2 = "BackUp\BackUp_" + Format(Now.Date, "dd.MM.yy")
    '        strs = Application.StartupPath & "\" + str2
    '        If File.Exists(strs + "\rotech.mdb") Then
    '            If MsgBox("You Have already made Backup of Data" & vbCrLf & "Do you want to Overwrite Backup Again", vbYesNo + vbInformation, "Overwrite BackUp") = vbYes Then
    '            Else
    '                Exit Sub
    '            End If
    '        End If

    '        If Directory.Exists(strs) Then
    '        Else
    '            Directory.CreateDirectory(strs)
    '        End If
    '        fileSource = Application.StartupPath & "\rotech.mdb"
    '        fileDestination = strs + "\rotech.mdb"

    '        File.Copy(fileSource, fileDestination, True)
    '        MsgBox("System Save  DATA  SUCESSFULLY...............", vbInformation, "Backup Done")
    '        Exit Sub
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub

    'Public Sub restoredata()
    '    '======================
    '    '    restore(database)
    '    '======================
    '    Try
    '        Dim fMainObj As New frmMain
    '        fMainObj.cdrestore.Title = "Restore Back Up Data"
    '        fMainObj.cdrestore.FilterIndex = 1
    '        fMainObj.cdrestore.Filter = "Database File(*.mdb)|*.mdb"
    '        fMainObj.cdrestore.InitialDirectory = Application.StartupPath & "\Backup"
    '        If fMainObj.cdrestore.ShowDialog = Windows.Forms.DialogResult.Cancel Then
    '            Exit Try
    '        End If
    '        'cdRestore.ShowDialog()
    '        Dim pos As Integer
    '        pos = InStr(Trim$(StrReverse(fMainObj.cdrestore.FileName)), "\")
    '        If StrReverse(Mid(Trim$(StrReverse(fMainObj.cdrestore.FileName)), 1, pos - 1)) = "rotech.mdb" Then
    '            If MsgBox("Do you want to Restore the Selected Data", vbInformation + vbYesNo, "Restore Data") = vbYes Then
    '                fMainObj.RecSet = Nothing
    '                fileSource = ""
    '                fileDestination = ""
    '                fileSource = fMainObj.cdrestore.FileName
    '                fileDestination = Application.StartupPath & "\rotech.mdb"
    '                File.Copy(fileSource, fileDestination, True)
    '                Call connection1()
    '                MsgBox("RESTORE DATA COMPLETED SUCESSFULLY...............", vbInformation, "Restore Completed")
    '                Exit Sub
    '            End If
    '        Else
    '            MsgBox("Please Select rotech.mdb File", vbInformation, "Information")
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'Public Sub reportconection()

    '    If conn.State = ConnectionState.Open Then conn.Close()
    '    Dim rppath As String = connectionPath()
    '    pathforreport = rppath
    'End Sub
    'Public Sub connection1()

    '    '=============================================
    '    '   make connection for both server and node
    '    '=============================================

    '    Try
    '        Dim strconection As String
    '        If dsntrue = True Then
    '            If conn.State = ConnectionState.Open Then conn.Close()
    '            strconection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strpath & ";Persist Security Info=True;Jet OLEDB:Database Password=rotechfan"
    '            conn = New OleDbConnection(strconection)
    '            conn.Open()
    '        Else
    '            If conn.State = ConnectionState.Open Then conn.Close()
    '            strconection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strpath & ";Persist Security Info=True;Jet OLEDB:Database Password=rotechfan"
    '            conn = New OleDbConnection(strconection)
    '        End If
    '        dsntrue = False

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'Public Function connectionPath(Optional ByVal newDbPath As String = "", Optional ByVal isWrite As Boolean = False) As String
    '    Dim dbFilePath = Application.StartupPath & "\dbpath.txt"
    '    If isWrite And newDbPath <> "" Then
    '        strpath = newDbPath
    '        SaveTextToFile(newDbPath, dbFilePath)
    '    Else
    '        strpath = GetFileContents(dbFilePath)
    '    End If
    '    Return strpath
    'End Function

    'Public Function GetFileContents(ByVal FullPath As String, _
    '   Optional ByRef ErrInfo As String = "") As String

    '    Dim strContents As String
    '    Dim objReader As StreamReader
    '    Try

    '        objReader = New StreamReader(FullPath)
    '        strContents = objReader.ReadLine()
    '        objReader.Close()
    '        Return Trim(strContents)
    '    Catch Ex As Exception
    '        ErrInfo = Ex.Message
    '        Return ""
    '    End Try
    'End Function

    Public Function SaveTextToFile(ByVal strData As String, _
     ByVal FullPath As String, _
       Optional ByVal ErrInfo As String = "") As Boolean

        Dim bAns As Boolean = False
        Dim objReader As StreamWriter
        Try
            objReader = New StreamWriter(FullPath, True)
            objReader.WriteLine(strData)
            objReader.Close()
            bAns = True
        Catch Ex As Exception
            ErrInfo = Ex.Message

        End Try
        Return bAns
    End Function
End Module
