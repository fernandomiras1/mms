angular.module("MyApp").controller("Est_SubCateController", function ($scope, $http, $filter, HomeService, Est_SubCateService) {
    $scope.Objeto = {};
    $scope.Objeto.Anio = new Date().getFullYear();
    $scope.categorias = [];
    $scope.Init = function () {
        $scope.ObtenerTipo();
       // $scope.ObtenerSubCategorias();
    };

    $scope.localLang = {
        selectAll: "Marcar Todo",
        selectNone: "Quitar Todo",
        reset: "Reiniciar",
        search: "Escriba aquí para Buscar",
        nothingSelected: "Selecionar..."
    };

    //Obtener Tipos
    $scope.ObtenerTipo = function () {
        HomeService.ObtenerTipo().then(function (res) {
            $scope.tipos = res.Lista;
        });
    };
    $scope.btn_Buscar = function () {
        if (!$scope.Objeto.Tipo) return false;
        var IdsSubCate = $scope.Objeto.SubCate.map(function (p) { return p.Id });
        $scope.Objeto.IdsSubCate = IdsSubCate;
        Est_SubCateService.BuscarListado($scope.Objeto).then(function (res) {
            $scope.listaBuscar = res.Lista;
        });
    }
    // Estilos de los Graficos. 
    $scope.chartColors = ["#147718", "#c7254e", "#ff5722"];
    $scope.myFormatter = function (input) {
        return input + '%';
    };

    // Combo de SubCategorias.
    $scope.ObtenerSubCategorias = function (Tipo) {
       Est_SubCateService.ObtenerSubCategorias(Tipo).then(function (res) {
            $scope.categorias = res.Lista;
            //   $scope.ModCate = angular.copy($scope.categorias);
        });
    };

})