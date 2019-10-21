angular.module("MyApp").controller("CierreMesController", function ($scope, $http, $filter, CierreMesService) {

    $scope.Objeto = {};
    $scope.Objeto.Mes = {};

    $scope.Estructuras = [];

    $scope.PaginaActual = 0;
    $scope.CantidadxPagina = 10;
    $scope.Estructura = {};

    $scope.Init = function () {
        $scope.ObtenerListado(1);
        $scope.ObtenerMes();
    };


    var habilitado = Utils.ObtenerParametrosURL('Post');
    var data = JSON.parse(sessionStorage.getItem('key'));
  
    $scope.ObtenerListado = function (pag) {
        CierreMesService.ObtenerListado(pag, $scope.CantidadxPagina).then(function (res) {
            $scope.ObtenerListadoCierreMes = res.Lista;
            $scope.TotalDatos = res.Total;
            $scope.PaginaActual = pag;
            angular.forEach($scope.ObtenerListadoCierreMes, function (val) {
                val.Fecha = Utils.parseJsonDate(val.Fecha);
            });
            if ($scope.ObtenerListadoCierreMes.length > 0) {
                $("#morris-bar-chart").empty();
                Morris.Bar(res.Lista[0].Grafico.MBar);
            }

        });
    }


    $scope.RangoPaginado = function (min, max) {
        var input = [];
        var numero = min;
        for (var i = min; i < max; i += $scope.CantidadxPagina) {
            numero++;
            input.push(numero);
        }
        return input;
    };



    $scope.ObtenerMes = function () {
        CierreMesService.ObtenerMes().then(function (res) {
            $scope.Mes = res.Lista;
        })
    }

    $scope.btn_Nuevo = function () {
        $scope.Objeto = {};
        if (habilitado) {
            var data = JSON.parse(sessionStorage.getItem('key'));
            $scope.Objeto.Monto = data;
        }
        else $scope.Objeto.Monto = 0;
        $scope.Objeto.Anio = new Date().getFullYear();
        $('#div_Modal_CierreMes').modal('show');
    }


    $scope.GuardarCierreMes = function () {
        if (!$scope.Objeto.Mes || !$scope.Objeto.Monto || !$scope.Objeto.Anio) return false;
        CierreMesService.GuardarCierreMes($scope.Objeto).then(function (res) {
            if (res.Ok) {
                $scope.ObtenerListado(1);
                $('#div_Modal_CierreMes').modal('hide');
                alertify.success(res.Mensaje);
                sessionStorage.removeItem('key');
            }
            else {
                $('#div_Modal_CierreMes').modal('hide');
                alertify.error(res.Mensaje);
            }
        })
    }


    $scope.Modificar = function (Objeto) {
        $scope.Objeto.Id = Objeto.Id;
        $scope.Objeto.Mes = Objeto.Mes;
        $scope.Objeto.Monto = Objeto.Monto;
        $scope.Objeto.Detalle = Objeto.Detalle;
        $scope.Objeto.Anio = parseInt(Objeto.Anio);
        $('#div_Modal_CierreMes').modal('show');
    }


    $scope.Eliminar = function (Objeto) {
        var funcionOk = function () {
             CierreMesService.Eliminar(Objeto.Id).then(function (res) {
                if (res.Ok) {
                    $scope.ObtenerListado(1);
                    alertify.success(res.Mensaje);
                }
                else {
                    alertify.error(res.Mensaje);
                }
            });

        }

        Utils.MensajeConfirmar("Atención", "Una vez eliminado no podra volver a recuperar el Registro. ¿Desea eliminar el Registro?", funcionOk, function () { /* funcion cancel */ }, "Borrar", "Cancelar");

    }


})