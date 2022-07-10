$(document).ready(function () {
    $(document).on("click", "#btn_update", function () {
        var mode = $("#OPERATION").val();

        if (mode == 1 || mode == 2) {

            var VDATA = JSON.stringify(
                {
                    'TIPUSU': parseInt($("#FTIPUSU").val()),
                    'DSCTUS': $("#FDSCTUS").val(),
                    'STAREC': $("#FSTAREC").is(':checked') ? 1 : 0
                });
            $.ajax({
                type: "POST",
                url: "/Cadastros/AtualizarTipoUsuario?modo=" + mode,
                data: VDATA,
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function () { },
                success: function (result) {
                    console.log(result);
                    var ok = parseInt(result.ReturnValue);
                    if (ok > 0) {
                        $('#modaltipousuario').modal('toggle');
                        document.location.reload();
                    }
                    else {
                        $.alert({ class: 'Warning', title: 'Tipo de Usuario', message: result.MessageToUser, effect: 'newspaper' });
                    };
                },
                error: function (data) {
                    $.alert({ class: 'danger', title: 'Tipo de Usuário', message: 'Ocorreu um erro inesperado, por favor tente novamente', effect: 'zoom' });
                }
            });
        }
        else
            $.alert({ class: 'danger', title: 'Tipo de Usuário', message: 'Operação inválida', effect: 'zoom' });
    });
});



function CallForEdit(pTIPUSU, pDSCTUS, pSTAREC) {
    $("#OPERATION").val("2");
    $("#FTIPUSU").val(pTIPUSU);
    $("#FDSCTUS").val(pDSCTUS);
    $("#FSTAREC").prop('checked', (pSTAREC == 1));
    $('#modaltipousuario').modal({ backdrop: "static", keyboard: false });
    $('#modaltipousuario').modal('toggle');
}
function CallForAdd() {
    $("#OPERATION").val("1");
    $("#FTIPUSU").val("0");
    $("#FDSCTUS").val("");
    $("#FSTAREC").prop('checked', "true");
    $('#modaltipousuario').modal({ backdrop: "static", keyboard: false });
    $('#modaltipousuario').modal('toggle');
}

