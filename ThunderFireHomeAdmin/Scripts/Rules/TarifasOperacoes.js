var TbDetalhe = null;

$(document).ready(function () {

    AtualizaDetalhe();

    $(document).on("click", "#btn_update", function () {
        var mode = $("#OPERATION").val();
        var VDATA = JSON.stringify({
            'NIDTXM': parseInt($("#FNIDTXM").val()),
            'CODTAR': parseInt($("#FCODTAR").val()),
            'CODMOV': parseInt($("#FCODMOV").val()),
            'IDEPRE': parseInt($("#FIDEPRE").val()),
            'STAREC': $("#FSTAREC").is(':checked') ? 1 : 0
        });
        $.ajax({
            type: "POST",
            url: GetUrl("/Tarifacao/AtualizarTarifaOperacao?modo=" + mode),
            data: VDATA,
            dataType: 'json',
            contentType: 'application/json',
            beforeSend: function () { },
            success: function (result) {
                $.alert({ class: 'success', title: 'Tarifa Operação', message: result.MessageToUser, effect: 'zoom' });
            },
            error: function (data) {
                $.alert({ class: 'danger', title: 'Tarifa Operação', message: 'Ocorreu um erro inesperado, por favor tente novamente', effect: 'zoom' });
            }
        });
    });

    $('#modaltarifaoperacao').on('hidden.bs.modal', function () {
        location.reload();
    });
});

function AtualizaDetalhe() {
    try {
        if (TbDetalhe == null) {
            TbDetalhe = $('#tbtarifaoperacao').DataTable(
                {
                    paging: true,
                    searching: false,
                    lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
                    processing: true,
                    serverSide: true,
                    orderMulti: false,
                    responsive: true,
                    ajax: {
                        url: GetUrl("Tarifacao/PesquisarTarifaOperacao"),
                        type: "POST",
                        datatype: "json",
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                    },
                    columns: [
                        { data: "NIDTXM" },
                        { data: "DSCREC" },
                        { data: "DSCTAR" },
                        { data: "DSCMOV" },
                        { data: "IDEPRE" },
                        { data: "LGNUSU" },
                        {
                            "render": function (data, type, row) {
                                return "<button id='btn_call_edt' value='Editar' class='btn btn-primary btn-sm' onclick=\"CallForEdit('" + row.NIDTXM + "');\"> <i class='fas fa-edit'></i></button>&nbsp;"
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
                        { className: "text-left", "targets": [1], "visible": true, "searchable": false, "width": "4%" },
                        { className: "text-left", "targets": [2], "visible": true, "searchable": false, "width": "4%" },
                        { className: "text-left", "targets": [3], "visible": true, "searchable": false, "width": "4%" },
                        { className: "text-center", "targets": [4], "visible": true, "searchable": false, "width": "2%" },
                        { className: "text-left", "targets": [5], "visible": true, "searchable": false, "width": "2%" },
                        { className: "text-left", "targets": [6], "visible": true, "searchable": false, "width": "2%" }
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


function CallForAdd() {
    $("#OPERATION").val("1");
    $("#FSTAREC").prop('checked', "true");
    $("#FNIDTXM").val("0");
    $('#modaltarifaoperacao').modal({ backdrop: 'static', Keyboard: 'true' });
    $('#modaltarifaoperacao').modal('toggle');
}
function CallForEdit(pNIDTXM) {
    $("#OPERATION").val("2");
    $.ajax({
        type: "GET",
        url: GetUrl("/Tarifacao/SelecionarTarifaOperacao"),
        data: { pNIDTXM: pNIDTXM },
        dataType: 'json',
        success: function (result) {
            $("#FNIDTXM").val(result.NIDTXM);
            $("#FCODTAR").val(result.CODTAR);
            $("#FCODMOV").val(result.CODMOV);
            $("#FIDEPRE").val(result.IDEPRE);
            $("#FSTAREC").prop('checked', (result.STAREC == 1));
        }
    });
    $('#modaltarifaoperacao').modal({ backdrop: 'static', Keyboard: 'true' });
    $('#modaltarifaoperacao').modal('toggle');
}