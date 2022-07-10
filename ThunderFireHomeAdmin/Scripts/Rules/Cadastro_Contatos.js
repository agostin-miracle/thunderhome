var TbDetalhe = null;

$(document).ready(function () {
    $('.phone').mask('0 0000-0000');
    $('.ddd').mask('000');
    var commandquery = "/Cadastros/QueryContactBook";

    $(document).on("click", "#btn_update_cto", function () {
        var mode = $("#OPERATION").val();
        var vCODPAI = $('#FCODPAI').val();
        if (vCODPAI === "" || vCODPAI == null)
            vCODPAI = 55;
        else
            vCODPAI = parseInt(vCODPAI);
        var vCODOPR = $('#FCODOPR').val();
        if (vCODOPR === "" || vCODOPR == null)
            vCODOPR = 0;
        var vDSCOBS = $("#FDSCOBS").val();
        if (vDSCOBS === "" || vDSCOBS == null)
            vDSCOBS = " ";

        var vNUMTEL = $("#FNUMTEL").val().replace('.', '').replace(',', '').replace('-', '').replace(' ', '');
        var VDATA = JSON.stringify({
            'CODCTO': parseInt($("#FCODCTO").val()),
            'CODEND': parseInt($("#FCODEND").val()),
            'CODUSU': parseInt($("#FCODUSU").val()),
            'TIPCTO': parseInt($("#FTIPCTO").val()),
            'REGATV': $("#FREGATV").is(':checked') ? 1 : 0,
            'CODPAI': vCODPAI,
            'CODOPR': parseInt(vCODOPR),
            'NUMDDD': $("#FNUMDDD").val(),
            'ISWHAT': $("#FISWHAT").is(':checked') ? 1 : 0,
            'NUMTEL': vNUMTEL,
            'DSCOBS': $("#FDSCOBS").val(),
            'STAREC': $("#FSTAREC").is(':checked') ? 1 : 0
        });
        $.ajax({
            type: "POST",
            url: GetUrl("Cadastros/AtualizarContato?modo=" + mode),
            data: VDATA,
            dataType: 'json',
            contentType: 'application/json',
            beforeSend: function () { },
            success: function (result) {
                console.log(result);
                var ok = parseInt(result.ReturnValue);
                if (ok > 0) {
                    $('#modalcontatos').modal('toggle');
                    location.href = location.href;
                }
                else {
                    $.alert({ class: 'Warning', title: 'Endereços', message: result.MessageToUser, effect: 'newspaper' });
                };
            },
            error: function (data) {
                $.alert({ class: 'danger', title: 'Endereços', message: 'Ocorreu um erro inesperado, por favor tente novamente', effect: 'zoom' });
            }
        });

    });


    $('#CCODUSU').on('change', function () {
        $('#tbcontatos').DataTable().ajax.reload();
    });

    AtualizaDetalhe();

});




function CallForAdd() {
    $("#OPERATION").val("1");
    $("#FSTAREC").prop('checked', "true");
    var pCODUSU = $("#CCODUSU").val();
    if (pCODUSU > 0) {
        $("#FCODUSU").val(pCODUSU);
        $("#FCODUSU").attr('disabled', 'disabled');
    }
    $("#FCODPAI").val(55);
    $("#FTIPCTO").val(3);
    $('#modalcontatos').modal({ backdrop: 'static', Keyboard: 'true' });
    $('#modalcontatos').modal('toggle');
}

function CallForEdit(pCODEND) {
    $("#OPERATION").val("2");
    $.ajax({
        type: "GET",
        url: GetUrl("Cadastros/SelecionarContato"),
        data: { pCODCTO: pCODCTO },
        dataType: 'json',
        success: function (result) {
            $("#FCODCTO").val(result.CODCTO);
            $("#FCODUSU").val(result.CODUSU);
            $("#FCODEND").val(result.CODEND);
            $("#FTIPCTO").val(result.TIPCTO);
            $("#FREGATV").prop('checked', (result.REGATV == 1));
            $("#FCODPAI").val(result.CODPAI);
            $("#FCODOPR").val(result.CODOPR);
            $("#FNUMDDD").val(result.NUMDDD);
            $("#FISWHAT").prop('checked', (result.ISWHAT == 1));
            $("#FNUMTEL").val(result.NUMTEL);
            $("#FDSCOBS").val(result.DSCOBS);
            $("#FSTAREC").prop('checked', (result.STAREC == 1));
        }
    })
    $('#modalcontatos').modal({ backdrop: 'static', Keyboard: 'true' });
    $('#modalcontatos').modal('toggle');
}


function AtualizaDetalhe() {
    try {
        if (TbDetalhe == null) {

            TbDetalhe = $('#tbcontatos').DataTable(
                {
                    paging: true,
                    searching: false,
                    lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
                    processing: true,
                    serverSide: true,
                    orderMulti: false,
                    responsive: true,
                    ajax: {
                        url: GetUrl('Cadastros/PesquisarContatos'),
                        type: "POST",
                        datatype: "json",
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                        data: {
                            pCODUSU: function () { return $('#CCODUSU').val() }
                        }
                    },
                    columns: [
                        { data: "CODCTO" },
                        { data: "DSCREC" },
                        { data: "DSCATV" },
                        { data: "DSCCTO" },
                        { data: "NUMDDD" },
                        { data: "NUMTEL" },
                        { data: "DATUPD" },
                        { data: "LGNUSU" },
                        {
                            "render": function (data, type, row) {
                                var botoes = "";
                                botoes += "<button id='btn_call_edt' value='Editar' class='btn btn-primary btn-sm' onclick=\"CallForEdit('" + row.CODEND + "');\"> <i class='fa fa-edit'></i></button>&nbsp;"
                                return botoes;
                            }
                        }
                    ],
                    "language": {
                        "processing": "Aguarde, buscando informações",
                        "loadingRecords": "Carregando",
                    },
                    "pageLength": 10,
                    "oLanguage": {
                        "sLengthMenu": "Mostrar _MENU_ registros por página",
                        "sZeroRecords": "Nenhum registro encontrado",
                        "sInfo": "Mostrando _START_ / _END_ de _TOTAL_ registro(s)",
                        "sInfoEmpty": "Mostrando 0 / 0 de 0 registros",
                        "sInfoFiltered": "(filtrado de _MAX_ registros)",
                        "sSearch": "Pesquisar: "
                    },
                    columnDefs: [
                        { "targets": [0], "visible": false, "searchable": false },
                        { className: "text-left", "targets": [1], "visible": true, "searchable": false, "width": "8%" },
                        { className: "text-center", "targets": [2], "visible": true, "searchable": false, "width": "4%" },
                        { className: "text-center", "targets": [3], "visible": true, "searchable": false, "width": "6%" },
                        { className: "text-left", "targets": [4], "visible": true, "searchable": false, "width": "2%" },
                        { className: "text-left", "targets": [5], "visible": true, "searchable": false, "width": "12%" },
                        { className: "text-left", "targets": [6], "visible": true, "searchable": false, "width": "8%" },
                        { className: "text-right", "targets": [7], "visible": true, "searchable": false, "width": "10%" },
                        { className: "text-right", "targets": [8], "visible": true, "searchable": false, "width": "10%" }
                    ]
                });
        }
        else {
            TbDetalhe.DataTable().ajax.reload();
        }
    }
    catch (e) {

    }
}

