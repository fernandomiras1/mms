angular.module("MyApp").service("HomeService", function ($http) {
    // cuando usamos un paginador 
    //this.ObtenerListado = function (pagina) {
    this.ObtenerListado = function (periodo) {
        //return $http.post("/Home/ObtenerListado", { pagina: pagina })
        $("#div_EspereMaster").modal('show');
        return $http.post("/Home/ObtenerListado", { 'periodo': periodo })
            .then(function (res) {
                $("#div_EspereMaster").modal('hide');
                return res.data;
            })
            .catch(function (res) {
                console.log(res);
                $("#div_EspereMaster").modal('hide');
            });
      
    }

    this.ObtenerTipo = function () {
        return $http.post("/Home/ObtenerTipo")
            .then(function (res) {
                return res.data;
            })
            .catch(function (res) {
                console.log(res);
            });

    }

    this.ObtenerCategorias = function () {
        return $http.post("/Home/ObtenerCategorias")
            .then(function (res) {
                return res.data;
            })
            .catch(function (res) {
                console.log(res);
            });
    }

    this.ObtenerSubCategorias = function () {
        //  return $http.post("/Home/ObtenerSubCategorias", { 'tipo': tipo })
        return $http.post("/Home/ObtenerSubCategorias")
        .then(function (res) {
            return res.data;
        })
            .catch(function (res) {
                console.log(res);
            });
    }

    this.Forma_Pago = function () {
        return $http.post("/Home/Forma_Pago")
        .then(function (res) {
            return res.data;
        })
        .catch(function (res) {
            console.log(res);
        });
    }

    this.Guardar = function (Objeto) {
        return $http.post("/Home/Guardar", { modelo: Objeto })
            .then(function (res) {
                return res.data;
            })
            .catch(function (res) {
                console.log(res);
            });

    }


    this.Eliminar = function (Id) {
        return $http.post("/Home/Eliminar", { 'Id': Id })
            .then(function (res) {
                return res.data;
            })
            .catch(function (res) {
                console.log(res);
            })

    }

    this.BusquedaGeneral = function (Objeto_Busqueda) {
        $("#div_EspereMaster").modal('show');
        return $http.post("/Home/BusquedaGeneral", { modelo: Objeto_Busqueda })
            .then(function (res) {
                $("#div_EspereMaster").modal('hide');
                return res.data;
            })
            .catch(function (res) {
                console.log(res);
            })
        $("#div_EspereMaster").modal('hide');
    }

    this.Grafico_Area = function () {
        return $http.post("/Home/Grafico_Area")
            .then(function (res) {
                return res.data;
            })
            .catch(function (res) {
                console.log(res);
            })

    }

    this.Grafico_Donut = function (Objeto_Busqueda) {
        return $http.post("/Home/Grafico_Donut", { modelo: Objeto_Busqueda })
            .then(function (res) {
                return res.data;
            })
            .catch(function (res) {
                console.log(res);
            });
    };

    this.Recordatorio_Alarmas = function () {
        return $http.post("/Home/Recordatorio_Alarmas")
           .then(function (res) {
               return res.data;
           })
            .catch(function (res) {
                console.log(res);
            });
    };

})