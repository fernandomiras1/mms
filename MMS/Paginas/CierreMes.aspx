<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Paginas/PaginaMaestra.Master" CodeBehind="CierreMes.aspx.vb" Inherits="MMS.CierreMes" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <header>
        <script src="../Scripts/Service/CierreMesService.js"></script>
        <script src="../Scripts/Controllers/CierreMesController.js"></script>
    </header>
    <div ng-controller="CierreMesController" ng-init="Init();" ng-cloak>
        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header">Cierre Mes</h1>
            </div>
        </div>


        <div class="row">

            <div class="col-md-12 col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading" data-toggle="collapse" data-parent="#accordion" href="#collapseOne_Bar" style="cursor: pointer;">
                        <i class="fa fa-bar-chart-o fa-fw"></i>Reporte Anual
                   
                    </div>
                    <div id="collapseOne_Bar" class="panel-collapse collapse in">
                        <div class="panel-body">
                            <div id="morris-bar-chart"></div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <a class="btn btn-sm btn-success" style="margin-left: 5px; margin-right: 5px;" ng-click="btn_Nuevo()">NUEVO <i class="fa fa-plus"></i></a>
                    </div>

                    <div class="panel-body">
                        <div class="row">

                            <div ng-show="ObtenerListadoCierreMes.length > 0" class="pull-right col-md-4">
                                <div class="form-group input-group">
                                    <span class="input-group-addon"><i class="fa fa-search"></i>
                                    </span>
                                    <input type="text" ng-model="text_buscar" class="form-control" placeholder="Buscar...">
                                </div>
                            </div>

                            <div class="col-md-12 col-lg-12">

                                <div ng-show="ObtenerListadoCierreMes.length < 1" class="alert alert-danger alert-dismissable">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    No Hay Datos Disponibles.
                                </div>

                            </div>

                        </div>
                        <div class="table-responsive" ng-show="ObtenerListadoCierreMes.length > 0">
                            <%--width="100%"id="dataTables-example"--%>
                            <table ng-show="ObtenerListadoCierreMes.length > 0">
                                <thead>
                                    <tr class="success">
                                        <th>Fecha</th>
                                        <th>Año</th>
                                        <th>Mes</th>
                                        <th>Monto</th>
                                        <th>Observación</th>
                                        <th>Seleccionar</th>
                                    </tr>
                                </thead>
                                <tbody>

                                    <tr ng-repeat="lista in ObtenerListadoCierreMes | filter: text_buscar" class="odd gradeX">
                                        <td data-label="Fecha">{{lista.Fecha | date: 'dd/MM/yyyy'}}</td>
                                        <td data-label="Año">{{lista.Anio}}</td>
                                        <td data-label="Mes">{{lista.Mes.Nombre}}</td>
                                        <td data-label="Monto"><span class="label" ng-class="{'label-danger' : lista.Monto < 0 , 'label-success' : lista.Monto >=0}">{{lista.Monto|number}}</td>
                                        <td data-label="Observación">{{lista.Detalle}}</td>
                                        <td data-label="Seleccionar">
                                            <a ng-click="Modificar(lista)"><i class="fa fa-2x fa-pencil-square"></i></a>
                                            <a ng-click="Eliminar(lista)"><i class="fa fa-2x fa-times-circle"></i></a>
                                        </td>
                                    </tr>

                                </tbody>
                            </table>
                            <ul class="pagination">
                                <li ng-repeat="i in RangoPaginado(0,TotalDatos)" ng-class="{active: i == PaginaActual}">
                                    <a ng-click="ObtenerListado(i)">{{i}}
                                    </a>
                                </li>
                            </ul>

                        </div>


                    </div>
                </div>
            </div>
        </div>


        <div class="modal fade" id="div_Modal_CierreMes">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 ng-show="!Objeto.Id" class="panel-title">Nuevo</h2>
                        <h3 ng-show="Objeto.Id" class="panel-title">Modificar</h3>
                    </div>
                    <div class="modal-body col-lg-12">
                        <div class="col-lg-12">
                            <div class="col-lg-4">
                                <label for="cmbCate">Año</label>
                                <input type="number" ng-model="Objeto.Anio" class="form-control" novalidate />
                            </div>
                            <div class="form-group col-lg-4">
                                <label for="cmbMes">Mes</label>
                                <select id="cmbMes" class="form-control" ng-model="Objeto.Mes" ng-options="mes as mes.Nombre for mes in Mes track by mes.Id">
                                    <option value=''>-- Seleccione --</option>
                                </select>
                            </div>
                            <div class="col-lg-4">
                                <label for="cmbSubCate">Monto</label>
                                <input type="number" class="form-control" ng-disabled="!Objeto.Mes" ng-model="Objeto.Monto" />
                            </div>
                            <div class="form-group col-lg-12">
                                <div class="form-group">
                                    <label for="message-text" class="form-control-label">Observacíon:</label>
                                    <textarea class="form-control" ng-model="Objeto.Detalle" id="message-text"></textarea>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <a type="button" class="btn btn-default" data-dismiss="modal">Cerrar</a>
                        <a ng-show="!Objeto.Id" type="button" class="btn btn-primary" ng-disabled="!Objeto.Monto || !Objeto.Mes || !Objeto.Anio" ng-click="GuardarCierreMes()">Guardar</a>
                        <a ng-show="Objeto.Id" type="button" class="btn btn-primary" ng-disabled="!Objeto.Monto || !Objeto.Mes || !Objeto.Anio" ng-click="GuardarCierreMes()">Modificar</a>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
