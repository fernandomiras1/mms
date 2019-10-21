var Utils = {};
var confirm = {};
Utils.MensajeConfirmar = function (titulo, mensaje, funcionOK, funcionERROR, txtConfimButton, txtCancelButton) {
    $.confirm({
        title: titulo,
        icon: "glyphicon glyphicon-warning-sign",
        text: mensaje,
        content: mensaje,
        confirmButtonClass: 'btn btn-success',
        cancelButtonClass: 'btn btn-danger',
        confirmButton: txtConfimButton,
        cancelButton: txtCancelButton,
        confirm: function () {
            funcionOK();
        },
        cancel: function () {
            funcionERROR();
        }
    });
}

Utils.MensajeOK = function (titulo, mensaje, txtBoton) {
    $.alert({
        title: titulo,
        confirmButtonClass: 'btn-success',
        content: mensaje,
        confirmButton: txtBoton
    });
}

Utils.MensajeERROR = function (titulo, mensaje, txtBoton) {
    $.alert({
        title: titulo,
        confirmButtonClass: 'btn-danger',
        content: mensaje,
        confirmButton: txtBoton
    });
}


Utils.parseJsonDate = function (jsonDateString) {

    if (jsonDateString == null) {
        return ""
    }
    else {
        return new Date(parseInt(jsonDateString.replace('/Date(', '')));
    }
}

Utils.ObtenerParametrosURL = function (parametro)
{
    var vars = {};
    var parts = window.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi, function (m, key, value) { vars[key] = value; });
    return vars[parametro];
}


$(document).ready(function () {
    $(window).scroll(function () {
        if ($(this).scrollTop() > 50) {
            $('#back-to-top').fadeIn();
        } else {
            $('#back-to-top').fadeOut();
        }
    });
    // scroll body to 0px on click
    $('#back-to-top').click(function () {
        $('#back-to-top').tooltip('hide');
        $('body,html').animate({
            scrollTop: 0
        }, 800);
        return false;
    });

    $('#back-to-top').tooltip('show');

});

