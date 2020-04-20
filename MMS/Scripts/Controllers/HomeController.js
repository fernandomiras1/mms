angular.module("MyApp").controller("HomeController", function ($scope, $http, $filter, HomeService) {


    $scope.Objeto = {};
    $scope.ObjetoBuscar = {};
    $scope.suma_total = 0;
    $scope.suma_ingresos = 0;
	$scope.suma_egresos = 0;
	$scope.name_cargo = document.getElementById('nameCargo').innerText;
    $scope.Fecha_Desde = new Date();
    $scope.Fecha_Hasta = new Date();

	$scope.MesCurso_Name = angular.uppercase(new Date().toLocaleString("es-AR", { month: "long" }));
	$scope.AnioCurso = new Date().getFullYear();
	$scope.AnioAnterior = new Date().getFullYear() - 1;
    $scope.Titulo_Periodo = $scope.MesCurso_Name;
    $scope.detallado = false;
  
    $scope.Open_Detallado = function () {
     //Limpio los campos cada vex que abro el chek detallado. 
          $scope.ObjetoBuscar = {};
    }

    $scope.status = {
        isFirstOpen: true,
        isFirstDisabled: false
    };

    $scope.LimpiarCalculos = function () {
        $scope.suma_total = 0;
        $scope.suma_ingresos = 0;
        $scope.suma_egresos = 0;
    }

    $scope.Init_Master = function () {
        $scope.Recordatorio_Alarmas();
    }

	$scope.Init = function () {
        $scope.ObtenerListado(1);
        $scope.ObtenerCategorias();
        $scope.Grafico_Area();
        $scope.ObtenerTipo();
        $scope.ObtenerSubCategoria();
		$scope.Forma_Pago();
    };

    //$scope.ObtenerListado = function (pagina) {
    $scope.ObtenerListado = function (periodo) {


        if (periodo == 1) {
            $scope.Titulo_Periodo = {
                Nombre: $scope.MesCurso_Name,
                Estado: 1
            };
        }
        else if (periodo == 2) {
            $scope.Titulo_Periodo = {
                Nombre: $scope.AnioCurso,
                Estado: 2
            };
		}
		else if (periodo == 3) {
			$scope.Titulo_Periodo = {
				Nombre: $scope.AnioAnterior,
				Estado: 3
			};
		}
        else {
            $scope.Titulo_Periodo = {
                Nombre: "El origen de los tiempos",
                Estado: 4
            };
        }


        //HomeService.ObtenerListado(pagina).then(function (res) {
        HomeService.ObtenerListado(periodo).then(function (res) {

            $scope.Listado = res.Lista;
            $scope.TotalRegistros = res.Total;

            $scope.LimpiarCalculos();
            angular.forEach($scope.Listado, function (val) {
                val.Fecha = Utils.parseJsonDate(val.Fecha);
                var Precio = val.Precio;
                $scope.suma_total += Precio;
                if (val.Tipo.Nombre === 'INGRESO')
                    $scope.suma_ingresos += Precio;
                else if (val.Tipo.Nombre === 'EGRESO')
                    $scope.suma_egresos += Precio;

            });

            if ($scope.Listado.length > 0) {
                $("#morris-bar-chart").empty();
                Morris.Bar(res.Lista[0].Grafico.MBar);

            }
			
        });
    };


    // Paginacion ui-boostrap
    $scope.pageChanged = function () {
        $scope.ObtenerListado($scope.PaginaActual);
    };


    // Fin de la Paginacion ui-b


    // Filtro los datos en el btn Buscar. y me calcula automaticamente.
    $scope.$watch('text_buscar', function (val) {
        var arrayFiltrado = $filter('filter')($scope.Listado, val);
        if (arrayFiltrado != null && arrayFiltrado.length > 0) {
            $scope.LimpiarCalculos();
        }

        angular.forEach(arrayFiltrado, function (val) {
            var Precio = val.Precio;
            $scope.suma_total += Precio;
            if (val.Tipo.Nombre == 'INGRESO')
                $scope.suma_ingresos += Precio;
            else if (val.Tipo.Nombre == 'EGRESO')
                $scope.suma_egresos += Precio;
        });

    });

    //Obtener Tipos
    $scope.ObtenerTipo = function () {
        HomeService.ObtenerTipo().then(function (res) {
			$scope.tipos = res.Lista;
        });
    };

    // combo de Categorias.
    $scope.ObtenerCategorias = function () {
        HomeService.ObtenerCategorias().then(function (res) {
            $scope.categorias = res.Lista;
        });
    };

    // combo de Sub_Categorias. 
    $scope.ObtenerSubCategoria = function () {
        HomeService.ObtenerSubCategorias().then(function (res) {
            $scope.sub_categorias = res.Lista;
        });

    }
    // Obtener Combo Forma_Pago
    $scope.Forma_Pago = function () {
        HomeService.Forma_Pago().then(function (res) {
			$scope.formaPago = res.Lista;
			if ($scope.name_cargo === 'openModal') {
				if (!localStorage.getItem('showModal')) {
					localStorage.setItem('showModal', '0');
					$scope.btn_Nuevo();
				}
			}
        });
    }

    // Btn_ Abrir POP UP - NUEVO 
    $scope.btn_Nuevo = function () {
		$scope.Objeto = {};
		if (sessionStorage.getItem('showModal')) {
			//var countModal = parseInt(sessionStorage.getItem('showModal'), 10);
			//var coutFinal = countModal + 1;
			//sessionStorage.setItem('showModal', coutFinal);
			//sessionStorage.removeItem('showModal');
		}
		if ($scope.tipos.length > 0) {
			$scope.Objeto.Tipo = $scope.tipos.find(function (x) { return x.id == 2 });
		}
       $scope.Objeto.Forma_Pago = $scope.formaPago.find(function (x) { return x.id == 1 });
       $scope.Objeto.Fecha = new Date();
       $('#div_Modal_Comentario').modal('show');
    }
    // Ordenamiento en la Grilla - Asendente
    $scope.OrdernarAsendente = function (campo) {
        $scope.reverse = false;
        $scope.campoOrdenado = "+" + campo;

    }
    // Ordenamiento en la Grilla - Descendente
    $scope.OrdernarDescendente = function (campo) {
        $scope.reverse = true;
        $scope.campoOrdenado = "-" + campo;

    }

    $scope.Guardar = function () {
        if (!$scope.Objeto.Precio || !$scope.Objeto.Fecha || !$scope.Objeto.Cate)
            return false;
        HomeService.Guardar($scope.Objeto).then(function (res) {
            if (res.Ok) {
                $scope.ObtenerListado($scope.Titulo_Periodo.Estado);
                $('#div_Modal_Comentario').modal('hide');
                alertify.success(res.Mensaje);
            }
            else {
                $('#div_Modal_Comentario').modal('hide');
                alertify.error(res.Mensaje);
            }

        });

    };

    $scope.Modificar = function (Objeto) {
        //$scope.Objeto = Objeto;
        $scope.Objeto.Id = Objeto.Id;
        $scope.Objeto.Tipo = Objeto.Tipo;
        $scope.Objeto.Precio = Objeto.Precio;
        $scope.Objeto.Cate = Objeto.Cate;
        $scope.Objeto.SubCate = Objeto.SubCate;
        $scope.Objeto.Fecha = Objeto.Fecha;
        $scope.Objeto.Forma_Pago = Objeto.Forma_Pago;
        $scope.Objeto.Observacion = Objeto.Observacion;
        $scope.ObtenerSubCategoria();
        $('#div_Modal_Comentario').modal('show');

    };

    $scope.Eliminar = function (Objeto) {
        var funcionOk = function () {
            HomeService.Eliminar(Objeto.Id).then(function (res) {
                if (res.Ok) {
                    $scope.ObtenerListado($scope.Titulo_Periodo.Estado);
                    alertify.success(res.Mensaje);
                }
                else {
                    alertify.error(res.Mensaje);
                }
            });

        }

        Utils.MensajeConfirmar("Atención", "Una vez eliminado no podra volver a recuperar el Registro. ¿Desea eliminar el Registro?", funcionOk, function () { /* funcion cancel */ }, "Borrar", "Cancelar");

    }


    $scope.btn_Buscar = function () {

   //     if (!$scope.ObjetoBuscar.sel_SubCate) $("#morris-donut-chart").empty();
        var Objeto_Busqueda = {
            Fecha_Desde: $scope.Fecha_Desde,
            Fecha_Hasta: $scope.Fecha_Hasta,
            detallado: $scope.detallado,
            Tipo: $scope.ObjetoBuscar.sel_Tipo,
            Cate: $scope.ObjetoBuscar.sel_Cate,
            SubCate: $scope.ObjetoBuscar.sel_SubCate

        };
         HomeService.BusquedaGeneral(Objeto_Busqueda).then(function (res) {
            $scope.Listado = res.Lista;
            $scope.LimpiarCalculos();
            $scope.TotalRegistros = res.Total;
            angular.forEach($scope.Listado, function (val) {
                val.Fecha = Utils.parseJsonDate(val.Fecha);
                var Precio = val.Precio;
                $scope.suma_total += Precio;
                if (val.Tipo.Nombre == 'INGRESO')
                    $scope.suma_ingresos += Precio;
                else if (val.Tipo.Nombre == 'EGRESO')
                    $scope.suma_egresos += Precio;

            });
            if ($scope.Listado.length > 0) {
                $("#morris-bar-chart").empty();
                Morris.Bar(res.Lista[0].Grafico.MBar);

            }
            if ($scope.ObjetoBuscar.sel_Cate) {
                $scope.Grafico_Donut(Objeto_Busqueda);
            }


        });

    };


    $scope.Grafico_Area = function () {
        HomeService.Grafico_Area().then(function (res) {

            if (res.Ok) {
                $("#morris-area-chart").empty();
                Morris.Area(res.Lista[0]);
            }
            else alertify.error(res.Mensaje);
        });
    }

    $scope.Grafico_Donut = function (Objeto_Busqueda) {
        HomeService.Grafico_Donut(Objeto_Busqueda).then(function (res) {
            if (res.Ok) {
                $("#morris-donut-chart").empty();
                Morris.Donut(res.Lista[0]);
            }
            else alertify.error(res.Mensaje);
        })
    }
    // Limpio los Campos Cate y SubCate- Cuando selecciono TIPO.
    $scope.SeleccionarTipo = function () {
        $scope.ObjetoBuscar.sel_Cate = {};
        $scope.ObjetoBuscar.sel_SubCate = {};
    }
    
    $scope.Recordatorio_Alarmas = function () {
      HomeService.Recordatorio_Alarmas().then(function (res) {
        $scope.Recordatorios = res.Lista;
        })
    }

    $scope.btn_CierreMes = function () {
        sessionStorage.setItem('key', JSON.stringify($scope.suma_ingresos));
        window.location.href = "CierreMes.aspx?Page=data&Post=" + true;

    }

})