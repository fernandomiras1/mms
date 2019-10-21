angular.module("MyApp").service("CierreMesService", function ($http) {

    this.ObtenerListado = function (pagina, cant) {
        $("#div_EspereMaster").modal('show');
        return $http.post("/CierreMes/ObtenerListado", { 'pagina': pagina, 'cant': cant })
         .then(function (res) {
             $("#div_EspereMaster").modal('hide');
             return res.data;
         })
         .catch(function (res) {
             console.log(res);
             $("#div_EspereMaster").modal('hide');
         });

    };
    
    this.ObtenerMes = function () {
        return $http.post("/CierreMes/ObtenerMes")
        .then(function (res) {
            return res.data;
        })
        .catch(function (res) {
            console.log(res);
        });

    }; 

    this.GuardarCierreMes = function (Objeto) {
      return $http.post("/CierreMes/GuardarCierreMes", { Objeto: Objeto })
      .then(function (res) {
          return res.data;
      })
      .catch(function (res) {
          console.log(res);
      });
    };

    
    this.Eliminar = function (Id) {
       return $http.post("/CierreMes/EliminarCierreMes", { 'Id': Id })
            .then(function (res) {
                return res.data;
            })
            .catch(function (res) {
                console.log(res);
            });
    };

})