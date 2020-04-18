<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Paginas/PaginaMaestra.Master" CodeBehind="Mensuales.aspx.vb" Inherits="MMS.Mensuales" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <header>
        <script src="../Scripts/Service/MensualesService.js"></script>
        <script src="../Scripts/Controllers/MensualesController.js"></script>
    </header>

    <div ng-controller="MensualesController" ng-init="Init();" ng-cloak>

        <!-- Page Heading -->
        <div class="row">
            <div class="col-lg-12">

                <h1 class="page-header">Mensuales</h1>
            </div>
        </div>
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <ol class="breadcrumb" style="background-color: rgba(119, 115, 115, 0.25);">

                    <li>
                        <i class="fa fa-fw fa-home"></i><a href="Home.aspx">Inicio</a>
                    </li>

                </ol>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Mensuales por Ingresos/Egresos
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <!-- Nav tabs -->
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#Ingresos" data-toggle="tab"><i class="fa fa-plus"></i><strong>Ingresos</strong></a>
                            </li>
                            <li><a href="#Egresos" ng-click="Open_Egresos()" data-toggle="tab"><i class="fa fa-minus"></i><strong>Egresos</strong></a>
                            </li>

                        </ul>

                        <div class="tab-content">
                            <div class="tab-pane fade in active" id="Ingresos">

                                <div class="panel panel-default">

                                    <div class="row">

                                        <div class="col-md-12 col-lg-12">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <a class="btn btn-outline btn-success btn-sm" style="margin-left: 5px; margin-right: 5px;" ng-click="btn_Nuevo_Ingreso(true)">INGRESO <i class="fa fa-plus"></i></a>
                                                </div>

                                                <!-- /.panel-heading -->
                                                <div class="panel-body">

                                                    <div class="row">
                                                        <div ng-show="ListadoIngreso.length > 0" class="pull-right col-md-4">
                                                            <div class="form-group input-group">
                                                                <span class="input-group-addon"><i class="fa fa-search"></i>
                                                                </span>
                                                                <input type="text" ng-model="text_buscar_Ingresos" class="form-control" placeholder="Buscar...">
                                                            </div>
                                                        </div>

                                                        <div class="col-md-12 col-lg-12">

                                                            <div ng-show="ListadoIngreso.length < 1" class="alert alert-danger alert-dismissable">
                                                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                                                No Hay Ingresos Disponibles.
                                                            </div>

                                                        </div>

                                                    </div>
                                                    <%--       <input  type="checkbox" ng-model="lista.Recordatorio" disabled="disabled" />--%>
                                                    <div class="table-fer" ng-show="ListadoIngreso.length > 0">
                                                        <%--    <table ng-show="ListadoIngreso.length > 0" class="table table-sm table-striped table-bordered table-hover">
                                                            <thead>
                                                                <tr class="info">
                                                                    <th>Vencimiento</th>
                                                                    <th>Nombre</th>
                                                                    <th>Precio</th>
                                                                    <th>Observación</th>
                                                                    <th>Pagado</th>
                                                                    <th>Recordatorio</th>
                                                                    <th>Seleccionar</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr ng-class="{success: lista.Pagado, danger: !lista.Pagado}" ng-repeat="lista in ListadoIngreso | filter: text_buscar_Ingresos | orderBy : campoOrdenado">
                                                                    <td data-label="Vencimiento">{{lista.FechaVencimiento | date:'dd/MM/yyyy'}}</td>
                                                                    <td data-label="Nombre">{{lista.Nombre.Nombre}}</td>
                                                                    <td data-label="Precio"><span class="label label-success">{{lista.Precio|number}}</span></td>
                                                                    <td data-label="Observación">{{lista.Detalle}}</td>
                                                                    <td data-label="Pagado"><span class="btn btn-circle" ng-class="{'btn-success': lista.Pagado , 'btn-danger': !lista.Pagado}" ng-click="Ingreso_Pagado_Ingreso(lista)"><i class="fa" ng-class="{'fa-check' : lista.Pagado , 'fa-times': !lista.Pagado}"></i></span></td>
                                                                    <td class="centrado" data-label="Recordatorio"><span class="fa" ng-class="{'fa-check': lista.Recordatorio , 'fa-times' : !lista.Recordatorio}"></span></td>
                                                                    <td data-label="Seleccionar">
                                                                        <a class="btn btn-success" ng-click="Open_ModificarIngreso(lista)"><i class="fa fa-pencil"></i></a>
                                                                        <a class="btn btn-danger" ng-click="Open_EliminarIngreso(lista)"><i class="fa fa-times"></i></a>
                                                                    </td>
                                                                </tr>

                                                            </tbody>
                                                        </table>--%>
                                                        <div style="background-color: #010a29;" ng-repeat="lista in ListadoIngreso | filter: text_buscar_Ingresos" class="panel panel-default bs-callout col-lg-12 col-md-12 bs-callout-success">
                                                            <div class="row">
                                                                <div class="col-lg-6 col-md-12">

                                                                    <h4>{{lista.Nombre.Nombre}} <span title="Recordatorio" data-toggle="tooltip" class="fa" ng-class="{'fa-check': lista.Recordatorio , 'fa-times' : !lista.Recordatorio}"></span></h4>
                                                                    <h5><strong>{{lista.Detalle}}</strong></h5>
                                                                    <h6><span title="Pagado" data-toggle="tooltip" class="btn btn-circle" ng-class="{'btn-success': lista.Pagado , 'btn-danger': !lista.Pagado}" ng-click="Ingreso_Pagado_Ingreso(lista)"><i class="fa" ng-class="{'fa-check' : lista.Pagado , 'fa-times': !lista.Pagado}"></i></span></h6>
                                                                    <p class="text" ng-class="{'text-success': lista.Tipo.Nombre == 'INGRESO' , 'text-danger': lista.Tipo.Nombre == 'EGRESO'}"><strong>${{lista.Precio|number}}</strong></p>

                                                                </div>

                                                                <div class="col-lg-6 col-md-12">
                                                                    <div class="text-right mobileChange">
                                                                        <h5><strong>{{lista.FechaVencimiento | date:'dd/MM/yyyy'}}</strong></h5>
                                                                        <h4><span class="label" ng-class="{'label-success': lista.Tipo.Nombre == 'INGRESO', 'label-danger': lista.Tipo.Nombre == 'EGRESO'}">{{lista.Tipo.Nombre}}</span></h4>
                                                                        <div class="form-group btn-group" style="padding-top: 10px;">
                                                                            <a class="btn btn-success" ng-click="Open_ModificarIngreso(lista)"><i class="fa fa-pencil"></i></a>
                                                                            <a class="btn btn-danger" ng-click="Open_EliminarIngreso(lista)"><i class="fa fa-times"></i></a>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                </div>

                                <!-- /.panel-body -->
                            </div>
                            <%--Egresos--%>
                            <div class="tab-pane fade" id="Egresos">
                                <div class="panel panel-default">

                                    <div class="row">
                                        <div class="col-md-12 col-lg-12">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <a class="btn btn-outline btn-danger btn-sm" style="margin-left: 5px; margin-right: 5px;" ng-click="btn_Nuevo_Ingreso(false)">EGRESO <i class="fa fa-plus"></i></a>
                                                </div>

                                                <div class="panel-body">

                                                    <div class="row">
                                                        <div ng-show="ListadoEgreso.length > 0" class="pull-right col-md-4">
                                                            <div class="form-group input-group">
                                                                <span class="input-group-addon"><i class="fa fa-search"></i>
                                                                </span>
                                                                <input type="text" ng-model="text_buscar_Egreso" class="form-control" placeholder="Buscar...">
                                                            </div>
                                                        </div>

                                                        <div class="col-md-12 col-lg-12">
                                                            <div ng-show="ListadoEgreso.length < 1" class="alert alert-danger alert-dismissable">
                                                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                                                No Hay Egresos Disponibles.
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <%--       <input  type="checkbox" ng-model="lista.Recordatorio" disabled="disabled" />--%>
                                                    <div class="table-fer" ng-show="ListadoEgreso.length > 0">
                                                        <%--  <table class="table table-sm table-striped table-bordered table-hover">
                                                            <thead>
                                                                <tr class="info">
                                                                    <th>Vencimiento</th>
                                                                    <th>Nombre</th>
                                                                    <th>Precio</th>
                                                                    <th>Observación</th>
                                                                    <th>Pagado</th>
                                                                    <th>Recordatorio</th>
                                                                    <th>Seleccionar</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr ng-class="{success: listaEgreso.Pagado, danger: !listaEgreso.Pagado}" ng-repeat="listaEgreso in ListadoEgreso | filter: text_buscar_Egreso | orderBy : campoOrdenado">
                                                                    <td data-label="Vencimiento">{{listaEgreso.FechaVencimiento | date:'dd/MM/yyyy'}}</td>
                                                                    <td data-label="Nombre">{{listaEgreso.Nombre.Nombre}}</td>
                                                                    <td data-label="Precio"><span class="label label-danger">{{listaEgreso.Precio|number}}</span></td>
                                                                    <td data-label="Observación">{{listaEgreso.Detalle}}</td>
                                                                    <td data-label="Pagado"><span class="btn btn-circle" ng-class="{'btn-success': listaEgreso.Pagado , 'btn-danger': !listaEgreso.Pagado}" ng-click="Ingreso_Pagado_Egreso(listaEgreso)"><i class="fa" ng-class="{'fa-check' : listaEgreso.Pagado , 'fa-times': !listaEgreso.Pagado}"></i></span></td>
                                                                    <td class="centrado" data-label="Recordatorio"><span class="fa" ng-class="{'fa-check': listaEgreso.Recordatorio , 'fa-times' : !listaEgreso.Recordatorio}"></span></td>
                                                                    <td data-label="Seleccionar">
                                                                        <a class="btn btn-success" ng-click="Open_Modificar_Egreso(listaEgreso)"><i class="fa fa-pencil"></i></a>
                                                                        <a class="btn btn-danger" ng-click="Open_Eliminar_Egreso(listaEgreso)"><i class="fa fa-times"></i></a>
                                                                    </td>
                                                                </tr>

                                                            </tbody>
                                                        </table>--%>
                                                        <div style="background-color: #010a29;" ng-repeat="listaEgreso in ListadoEgreso | filter: text_buscar_Egreso" class="panel panel-default bs-callout col-lg-12 col-md-12 bs-callout-danger">
                                                            <div class="row">
                                                                <div class="col-lg-6 col-md-12">

                                                                    <h4>{{listaEgreso.Nombre.Nombre}} <span title="Recordatorio" data-toggle="tooltip" class="fa" ng-class="{'fa-check': listaEgreso.Recordatorio , 'fa-times' : !listaEgreso.Recordatorio}"></span></h4>
                                                                    <h5><strong>{{listaEgreso.Detalle}}</strong></h5>
                                                                    <h6><span title="Pagado" data-toggle="tooltip" class="btn btn-circle" ng-class="{'btn-success': listaEgreso.Pagado , 'btn-danger': !listaEgreso.Pagado}" ng-click="Ingreso_Pagado_Egreso(listaEgreso)"><i class="fa" ng-class="{'fa-check' : listaEgreso.Pagado , 'fa-times': !listaEgreso.Pagado}"></i></span></h6>
                                                                    <p class="text" ng-class="{'text-success': listaEgreso.Tipo.Nombre == 'INGRESO' , 'text-danger': listaEgreso.Tipo.Nombre == 'EGRESO'}"><strong>${{listaEgreso.Precio|number}}</strong></p>

                                                                </div>

                                                                <div class="col-lg-6 col-md-12">
                                                                    <div class="text-right mobileChange">
                                                                        <h5><strong>{{listaEgreso.FechaVencimiento | date:'dd/MM/yyyy'}}</strong></h5>
                                                                        <h4><span class="label" ng-class="{'label-success': listaEgreso.Tipo.Nombre == 'INGRESO', 'label-danger': listaEgreso.Tipo.Nombre == 'EGRESO'}">{{listaEgreso.Tipo.Nombre}}</span></h4>
                                                                        <div class="form-group btn-group" style="padding-top: 10px;">
                                                                            <a class="btn btn-success" ng-click="Open_Modificar_Egreso(listaEgreso)"><i class="fa fa-pencil"></i></a>
                                                                            <a class="btn btn-danger" ng-click="Open_Eliminar_Egreso(listaEgreso)"><i class="fa fa-times"></i></a>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </div>

                        </div>

                    </div>

                </div>
            </div>

        </div>


        <div class="modal fade" id="div_Modal_Ingreso">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h3 ng-show="!Objeto.Id" class="panel-title">Nuevo</h3>
                        <h3 ng-show="Objeto.Id" class="panel-title">Modificar</h3>
                    </div>
                    <div class="modal-body col-lg-12">
                        <%--  <div class="col-lg-12">--%>
                        <div class="form-group col-lg-12">
                            <label for="cmbCate">Sub Categoría</label>
                            <ui-select ng-model="Objeto.Nombre" theme="select2" ng-disabled="disabled" style="min-width: 100%;" title="Seleccione una Categoría">
                                <ui-select-match placeholder="Selecionar...">{{$select.selected.Nombre}}</ui-select-match>
                                <ui-select-choices repeat="cate in categorias | filter: $select.search track by cate.Id">
                                  <div Style="color:darkblue;font-weight: bold;" ng-bind-html="cate.Nombre | highlight: $select.search"></div>
                                  <small>
                                  <strong>{{cate.Categoria.Nombre}}</strong>
                                  <div></div>
                                  <span class="label" ng-class="{'label-success': cate.Tipo.Nombre == 'INGRESO', 'label-danger': cate.Tipo.Nombre == 'EGRESO'}">{{cate.Tipo.Nombre}}</span>
                                  </small>
                                </ui-select-choices>
                              </ui-select>
                        </div>
                        <div class="form-group col-lg-12">
                            <div class="col-md-4 col-lg-4">
                                <label for="cmbSubCate">Precio</label>
                                <input type="number" class="form-control" ng-disabled="!Objeto.Fecha" ng-model="Objeto.Precio" />
                            </div>

                            <div class="col-lg-4">
                                <label for="cmbTipo">Fecha Vencimiento</label>
                                <input type="date" class="form-control" ng-model="Objeto.FechaVencimiento" id="FechaVen" name="FechaVen" />
                            </div>

                            <div class="col-lg-4">

                                <div class="form-group">
                                    <label for="cmbTipo">Recordatorio</label>
                                    <div></div>
                                    <div class="btn-group">
                                        <input class="form-control" type="checkbox" name="fancy-checkbox-default" id="fancy-checkbox-default" ng-model="Objeto.Recordatorio" autocomplete="off" />
                                        <div class="btn-group">
                                            <label for="fancy-checkbox-default" class="btn btn-default">
                                                <span class="fa fa-check"></span>
                                                <span></span>
                                            </label>
                                            <label for="fancy-checkbox-default" class="btn btn-default active">
                                                Activar
                                            </label>
                                        </div>

                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="form-group col-lg-12">

                            <div class="form-group">
                                <label for="message-text" class="form-control-label">Observacíon:</label>
                                <textarea class="form-control" ng-model="Objeto.Detalle" id="message-text"></textarea>
                            </div>

                            <div class="form-group">
                                <label for="cmbTipo">Pagado</label>
                                <div></div>
                                <div class="btn-group">
                                    <input class="form-control" type="checkbox" name="checkbox-Pagado" id="checkbox-Pagado" ng-model="Objeto.Pagado" autocomplete="off" />
                                    <div class="btn-group">
                                        <label for="fancy-checkbox-default" class="btn btn-default">
                                            <span class="fa fa-check"></span>
                                            <span></span>
                                        </label>
                                        <label for="checkbox-Pagado" class="btn btn-default active">
                                            Activar
                                        </label>
                                    </div>

                                </div>

                            </div>

                        </div>

                    </div>
                    <div class="modal-footer">
                        <a type="button" class="btn btn-default" data-dismiss="modal">Cerrar</a>
                        <a ng-show="!Objeto.Id" type="button" class="btn btn-primary" ng-disabled="!Objeto.Precio || !Objeto.FechaVencimiento || !Objeto.Fecha || !Objeto.Nombre" ng-click="GuardadoGeneral()">Guardar</a>
                        <a ng-show="Objeto.Id" type="button" class="btn btn-primary" ng-disabled="!Objeto.Precio || !Objeto.FechaVencimiento  || !Objeto.Fecha || !Objeto.Nombre" ng-click="GuardadoGeneral()">Modificar</a>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="div_Modal_Pagado">
            <div class="modal-dialog">
                <div class="modal-content">
                    <%--  <div class="modal-header" style="background-color: rgb(224,224,224);">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <div class="panel-title"><strong>INGRESO</strong></div>
                    </div>--%>
                    <div class="modal-body col-lg-12">
                        <div class="form-group col-lg-12">
                            <label for="cmbCate">Sub Categoría</label>
                            <ui-select ng-model="Objeto_Pagado.Nombre" theme="select2" ng-disabled="disabled" style="min-width: 100%;" title="Seleccione una Categoría">
                                <ui-select-match placeholder="Selecionar...">{{$select.selected.Nombre}}</ui-select-match>
                                <ui-select-choices repeat="cate in categorias | filter: $select.search track by cate.Id">
                                  <div Style="color:darkblue;font-weight: bold;" ng-bind-html="cate.Nombre | highlight: $select.search"></div>
                                  <small>
                                  <strong>{{cate.Categoria.Nombre}}</strong>
                                  <div></div>
                                  <span class="label" ng-class="{'label-success': cate.Tipo.Nombre == 'INGRESO', 'label-danger': cate.Tipo.Nombre == 'EGRESO'}">{{cate.Tipo.Nombre}}</span>
                                  </small>
                                </ui-select-choices>
                              </ui-select>
                        </div>
                        <div class="form-group col-lg-12">
                            <div class="col-md-4 col-lg-4">
                                <label for="cmbSubCate">Precio</label>
                                <input type="number" class="form-control" ng-disabled="!Objeto_Pagado.Nombre" ng-model="Objeto_Pagado.Precio" />
                            </div>

                            <div class="col-lg-4">
                                <label for="cmbTipo">Fecha</label>
                                <input type="date" class="form-control" ng-model="Objeto_Pagado.Fecha" id="Fecha" name="Fecha" />
                            </div>

                            <div class="col-lg-4">
                                <label for="cmbTipo">Forma Pago</label>
                                <ui-select ng-model="Objeto_Pagado.Forma_Pago" theme="select2" ng-disabled="!Objeto_Pagado.Precio" style="min-width: 100%;">
                                <ui-select-match placeholder="Selecionar...">{{$select.selected.Nombre}}</ui-select-match>
                                <ui-select-choices repeat="pago in formaPago track by pago.id">
                             <%--     <div class="fa fa-money" ng-bind-html="pago.Nombre"></div>--%>
                                    <span Style="font-weight: bold;"><i class="fa" ng-class="{'fa-dollar': pago.Nombre == 'Efectivo' , 'fa-credit-card': pago.Nombre == 'Tarjeta'}"></i> {{pago.Nombre}}</span>
                                </ui-select-choices>
                              </ui-select>

                            </div>
                        </div>

                        <div class="form-group col-lg-12">
                            <div class="form-group">
                                <label for="message-text" class="form-control-label">Observacíon:</label>
                                <textarea class="form-control" ng-model="Objeto_Pagado.Observacion" id="message-text"></textarea>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <a type="button" class="btn btn-default" data-dismiss="modal">Cerrar</a>
                        <a type="button" class="btn btn-primary" ng-disabled="!Objeto_Pagado.Precio || !Objeto_Pagado.Nombre || !Objeto_Pagado.Fecha || !Objeto_Pagado.Forma_Pago" ng-click="GuardarPagado()">Guardar</a>
                    </div>
                </div>
            </div>
        </div>




    </div>


</asp:Content>
