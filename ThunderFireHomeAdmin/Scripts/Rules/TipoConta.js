$(document).ready(function () {
    $(document).on("click", "#btn_update", function () {
        var mode = $("#OPERATION").val();

        if (mode == 1 || mode == 2) {

            var VDATA = JSON.stringify(
                {
                    'TIPCTA': parseInt($("#FTIPCTA").val()),
                    'DSCCTA': $("#FDSCCTA").val(),
                    'TIPEXT': $("#FTIPEXT").val(),
                    'STAREC': $("#FSTAREC").is(':checked') ? 1 : 0
                });
            $.ajax({
                type: "POST",
                url: "/Cadastros/AtualizarTipoConta?modo=" + mode,
                data: VDATA,
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function () { },
                success: function (result) {
                    var ok = parseInt(result.ReturnValue);
                    if (ok > 0) {
                        $('#modalaccountype').modal('toggle');
                        document.location.reload();
                    }
                    else {
                        $.alert({ class: 'Warning', title: 'Tipo de Conta', message: result.MessageToUser, effect: 'newspaper' });
                    };
                },
                error: function (data) {
                    $.alert({ class: 'danger', title: 'Tipo de Conta', message: 'Ocorreu um erro inesperado, por favor tente novamente', effect: 'zoom' });
                }
            });
        }
        else
            $.alert({ class: 'danger', title: 'Tipo de Conta', message: 'Operação inválida', effect: 'zoom' });
    });
});



function CallForEdit(pTIPCTA, pDSCCTA, pTIPEXT, pSTAREC) {
    $("#OPERATION").val("2");
    $("#FTIPCTA").val(pTIPCTA);
    $("#FDSCCTA").val(pDSCCTA);
    $("#FTIPEXT").val(pTIPEXT);
    $("#FSTAREC").prop('checked', (pSTAREC == 1));
    $('#modalaccountype').modal({ backdrop: "static", keyboard: false });
    $('#modalaccountype').modal('toggle');
}
function CallForAdd() {
    $("#OPERATION").val("1");
    $("#FTIPCTA").val("0");
    $("#FDSCCTA").val("");
    $("#FTIPEXT").val("");
    $("#FSTAREC").prop('checked', "true");
    $('#modalaccountype').modal({ backdrop: "static", keyboard: false });
    $('#modalaccountype').modal('toggle');
}

