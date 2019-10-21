Imports MySql.Data.MySqlClient
Imports MMS_Utiles
Public Class Mod_Home
    Inherits System.Web.UI.Page
    Private C As New CG
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Traer_Datos()
        End If
    End Sub

    Public Sub Traer_Datos()

        If Request.QueryString("E") <> Nothing Then

            Dim Id_User As String = Request.QueryString("E")
            Dim Id_User_Final As String = C.Decriptar_Url(Id_User.ToString)
            TituloPrincipal.InnerText = "Modificación y Eliminación de Ingresos"
            SubmitGuardar.Text = "Modificar"
            btnEliminar.Visible = True
            Dim cn16 As MySqlConnection
            Dim cmd16 As MySqlCommand
            Dim rdr16 As MySqlDataReader
            Dim Conexion As String = C.StrConexion

            Dim tipo As String
            Dim cate As String
            Dim nombre As String
            Dim precio As String
            Dim formapago As String
            Dim fecha As Date
            Dim Obs As String

            Try
                cn16 = New MySqlConnection(Conexion)
                cmd16 = New MySqlCommand("select * from ingresos where id = '" & Id_User_Final.ToString & "' and Borrar='NO' limit 1", cn16)
                cn16.Open()
                rdr16 = cmd16.ExecuteReader(CommandBehavior.CloseConnection)
                If rdr16.Read() Then

                    tipo = rdr16("Tipo").ToString
                    cate = rdr16("Categoría").ToString
                    nombre = rdr16("Nombre").ToString
                    precio = rdr16("Precio").ToString
                    formapago = rdr16("Forma_Pago").ToString
                    fecha = rdr16("Fecha")
                    Obs = rdr16("Observación").ToString

                    DropDownTIPO.SelectedValue = tipo.ToString
                    DropDownCate.Items.Add(cate.ToString)
                    DropDownSubCategoria.Items.Add(nombre.ToString)
                    DropDownFormaPago.SelectedValue = formapago.ToString
                    DateTimeFecha.Text = fecha.Date.ToString("yyyy-MM-dd")
                    txtPrecio.Text = precio.ToString
                    TextBoxDetalle.Text = Obs.ToString


                End If
            Catch ex As Exception

            End Try
        Else
            btnEliminar.Visible = False
            TituloPrincipal.InnerText = "Nuevo"
        End If
     
    End Sub

    Protected Sub SubmitGuardar_Click(sender As Object, e As EventArgs) Handles SubmitGuardar.Click
        If DropDownCate.Text = String.Empty Then
            MessageBoxError("Ingrese la Categoria")
            Return
        End If
        If DropDownSubCategoria.Text = String.Empty Then
            MessageBoxError("Ingrese la Sub Categoria")
            Return
        End If
        If DateTimeFecha.Text = String.Empty Then
            MessageBoxError("Ingrese la Fecha")
            Return
        End If
        If txtPrecio.Text = String.Empty Then
            MessageBoxError("Ingrese el Precio")
            Return
        End If
        Dim Entidad As String = C.Pedir_Valoreas_a_Cookie("MMS-C", "IEH")
        Dim precioFinal As String
        precioFinal = txtPrecio.Text.Replace(",", ".")


        If Request.QueryString("E") <> Nothing Then

            Dim Id_User As String = Request.QueryString("E")
            Dim Id_User_Final As String = C.Decriptar_Url(Id_User.ToString)

            C.Insert_Update("UPDATE `ingresos` SET `Categoría`='" & DropDownCate.Text & "', `Nombre`='" & DropDownSubCategoria.Text & "', `Entidad`='" & Entidad.ToString & "' , `Observación`='" & TextBoxDetalle.Text & "',`Tipo`='" & DropDownTIPO.Text & "' , `Precio`='" & precioFinal & "', `Forma_Pago`='" & DropDownFormaPago.Text & "', `Fecha`='" & DateTimeFecha.Text & "' WHERE  `id`='" & Id_User_Final.ToString & "'")
            MessageBoxOK("El Dato fue Modificado con Exito.")

        Else
            ' Insert
            C.Insert_Update("INSERT INTO `ingresos` (`Tipo`,`Categoría`, `Entidad` ,`Nombre`, `Observación`, `Precio` , `Forma_Pago` ,`Fecha`, `Borrar`) VALUES (('" & DropDownTIPO.Text & "'),('" & DropDownCate.Text & "'), ('" & Entidad.ToString & "')('" & DropDownSubCategoria.Text & "'), ('" & TextBoxDetalle.Text & "'), ('" & precioFinal & "'),('" & DropDownFormaPago.Text & "'),('" & DateTimeFecha.Text & "'), 'NO');")
            MessageBoxOK("El Dato fue Guardado con Exito.")

        End If

    End Sub

    Protected Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click

        If Request.QueryString("E") <> Nothing Then

            If DropDownCate.Text = String.Empty Then
                MessageBoxError("Ingrese la Categoria")
                Return
            End If
            If DropDownSubCategoria.Text = String.Empty Then
                MessageBoxError("Ingrese la Sub Categoria")
                Return
            End If

            Dim Id_User As String = Request.QueryString("E")
            Dim Id_User_Final As String = C.Decriptar_Url(Id_User.ToString)

            C.Insert_Update("UPDATE `ingresos` SET `Borrar`='SI' WHERE `Id`=('" & Id_User_Final.ToString & "');")
            MessageBoxOK("El Dato fue Eliminado con Exito.")

        Else

            MessageBoxError("Seleccione el Dato a Eliminar.")

        End If


    End Sub

    Protected Sub ComboTipo_Click(sender As Object, e As EventArgs) Handles ComboTipo.Click
        'El Boton TIPO
        CargarComboCategoria()
    End Sub

    Protected Sub ComboCategoria_Click(sender As Object, e As EventArgs) Handles ComboCategoria.Click
        'El Boton Categoria
        CargarComboSub_Categoria()
    End Sub

    Public Sub CargarComboCategoria()
        Dim Entidad As String = C.Pedir_Valoreas_a_Cookie("MMS-C", "IEH")
        'and Entidad='" & Entidad.ToString & "' 
        C.Llenar_Combos(DropDownCate, "tipo", "select tipo from combo_tipo where `ingreso_egreso`='" & DropDownTIPO.Text & "' and Entidad='" & Entidad.ToString & "' and Borrar='NO' ORDER BY tipo ASC")
    End Sub

    Public Sub CargarComboSub_Categoria()
        Dim Entidad As String = C.Pedir_Valoreas_a_Cookie("MMS-C", "IEH")
        'and Entidad='" & Entidad.ToString & "' 
        C.Llenar_Combos(DropDownSubCategoria, "Nombre", "select Nombre from combo_tipo_detalle where `Tipo`='" & DropDownCate.Text & "' and Entidad='" & Entidad.ToString & "' and Borrar='NO' ORDER BY Nombre ASC")

    End Sub

    Public Sub MessageBoxError(ByVal msg As String)
        Dim lbl As New Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine & "alertify.error('" + msg + "')</script>"
        Page.Controls.Add(lbl)
    End Sub
    Public Sub MessageBoxOK(ByVal msg As String)
        Dim lbl As New Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine & "alertify.success('" + msg + "')</script>"
        Page.Controls.Add(lbl)
    End Sub

End Class