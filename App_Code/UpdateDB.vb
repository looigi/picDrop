Public Class UpdateDB
    Public VersioneDBApplicazione As Integer = 2

    Public Sub ControllaAggiornamentoDB()
        Dim varConnessione As SQLSERVERCE
        Dim conn As Object = CreateObject("ADODB.Connection")
        Dim rec As Object = "ADODB.Recordset"

        varConnessione = New SQLSERVERCE
        varConnessione.ImpostaNomeDB(Application.StartupPath & "\DB\picDrop.sdf")
        varConnessione.LeggeImpostazioniDiBase()
        conn = varConnessione.ApreDB()

        Dim Sql As String
        Sql = "Select * From VersioneDB"
        rec = varConnessione.LeggeQuery(conn, Sql)
        If rec Is Nothing = True Then
            Sql = "Create Table VersioneDB (Versione int)"
        Else
            Sql = ""
        End If
        Try
            rec.Close()
        Catch ex As Exception

        End Try

        If Sql <> "" Then
            varConnessione.EsegueSql(conn, Sql)

            Sql = "Insert Into VersioneDB Values(-1)"
            varConnessione.EsegueSql(conn, Sql)
        End If

        EsegueAggiornamentoDB(varConnessione, conn)

        conn.Close()
        varConnessione = Nothing
    End Sub

    Private Sub EsegueAggiornamentoDB(DB As SQLSERVERCE, Conn As Object)
        Dim rec As Object = CreateObject("ADODB.Recordset")
        Dim Sql As String
        Dim VersioneDB As Integer = 0

        Sql = "Select Versione From VersioneDB"
        rec = DB.LeggeQuery(Conn, Sql)
        If Not rec Is Nothing Then
            If rec.eof Then
                VersioneDB = 0
            Else
                VersioneDB = rec(0).value
            End If
            rec.close()
        End If

        If VersioneDB < VersioneDBApplicazione Then
            If VersioneDB < 0 Then
                Sql = "Create Table Ricerche (Ricerca nVarChar(100), InizioPagina int, FinePagina int)"
                DB.EsegueSql(Conn, Sql)

                Sql = "ALTER TABLE [Ricerche] ADD CONSTRAINT  [Ricerche_PK] PRIMARY KEY ([Ricerca]);"
                DB.EsegueSql(Conn, Sql)
            End If

            If VersioneDB < 2 Then
                Sql = "ALTER TABLE [Ricerche] ADD Scaricate Int;"
                DB.EsegueSql(Conn, Sql)
            End If

            Sql = "Update VersioneDB Set Versione=" & VersioneDBApplicazione
            DB.EsegueSql(Conn, Sql)
        End If
    End Sub
End Class
