$(document).ready(function () {
    $(document).on("click", "#btn_update", function () {
        var mode = $("#OPERATION").val();

        if (mode == 1 || mode == 2) {
            var VDATA = JSON.stringify(
                {
                    'TIPEND': parseInt($("#FTIPEND").val()),
                    'DSCTEN': $("#FDSCTEN").val(),
                    'STAREC': $("#FSTAREC").is(':checked') ? 1 : 0,
                    'REFCTO': $("#FREFCTO").is(':checked') ? 1 : 0
                });
            $.ajax({
                type: "POST",
                url: GetUrl("Cadastros/AtualizarTipoEndereco?modo=" + mode),
                data: VDATA,
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function () { },
                success: function (result) {
                    console.log(result);
                    var ok = parseInt(result.ReturnValue);
                    if (ok > 0) {
                        $('#modaladdresstype').modal('toggle');
                        document.location.reload();
                    }
                    else {
                        $.alert({
                            class: 'Warning', title: 'Tipo de Endereço', message: result.MessageToUser, effect: 'newspaper'
                        });
                    };
                },
                error: function (data) {
                    $.alert({ class: 'danger', title: 'Tipo de Endereço', message: 'Ocorreu um erro inesperado, por favor tente novamente', effect: 'zoom' });
                }
            });
        }
        else
            $.alert({ class: 'danger', title: 'Tipo de Endereço', message: 'Operação inválida', effect: 'zoom' });
    });

});


function CallForEdit(pTIPEND, pDSCTEN, pSTAREC, pREFCTO) {
    $("#OPERATION").val("2");
    $("#FTIPEND").val(pTIPEND);
    $("#FDSCTEN").val(pDSCTEN);
    $("#FSTAREC").prop('checked', (pSTAREC == 1));
    $("#FREFCTO").prop('checked', (pREFCTO == 1));
    $('#modaladdresstype').modal({ backdrop: "static", keyboard: false });
    $('#modaladdresstype').modal('toggle');
}
function CallForAdd() {
    $("#OPERATION").val("1");
    $("#FTIPEND").val("0");
    $("#FDSCTEN").val("");
    $("#FSTAREC").prop('checked', "true");
    $("#FREFCTO").prop('checked', "false");
    $('#modaladdresstype').modal({ backdrop: "static", keyboard: false });
    $('#modaladdresstype').modal('toggle');
}

