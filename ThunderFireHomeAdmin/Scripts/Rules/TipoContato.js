$(document).ready(function () {
    $(document).on("click", "#btn_update", function () {
        var mode = $("#OPERATION").val();

        if (mode == 1 || mode == 2) {

            var VDATA = JSON.stringify(
                {
                    'TIPCTO': parseInt($("#FTIPCTO").val()),
                    'DSCCTO': $("#FDSCCTO").val(),
                    'STAREC': $("#FSTAREC").is(':checked') ? 1 : 0
                });
            $.ajax({
                type: "POST",
                url: "/Cadastros/AtualizarTipoContato?modo=" + mode,
                data: VDATA,
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function () { },
                success: function (result) {
                    console.log(result);
                    var ok = parseInt(result.ReturnValue);
                    if (ok > 0) {
                        $('#modaltipocontato').modal('toggle');
                        document.location.reload();
                    }
                    else {
                        $.alert({ class: 'Warning', title: 'Tipo de Contato', message: result.MessageToUser, effect: 'newspaper' });
                    };
                },
                error: function (data) {
                    $.alert({ class: 'danger', title: 'Tipo de Contato', message: 'Ocorreu um erro inesperado, por favor tente novamente', effect: 'zoom' });
                }
            });
        }
        else
            $.alert({ class: 'danger', title: 'Tipo de Contato', message: 'Operação inválida', effect: 'zoom' });
    });
});



function CallForEdit(pTIPCTO, pDSCCTO, pSTAREC) {
    $("#OPERATION").val("2");
    $("#FTIPCTO").val(pTIPCTO);
    $("#FDSCCTO").val(pDSCCTO);
    $("#FSTAREC").prop('checked', (pSTAREC == 1));
    $('#modaltipocontato').modal({ backdrop: "static", keyboard: false });
    $('#modaltipocontato').modal('toggle');
}
function CallForAdd() {
    $("#OPERATION").val("1");
    $("#FTIPCTO").val("0");
    $("#FDSCCTO").val("");
    $("#FSTAREC").prop('checked', "true");
    $('#modaltipocontato').modal({ backdrop: "static", keyboard: false });
    $('#modaltipocontato').modal('toggle');
}

