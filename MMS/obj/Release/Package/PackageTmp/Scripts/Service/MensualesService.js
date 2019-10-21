angular.module("MyApp").service("MensualesService", function ($http) {

    this.Obtener_ListadoIngreso = function () {
        return $http.post("/Mensuales/ListadoIngreso")
        .then(function (res) {
            return res.data;
        })
        .catch(function (res) {
            console.log(res);
        });
    };

    this.ObtenerSubCategorias = function (Tipo) {
       // return $http.post("/Mensuales/ObtenerSubCategorias", { 'Tipo': Tipo })
        return $http.post("/Mensuales/ObtenerSubCategorias")
         .then(function (res) {
             return res.data;
         })
         .catch(function (res) {
             console.log(res);
         });
    }

    this.GuardarIngreso = function (Objeto) {
        return $http.post("/Mensuales/GuardarIngreso", { Objeto: Objeto })
        .then(function (res) {
            return res.data;
        })
        .catch(function (res) {
            console.log(res);
        });
    };

    this.EliminarMensual_Ingreso = function (Objeto) {
        return $http.post("/Mensuales/EliminarMensual_Ingreso", { Objeto: Objeto })
            .then(function (res) {
                return res.data;
            })
            .catch(function (res) {
                console.log(res);
            });
    };


    this.Obtener_ListadoEgreso = function () {
        return $http.post("/Mensuales/Obtener_ListadoEgreso")
        .then(function (res) {
            return res.data;
        })
         .catch(function (res) {
                  console.log(res);
              });

        };


    this.EstadoPagado = function (Objeto) {
        return $http.post("/Mensuales/EstadoPagado", { Objeto: Objeto })
         .then(function (res) {
             return res.data;
         })
        .catch(function (res) {
            console.log(res);
        });
    };


    this.GuardarEgreso = function (Objeto) {
        return $http.post("/Mensuales/GuardarEgreso", { Objeto: Objeto })
        .then(function (res) {
            return res.data;
        })
        .catch(function (res) {
            console.log(res);
        });
    };

    this.EliminarMensual_Egreso = function (Objeto) {
        return $http.post("/Mensuales/EliminarMensual_Egreso", { Objeto: Objeto })
            .then(function (res) {
                return res.data;
            })
            .catch(function (res) {
                console.log(res);
            });
    };


    this.EstadoPagadoEgreso = function (Objeto) {
        return $http.post("/Mensuales/EstadoPagadoEgreso", { Objeto: Objeto })
         .then(function (res) {
             return res.data;
         })
        .catch(function (res) {
            console.log(res);
        });
    };

})