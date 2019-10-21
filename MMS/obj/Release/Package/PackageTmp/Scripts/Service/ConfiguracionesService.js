angular.module("MyApp").service("ConfiguracionService", function ($http) {

    this.Obtener_ListadoCategorias = function () {
        return $http.post("/Configuracion/Obtener_ListadoCategorias")
        .then(function (res) {
            return res.data;
        })
        .catch(function (res) {
            return console.log(res);
        });
    };

    this.GuardarCategoria = function (Nombre, id_Tipo) {
        return $http.post("/Configuracion/GuardarCategoria", { 'Nombre': Nombre, 'id_Tipo': id_Tipo })
        .then(function (res) {
            return res.data;
        })
        .catch(function (res) {
            return console.log(res);
        });
    };


    this.ObtenerListado_SubCate = function () {
        return $http.post("/Configuracion/ObtenerListado_SubCate")
        .then(function (res) {
            return res.data;
        })
        .catch(function (res) {
            return console.log(res);
        });
    };

    this.ModificarCate = function (Objeto) {
        return $http.post("/Configuracion/ModificarCate", { modelo: Objeto })
        .then(function (res) {
            return res.data;
        })
        .catch(function (res) {
            return console.log(res);
        });
    };

    this.EliminarCate = function (Objeto) {
        return $http.post("/Configuracion/EliminarCate", { modelo: Objeto })
        .then(function (res) {
            return res.data;
        })
        .catch(function (res) {
            return console.log(res);
        });
    };

    this.GuardarSubCate = function (Id_Cate, Nombre) {
        return $http.post("/Configuracion/GuardarSubCate", { 'Id_Cate': Id_Cate, 'Nombre': Nombre })
            .then(function (res) {
                return res.data;
            })
            .catch(function (res) {
                console.log(res);
            });
    }

    this.ModificarSubCate = function (modelo) {
        return $http.post("/Configuracion/ModificarSubCate", { modelo: modelo })
        .then(function (res) {
            return res.data;
        })
        .catch(function (res) {
            console.log(res);
        });
    }

    this.EliminarSubCate = function (Objeto) {
        return $http.post("/Configuracion/EliminarSubCate", { Objeto: Objeto })
             .then(function (res) {
                 return res.data;
             })
             .catch(function (res) {
                 console.log(res);
             });

    }

})