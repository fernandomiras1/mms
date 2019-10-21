
Imports MMS_Utiles
Imports System.Web.Mvc

Public Class Home
    Inherits System.Web.UI.Page
    Private C As New CG
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            '   Calcular()

        End If

        ' ActualizarGrilla()

    End Sub
    'Public Sub Calcular()

    '    ' CalcularCaja
    '    Dim Entidad As String = C.Pedir_Valoreas_a_Cookie("MMS-C", "IEH")
    '    Dim totalIngrso As Double = C.Reader_Enteros("Precio", "select SUM(Precio) as 'Precio' from ingresos where Tipo='INGRESO' and Entidad='" & Entidad.ToString & "' and Borrar='NO' limit 1")
    '    Dim totalEgreso As Double = C.Reader_Enteros("Precio", "select SUM(Precio) as 'Precio' from ingresos where Tipo='EGRESO' and Entidad='" & Entidad.ToString & "' and Borrar='NO'  limit 1")

    '    ' aca hacemos la resta 
    '    Dim resu As Double
    '    Dim resultString As String
    '    resu = totalIngrso - totalEgreso
    '    resultString = resu.ToString("#.##")
    '    ingresos_num.InnerText = totalIngrso.ToString("#.##")
    '    egresos_num.InnerText = totalEgreso.ToString("#.##")
    '    total_num.InnerText = resultString

    'End Sub

    Public Sub ActualizarGrilla()
        Dim Entidad As String = C.Pedir_Valoreas_a_Cookie("MMS-C", "IEH")
        'and Entidad='" & Entidad.ToString & "' 
        'C.Selectt(GridView1, "select id,Tipo,Categoría,Nombre,Observación,Precio,Fecha from ingresos where DATE_SUB(CURDATE(),INTERVAL 30 DAY)<=Fecha and  Borrar='NO' order by Id DESC")
        'actual
        'C.Selectt(GridView1, "select id,Tipo,Categoría,Nombre,Observación,Precio,Forma_Pago as 'Forma Pago' ,Fecha from ingresos where Entidad='" & Entidad.ToString & "' and Borrar='NO' order by Id DESC")

    End Sub

    'Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
    '    GridView1.PageIndex = e.NewPageIndex
    '    GridView1.DataBind()
    'End Sub

    Public Sub Editar_Ingresos()

        Dim ID As String
        Dim ID_Hash As String
        ID = 1
        'ID = GridView1.SelectedRow.Cells(1).Text
        ID_Hash = C.Encriptar_Url(ID)

        Dim urlFinal As String = "Mod_Home.aspx?E=" & ID_Hash.ToString
        Response.Redirect(urlFinal)

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

    'Protected Sub SubmitNuevo_Click(sender As Object, e As EventArgs) Handles SubmitNuevo.Click
    '    Response.Redirect("Mod_Home.aspx")
    'End Sub

    Private Function Json(p1 As Object) As JsonResult
        Throw New NotImplementedException
    End Function

End Class