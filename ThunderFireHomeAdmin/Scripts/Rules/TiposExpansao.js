var TbDetalhe = null;


$(document).ready(function () {

    AtualizaDetalhe();

    $(document).on("click", "#btn_update", function () {
        var mode = $("#OPERATION").val();
            var VDATA = JSON.stringify({
                'TIPEXP': parseInt($("#FTIPEXP").val()),
                'DSCEXP': $("#FDSCEXP").val(),
                'LVLREG': parseInt($("#FLVLREG").val()),
                'STAREC': $("#FSTAREC").is(':checked') ? 1 : 0
            });
            $.ajax({
                type: "POST",
                url: GetUrl("/Tarifacao/AtualizarTipoExpansao?modo=" + mode),
                data: VDATA,
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function () { },
                success: function (result) {
                    $.alert({ class: 'danger', title: 'Tipo de Expansão de Tarifa', message: result.MessageToUser, effect: 'zoom' });

                },
                error: function (data) {
                    $.alert({ class: 'danger', title: 'Tipo de Expansão de Tarifa', message: 'Ocorreu um erro inesperado, por favor tente novamente', effect: 'zoom' });
                }
            });
    });

    $(document).on('hidden.bs.modal', '#modaltipoexpansaotarifa', function () {
        window.location = GetUrl("/Tarifacao/tiposExpansao");
    });
});

function AtualizaDetalhe() {
    try {
        if (TbDetalhe == null) {
            TbDetalhe = $('#tbtipoexpansaotarifa').DataTable(
                {
                    paging: true,
                    searching: false,
                    lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
                    processing: true,
                    serverSide: true,
                    orderMulti: false,
                    responsive: true,
                    ajax: {
                        url: GetUrl("Tarifacao/PesquisarTipoExpansao"),
                        type: "POST",
                        datatype: "json",
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                    },
                    columns: [
                        { data: "TIPEXP" },
                        { data: "DSCREC" },
                        { data: "DSCEXP" },
                        { data: "LGNUSU" },
                        {
                            "render": function (data, type, row) {
                                return "<button id='btn_call_edt' value='Editar' class='btn btn-primary btn-sm' onclick=\"CallForEdit('" + row.TIPEXP + "');\"> <i class='fas fa-edit'></i></button>&nbsp;"
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
                        { className: "text-right", "targets": [4], "visible": true, "searchable": false, "width": "2%" }
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
    $("#FTIPEXP").val("0");
    $('#modaltipoexpansaotarifa').modal({ backdrop: 'static', Keyboard: 'true' });
    $('#modaltipoexpansaotarifa').modal('toggle');
}
function CallForEdit(pTIPEXP) {
    $("#OPERATION").val("2");
    $.ajax({
        type: "GET",
        url: GetUrl("/Tarifacao/SelecionarTipoExpansao"),
        data: { pTIPEXP: pTIPEXP },
        dataType: 'json',
        success: function (result) {
            $("#FTIPEXP").val(result.TIPEXP);
            $("#FDSCEXP").val(result.DSCEXP);
            $("#FLVLREG").val(result.LVLREG);
            $("#FSTAREC").prop('checked', (result.STAREC == 1));
        }
    });
    $('#modaltipoexpansaotarifa').modal({ backdrop: 'static', Keyboard: 'true' });
    $('#modaltipoexpansaotarifa').modal('toggle');
}