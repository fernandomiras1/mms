<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Paginas/PaginaMaestra.Master" CodeBehind="Home.aspx.vb" Inherits="MMS.Home" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div ng-controller="HomeController" ng-init="Init();" ng-cloak>

        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header">Gestión de Ingresos</h1>
            </div>
        </div>

        <div class="row">

            <div class="col-md-4 col-lg-4">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <a class="btn btn-outline btn-success btn-sm" style="margin-left: 5px; margin-right: 5px;" ng-click="btn_Nuevo()">NUEVO <i class="fa fa-plus"></i></a>
                        <%--  estos son los botones al costado. del div --%>
                        <div class="pull-right">
                            <div class="btn-group">
                                <button type="button" class="btn btn-default dropdown-toggle" ng-big data-toggle="dropdown">
                                    <strong><span ng-bind="Titulo_Periodo.Nombre"></span></strong>
                                    <span class="caret"></span>
                                </button>
                                <ul class="dropdown-menu pull-right" role="menu">
                                    <li><a ng-click="ObtenerListado(1)">{{MesCurso_Name}}</a>
                                    </li>
                                    <li><a ng-click="ObtenerListado(2)">{{AnioCurso}}</a>
                                    </li>
                                    <li><a ng-click="ObtenerListado(3)">El origen de los tiempos</a>
                                    </li>

                                </ul>
                            </div>
                        </div>

                    </div>

                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-12 col-md-12">
                                <div class="panel panel-green">
                                    <div class="panel-heading">
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <i class="fa fa-thumbs-o-up fa-3x"></i>
                                            </div>
                                            <div class="col-xs-9 text-right">
                                                <div style="font-size: 22px;">{{suma_ingresos|number}}</div>
                                                <div>INGRESOS</div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12 col-md-12">
                                <div class="panel panel-red">
                                    <div class="panel-heading">
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <i class="fa fa-thumbs-o-down fa-3x"></i>
                                            </div>
                                            <div class="col-xs-9 text-right">
                                                <div style="font-size: 22px;">{{suma_egresos|number}}</div>
                                                <div>EGRESOS</div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="col-lg-12 col-md-12">
                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <i class="fa fa-hand-o-right fa-3x"></i>
                                            </div>
                                            <div class="col-xs-9 text-right">
                                                <div style="font-size: 22px;">{{suma_ingresos - suma_egresos|number}}</div>
                                                <div>TOTAL</div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>


                        </div>

                    </div>
                </div>


            </div>


            <div class="col-md-8 col-lg-8">
                <div class="panel panel-default">
                    <div class="panel-heading" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" style="cursor: pointer;">
                        <i class="fa fa-bar-chart-o fa-fw"></i>Estadísticas Anuales
                 
                    </div>

                    <div id="collapseOne" class="panel-collapse collapse in">

                        <div class="panel-body">
                            <div id="morris-area-chart"></div>
                        </div>
                        <!-- /.panel-body -->
                    </div>
                </div>
                <!-- /.panel -->
            </div>

        </div>

        <div class="row">
            <div class="col-lg-12 col-md-12">
                <div>
                    <div class="panel panel-default">
                        <div class="panel-heading" data-toggle="collapse" data-parent="#accordion" href="#collapseOne_Bar" style="cursor: pointer;">
                            <i class="fa fa-bar-chart-o fa-fw"></i>Estadísticas del Período {{Titulo_Periodo.Nombre}} 
                   
                        </div>
                        <div id="collapseOne_Bar" class="panel-collapse collapse in">
                            <!-- /.panel-heading -->
                            <div class="panel-body">
                                <div id="morris-bar-chart"></div>
                            </div>
                            <!-- /.panel-body -->
                            <!-- /.panel-heading -->
                            <div class="panel-body">
                                <div id="morris-donut-chart"></div>
                            </div>
                            <!-- /.panel-body -->

                        </div>
                        <!-- /.panel -->
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div uib-collapse="!isCollapsed" class="col-lg-6">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        Busqueda General
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="col-lg-12">

                                <div class="col-lg-12">
                                    <div class="form-group col-lg-12">
                                        <label for="cmbfecha">Fecha Desde</label>
                                        <input type="date" class="form-control" ng-model="Fecha_Desde" name="Fecha_Desde" />
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <label for="cmbfecha">Fecha Hasta</label>
                                        <input type="date" class="form-control" ng-model="Fecha_Hasta" name="Fecha_Hasta" />
                                    </div>

                                </div>
                                <div class="col-md-12">
                                    <div class="form-group col-lg-12">
                                        <div class="btn-group">
                                            <input class="form-control" type="checkbox" name="fancy-checkbox-default" id="fancy-checkbox-default" ng-model="detallado" ng-click="Open_Detallado()" autocomplete="off" />
                                            <div class="btn-group">
                                                <label for="fancy-checkbox-default" class="btn btn-default">
                                                    <span class="fa fa-check"></span>
                                                    <span></span>
                                                </label>
                                                <label for="fancy-checkbox-default" class="btn btn-default active">
                                                    Detallado
                                                </label>
                                            </div>

                                        </div>

                                        <div style="float: right;">
                                            <a class="btn btn-success ng-binding" ng-click="btn_Buscar()">Buscar <span class="fa fa-search"></span></a>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>

                </div>
            </div>


            <div ng-show="detallado" class="col-lg-6">
                <div class="panel panel-danger">
                    <div class="panel-heading">

                        <div class="row">
                            <div class="col-lg-12">
                                <div class="pull-left">
                                    <h5>Busqueda Detallada</h5>
                                </div>
                                <div class="pull-right">
                                    <div class="btn-group">
                                        <a class="btn btn-outline btn-sm btn-primary" ng-click="btn_CierreMes()">Cierre Mes <i class="fa fa-calendar"></i></a>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                    <div class="panel-body">
                        <div class="col-lg-12">
                            <label for="cmbTipo">Tipo</label>
                            <ui-select ng-model="ObjetoBuscar.sel_Tipo" on-select="SeleccionarTipo();" theme="select2" ng-disabled="disabled" style="min-width: 100%;">
                                   <ui-select-match placeholder="Selecionar...">{{$select.selected.Nombre}}</ui-select-match>
                                   <ui-select-choices repeat="tipo in tipos track by tipo.id">
                                  <div class="label" ng-class="{'label-success': tipo.Nombre == 'INGRESO', 'label-danger': tipo.Nombre == 'EGRESO'}" ng-bind-html="tipo.Nombre"></div>
                                </ui-select-choices>
                              </ui-select>

                        </div>
                        <div class="col-lg-12">
                            <label for="cmbCate">Categoría</label>
                            <ui-select ng-model="ObjetoBuscar.sel_Cate" theme="select2" ng-disabled="!ObjetoBuscar.sel_Tipo" style="min-width: 100%;">
                                                <ui-select-match placeholder="Selecionar...">{{$select.selected.Nombre}}</ui-select-match>
                                               <ui-select-choices repeat="cate in categorias | propsFilter: {Nombre: $select.search , Tipo: $select.search} | filter:{ Tipo:ObjetoBuscar.sel_Tipo.Nombre } track by cate.Id">
                                                  <div Style="font-weight: bold; font-size: 11px;" ng-bind-html="cate.Nombre | highlight: $select.search"></div>
                                                  <small>
                                                 <span class="label" ng-class="{'label-success': cate.Tipo == 'INGRESO', 'label-danger': cate.Tipo == 'EGRESO'}">{{cate.Tipo}}</span>
                                                  </small>
                                                </ui-select-choices>
                                              </ui-select>

                        </div>
                        <div class="form-group col-lg-12">
                            <label for="cmbSubCate">Sub Categoría</label>
                            <ui-select ng-model="ObjetoBuscar.sel_SubCate" theme="select2" style="min-width: 100%;">
                                                <ui-select-match placeholder="Selecionar...">{{$select.selected.Nombre}}</ui-select-match>
                                                <ui-select-choices repeat="sub_cate in sub_categorias | propsFilter: {Nombre: $select.search}  | filter:{ Tipo:ObjetoBuscar.sel_Cate.Nombre } track by sub_cate.Id">
                                                  <div Style="font-weight: bold; font-size: 11px;" ng-bind-html="sub_cate.Nombre | highlight: $select.search"></div>
                                                  <small>
                                                 <span class="label label-primary">{{sub_cate.Tipo}}</span>
                                                  </small>
                                                </ui-select-choices>
                                              </ui-select>

                        </div>

                    </div>
                </div>
            </div>


        </div>



        <div class="row">
            <div class="col-md-12 col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <button type="button" class="btn btn-outline btn-warning" ng-click="isCollapsed = !isCollapsed">Buscar <i class="fa fa-caret-up" ng-class="{'fa fa-caret-down': !isCollapsed, 'fa fa-caret-right': isCollapsed}"></i></button>
                        <%--  estos son los botones al costado. del div --%>
                        <div class="pull-right">
                            <div class="btn-group">
                                <button type="button" class="btn btn-default dropdown-toggle" ng-big data-toggle="dropdown">
                                    <strong><span ng-bind="Titulo_Periodo.Nombre"></span></strong>
                                    <span class="caret"></span>
                                </button>
                                <ul class="dropdown-menu pull-right" role="menu">
                                    <li><a ng-click="ObtenerListado(1)">{{MesCurso_Name}}</a>
                                    </li>
                                    <li><a ng-click="ObtenerListado(2)">{{AnioCurso}}</a>
                                    </li>
                                    <li><a ng-click="ObtenerListado(3)">El origen de los tiempos</a>
                                    </li>

                                </ul>
                            </div>
                        </div>

                    </div>

                    <!-- /.panel-heading -->
                    <div class="panel-body" style="background-color: rgb(248, 248, 248);">

                        <div class="row">

                            <div ng-show="Listado.length > 0" class="pull-right col-md-4">
                                <div class="form-group input-group">
                                    <span class="input-group-addon"><i class="fa fa-search"></i>
                                    </span>
                                    <input type="text" ng-model="text_buscar" class="form-control" placeholder="Buscar...">
                                </div>
                            </div>

                            <div class="col-md-12 col-lg-12">

                                <div ng-show="Listado.length < 1" class="alert alert-danger alert-dismissable">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    No Hay Datos Disponibles.
                                </div>

                            </div>

                        </div>

                        <div class="table-fer" ng-show="Listado.length > 0">

                            <table ng-show="Listado.length > 0">
                                <thead>
                                    <tr class="info">
                                        <th>Tipo<a ng-show="OrdernarAsendente" ng-click="OrdernarAsendente('Tipo')"><span class="sortorder" ng-show="reverse" ng-class="{reverse: reverse}"></span></a><a ng-show="OrdernarDescendente" ng-click="OrdernarDescendente('Tipo')"><span class="sortorder" ng-show="!reverse" ng-class="{reverse: reverse}"></span></a></th>
                                        <th>Nombre<a ng-click="OrdernarAsendente('Nombre')"><span class="sortorder" ng-show="reverse" ng-class="{reverse: reverse}"></a><a ng-click="OrdernarDescendente('Nombre')"><span class="sortorder" ng-show="!reverse" ng-class="{reverse: reverse}"></span></a></th>
                                        <th>Categoría<a ng-click="OrdernarAsendente('Cate')"><span class="sortorder" ng-show="reverse" ng-class="{reverse: reverse}"></a><a ng-click="OrdernarDescendente('Cate')"><span class="sortorder" ng-show="!reverse" ng-class="{reverse: reverse}"></span></a></th>
                                        <th>Observación<a ng-click="OrdernarAsendente('Observacion')"><span class="sortorder" ng-show="reverse" ng-class="{reverse: reverse}"></a><a ng-click="OrdernarDescendente('Observacion')"><span class="sortorder" ng-show="!reverse" ng-class="{reverse: reverse}"></span></a></th>
                                        <th>Pago<a ng-click="OrdernarAsendente('Forma_Pago')"><span class="sortorder" ng-show="reverse" ng-class="{reverse: reverse}"></a><a ng-click="OrdernarDescendente('Forma_Pago')"><span class="sortorder" ng-show="!reverse" ng-class="{reverse: reverse}"></span></a></th>
                                        <th>Precio<a ng-click="OrdernarAsendente('Precio')"><span class="sortorder" ng-show="reverse" ng-class="{reverse: reverse}"></a><a ng-click="OrdernarDescendente('Precio')"><span class="sortorder" ng-show="!reverse" ng-class="{reverse: reverse}"></span></a></th>
                                        <th>Fecha<a ng-show="OrdernarAsendente" ng-click="OrdernarAsendente('Fecha')"><span class="sortorder" ng-show="reverse" ng-class="{reverse: reverse}"></span></a><a ng-show="OrdernarDescendente" ng-click="OrdernarDescendente('Fecha')"><span class="sortorder" ng-show="!reverse" ng-class="{reverse: reverse}"></span></a></th>
                                        <th>Seleccionar</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr ng-repeat="lista in Listado | filter: text_buscar | orderBy : campoOrdenado" class="odd gradeX">

                                        <td data-label="Tipo"><span class="label" ng-class="{'label-success': lista.Tipo.Nombre == 'INGRESO', 'label-danger': lista.Tipo.Nombre == 'EGRESO'}">{{lista.Tipo.Nombre}}</span></td>
                                        <td data-label="Nombre">{{lista.SubCate.Nombre}}</td>
                                        <td data-label="Categoría">{{lista.Cate.Nombre}}</td>
                                        <td data-label="Observación">{{lista.Observacion}}</td>
                                        <td data-label="Pago">{{lista.Forma_Pago.Nombre}}</td>
                                        <td data-label="Precio">{{lista.Precio|number}}</td>
                                        <td data-label="Fecha">{{lista.Fecha | date:'dd/MM/yyyy'}}</td>
                                        <td data-label="Seleccionar">
                                            <a class="btn btn-success" ng-click="Modificar(lista)"><i class="fa fa-pencil"></i></a>
                                            <a class="btn btn-danger" ng-click="Eliminar(lista)"><i class="fa fa-times"></i></a>
                                        </td>
                                    </tr>

                                </tbody>
                            </table>

                            <%--      <div class='btn-group'>
                                    <ul uib-pagination total-items="TotalRegistros" ng-model="PaginaActual" ng-change="pageChanged()" items-per-page="5"></ul>
                                </div>--%>

                            <%--   <div ng-repeat="lista in Listado | filter: text_buscar" class="panel panel-default bs-callout col-lg-12 col-md-12" ng-class="{'bs-callout-success': lista.Tipo.Nombre == 'INGRESO', 'bs-callout-danger': lista.Tipo.Nombre == 'EGRESO'}">
                                <div class="row">
                                    <div class="col-lg-6 col-md-12">

                                        <h4>{{lista.SubCate.Nombre}}</h4>
                                        <h5><strong>{{lista.Cate.Nombre}}</strong></h5>
                                        <h6>{{lista.Observacion}}</h6>
                                        <p class="text" ng-class="{'text-success': lista.Tipo.Nombre == 'INGRESO' , 'text-danger': lista.Tipo.Nombre == 'EGRESO'}"><strong>${{lista.Precio|number}}</strong> <small>({{lista.Forma_Pago.Nombre}})</small></p>

                                    </div>

                                    <div class="col-lg-6 col-md-12">
                                        <div class="text-right mobileChange">
                                            <h5><strong>{{lista.Fecha | date:'dd/MM/yyyy'}}</strong></h5>
                                            <h4><span class="label" ng-class="{'label-success': lista.Tipo.Nombre == 'INGRESO', 'label-danger': lista.Tipo.Nombre == 'EGRESO'}">{{lista.Tipo.Nombre}}</span></h4>
                                            <div class="form-group btn-group" style="padding-top: 10px;">
                                                <a class="btn btn-success" ng-click="Modificar(lista)"><i class="fa fa-pencil"></i></a>
                                                <a class="btn btn-danger" ng-click="Eliminar(lista)"><i class="fa fa-times"></i></a>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                        </div>



                    </div>
                </div>
            </div>
        </div>


        <div class="modal fade" id="div_Modal_Comentario">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: rgb(224,224,224);">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h3 ng-show="!Objeto.Id" class="panel-title">Nuevo</h3>
                        <h3 ng-show="Objeto.Id" class="panel-title">Modificar</h3>
                    </div>
                    <div class="modal-body col-lg-12">
                        <div class="col-lg-12">
                            <div class="col-lg-4">
                                <label for="cmbTipo">Tipo</label>
                                <ui-select ng-model="Objeto.Tipo" theme="select2" ng-disabled="disabled" style="min-width: 100%;">
                                <ui-select-match placeholder="Selecionar...">{{$select.selected.Nombre}}</ui-select-match>
                                <ui-select-choices repeat="tipo in tipos track by tipo.id">
                                  <div class="label" ng-class="{'label-success': tipo.Nombre == 'INGRESO', 'label-danger': tipo.Nombre == 'EGRESO'}" ng-bind-html="tipo.Nombre"></div>
                                </ui-select-choices>
                              </ui-select>
                            </div>
                            <div class="col-lg-4">
                                <label for="cmbCate">Categoría</label>
                                <ui-select ng-model="Objeto.Cate" theme="select2" ng-disabled="!Objeto.Tipo" style="min-width: 100%;">
                                                <ui-select-match placeholder="Selecionar...">{{$select.selected.Nombre}}</ui-select-match>
                                                <ui-select-choices repeat="cate in categorias | propsFilter: {Nombre: $select.search , Tipo: $select.search} | filter:{ Tipo:Objeto.Tipo.Nombre } track by cate.Id">
                                                  <div Style="font-weight: bold; font-size: 11px;" ng-bind-html="cate.Nombre | highlight: $select.search"></div>
                                                  <small>
                                                 <span class="label" ng-class="{'label-success': cate.Tipo == 'INGRESO', 'label-danger': cate.Tipo == 'EGRESO'}">{{cate.Tipo}}</span>
                                                  </small>
                                                </ui-select-choices>
                                              </ui-select>

                            </div>
                            <div class="form-group col-lg-4">
                                <label for="cmbSubCate">Sub Categoría</label>
                                <ui-select ng-model="Objeto.SubCate" theme="select2" ng-disabled="!Objeto.Cate" style="min-width: 100%;">
                                                <ui-select-match placeholder="Selecionar...">{{$select.selected.Nombre}}</ui-select-match>
                                                <ui-select-choices repeat="sub_cate in sub_categorias | propsFilter: {Nombre: $select.search}  | filter:{ Tipo:Objeto.Cate.Nombre } track by sub_cate.Id">
                                                  <div Style="font-weight: bold; font-size: 11px;" ng-bind-html="sub_cate.Nombre | highlight: $select.search"></div>
                                                  <small>
                                                 <span class="label label-primary">{{sub_cate.Tipo}}</span>
                                                  </small>
                                                </ui-select-choices>
                                              </ui-select>

                            </div>

                            <div class="form-group col-lg-4">
                                <label for="cmbfecha">Fecha</label>
                                <input type="date" class="form-control" ng-model="Objeto.Fecha" id="Fecha" name="Fecha" />
                            </div>

                            <div class="col-lg-4">
                                <label for="cmbSubCate">Precio</label>
                                <input type="number" class="form-control" ng-disabled="!Objeto.Fecha" ng-model="Objeto.Precio" />
                            </div>

                            <div class="col-lg-4">
                                <label for="cmbTipo">Forma Pago</label>
                                <ui-select ng-model="Objeto.Forma_Pago" theme="select2" ng-disabled="!Objeto.Precio" style="min-width: 100%;">
                                <ui-select-match placeholder="Selecionar...">{{$select.selected.Nombre}}</ui-select-match>
                                <ui-select-choices repeat="pago in formaPago track by pago.id">
                             <%--     <div class="fa fa-money" ng-bind-html="pago.Nombre"></div>--%>
                                    <span Style="font-weight: bold;"><i class="fa" ng-class="{'fa-dollar': pago.Nombre == 'Efectivo' , 'fa-credit-card': pago.Nombre == 'Tarjeta'}"></i> {{pago.Nombre}}</span>
                                </ui-select-choices>
                              </ui-select>

                            </div>

                            <div class="form-group col-lg-12">
                                <div class="form-group">
                                    <label for="message-text" class="form-control-label">Observacíon:</label>
                                    <textarea class="form-control" style="text-transform: uppercase" ng-model="Objeto.Observacion" id="message-text"></textarea>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <a type="button" class="btn btn-default" data-dismiss="modal">Cerrar</a>
                        <a ng-show="!Objeto.Id" type="button" class="btn btn-primary" ng-disabled="!Objeto.Precio || !Objeto.Fecha || !Objeto.Cate" ng-click="Guardar()">Guardar</a>
                        <a ng-show="Objeto.Id" type="button" class="btn btn-primary" ng-disabled="!Objeto.Precio || !Objeto.Fecha || !Objeto.Cate" ng-click="Guardar()">Modificar</a>
                    </div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>

