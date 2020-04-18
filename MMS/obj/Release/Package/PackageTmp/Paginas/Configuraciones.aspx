<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Paginas/PaginaMaestra.Master" CodeBehind="Configuraciones.aspx.vb" Inherits="MMS.Configuraciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <header>
        <script src="../Scripts/Service/ConfiguracionesService.js"></script>
        <script src="../Scripts/Controllers/ConfiguracionesController.js"></script>

    </header>

    <div ng-controller="ConfiguracionController" ng-init="Init();" ng-cloak>

        <!-- Page Heading -->
        <div class="row">
            <div class="col-lg-12">

                <h1 class="page-header">Configuraciones</h1>
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

        <!-- /.row -->
        <div class="row">
            <div class="col-md-12 col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Panel de Configuraciones
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <!-- Nav tabs -->
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#Categoriras" data-toggle="tab"><strong>Categorías</strong></a>
                            </li>
                            <li><a href="#Sub_Categoriras" ng-click="Open_SubCategorias()" data-toggle="tab"><strong>Sub Categorías</strong></a>
                            </li>
                        </ul>

                        <!-- Tab panes -->
                        <div class="tab-content">
                            <div class="tab-pane fade in active" id="Categoriras">

                                <div class="panel panel-default">

                                    <!-- /.panel-heading -->
                                    <div class="panel-body">

                                        <div class="form-group panel panel-default col-md-4 col-lg-4">
                                            <div ng-show="ListadoCate.length > 0" class="form-group col-lg-12">
                                                <br />
                                                <div class="input-group">
                                                    <span class="input-group-addon"><i class="fa fa-search"></i>
                                                    </span>
                                                    <input type="text" ng-model="text_buscar" class="form-control" placeholder="Buscar...">
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-12">
                                                <label for="cmbTipo">Tipo</label>
                                                <ui-select ng-model="ObjetoTipo.sel_Tipo" ng-disabled="disabled" style="min-width: 100%;" title="Seleccione un Tipo">
                                                <ui-select-match placeholder="Selecionar...">{{$select.selected.Nombre}}</ui-select-match>
                                                <ui-select-choices repeat="tipo in tipos track by tipo.id">
                                                  <div class="label" ng-class="{'label-success': tipo.Nombre == 'INGRESO', 'label-danger': tipo.Nombre == 'EGRESO'}" ng-bind-html="tipo.Nombre"></div>
                                                </ui-select-choices>
                                              </ui-select>

                                            </div>

                                            <div class="form-group col-lg-12">
                                                <label for="NombreCate">Nombre</label>
                                                <input type="text" class="form-control" ng-disabled="!ObjetoTipo.sel_Tipo" ng-model="text_Nombre" name="Cate" placeholder="Nombre de la Categoría" />
                                            </div>

                                            <div class="form-group col-lg-12">
                                                <a class="btn btn-success btn-sm" ng-disabled="!text_Nombre" ng-click="btn_GuardarCate()">Guardar
                                                    <span class="fa fa-save"></span>
                                                </a>
                                            </div>

                                        </div>

                                        <div class="form-group col-md-8 col-lg-8">
                                            <div ng-show="ListadoCate.length < 1" class="alert alert-danger alert-dismissable">
                                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                                No Hay Datos Disponibles.
                                            </div>

                                            <div class="table-responsive table-fer">
                                                <table ng-show="ListadoCate.length > 0" class="table table-sm table-striped table-bordered table-hover">
                                                    <thead>
                                                        <tr class="info">
                                                            <th>Tipo</th>
                                                            <th>Nombre</th>
                                                            <th>Fecha</th>
                                                            <th>Seleccionar</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr ng-class="{success: lista.Tipo == 'INGRESO', danger: lista.Tipo == 'EGRESO'}" ng-repeat="lista in ListadoCate | filter: text_buscar">
                                                            <td data-label="Tipo"><span class="label" ng-class="{'label-success': lista.Tipo.Nombre == 'INGRESO', 'label-danger': lista.Tipo.Nombre == 'EGRESO'}">{{lista.Tipo.Nombre}}</span></td>
                                                            <td data-label="Nombre">{{lista.Nombre}}</td>
                                                            <td data-label="Fecha">{{lista.Fecha | date: 'dd/MM/yyyy'}}</td>
                                                            <td data-label="Seleccionar">
                                                                <a ng-click="Open_ModificarCate(lista)"><i class="fa fa-2x fa-pencil-square"></i></a>
                                                                <a ng-click="EliminarCate(lista)"><i class="fa fa-2x fa-times-circle"></i></a>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>

                                        </div>
                                    </div>

                                </div>

                                <!-- /.panel-body -->
                            </div>
                            <%--SUB CATEGORIAS--%>
                            <div class="tab-pane fade" id="Sub_Categoriras">

                                <div class="panel panel-default">

                                    <!-- /.panel-heading -->
                                    <div class="panel-body">

                                        <div class="form-group panel panel-default col-md-4 col-lg-4">
                                            <div ng-show="ListadoCate.length > 0" class="form-group col-lg-12">
                                                <br />
                                                <div class="input-group">
                                                    <span class="input-group-addon"><i class="fa fa-search"></i>
                                                    </span>
                                                    <input type="text" ng-model="text_buscar_SubCate" class="form-control" placeholder="Buscar...">
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-12">
                                                <label for="cmbCate">Categoría</label>
                                                <ui-select ng-model="Selector.sel_Cate" theme="select2" ng-disabled="disabled" style="min-width: 100%;" title="Seleccione una Categoría">
                                                <ui-select-match placeholder="Selecionar...">{{$select.selected.Nombre}}</ui-select-match>
                                                <ui-select-choices repeat="cate in categorias | propsFilter: {Nombre: $select.search , Tipo: $select.search} track by cate.Id">
                                                  <div Style="color:darkblue;font-weight: bold;" ng-bind-html="cate.Nombre | highlight: $select.search"></div>
                                                  <small>
                                                 <span class="label" ng-class="{'label-success': cate.Tipo == 'INGRESO', 'label-danger': cate.Tipo == 'EGRESO'}">{{cate.Tipo}}</span>
                                                  </small>
                                                </ui-select-choices>
                                              </ui-select>

                                            </div>

                                            <div class="form-group col-lg-12">
                                                <label for="NombreCate">Nombre</label>
                                                <input type="text" class="form-control" ng-disabled="!Selector.sel_Cate" ng-model="text_SubCate_Nom" name="Cate" placeholder="Nombre de la Sub Categoría" />
                                            </div>

                                            <div class="form-group col-lg-12">
                                                <a class="btn btn-success btn-sm" ng-disabled="!text_SubCate_Nom" ng-click="btn_GuardarSubCate()">Guardar
                                                    <span class="fa fa-save"></span>
                                                </a>
                                            </div>
                                        </div>

                                        <div class="form-group col-md-8 col-lg-8">
                                            <div ng-show="Listado_SubCate.length < 1" class="alert alert-danger alert-dismissable">
                                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                                No Hay Datos Disponibles.
                                            </div>

                                            <div class="table-responsive table-fer">
                                                <table ng-show="Listado_SubCate.length > 0" class="table table-sm table-striped table-bordered table-hover">
                                                    <thead>
                                                        <tr class="info">
                                                            <th>Categoría</th>
                                                            <th>Nombre</th>
                                                            <th>Fecha</th>
                                                            <th>Seleccionar</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr ng-class="{success: listaSubCate.Categoria.Tipo == 'INGRESO', danger: listaSubCate.Categoria.Tipo == 'EGRESO'}" ng-repeat="listaSubCate in Listado_SubCate | filter: text_buscar_SubCate">
                                                            <td data-label="Categoría"><span class="label" ng-class="{'label-success': listaSubCate.Categoria.Tipo == 'INGRESO', 'label-danger': listaSubCate.Categoria.Tipo == 'EGRESO'}">{{listaSubCate.Categoria.Nombre}}</span></td>
                                                            <td data-label="Sub Categoría">{{listaSubCate.Nombre}}</td>
                                                            <td data-label="Fecha">{{listaSubCate.Fecha | date: 'dd/MM/yyyy'}}</td>
                                                            <td data-label="Seleccionar">
                                                                <a ng-click="Open_ModificarSubCate(listaSubCate)"><i class="fa fa-2x fa-pencil-square"></i></a>
                                                                <a ng-click="EliminarSubCate(listaSubCate)"><i class="fa fa-2x fa-times-circle"></i></a>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
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


        <div class="modal fade" id="div_Modal_Cate" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: rgb(224,224,224);">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h3 class="panel-title">Modificar Categoría</h3>
                    </div>
                    <div class="modal-body col-lg-12">

                        <div class="col-md-4 col-lg-4">
                            <label for="cmbTipo">Tipo</label>
                            <ui-select ng-model="Objeto.Tipo" ng-disabled="disabled" style="min-width: 100%;" title="Seleccione un Tipo">
                              <ui-select-match placeholder="Selecionar...">{{$select.selected.Nombre}}</ui-select-match>
                              <ui-select-choices repeat="tipo in tipos track by tipo.id">
                                <div class="label" ng-class="{'label-success': tipo.Nombre == 'INGRESO', 'label-danger': tipo.Nombre == 'EGRESO'}" ng-bind-html="tipo.Nombre"></div>
                              </ui-select-choices>
                            </ui-select>
                        </div>

                        <div class="col-md-8 col-lg-8">
                            <label for="TextNombre">Nombre</label>
                            <input type="type" ng-disabled="!Objeto.Tipo" class="form-control" ng-model="Objeto.Nombre" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <a type="button" class="btn btn-default" data-dismiss="modal">Cerrar</a>
                        <a type="button" class="btn btn-primary" ng-disabled="!Objeto.Nombre" ng-click="ModificarCate(Objeto)">Modificar</a>
                    </div>
                </div>
            </div>
        </div>


        <div class="modal fade" id="div_Modal_Sub_Cate" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: rgb(224,224,224);">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h3 class="panel-title">Modificar Sub Categoría</h3>
                    </div>
                    <div class="modal-body col-lg-12">
                        <div class="col-md-6 col-lg-6">
                            <label for="cmbCate">Categoría</label>
                            <%--  <select id="cmbCate" class="form-control" ng-model="sel_Mod_Cate" ng-options="cate as cate.Nombre for cate in ModCate track by cate.Id">
                                <option value=''>-- Seleccione --</option>
                            </select>--%>
                            <ui-select ng-model="Selector.sel_Mod_Cate" ng-disabled="disabled" style="min-width: 100%;" title="Seleccione una Categoría">
                                                <ui-select-match placeholder="Selecionar...">{{$select.selected.Nombre}}</ui-select-match>
                                                <ui-select-choices repeat="cate in ModCate | propsFilter: {Nombre: $select.search , Tipo: $select.search} track by cate.Id">
                                                  <div Style="color:darkblue;font-weight: bold;" ng-bind-html="cate.Nombre | highlight: $select.search"></div>
                                                  <small>
                                                 <span class="label" ng-class="{'label-success': cate.Tipo == 'INGRESO', 'label-danger': cate.Tipo == 'EGRESO'}">{{cate.Tipo}}</span>
                                                  </small>
                                                </ui-select-choices>
                                              </ui-select>


                        </div>

                        <div class="col-md-6 col-lg-6">
                            <label for="TextNombre">Nombre</label>
                            <input type="type" ng-disabled="!Selector.sel_Mod_Cate" class="form-control" ng-model="text_Nombre" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <a type="button" class="btn btn-default" data-dismiss="modal">Cerrar</a>
                        <a type="button" class="btn btn-primary" ng-disabled="!text_Nombre" ng-click="ModificarSubCate()">Modificar</a>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>

