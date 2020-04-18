<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Paginas/PaginaMaestra.Master" CodeBehind="Estadisticas_SubCate.aspx.vb" Inherits="MMS.Estadisticas_SubCate" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <header>
        <script src="../Scripts/Service/Est_SubCateService.js"></script>
        <script src="../Scripts/Controllers/Est_SubCateController.js"></script>
    </header>
    <div ng-controller="Est_SubCateController" ng-init="Init();" ng-cloak>

        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header">Estadísticas</h1>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Estadísticas - Sub Categorías
                    </div>
                    <div class="panel-body">

                        <div class="panel-group" id="accordion">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">Búsqueda</a>
                                    </h4>
                                </div>
                                <div id="collapseOne" class="panel-collapse collapse in">
                                    <div class="panel-body">
                                        <div class="col-lg-12">
                                            <div class="col-lg-2">
                                                <label for="cmbCate">Año</label>
                                                <input type="number" ng-model="Objeto.Anio" class="form-control" novalidate />
                                            </div>

                                            <div class="col-lg-4">
                                                <label for="cmbTipo">Tipo</label>
                                                <ui-select ng-model="Objeto.Tipo" theme="select2" ng-disabled="disabled" on-select="ObtenerSubCategorias($item, $model)" style="min-width: 100%;">
                                                <ui-select-match placeholder="Selecionar...">{{$select.selected.Nombre}}</ui-select-match>
                                                <ui-select-choices repeat="tipo in tipos track by tipo.id">
                                                  <div class="label" ng-class="{'label-success': tipo.Nombre == 'INGRESO', 'label-danger': tipo.Nombre == 'EGRESO'}" ng-bind-html="tipo.Nombre"></div>
                                                </ui-select-choices>
                                              </ui-select>
                                            </div>

                                           <%--   <div class="row">   ObtenerSubCategorias --%>
                                                   <div class="col-lg-4">
                                                   
                                                   <label for="cmbCate">Sub Categoría</label>
                                                         <div isteven-multi-select id="cmbSubCategoria"
                                                                             is-disabled="!Objeto.Tipo"
                                                                             input-model="categorias"
                                                                             output-model="Objeto.SubCate"
                                                                             button-label="Nombre"
                                                                             item-label="Nombre"
                                                                             translation="localLang"
                                                                             tick-property="ticked"
                                                                             max-labels="1"
                                                                        </div>
                                                   
                                                       </div>
                                    <%--    </div>--%>
                                          
                                        </div>
                                          <div class="col-lg-12">
                                                <a class="btn btn-outline btn-success" ng-disabled="!Objeto.Tipo" style="margin-top: 20px;" ng-click="btn_Buscar()">Buscar <i class="fa fa-search"></i></a>
                                            </div>

                                    </div>
                                </div>
                            </div>
                    
                          </div>
                          </div>
                     
                        <!-- /.col-lg-12 -->
                        <div class="row">
                            <div class="col-lg-6" ng-repeat="meses in listaBuscar">
                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        <span><strong>{{meses.Mes}}</strong></span>
                                    </div>
                                    <!-- /.panel-heading -->
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <div donut-chart="" donut-data="meses.Grafico" donut-colors="chartColors" donut-formatter="'currency'"></div>
                                            </div>

                                            <div class="col-lg-6">
                                                <div class="table-responsive table-EstSubCate">
                                                    <table>
                                                        <thead>
                                                            <tr>
                                                                <th>Nombre</th>
                                                                <th>Precio</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr ng-repeat="lista in meses.Datos">
                                                                <td data-label="Nombre">{{lista.SubCate.Nombre}}</td>
                                                                <td data-label="Precio">{{lista.Monto|number}}</td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <!-- /.table-responsive -->
                                            </div>

                                        </div>
                                    </div>
                                    <!-- /.panel-body --> 
                                    <div class="panel-footer text-right">
                                    <span>Total $<strong>{{meses.Total|number}}</strong></span>
                                    </div>
                                </div>
                                <!-- /.panel -->
                            </div>
                        </div>
                    </div>
                    <!-- /.row -->


                </div>

            </div>
            <!-- /.panel -->



        </div>
        <!-- /.col-lg-12 -->

</asp:Content>
