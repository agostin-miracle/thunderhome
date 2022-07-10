$(document).ready(function () {
    $(document).on("click", "#btn_update", function () {

        var mode = $('#OPERATION').val();
        if (mode == 1 || mode == 2) {
            var VDATA = JSON.stringify({
                'CODATR': parseInt($("#FCODATR").val()),
                'DSCATR': $("#FDSCATR").val(),
                'USELGN': $("#FUSELGN").is(':checked') ? 1 : 0,
                'USEACT': $("#FUSEACT").is(':checked') ? 1 : 0,
                'STAREC': $("#FSTAREC").is(':checked') ? 1 : 0
            });
            $.ajax({
                type: "POST",
                url: GetUrl("Cadastros/AtualizarTipoAtributo?modo=" + mode),
                data: VDATA,
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function () { },
                success: function (result) {
                    var ok = parseInt(result.ReturnValue);
                    if (ok > 0)
                    {
                        $('#modalattributetype').modal('toggle');
                        document.location.reload();
                    }
                    else {
                        $.alert({ class: 'Warning', title: 'Tipo de Atributo', message: result.MessageToUser, effect: 'newspaper' });
                    };
                },
                error: function (data) {
                    $.alert({ class: 'danger', title: 'Tipo de Atributo', message: 'Ocorreu um erro inesperado, por favor tente novamente', effect: 'zoom' });
                }

            });
        }
        else
            $.alert({ class: 'danger', title: 'Tipo de Atributo', message: 'Modo de Manutenção não definido', effect: 'zoom' });
    });

});


function CallForAdd() {
    $("#OPERATION").val("1");
    $("#FSTAREC").prop('checked', "true");
    $('#modalattributetype').modal({ backdrop: 'static', Keyboard: 'true' });
    $('#modalattributetype').modal('toggle');
}
function CallForEdit(pCODATR, pDSCATR, pUSELGN, pSTAREC, pUSEACT) {
    $("#OPERATION").val("2");
    $("#FCODATR").val(pCODATR);
    $("#FDSCATR").val(pDSCATR);
    $("#FUSEACT").prop('checked', (pUSEACT == 1));
    $("#FUSELGN").prop('checked', (pUSELGN == 1));
    $("#FSTAREC").prop('checked', (pSTAREC == 1));
    $('#modalattributetype').modal({ backdrop: 'static', Keyboard: 'true' });
    $('#modalattributetype').modal('toggle');
}
