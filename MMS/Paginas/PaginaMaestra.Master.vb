Imports MySql.Data.MySqlClient
Imports MMS_Utiles
Public Class PaginaMaestra
    Inherits System.Web.UI.MasterPage
    Private C As New CG
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
        Response.Cache.SetNoStore()
        Validar_Seguridad()
        Ocultar_Controles()
    End Sub
    Public Sub Ocultar_Controles()

        Dim Entidad As String = C.Pedir_Valoreas_a_Cookie("MMS-C", "IEH")

        'If Entidad <> "38753c73fabd9f49284aae8a6410fb00207d4d16" Then
        '    Me.mysql.Visible = False
        'End If

    End Sub

    Public Sub Validar_Seguridad()
        Dim Clave_Temp As String = C.Pedir_Valoreas_a_Cookie("MMS-C", "ICT")
        Dim Entidad_Hash As String = C.Pedir_Valoreas_a_Cookie("MMS-C", "IEH")
        Dim Nombre_usuario As String = C.Pedir_Valoreas_a_Cookie("MMS-C", "INA")
		Dim Entidad_Nombre As String = C.Pedir_Valoreas_a_Cookie("MMS-C", "IE")
		Dim Cargo As String = C.Pedir_Valoreas_a_Cookie("MMS-C", "ITD")

		Try

            Validar_Acceso_Por_Clave_Temporal(Clave_Temp, Entidad_Hash)
			' Me.nombreuser.InnerText = " " & Entidad_Nombre.ToString & " - " & Nombre_usuario.ToString
			Me.nombreuser.InnerText = " " & Nombre_usuario.ToString
			Me.nameCargo.InnerText = Cargo.ToString
			Dim Script_Paso As String = ""
            Dim URL_ACTUAL As String = Request.Url.Segments(Request.Url.Segments.Count - 1)

            Select Case URL_ACTUAL
                Case "Home.aspx"
                 '   Script_Paso = Script_Paso & " " & "$('#cnc').addClass('active'); $('#ldp').removeClass('active'); $('#mysql').removeClass('active'); $('#bots').removeClass('active'); $('#histo').removeClass('active');"
                Case "Lineas_Disp.aspx"
            '        Script_Paso = Script_Paso & " " & "$('#ldp').addClass('active'); $('#cnc').removeClass('active'); $('#mysql').removeClass('active'); $('#bots').removeClass('active'); $('#histo').removeClass('active');"
                Case "Estado_Mysql.aspx"
            '        Script_Paso = Script_Paso & " " & "$('#mysql').addClass('active'); $('#ldp').removeClass('active'); $('#cnc').removeClass('active'); $('#bots').removeClass('active'); $('#histo').removeClass('active');"
                Case "Robots.aspx"
            '        Script_Paso = Script_Paso & " " & "$('#bots').addClass('active'); $('#ldp').removeClass('active'); $('#mysql').removeClass('active'); $('#cnc').removeClass('active'); $('#histo').removeClass('active');"
                Case "Historial.aspx"
                    '          Script_Paso = Script_Paso & " " & "$('#bots').removeClass('active'); $('#ldp').removeClass('active'); $('#mysql').removeClass('active'); $('#cnc').removeClass('active'); $('#histo').addClass('active');"

            End Select

            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(Page), "Script", Script_Paso.ToString, True)
        Catch ex As Exception
            HttpContext.Current.Response.Redirect("Login.aspx")
        End Try
    End Sub

    Public Sub Validar_Acceso_Por_Clave_Temporal(ByVal Clave_Temporal As String, ByVal Id_Call As String)
        Dim Estado_De_La_Clave As String

        Estado_De_La_Clave = Reader("Activo", "select Activo from entidades where Entidad_Hash = '" & Id_Call.ToString & "' and Clave_Temporal = '" & Clave_Temporal.ToString & "' and Activo = 'SI'")
        If Estado_De_La_Clave <> "SI" Then
            HttpContext.Current.Response.Redirect("Login.aspx")
        End If
    End Sub

    Public Function Reader(ByVal V As String, ByVal Q As String)
        Dim cn16 As MySqlConnection
        Dim cmd16 As MySqlCommand
        Dim rdr16 As MySqlDataReader
        Dim Conexion As String = (C.StrConexion())
        Dim Valor As String

        Try
            cn16 = New MySqlConnection(Conexion)
            cmd16 = New MySqlCommand(Q.ToString, cn16)
            cn16.Open()
            rdr16 = cmd16.ExecuteReader(CommandBehavior.CloseConnection)
            If rdr16.Read() Then
                Valor = rdr16(V).ToString
                rdr16.Close()
                cn16.Close()
                Return Valor.ToString
            Else
                rdr16.Close()
                cn16.Close()
                Valor = "Vacio"
                Return Valor.ToString
            End If
        Catch ex As Exception
        End Try
    End Function
End Class