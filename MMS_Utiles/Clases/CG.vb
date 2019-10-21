Imports MySql.Data.MySqlClient
Imports System.Net
Imports System.Web.Routing
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Imports System.Security
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports System.Web

Public Class CG
    Inherits System.Web.UI.Page

    Dim cookie_Nombre As String = "MMS-C"
    Private objConn As New MySqlConnection
    Public Function versionapp() As String

        Dim Numero_Version As String

        Numero_Version = "1.1.6.2"

        Return Version_String_NO_Borrar.ToString & Numero_Version.ToString
    End Function
    'CAMBIAR DESPUES DEL DEBUGGG!!!! NO TE OLVIDESS CABEZAAAA!!!
    '#####################################################'###########TESTING LOCAL####################'#########################################################

    ' EN TESTING LOCAL (DESDE LA OFICINA TESTING)

    Public Cookie_HTPPONLY As Boolean = False
    Public Produccion As Boolean = False
    Public Cookie_SSL As Boolean = False
    Public Publico_Acceso As Boolean = False
    '#####################################################'############TESTING PUBLICO#############################'###############################################

    ' EN TESTING PUBLICO (DESDE TU CASA TESTING)

    'Public Cookie_HTPPONLY As Boolean = False
    'Public Produccion As Boolean = False
    'Public Cookie_SSL As Boolean = False
    'Public Publico_Acceso As Boolean = True

    '#####################################################'############PRODUCCION PUBLICO########################'##################################################

    'EN PRODUCCION PUBLICO (DESDE TU CASA EN PRODUCCION)

    'Public Cookie_HTPPONLY As Boolean = False
    'Public Cookie_SSL As Boolean = False
    'Public Produccion As Boolean = True
    'Public Publico_Acceso As Boolean = True
    '#####################################################'############EN PRODUCCION LOCAL (ESTE HAY QUE SUBIR A PRODUCCION)!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!#######

    'EN PRODUCCION LOCAL (ESTE HAY QUE SUBIR A PRODUCCION, SOLO SE USA CUANDO SE SUBE AL SERVIDOR)!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    'Public Cookie_HTPPONLY As Boolean = True
    'Public Cookie_SSL As Boolean = True
    'Public Produccion As Boolean = True
    'Public Publico_Acceso As Boolean = False
    '#####################################################'#####################################################'####################################################

    '#####################################################'###########TESTING PUBLICADOOOOO####################'#########################################################

    ' EN TESTING PUBLICADO (SUBIDO A TESTING)

    'Public Cookie_HTPPONLY As Boolean = True
    'Public Produccion As Boolean = False
    'Public Cookie_SSL As Boolean = True
    'Public Publico_Acceso As Boolean = False

    '######################## HASHEO ######################
    Public FRASE As String = "2ic04ncr!3t470s"
    Public SALTVALOR As String = "Q3u21L70i"
    Public HASHTIPO As String = "SHA1"
    Public ITERACIONES As Integer = 70
    Public INITVECTOROK As String = "!@i313cL21vU9LDn"
    Public TAMAÑO As Integer = 256
    Public CLAVEURLENCRIPT As String = "P2T0y95qQ!123iY79"
    Public Intervalo_Dias_select As String = "100"
    Public ENCRYPT_ALERTA As String = "L9s_G8stA_L@_p13r4!_"
    Public DIVISOR_KEY_SIZE As Integer = 8
    Public DAMOS_BASE_H As String = "B4S3"
    Public MULTI_CUE_H As String = "MUL71"
    Public SUB_DOC_H As String = "SU8D03"
    Public Version_String_NO_Borrar As String = "Versión: "

    '#####################################################

    '-- Función privada para el manejo del String de conexion (Esta función me "construye" y regresa mi cadena de Conexión)
    Public Function StrConexion() As String
        '-- Declaro mi variable Cadena de Conexión
        Dim strConn As String
        Try

            If Produccion = True Then

                If Publico_Acceso = True Then
                    'PRODUCCION_PUBLIC
                    '  strConn = "data source = ajproducciones.dyndns.org; Port=3306; initial catalog = mms_virtual_power; user id = root; password = fernandomiras"
                    '  strConn = "data source = 127.0.0.1; Port=3306; initial catalog = mms_virtual_power; user id = root; password = fernandomiras"

                Else

                    'PRODUCCION_LOCAL AJ PRODUCCIONES  my_money_system
                    ' strConn = "data source = 192.168.1.160 ; Port=3306; initial catalog = my_money_system; user id = mms; password = mms2016"


                    'PRODUCCION_LOCAL VIRTUAL POWER   mms_virtual_power
                    strConn = "data source = 192.168.1.160 ; Port=3306; initial catalog = mms_virtual_power; user id = mms.vi.power; password = mms_virtual_power2017"
                    '  strConn = "data source = 127.0.0.1; initial catalog = mms_virtual_power; user id = root; password = fernandomiras"

                End If

            Else

                If Publico_Acceso = True Then

                    'TESTING_PUBLIC  AJ PRODUCCIONES
                    '  strConn = "data source = ajproducciones.dyndns.org; Port=3306; initial catalog = mms_virtual_power; user id = root; password = fernandomiras"
                Else

                    'TESTING LOCAL
                    strConn = "data source = 127.0.0.1; initial catalog = mms; user id = root; password = fernandomiras"

                End If

            End If

            ' CAMBIAR LA VERSION TAMBIEN y el SUB MENSAJES INSTANTANEOS! 
        Catch ex As Exception
        End Try
        Return strConn
    End Function

    Public Sub Modificar_Valor_Cookie(ByVal Variable_Donde_Se_Guarda As String, Nuevo_Valor As String)
        Try
            Dim Galleta As HttpCookie
            Dim Nuevo_Valor_F As String = Encrypt(Nuevo_Valor) 'asd

            Galleta = HttpContext.Current.Request.Cookies("MMS-C")
            Galleta.HttpOnly = Cookie_HTPPONLY
            Galleta.Secure = Cookie_SSL
            Galleta.Values.Set(Variable_Donde_Se_Guarda, Nuevo_Valor_F)
            Galleta.Expires = DateTime.Now.AddDays(1) '1 dia
            HttpContext.Current.Response.Cookies.Set(Galleta)
        Catch ex As Exception
            HttpContext.Current.Response.Redirect("Login.aspx")
        End Try
    End Sub

  
    Public Sub eliminar_cookies(ByVal cookie_nombre As String)
        Dim acookie As New HttpCookie(cookie_nombre.ToString)
        acookie.HttpOnly = Cookie_HTPPONLY
        acookie.Secure = Cookie_SSL
        acookie.Values("IU") = ""
        acookie.Values("INA") = ""
        acookie.Values("IE") = ""
        acookie.Values("E") = ""
        acookie.Values("IEH") = ""
        acookie.Values("ICT") = ""
        acookie.Values("ITD") = ""

        acookie.Expires = DateTime.Now.AddDays(-450)
        HttpContext.Current.Response.Cookies.Add(acookie)
    End Sub

    Public Sub Comillas(ByVal O As Object, ByVal caracter As String, ByVal mensaje As String)
        Dim validar As String = Validar_Comillas(O, caracter.ToString)
        If validar = 1 Then
            MessageBoxError(mensaje.ToString)
            Return
        End If
    End Sub

    Public Function Validar_Si_Existe_Cookie(ByVal cookie_Nombres As String)
        If Request.Cookies(cookie_Nombres.ToString) Is Nothing Then
            Return "NO"
        Else
            Return "SI"
        End If
    End Function

    Public Sub MessageBoxError(ByVal msg As String)
        Dim lbl As New Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine & "alertify.error('" + msg + "')</script>"
        Page.Controls.Add(lbl)
    End Sub

    Public Function Encrypt(ByVal plainText As String) As String
        Try

            Dim passPhrase As String = FRASE
            Dim saltValue As String = SALTVALOR
            Dim hashAlgorithm As String = HASHTIPO

            Dim passwordIterations As Integer = ITERACIONES
            Dim initVector As String = INITVECTOROK
            Dim keySize As Integer = TAMAÑO

            Dim initVectorBytes As Byte() = Encoding.ASCII.GetBytes(initVector)
            Dim saltValueBytes As Byte() = Encoding.ASCII.GetBytes(saltValue)

            Dim plainTextBytes As Byte() = Encoding.UTF8.GetBytes(plainText)


            Dim password As New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)

            Dim keyBytes As Byte() = password.GetBytes(keySize \ DIVISOR_KEY_SIZE)
            Dim symmetricKey As New RijndaelManaged()

            symmetricKey.Mode = CipherMode.CBC

            Dim encryptor As ICryptoTransform = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)

            Dim memoryStream As New MemoryStream()
            Dim cryptoStream As New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)

            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)
            cryptoStream.FlushFinalBlock()
            Dim cipherTextBytes As Byte() = memoryStream.ToArray()
            memoryStream.Close()
            cryptoStream.Close()
            Dim cipherText As String = Convert.ToBase64String(cipherTextBytes)
            Return cipherText

        Catch ex As Exception
            HttpContext.Current.Response.Redirect("Login.aspx")
        End Try
    End Function


   
    Public Function Decrypt(ByVal cipherText As String) As String
        Try



            Dim passPhrase As String = FRASE
            Dim saltValue As String = SALTVALOR
            Dim hashAlgorithm As String = HASHTIPO


            Dim passwordIterations As Integer = ITERACIONES
            ' Dim initVector As String = "@1B2c3D4e5F6g7H8"
            Dim initVector As String = INITVECTOROK
            Dim keySize As Integer = TAMAÑO
            ' Convert strings defining encryption key characteristics into byte
            ' arrays. Let us assume that strings only contain ASCII codes.
            ' If strings include Unicode characters, use Unicode, UTF7, or UTF8
            ' encoding.
            Dim initVectorBytes As Byte() = Encoding.ASCII.GetBytes(initVector)
            Dim saltValueBytes As Byte() = Encoding.ASCII.GetBytes(saltValue)

            ' Convert our ciphertext into a byte array.
            Dim cipherTextBytes As Byte() = Convert.FromBase64String(cipherText)

            ' First, we must create a password, from which the key will be 
            ' derived. This password will be generated from the specified 
            ' passphrase and salt value. The password will be created using
            ' the specified hash algorithm. Password creation can be done in
            ' several iterations.
            Dim password As New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)

            ' Use the password to generate pseudo-random bytes for the encryption
            ' key. Specify the size of the key in bytes (instead of bits).
            Dim keyBytes As Byte() = password.GetBytes(keySize \ DIVISOR_KEY_SIZE)

            ' Create uninitialized Rijndael encryption object.
            Dim symmetricKey As New RijndaelManaged()

            ' It is reasonable to set encryption mode to Cipher Block Chaining
            ' (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC

            ' Generate decryptor from the existing key bytes and initialization 
            ' vector. Key size will be defined based on the number of the key 
            ' bytes.
            Dim decryptor As ICryptoTransform = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes)

            ' Define memory stream which will be used to hold encrypted data.
            Dim memoryStream As New MemoryStream(cipherTextBytes)

            ' Define cryptographic stream (always use Read mode for encryption).
            Dim cryptoStream As New CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)

            ' Since at this point we don't know what the size of decrypted data
            ' will be, allocate the buffer long enough to hold ciphertext;
            ' plaintext is never longer than ciphertext.
            Dim plainTextBytes As Byte() = New Byte(cipherTextBytes.Length - 1) {}

            ' Start decrypting.
            Dim decryptedByteCount As Integer = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length)

            ' Close both streams.
            memoryStream.Close()
            cryptoStream.Close()

            ' Convert decrypted data into a string. 
            ' Let us assume that the original plaintext string was UTF8-encoded.
            Dim plainText As String = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount)

            ' Return decrypted string.   
            Return plainText

        Catch ex As Exception
            HttpContext.Current.Response.Redirect("Login.aspx")
        End Try
    End Function



    Public Sub Crear_Cookie(ByVal Id_Usuario As String, ByVal Nombre_Usuario As String, ByVal Entidad_Nombre As String, ByVal ID_Entidad As String, ByVal Entidad_Hash As String, ByVal Clave_Temporal As String, ByVal Tipo_Datos As String)

        Dim Id_Usuario_F As String = Encrypt(Id_Usuario)
        Dim Nombre_Usuario_F As String = Encrypt(Nombre_Usuario)
        Dim Entidad_Nombre_F As String = Encrypt(Entidad_Nombre)
        Dim Entidad_ID_F As String = Encrypt(ID_Entidad)
        Dim Entidad_Hash_F As String = Encrypt(Entidad_Hash)
        Dim Clave_Temporal_F As String = Encrypt(Clave_Temporal)
        Dim Tipo_Datos_F As String = Encrypt(Tipo_Datos)

        Dim aCookie As New HttpCookie(cookie_Nombre.ToString)
        aCookie.HttpOnly = Cookie_HTPPONLY
        aCookie.Secure = Cookie_SSL

        aCookie.Values("IU") = Id_Usuario_F         'ID USER
        aCookie.Values("INA") = Nombre_Usuario_F    'NOMBRE APELLIDO USUARIO
        aCookie.Values("E") = Entidad_Nombre_F     'ENTIDAD NOMBRE
        aCookie.Values("IE") = Entidad_ID_F        'ID NOMBRE
        aCookie.Values("IEH") = Entidad_Hash_F      'HASH ENTIDAD
        aCookie.Values("ICT") = Clave_Temporal_F    'CLAVE TEMPORAL
        aCookie.Values("ITD") = Tipo_Datos_F        'TIPO DE DATOS (PERSONAL-CLARO-MOVISTAR-TODOS)

        aCookie.Expires = DateTime.Now.AddDays(1) ' Expira a las 24 horas
        HttpContext.Current.Response.Cookies.Add(aCookie)
    End Sub

    Public Function Validar_Comillas(ByVal objeto As String, ByVal Caracter As String)
        Dim caracteres As String = Caracter
        Dim Caracterenobjeto As Object = objeto

        If Caracterenobjeto.Contains(caracteres.ToString) = True Then
            Return 1
        Else
            Return 0
        End If

    End Function

    Public Function encodercokie(ByVal valor As String)
        Try
            Dim byt As Byte() = System.Text.Encoding.UTF8.GetBytes(valor)
            Return Convert.ToBase64String(byt)
        Catch ex As Exception
            HttpContext.Current.Response.Redirect("Login.aspx")
        End Try
    End Function

    Public Function decodercokie(ByVal Valor As String)
        Try
            Dim b As Byte() = Convert.FromBase64String(Valor)
            Return System.Text.Encoding.UTF8.GetString(b)
        Catch ex As Exception
            HttpContext.Current.Response.Redirect("Login.aspx")
        End Try
    End Function

    Public Function Insert_Update(ByVal Q As String)
        Dim Estado As Integer ' 1 = ok; 0 = No ok
        Try
            Dim ds6 As New DataSet
            Dim Conexion6 As String = (StrConexion())
            Dim Cmd6 As New MySqlCommand
            Dim Cnn6 As New MySqlConnection
            Dim Sql6 As String
            Dim Da6 As New MySqlDataAdapter

            Sql6 = Q.ToString

            Cnn6 = New MySqlConnection(Conexion6)
            ds6.Clear()
            Cnn6.Open()
            Da6 = New MySqlDataAdapter(Sql6, Cnn6)
            Da6.Fill(ds6)
            Cnn6.Close()
            Estado = 1
            Return Estado
        Catch ex As Exception
            Estado = 0
            Return Estado
        End Try
    End Function

    Public Sub Selectt(O As Object, ByVal Q As String)
        'ACTUALIZACION DE LA GRILLA
        O.Visible = True
        Dim ds As New DataSet
        Dim Conexion As String = (StrConexion())
        Dim Cmd As New MySqlCommand
        Dim Cnn As New MySqlConnection
        Dim Sql As String
        Dim Da As New MySqlDataAdapter

        Sql = Q.ToString
        Cnn = New MySqlConnection(Conexion)
        Cnn.Open()
        Da = New MySqlDataAdapter(Sql, Cnn)
        Da.Fill(ds)
        O.DataSource = ds
        Cnn.Close()
        O.DataBind()
    End Sub

    Public Function Reader(ByVal V As String, ByVal Q As String)
        Dim cn16 As MySqlConnection
        Dim cmd16 As MySqlCommand
        Dim rdr16 As MySqlDataReader
        Dim Conexion As String = (StrConexion())
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


    Public Function Reader_Muchos_Emails(ByVal V As String, ByVal Q As String)
        Dim cn16 As MySqlConnection
        Dim cmd16 As MySqlCommand
        Dim rdr16 As MySqlDataReader
        Dim Conexion As String = (StrConexion())
        Dim Valor As String = ""

        Try
            cn16 = New MySqlConnection(Conexion)
            cmd16 = New MySqlCommand(Q.ToString, cn16)
            cn16.Open()
            rdr16 = cmd16.ExecuteReader()
            If rdr16.Read() Then

                Do While rdr16.Read()
                    Valor = Valor & ";" & rdr16(V).ToString
                Loop


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


    Public Function Reader_Enteros(ByVal V As String, ByVal Q As String)
        Dim cn16 As MySqlConnection
        Dim cmd16 As MySqlCommand
        Dim rdr16 As MySqlDataReader
        Dim Conexion As String = (StrConexion())
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

                If Valor = "" Then
                    Valor = 0
                End If
                Return Valor.ToString
            Else
                rdr16.Close()
                cn16.Close()
                Valor = 0
                Return Valor.ToString
            End If
        Catch ex As Exception
        End Try
    End Function

    Public Sub Crear_Cokie_de_cajas(ByVal idcaja As String)
        ' Borra Cookie
        Dim Excookie As New HttpCookie("CM")
        Excookie.Values("IC") = "0"
        Excookie.Expires = DateTime.Now.AddDays(-2)

        HttpContext.Current.Response.Cookies.Add(Excookie)

        ' Crea Cookie
        Dim Idcajafinal As String = encodercokie(idcaja)
        Dim aCookie As New HttpCookie("CM")
        aCookie.Values("IC") = Idcajafinal

        aCookie.Expires = DateTime.Now.AddDays(1)
        HttpContext.Current.Response.Cookies.Add(aCookie)
    End Sub

    Public Function Pedir_Valoreas_a_Cookie(ByVal CookieNombre As String, ByVal Tipo As String)
        Try

            If CookieNombre = "MMS-C" Then

                Dim Id_Usuario As String = HttpContext.Current.Request.Cookies(CookieNombre)("IU")
                Dim Nombre_Usuario As String = HttpContext.Current.Request.Cookies(CookieNombre)("INA")
                Dim Entidad_ID As String = HttpContext.Current.Request.Cookies(CookieNombre)("IE")
                Dim Entidad_Nombre As String = HttpContext.Current.Request.Cookies(CookieNombre)("E")
                Dim Entidad_Hash As String = HttpContext.Current.Request.Cookies(CookieNombre)("IEH")
                Dim Clave_Temporal As String = HttpContext.Current.Request.Cookies(CookieNombre)("ICT")
                Dim Tipo_Datos As String = HttpContext.Current.Request.Cookies(CookieNombre)("ITD")

                Dim Id_Usuario_F As String = Decrypt(Id_Usuario)

                Dim Nombre_Usuario_F As String = Decrypt(Nombre_Usuario)

                Dim Entidad_Nombre_F As String = Decrypt(Entidad_Nombre)

                Dim Entidad_ID_F As String = Decrypt(Entidad_ID)

                Dim Entidad_Hash_F As String = Decrypt(Entidad_Hash)

                Dim Clave_Temporal_F As String = Decrypt(Clave_Temporal)

                Dim Tipo_Datos_F As String = Decrypt(Tipo_Datos)

                If Tipo = "IU" Then
                    Return Id_Usuario_F.ToString

                End If


                If Tipo = "INA" Then
                    Return Nombre_Usuario_F.ToString

                End If


                If Tipo = "IE" Then
                    Return Entidad_ID_F.ToString

                End If

                If Tipo = "E" Then
                    Return Entidad_Nombre_F.ToString

                End If


                If Tipo = "IEH" Then
                    Return Entidad_Hash_F.ToString

                End If

                If Tipo = "ICT" Then
                    Return Clave_Temporal_F.ToString

                End If


                If Tipo = "ITD" Then
                    Return Tipo_Datos_F.ToString
                End If


            End If
        Catch ex As Exception
            HttpContext.Current.Response.Redirect("Login.aspx")
        End Try
    End Function



    Public Sub Llenar_Combos(ByVal O As Object, ByVal V As String, ByVal Q As String)
        Dim ds2 As New DataSet
        Dim sql2 As String
        Dim Cnn2 As New MySqlConnection
        Dim Cmd2 As New MySqlCommand
        Dim Da2 As MySqlDataAdapter
        Try
            sql2 = Q.ToString
            Cnn2 = New MySqlConnection(StrConexion)
            ds2.Clear()
            Cnn2.Open()
            Da2 = New MySqlDataAdapter(sql2, Cnn2)
            Da2.Fill(ds2)
            O.DataSource = ds2
            O.DataSource = ds2
            O.DataValueField = V.ToString
            O.DataTextField = V.ToString
            O.DataBind()
            Cnn2.Close()
        Catch ex As Exception
        End Try
    End Sub

    Public Function Obtener_IP_Cliente()
        Try


            Dim Web_Consulta As New System.Net.WebClient
            Dim IP_Publica_Cliente As String = Web_Consulta.DownloadString("http://myip.dnsomatic.com/")
            Return IP_Publica_Cliente
        Catch ex As Exception

        End Try
    End Function

    Public Function Encriptar_Url(clearText As String) As String
        Try

            Dim EncryptionKey As String = CLAVEURLENCRIPT
            'Dim EncryptionKey As String = "MAKV2SPBNI99212"
            Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(clearText)
            Using encryptor As Aes = Aes.Create()
                Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
                 &H65, &H64, &H76, &H65, &H64, &H65, _
                 &H76})
                encryptor.Key = pdb.GetBytes(32)
                encryptor.IV = pdb.GetBytes(16)
                Using ms As New MemoryStream()
                    Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                        cs.Write(clearBytes, 0, clearBytes.Length)
                        cs.Close()
                    End Using
                    clearText = Convert.ToBase64String(ms.ToArray())
                End Using
            End Using
            Return clearText
        Catch ex As Exception
            HttpContext.Current.Response.Redirect("Login.aspx")
        End Try
    End Function


    Public Function Decriptar_Url(cipherText As String) As String
        Try
            'Dim EncryptionKey As String = "MAKV2SPBNI99212"
            Dim EncryptionKey As String = CLAVEURLENCRIPT
            cipherText = cipherText.Replace(" ", "+")
            Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)
            Using encryptor As Aes = Aes.Create()
                Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
                 &H65, &H64, &H76, &H65, &H64, &H65, _
                 &H76})
                encryptor.Key = pdb.GetBytes(32)
                encryptor.IV = pdb.GetBytes(16)
                Using ms As New MemoryStream()
                    Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                        cs.Write(cipherBytes, 0, cipherBytes.Length)
                        cs.Close()
                    End Using
                    cipherText = Encoding.Unicode.GetString(ms.ToArray())
                End Using
            End Using
            Return cipherText
        Catch ex As Exception
            HttpContext.Current.Response.Redirect("Login.aspx")
        End Try
    End Function

    Public Sub Validar_Acceso_Por_Clave_Temporal(ByVal Clave_Temporal As String, ByVal Id_Call As String)
        Dim Estado_De_La_Clave As String

        Estado_De_La_Clave = Reader("Estado", "select Estado from keys_call where Id_Call = '" & Id_Call.ToString & "' and Clave_Temporal_Login = '" & Clave_Temporal.ToString & "' and Activo = 'SI'")
        If Estado_De_La_Clave <> "OK" Then
            HttpContext.Current.Response.Redirect("Login.aspx")
        End If
    End Sub


    'Public FRASE As String = "N1coEncr3ptors4"
    'Public SALTVALOR As String = "Qeu3sEs70"
    'Public HASHTIPO As String = "SHA1"
    'Public ITERACIONES As Integer = 2
    'Public INITVECTOROK As String = "@3J34cE7hfGRS4h8"
    'Public TAMAÑO As Integer = 256
    'Public CLAVEURLENCRIPT As String = "NIV0URLD3CRIP72"


End Class
