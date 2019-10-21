angular.module("MyApp").controller("ConfiguracionController", function ($scope, $http, $filter, ConfiguracionService, HomeService) {
    $scope.Objeto = {};
 
    $scope.Init = function () {
        $scope.Obtener_ListadoCategorias();
        $scope.ObtenerCategorias();
        $scope.ObtenerTipo();
    };

    //Combo_Tipo
    $scope.ObjetoTipo = {};
    $scope.sel_Tipo = [];

    // Combo_Sub_Cate
    $scope.Selector = {};
    $scope.sel_Cate = [];

    $scope.Open_SubCategorias = function () {
        $scope.ObtenerCategorias();
        $scope.Obtener_Listado_SubCategorias();
    }

    $scope.Obtener_ListadoCategorias = function () {
        ConfiguracionService.Obtener_ListadoCategorias().then(function (res) {
            $scope.ListadoCate = res.Lista;
            angular.forEach($scope.ListadoCate, function (val) {
                val.Fecha = Utils.parseJsonDate(val.Fecha);
            });

            //limpio los campos.
            $scope.sel_Tipo ='' ;
            $scope.text_Nombre = '';

        });

    };

    $scope.btn_GuardarCate = function () {
        if (!$scope.text_Nombre) return false;
        ConfiguracionService.GuardarCategoria($scope.text_Nombre, $scope.ObjetoTipo.sel_Tipo.id).then(function (res) {
            if (res.Ok) {
                $scope.Obtener_ListadoCategorias();
                alertify.success(res.Mensaje);
            }
            else alertify.error(res.Mensaje);

        });

    };

    $scope.Obtener_Listado_SubCategorias = function () {
        ConfiguracionService.ObtenerListado_SubCate().then(function (res) {
            $scope.Listado_SubCate = res.Lista;
            angular.forEach($scope.Listado_SubCate, function (val) {
                val.Fecha = Utils.parseJsonDate(val.Fecha);

            });
            //limpio los campos.
            $scope.Selector.sel_Cate = '';
            $scope.text_SubCate_Nom = '';

        });

    };

    $scope.Open_ModificarCate = function (Objeto) {
        $scope.Objeto = Objeto;
        //$scope.Objeto.Id = Objeto.Id;
        //$scope.Objeto.Tipo = Objeto.Tipo;
        //$scope.Objeto.Nombre = Objeto.Nombre;
        $('#div_Modal_Cate').modal('show');
    }

    $scope.ModificarCate = function (Objeto) {
        if (!$scope.Objeto.Nombre) return false;
        var funcionOk = function () {
            ConfiguracionService.ModificarCate(Objeto).then(function (res) {
                if (res.Ok) {
                    $scope.Obtener_ListadoCategorias();
                    $('#div_Modal_Cate').modal('hide');
                    alertify.success(res.Mensaje);

                }
                else {
                    $('#div_Modal_Cate').modal('hide');
                    alertify.error(res.Mensaje);
                }

            });

        }
        Utils.MensajeConfirmar("Atención", "Si modifica esta Categoría, se cambiaran tambíen de sus Ingresos/Egresos que pertenecen a esa Categoría. ¿Desea continuar?", funcionOk, function () { /* funcion cancel */ }, "Modificar", "Cancelar");


    }

    $scope.EliminarCate = function (Objeto) {

        if (!Objeto.Id) return false;
        var funcionOk = function () {
            ConfiguracionService.EliminarCate(Objeto).then(function (res) {
                if (res.Ok) {
                    $scope.Obtener_ListadoCategorias();
                    alertify.success(res.Mensaje);
                }
                else alertify.error(res.Mensaje);

            });

        }

        Utils.MensajeConfirmar("Atención", "Si elimina esta Categoría, se eliminaran tambíen de sus Ingresos/Egresos que pertenecen a esa Categoría. ¿Desea continuar?", funcionOk, function () { /* funcion cancel */ }, "Borrar", "Cancelar");

    }

    // combo de Categorias.
    $scope.ObtenerCategorias = function () {
        HomeService.ObtenerCategorias().then(function (res) {
            $scope.categorias = res.Lista;
            $scope.ModCate = angular.copy($scope.categorias);
        });
    };

    // combo Tipo
    $scope.ObtenerTipo = function () {
        HomeService.ObtenerTipo().then(function (res) {
            $scope.tipos = res.Lista;
        })
    }

    $scope.btn_GuardarSubCate = function () {
        if (!$scope.text_SubCate_Nom) return false;
        ConfiguracionService.GuardarSubCate($scope.Selector.sel_Cate.Id, $scope.text_SubCate_Nom).then(function (res) {
            if (res.Ok) {
                $scope.Obtener_Listado_SubCategorias();
                alertify.success(res.Mensaje);
            }
            else alertify.error(res.Mensaje);

        });

    }

    $scope.Open_ModificarSubCate = function (Objeto) {
        $scope.id_SubCateModificar = Objeto.Id;
        $scope.Selector.sel_Mod_Cate = Objeto.Categoria;
        $scope.text_Nombre = Objeto.Nombre;
        $('#div_Modal_Sub_Cate').modal('show');
    }


    $scope.ModificarSubCate = function () {
        if (!$scope.text_Nombre) return false;
        var funcionOk = function () {
            var Objeto_ModiSubCate = {
                Id: $scope.id_SubCateModificar,
                Categoria: $scope.Selector.sel_Mod_Cate,
                Nombre: $scope.text_Nombre
            }
           ConfiguracionService.ModificarSubCate(Objeto_ModiSubCate).then(function (res) {
                if (res.Ok) {
                    $scope.Obtener_Listado_SubCategorias();
                    $('#div_Modal_Sub_Cate').modal('hide');
                    alertify.success(res.Mensaje);
                }
                else alertify.error(res.Mensaje);
            });
        }
        Utils.MensajeConfirmar("Atención", "Si modifica esta Sub Categoría, se modificaran tambíen de sus Ingresos/Egresos que pertenecen a esta Sub Categoría. ¿Desea continuar?", funcionOk, function () { /* funcion cancel */ }, "Modificar", "Cancelar");

    }

    $scope.EliminarSubCate = function (Objeto) {
        if (!Objeto.Id) return false;
        debugger;
        var funcionOk = function () {
            ConfiguracionService.EliminarSubCate(Objeto).then(function (res) {
                if (res.Ok) {
                    $scope.Obtener_Listado_SubCategorias();
                    alertify.success(res.Mensaje);
                }
                else alertify.error(res.Mensaje);
           });

        }

        Utils.MensajeConfirmar("Atención", "Si elimina esta Sub Categoría, se eliminaran tambíen de sus Ingresos/Egresos que pertenecen a esta Sub Categoría. ¿Desea continuar?", funcionOk, function () { /* funcion cancel */ }, "Borrar", "Cancelar");

    }


})