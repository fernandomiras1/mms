angular.module("MyApp").service("Est_SubCateService", function ($http) {

    this.BuscarListado = function (Objeto) {
         return $http.post("/Est_SubCate/BuscarListado", { Objeto: Objeto })
        .then(function (res) {
            return res.data;
        })
        .catch(function (res) {
            console.log(res);
        });
    };

    this.ObtenerSubCategorias = function (Tipo) {
        // return $http.post("/Mensuales/ObtenerSubCategorias", { 'Tipo': Tipo })
        return $http.post("/Est_SubCate/ObtenerSubCategorias", { 'Tipo': Tipo })
         .then(function (res) {
             return res.data;
         })
         .catch(function (res) {
             console.log(res);
         });
    }
   

})