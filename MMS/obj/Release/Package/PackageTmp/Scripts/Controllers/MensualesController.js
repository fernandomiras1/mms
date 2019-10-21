angular.module("MyApp").controller("MensualesController", function ($scope, $http, $filter, MensualesService, HomeService) {

    $scope.Objeto = {};
    $scope.Nombre = [];
    $scope.TipoMensual = {};
    $scope.Objeto.Recordatorio = false;
    $scope.Init = function () {
        $scope.Obtener_ListadoIngreso();
        $scope.Obtener_ListadoEgreso();
        $scope.ObtenerSubCategorias();
        $scope.Forma_Pago();
    };

    $scope.Obtener_ListadoIngreso = function () {
        MensualesService.Obtener_ListadoIngreso().then(function (res) {
            $scope.ListadoIngreso = res.Lista;
            angular.forEach($scope.ListadoIngreso, function (val) {
                val.FechaVencimiento = Utils.parseJsonDate(val.FechaVencimiento);
            });
        });

    };

    // Combo de SubCategorias.
    $scope.ObtenerSubCategorias = function () {
        MensualesService.ObtenerSubCategorias().then(function (res) {
            $scope.categorias = res.Lista;
            //   $scope.ModCate = angular.copy($scope.categorias);
        });
    };

    $scope.btn_Nuevo_Ingreso = function (TipoMensual) {
        $scope.TipoMensual = TipoMensual;
        $scope.Objeto = {};
        $scope.Objeto.Fecha = new Date();
        $scope.Objeto.FechaVencimiento = new Date();
        $('#div_Modal_Ingreso').modal('show');

    }

    $scope.GuardadoGeneral = function () {
        if ($scope.TipoMensual) $scope.GuardarIngreso(); else $scope.GuardarEgreso();
    }

    $scope.GuardarIngreso = function () {
        if (!$scope.Objeto.Precio || !$scope.Objeto.Fecha || !$scope.Objeto.FechaVencimiento || !$scope.Objeto.Nombre)
            return false;
        MensualesService.GuardarIngreso($scope.Objeto).then(function (res) {
            if (res.Ok) {
                $scope.Obtener_ListadoIngreso();
                $('#div_Modal_Ingreso').modal('hide');
                alertify.success(res.Mensaje);
            }
            else {
                alertify.error(res.Mensaje);
                $('#div_Modal_Ingreso').modal('hide');
            }
        })

    }


    $scope.Open_ModificarIngreso = function (Objeto) {
        $scope.TipoMensual = true;
        $('#div_Modal_Ingreso').modal('show');
        $scope.Objeto = Objeto;
    }
    $scope.Open_EliminarIngreso = function (Objeto) {
        var funcionOk = function () {
            MensualesService.EliminarMensual_Ingreso(Objeto).then(function (res) {
                if (res.Ok) {
                    $scope.Obtener_ListadoIngreso();
                    alertify.success(res.Mensaje);
                }
                else alertify.error(res.Mensaje);

            });

        }
        Utils.MensajeConfirmar("Atención", "Una vez eliminado no podra volver a recuperar el Registro. ¿Desea eliminar el Registro?", funcionOk, function () { /* funcion cancel */ }, "Borrar", "Cancelar");

    }

    // btn_Pagado_Ingreso.
    $scope.Ingreso_Pagado_Ingreso = function (Objeto) {
        $scope.TipoMensual = true;
        $('#div_Modal_Pagado').modal('show');
        $scope.Objeto_Pagado = Objeto;
       // var NombreMes = new Date().toLocaleString("es-AR", { month: "long" }).toUpperCase();
        $scope.Objeto_Pagado.Observacion = "MENSUAL " + Objeto.Num_MesAnterior + " - " + Objeto.Nombre.Nombre.toUpperCase() + "(" + Objeto.Detalle.toUpperCase() + ") POR $ " + Objeto.Precio +"";
        $scope.Objeto_Pagado.Fecha = new Date();

   }

    // Obtener Combo Forma_Pago
    $scope.Forma_Pago = function () {
        HomeService.Forma_Pago().then(function (res) {
            $scope.formaPago = res.Lista;
        });
    }

    $scope.GuardarPagado = function () {
        if ($scope.TipoMensual) $scope.GuardarPagado_Ingreso(); else $scope.GuardarPagado_Egreso();
    }

    $scope.GuardarPagado_Ingreso = function () {
        if (!$scope.Objeto_Pagado.Precio || !$scope.Objeto_Pagado.Fecha || !$scope.Objeto_Pagado.Nombre || !$scope.Objeto_Pagado.Forma_Pago)
            return false;
         MensualesService.EstadoPagado($scope.Objeto_Pagado).then(function (res) {
            if (res.Ok) {

                $('#div_Modal_Pagado').modal('hide');
                $scope.Obtener_ListadoIngreso();
                alertify.success(res.Mensaje);
            }
            else {
                $('#div_Modal_Pagado').modal('hide');
                alertify.error(res.Mensaje);
            }

        });
    }
    ///////////////// EGRESOS /////////////////

    $scope.Obtener_ListadoEgreso = function () {
        MensualesService.Obtener_ListadoEgreso().then(function (res) {
            $scope.ListadoEgreso = res.Lista;
            angular.forEach($scope.ListadoEgreso, function (val) {
                val.FechaVencimiento = Utils.parseJsonDate(val.FechaVencimiento);
            });
        });
    }

    $scope.GuardarEgreso = function () {
        if (!$scope.Objeto.Precio || !$scope.Objeto.Fecha || !$scope.Objeto.FechaVencimiento || !$scope.Objeto.Nombre)
            return false;
        MensualesService.GuardarEgreso($scope.Objeto).then(function (res) {
            if (res.Ok) {
                $scope.Obtener_ListadoEgreso();
                $('#div_Modal_Ingreso').modal('hide');
                alertify.success(res.Mensaje);
            }
            else {
                alertify.error(res.Mensaje);
                $('#div_Modal_Ingreso').modal('hide');
            }
        })

    }

    $scope.Open_Modificar_Egreso = function (Objeto) {
        $scope.TipoMensual = false;
        $('#div_Modal_Ingreso').modal('show');
        $scope.Objeto = Objeto;
    }

    $scope.Open_Eliminar_Egreso = function (Objeto) {
        var funcionOk = function () {
            MensualesService.EliminarMensual_Egreso(Objeto).then(function (res) {
                if (res.Ok) {
                    $scope.Obtener_ListadoEgreso();
                    alertify.success(res.Mensaje);
                }
                else alertify.error(res.Mensaje);

            });

        }
        Utils.MensajeConfirmar("Atención", "Una vez eliminado no podra volver a recuperar el Registro. ¿Desea eliminar el Registro?", funcionOk, function () { /* funcion cancel */ }, "Borrar", "Cancelar");

    }


    $scope.GuardarPagado_Egreso = function () {
        if (!$scope.Objeto_Pagado.Precio || !$scope.Objeto_Pagado.Fecha || !$scope.Objeto_Pagado.Nombre || !$scope.Objeto_Pagado.Forma_Pago)
            return false;
        MensualesService.EstadoPagadoEgreso($scope.Objeto_Pagado).then(function (res) {
            if (res.Ok) {

                $('#div_Modal_Pagado').modal('hide');
                $scope.Obtener_ListadoEgreso();
                alertify.success(res.Mensaje);
            }
            else {
                $('#div_Modal_Pagado').modal('hide');
                alertify.error(res.Mensaje);
            }

        });
    }

    // btn_Pagado_Egreso
    $scope.Ingreso_Pagado_Egreso = function (Objeto) {
        $scope.TipoMensual = false;
        $('#div_Modal_Pagado').modal('show');
        $scope.Objeto_Pagado = Objeto;
        var NombreMes = new Date().toLocaleString("es-AR", { month: "long" }).toUpperCase();
        $scope.Objeto_Pagado.Observacion = "MENSUAL " + NombreMes + " - " + Objeto.Nombre.Nombre.toUpperCase() + "(" + Objeto.Detalle.toUpperCase() + ") POR $ " + Objeto.Precio + "";
        $scope.Objeto_Pagado.Fecha = new Date();

        //   $scope.Objeto_Pagado.Forma_Pago.Nombre = "Efectivo"; 
    }

})