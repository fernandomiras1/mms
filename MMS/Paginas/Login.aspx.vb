Imports MySql.Data.MySqlClient
Imports MMS_Utiles
Public Class Login
    Inherits System.Web.UI.Page
    Private C As New CG
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            C.eliminar_cookies("MMS-C")
            LabelVer.InnerText = C.versionapp.ToString
        End If
    End Sub

    Protected Sub ingrearL_Click(sender As Object, e As EventArgs) Handles ingrearL.Click
        Login()
    End Sub

    Public Sub Login()

        Dim usuario As String
        Dim Passw As String
        Dim Ip_Cliente As String = Obtener_IP_Cliente()
        usuario = Me.usuarios.Value.ToString
        Passw = contras.Value.ToString
        usuario = usuario.Replace("'", "").Replace("""", "").Replace("%", "").Replace("\x00", "").Replace("\x1a", "").Replace("\", "").Replace("/", "")
        Passw = Passw.Replace("'", "").Replace("""", "").Replace("%", "").Replace("\x00", "").Replace("\x1a", "").Replace("\", "").Replace("/", "")

        If usuario = String.Empty Then
            MessageBoxError("Ingrese el Usuario")
            Return
        End If
        If Passw = String.Empty Then
            MessageBoxError("Ingrese la Contraseña")
            Return
        End If

        Dim cn16 As MySqlConnection
        Dim cmd16 As MySqlCommand
        Dim rdr16 As MySqlDataReader
        Dim Conexion As String = C.StrConexion

        Dim Id_User As String
        Dim Nombre_Apellido As String
        Dim Entidad As String
        Dim ID_Entidad As String
        Dim Entidad_Hash As String
        Dim Clave_Temporal As String
        Dim Tipo_Datos As String
        Dim Version As String = LabelVer.InnerText.Replace("Version ", "").ToString
        ' La palabra clave para Desencrypar el codigo es: m.m.y_#%_2017
        Dim PassEncryptado As String
        PassEncryptado = Crypt.Encrypt(Passw.ToString, "m.m.y_#%_2017")

        Try
            cn16 = New MySqlConnection(Conexion)
            cmd16 = New MySqlCommand("select l.Id ,l.Nombre_Apellido,l.Cargo, e.Entidad, e.id as id_entidad , e.Entidad_Hash, e.Clave_Temporal  from  login as l , entidades as e where l.Entidad = e.Entidad_Hash and l.Usuario = '" & usuario.ToString & "' and l.Pass = '" & PassEncryptado.ToString & "' and l.Activo = 1 limit 1", cn16)
            cn16.Open()
            rdr16 = cmd16.ExecuteReader(CommandBehavior.CloseConnection)
            If rdr16.Read() Then
                Id_User = rdr16("Id").ToString
                Nombre_Apellido = rdr16("Nombre_Apellido").ToString
                Entidad = rdr16("Entidad").ToString
                ID_Entidad = rdr16("id_entidad").ToString
                Entidad_Hash = rdr16("Entidad_Hash").ToString
                Clave_Temporal = rdr16("Clave_Temporal").ToString
                Tipo_Datos = rdr16("Cargo").ToString
                'Crear Cookie
                C.Crear_Cookie(Id_User, Nombre_Apellido, Entidad, ID_Entidad, Entidad_Hash, Clave_Temporal, Tipo_Datos)

                rdr16.Close()
                cn16.Close()
                'Insertar Ingreso OK

                C.Insert_Update("INSERT INTO `log_conexiones` (`Entidad`, `Email`, `Pass`, `Ip_Cliente`, `Bloqueado`, `Version_Actual`) VALUES ('" & Entidad_Hash.ToString & "', '" & usuario.ToString & "', '" & PassEncryptado.ToString & "', '" & Ip_Cliente.ToString & "', 'NO', '" & Version.ToString & "');")
                Response.Redirect("Home.aspx", False)

            Else
                MessageBoxError("Usuario o Contraseña Incorrecto")
                rdr16.Close()
                cn16.Close()
                'Insertar Ingreso NO OK

                C.Insert_Update("INSERT INTO `log_conexiones` (`Entidad`, `Email`, `Pass`, `Ip_Cliente`, `Bloqueado`, `Version_Actual`) VALUES ('INCORRECTO', '" & usuario.ToString & "', '" & PassEncryptado.ToString & "', '" & Ip_Cliente.ToString & "', 'SI', '" & Version.ToString & "');")

            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Sub MessageBoxError(ByVal msg As String)
        Dim lbl As New Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine & "alertify.error('" + msg + "')</script>"
        Page.Controls.Add(lbl)
    End Sub

    Private Function Obtener_IP_Cliente() As String

        Dim ipaddress As String
        ipaddress = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        If ipaddress = "" Or ipaddress Is Nothing Then
            ipaddress = Request.ServerVariables("REMOTE_ADDR")
        End If
        Return ipaddress
    End Function



End Class