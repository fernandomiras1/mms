Imports System.Web.UI.DataVisualization.Charting
Imports System.IO
Imports MySql.Data.MySqlClient
Imports MMS_Utiles

Public Class Reportes
    Inherits System.Web.UI.Page
    Private C As New CG
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = True Then

            Dim Id_Año As String = comboaño.SelectedValue.ToString

         

        Else
            Dim Espanol As New Globalization.CultureInfo("es-ES")
            Dim Num_Mes As String = Now.Month
            Dim Nombre_Mes As String
            Nombre_Mes = MonthName(Num_Mes)
            Nombre_Mes.ToString(Espanol)
            ab2.Text = UCase(Nombre_Mes)


            Dim Id_Año As String = comboaño.SelectedValue.ToString
            Dim Tipo As String = comboTipo.SelectedValue.ToString


        End If
    End Sub



    Public Sub Constructor_Graficos(ByVal Id_Mes As String, ByVal Id_Año As String, ByVal Tipo As String)

        Dim Indice_Grafico As Integer
        Dim Nombre_Grafico As String
        Nombre_Grafico = "Grafico" & Indice_Grafico
        Dim Rojo_Cant As Integer = 0
        Dim Verde_Cant As Integer = 0
        Dim Amarillo_Cant As Integer = 0
        Dim Label_Tot As New Label

        Dim cn9 As MySqlConnection
        Dim cmd9 As MySqlCommand
        Dim rdr9 As MySqlDataReader

        Dim Entidad As String = C.Pedir_Valoreas_a_Cookie("MMS-C", "IE")

        cn9 = New MySqlConnection(C.StrConexion)
        cmd9 = New MySqlCommand("select Categoría from ingresos where Tipo = '" & Tipo.ToString & "' and Month(Fecha) = '" & Id_Mes.ToString & "' and Borrar='NO' and  Year(Fecha) = '" & Id_Año.ToString & "' group by Categoría ORDER BY Categoría ASC", cn9)
        cn9.Open()
        rdr9 = cmd9.ExecuteReader

        While rdr9.Read()

            Dim cate As String
            cate = rdr9.GetString("Categoría")
            Dim cn91 As MySqlConnection
            Dim cmd91 As MySqlCommand
            Dim rdr91 As MySqlDataReader

            cn91 = New MySqlConnection(C.StrConexion)
            cmd91 = New MySqlCommand("select Nombre , sum(Precio) as 'TOTAL'  from ingresos where Categoría = '" & cate.ToString & "' and Month(Fecha) = '" & Id_Mes.ToString & "' and Borrar='NO' and Year(Fecha) = '" & Id_Año.ToString & "' group by Nombre", cn91)
            Dim Total As Double = C.Reader_Enteros("Total", "select sum(Precio) as 'Total'  from ingresos where Categoría = '" & cate.ToString & "' and Month(Fecha) = '" & Id_Mes.ToString & "' and Borrar='NO' and Year(Fecha) = '" & Id_Año.ToString & "'")
        
            'select  Abono , count(*) as total from ventas_gral where Id_call = '" & Id_Call.ToString & "'  and Mes = 'NOVIEMBRE'  and Ano = '" & Id_Año.ToString & "'  and Activo = 'SI' and Estado REGEXP 'APROBADA|LINEA NUEVA APROBADA|ENVIADO ABD|APROBADA FINALIZADA' group by Abono", cn9)
            cn91.Open()
            rdr91 = cmd91.ExecuteReader

            Dim Grafico_Nuevo As New Chart
            Grafico_Nuevo.Series.Add("INCIDENTES")

            Grafico_Nuevo.Legends.Add("Legend1")
            Grafico_Nuevo.Series("INCIDENTES").Points.Clear()

            Grafico_Nuevo.ChartAreas.Add("ChartArea1")
            Grafico_Nuevo.ChartAreas("ChartArea1").BackColor = Drawing.Color.Transparent
            Grafico_Nuevo.Series.Item("INCIDENTES").ChartType = SeriesChartType.Doughnut

            Grafico_Nuevo.ChartAreas("ChartArea1").AxisX.LabelStyle.Interval = 1
            Grafico_Nuevo.ChartAreas("ChartArea1").AxisY.MajorGrid.Enabled = False
            Grafico_Nuevo.ChartAreas("ChartArea1").AxisX.MajorGrid.Enabled = False
            Grafico_Nuevo.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True
            Grafico_Nuevo.Legends("Legend1").Enabled = True

            ' series_overal_laptop.Label = "#VALY"
            Grafico_Nuevo.Series("INCIDENTES").Label = "#PERCENT{P0}"
            Grafico_Nuevo.Series("INCIDENTES").IsVisibleInLegend = True
            Grafico_Nuevo.Series("INCIDENTES").LegendText = "#AXISLABEL"
            Grafico_Nuevo.Titles.Add(UCase(cate))
            'Grafico_Nuevo.Titles.Add("INGRESOS TOTAL: $" & Ingreso)
            'Grafico_Nuevo.Titles.Add("EGRESOS TOTAL: $" & Egreso)
            Grafico_Nuevo.Titles.Add("TOTAL: $" & Total)
            Grafico_Nuevo.Titles.Item(0).IsDockedInsideChartArea = True

            'Grafico_Nuevo.Style.Add("height", "24%")
            'Grafico_Nuevo.Style.Add("width", "24%")
            Grafico_Nuevo.Style.Add("margin-right", "1%")
            Grafico_Nuevo.Style.Add("margin-bottom", "1%")
            Grafico_Nuevo.Style.Add("font-weight", "bolder")

            Dim LabelChart As New Label

            While rdr91.Read()

                Grafico_Nuevo.Series("INCIDENTES").Points.AddXY(rdr91.GetString("Nombre"), rdr91.GetString("TOTAL"))
               

            End While

            'Dim QPuntos As Integer
            'Dim Medida_Final As Integer
            'Dim Puntos_Restantes As Integer
            'Dim Signo_Mas As String = "+"


            'QPuntos = CInt((Puntos_Netos * 100) / Puntos_Objetivo)
            'Medida_Final = QPuntos - 100
            'Puntos_Restantes = Puntos_Netos - Puntos_Objetivo


            'If Medida_Final >= 0 Then

            '    Grafico_Nuevo.Style.Add("box-shadow", "6px 6px 6px 0px darkgreen;")
            '    Verde_Cant = Verde_Cant + 1

            'End If

            'If Medida_Final < 0 Then

            '    If Medida_Final <= -19 Then
            '        Grafico_Nuevo.Style.Add("box-shadow", "6px 6px 6px 0px darkred;")
            '        Rojo_Cant = Rojo_Cant + 1
            '    Else
            '        Grafico_Nuevo.Style.Add("box-shadow", "6px 6px 6px 0px goldenrod;")
            '        Amarillo_Cant = Amarillo_Cant + 1
            '    End If



            ' End If
            Grafico_Nuevo.Style.Add("box-shadow", "6px 6px 6px 0px darkgreen;")


            inferior.Controls.Add(Grafico_Nuevo)



            rdr91.Close()
            cn91.Close()



        End While
        ' 


        ' & "(" & rdr9.GetString("Total Incidentes") & ")"

        'Label_Tot.Text = "O:(OBJETIVO) - P:(PUNTOS) - V:(VENTAS) - T:(TURNO) - E:(EFECTIVIDAD) - I:(INGRESO) " & "<i class='fa fa-certificate' style='color: #036503;'></i>" & Verde_Cant & " - <i class='fa fa-certificate' style='color: #D8A422;'></i>" & Amarillo_Cant & " - <i class='fa fa-certificate' style='color: #8B0303;'></i>" & Rojo_Cant

        'total.Controls.Add(Label_Tot)
        '#8B0303
        rdr9.Close()
        cn9.Close()


    End Sub

    Public Sub MessageBoxError(ByVal msg As String)
        Dim lbl As New Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine & "alertify.error('" + msg + "')</script>"
        Page.Controls.Add(lbl)
    End Sub

    Protected Sub btn_Filtrar_Click(sender As Object, e As EventArgs) Handles btn_Filtrar.Click
        ' Dim Id_Mes As String = C.Mes_Numero(ab2.Text)
        ' Dim Id_Año As String = C.Pedir_Valoreas_a_Cookie("SPA-C", "IA")
        'Constructor_Graficos(Id_Mes, Id_Año)


        Dim Id_Año As String = comboaño.SelectedValue.ToString
        Dim Tipo As String = comboTipo.SelectedValue.ToString


    End Sub


End Class