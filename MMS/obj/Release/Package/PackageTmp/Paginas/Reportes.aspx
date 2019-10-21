<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Paginas/PaginaMaestra.Master" CodeBehind="Reportes.aspx.vb" Inherits="MMS.Reportes" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../Scripts/Controllers/ReportesController.js"></script>
   
    <div id="non-printable2">
        <!-- Page Heading -->
        <div class="row">
            <div class="col-lg-12">

                <h1 class="page-header">Reporte</h1>

            </div>
        </div>
    </div>


    
   <%--    <div class="row">
        <div class="col-sm-6">
            <div class="box">
                <header class="b-b">
                    <h4>Bar Chart</h4>
                    <!-- begin box-tools -->
                    <div class="box-tools">
                        <a class="fa fa-fw fa-minus" href="#" data-box="collapse"></a>
                        <a class="fa fa-fw fa-square-o" href="#" data-fullscreen="box"></a>
                        <a class="fa fa-fw fa-refresh" href="#" data-box="refresh"></a>
                        <a class="fa fa-fw fa-times" href="#" data-box="close"></a>
                    </div>
                    <!-- END: box-tools -->
                </header>
                <div class="box-body collapse in">
                    <iframe class="chartjs-hidden-iframe" tabindex="-1" style="display: block; overflow: hidden; border: 0px; margin: 0px; top: 0px; left: 0px; bottom: 0px; right: 0px; height: 100%; width: 100%; position: absolute; pointer-events: none; z-index: -1;"></iframe>
                    <canvas id="barChart" width="492" height="246" style="display: block; width: 492px; height: 246px;"></canvas>
                    <div class="loader hidden">
                        <div class="loader-inner ball-scale-ripple-multiple">
                            <div></div>
                            <div></div>
                            <div></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>


    <div class="row">
        <div class="col-lg-12">

            <ol class="breadcrumb" style="background-color: rgba(119, 115, 115, 0.25);">

                <li>
                    <i class="fa fa-fw fa-home"></i><a href="Home.aspx">Inicio</a>
                </li>

            </ol>
        </div>
    </div>


    <!-- /.row -->


    <div class="panel panel-default">

        <div id="non-printable">

            <div class="panel-heading" style="background-color: #2C75AF;">
                Grafico - Mensuales  
            </div>


            <div class="panel-body">

                <div class="form-group input-group">
                    <span class="input-group-addon">FILTRO MES</span>

                    <asp:DropDownList runat="server" ID="ab2" name="DropDownListestveta" class="form-control">
                        <asp:ListItem Text="ENERO" />
                        <asp:ListItem Text="FEBRERO" />
                        <asp:ListItem Text="MARZO" />
                        <asp:ListItem Text="ABRIL" />
                        <asp:ListItem Text="MAYO" />
                        <asp:ListItem Text="JUNIO" />
                        <asp:ListItem Text="JULIO" />
                        <asp:ListItem Text="AGOSTO" />
                        <asp:ListItem Text="SEPTIEMBRE" />
                        <asp:ListItem Text="OCTUBRE" />
                        <asp:ListItem Text="NOVIEMBRE" />
                        <asp:ListItem Text="DICIEMBRE" />
                    </asp:DropDownList>

                </div>

                <div class="form-group input-group">
                    <span class="input-group-addon">AÑO</span>

                    <asp:DropDownList runat="server" ID="comboaño" name="comboaño" class="form-control">
                        <asp:ListItem Text="2016" />
                        <asp:ListItem Text="2017" />
                        <asp:ListItem Text="2018" />


                    </asp:DropDownList>

                </div>

                <div class="form-group input-group">
                    <span class="input-group-addon">TIPO</span>

                    <asp:DropDownList runat="server" ID="comboTipo" name="comboTipo" class="form-control">
                        <asp:ListItem Text="INGRESO" />
                        <asp:ListItem Text="EGRESO" />

                    </asp:DropDownList>

                </div>




                <asp:Button class="btn btn-primary btn-sm" ID="btn_Filtrar" runat="server" Text=" Filtrar" Style="background-color: #008287; color: white;"></asp:Button>


            </div>
        </div>
        <%--  <div class="panel panel-default">--%>

        <div id="inferior" runat="server" class="notice marker-on-bottom" style="padding: 15px; background-color: #c1c1c1;">
        </div>



        <%--</div>--%>




        <div class="panel-content" style="width: 100%; padding: 2%;">




            <p id="total" runat="server" style="text-align: center;">
            </p>

        </div>




    </div>







</asp:Content>
