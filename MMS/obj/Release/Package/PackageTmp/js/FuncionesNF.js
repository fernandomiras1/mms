//Funciones propias Por Nicolas Flores Mitar ("N!(0.F.M")

//Pasar id del objeto y armar un msgbox!
function  comprueba(ID_O,V_STRING,MSG,COLOR,DELAY) {
    var valor1 = document.getElementById(ID_O).value;
    if (valor1.length < V_STRING) {
        $.Notify({ style: { background: COLOR, color: 'white' }, content: MSG, timeout: DELAY });
        return response = false;
    }
    else {
        return response = true;
        }
}
//function load() {
//    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
//}

function EndRequestHandler() {

    $(document).ready(function () {
        $("[id*=GridView] td").hover(function () {
            $("td", $(this).closest("tr")).addClass("Pintar_Fila");
        }, function () {
            $("td", $(this).closest("tr")).removeClass("Pintar_Fila");
        });
    });

}


//Limpiar Campos Pasando Objeto!
function LimpiarCampos(O) {
    O.value = "";
}

function Recargar() {
    window.location.reload();
}
//Pasar Target Nueva Pestaña!
function ObtenerTarget() {
    document.forms[0].target = "_blank";
}
//Pintar Filas al Pasar el Mouse por los Gridview! 
$(document).ready(function () {
    $("[id*=GridView] td").hover(function () {
        $("td", $(this).closest("tr")).addClass("Pintar_Fila");
    }, function () {
        $("td", $(this).closest("tr")).removeClass("Pintar_Fila");
    });
});
//Validar Solo Numeros!
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}
//Validar Actualizacion de Venta


function ConfirmarNF() {
    if (confirm("¿Desea Actualizar la Gestion?"))
        return true;
    else
        return false;
}





function ConfirmarReporte() {
    if (confirm("Se Enviara por Email el Cierre del Dia ¿Desea Enviarlo ahora?"))
        return true;
    else
        return false;
}



function V_Registrarse() {

    var nap = document.getElementById('nomape').value;
    var dni = document.getElementById('dc').value;
    var patente = document.getElementById('patente').value;
    var email = document.getElementById('email').value;
    var pass = document.getElementById('pass').value;
    var pass2 = document.getElementById('pass2').value;

    var pw_usua = document.getElementById("pass");
    var pw_usua2 = document.getElementById("pass2");

    if (pw_usua.value == pw_usua2.value) {
        document.miFormulario.submit()
    }
    else {
        form.pw_usua.value = "";
        form.pw_usua2.value = "";
        alertify.error("Los Password no coinciden");
    }


    if (nap.length < '4') {
        alertify.error("Falta el Nombre y Apellido");
        return response = false;
    }

    if (dni == '') {
        alertify.error("Ingrese el Nro de DNI" );
        return response = false;
    }

    if (dni.length < '7') {
        alertify.error("El Nro de DNI es Invalido");
        return response = false;
    }


    if (patente == '') {
        alertify.error("Ingrese el Nro de Patente ");
        return response = false;
    }

    if (pass.length < '8') {
        alertify.error("La Clave debe tener minimo 8 caracteres");
        return response = false;
    }

    if (email = '') {
        alertify.error("Ingrese el Email");
        return response = false;
    }

}

//Validar Form Costos
function V_Costos() {

    var nombcos = document.getElementById('ContentPlaceHolder1_nombco').value;
    var cant = document.getElementById('ContentPlaceHolder1_tcantidad').value;
    var tpreuni = document.getElementById('ContentPlaceHolder1_tpreuni').value;
   

    if (nombcos.length < '2') {
        alertify.error("Falta la Descripcion del costo");
        return response = false;
    }

    if (cant.length < '1') {
        alertify.error("Falta la Cantidad");
        return response = false;
    }

    if (tpreuni.length < '1') {
        alertify.error("Falta el Precio Unitario");
        return response = false;
    }

  
 



}

//Validar Form Venta y Postback del Form! 
function V_Venta() {
    var ta = document.getElementById('ContentPlaceHolder1_ta').value;
    var ncl         = document.getElementById('ContentPlaceHolder1_nc').value;
    var acl         = document.getElementById('ContentPlaceHolder1_ac').value;
    var dcl         = document.getElementById('ContentPlaceHolder1_dc').value;
    var fcl         = document.getElementById('ContentPlaceHolder1_fn').value;
    var tcl = document.getElementById('ContentPlaceHolder1_tc').value;
    var FechaEnvio = document.getElementById('ContentPlaceHolder1_fe').value;
    var abc         = document.getElementById("ContentPlaceHolder1_ab");
    var tab_Valor   = abc.options[abc.selectedIndex].value;
    var ata         = document.getElementById("ContentPlaceHolder1_ab2");
    var ata_Valor   = ata.options[ata.selectedIndex].value;
    var lpc         = document.getElementById("ContentPlaceHolder1_lp");
    var lpc_Valor   = lpc.options[lpc.selectedIndex].value;
    var rc          = document.getElementById("ContentPlaceHolder1_r");
    var rc_Valor    = rc.options[rc.selectedIndex].value;
    var dire = document.getElementById('ContentPlaceHolder1_dire').value;
    var tipoAbono = document.getElementById('ContentPlaceHolder1_abvend').value;
    var dire_Fac = document.getElementById('ContentPlaceHolder1_dirf').value;
    var hc          = document.getElementById("ContentPlaceHolder1_DropDownListhc");
    var hc_Valor    = hc.options[hc.selectedIndex].value;
    var pin         = document.getElementById('ContentPlaceHolder1_pin').value;
    var me          = document.getElementById('ContentPlaceHolder1_me').value;

    if (ncl.length < '2') {
        alertify.error("Falta el Nombre del Cliente");
        return response = false;
    }
    if (acl.length < '2') {
        alertify.error("Falta el Apellido del Cliente");
        return response = false;
    }
    if (dcl.length < '7') {
        alertify.error("Falta el DNI del Cliente");
        return response = false;
    }
    if (fcl == 'FECHA DE NACIMIENTO') {
        alertify.error("Falta la Fecha del Cliente");
        return response = false;
    }
    if (FechaEnvio == 'Fecha de Envio') {
        alertify.error("Falta la Fecha del Envio");
        return response = false;
    }
    if (tipoAbono == 'Tipo Abono Vendido') {
        alertify.error("Seleccione el Tipo de Abono Vendido");
        return response = false;
    }
    if (tcl.length < '10') {
        alertify.error("Falta el Telefono del Cliente (Formato sin 0 y sin 15)");
        return response = false;
    }
    if (ta.length < '7') {
        alertify.error("Falta el Telefono Alternativo");
        return response = false;
    }
    if (dire.length < '2') {
        alertify.error("Falta la Direccion de Envio");
        return response = false;
    }
    if (dire_Fac.length < '2') {
        alertify.error("Falta la Direccion de la Factura");
        return response = false;
    }
    //if (pin.length < '2') {
    //    alertify.error("Falta el PIN Cuenta");
    //    return response = false;
    //}
    if (me.length < '1') {
        alertify.error("Falta el Monto del Envio");
        return response = false;
    }
    if (tab_Valor == 'Abono Vendido') {
        alertify.error("Seleccione el Abono Vendido");
        return response = false;
    }
    if (ata_Valor == 'Tipo Abono Actual') {
        alertify.error("Seleccione el Tipo de Abono Actual");
        return response = false;
    }
     if (lpc_Valor == 'Linea Nueva o Portabilidad') {
        alertify.error("Seleccione Linea nueva o Portabilidad");
        return response = false;
    }
    if (rc_Valor == 'Riesgo') {
        alertify.error("Seleccione el Riesgo");
        return response = false;
    }
    if (hc_Valor == 'Fecha de Envio') {
        alertify.error("Seleccione la Fecha del Envio");
        return response = false;
    }
    if (hc_Valor == 'Horario de Contacto') {
        alertify.error("Seleccione el Horario de Contacto");
        return response = false;
    }

}
//Funciones propias Por Nicolas Flores Mitar ("N!(0.F.M") - 2015-2016