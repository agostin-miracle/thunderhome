var TbDetalhe = null;


$(document).ready(function () {
    $('.money').mask('000.000,00', { reverse: true });
    $('.money2').mask("#.##0,00", { reverse: true });


    AtualizaDetalhe();

    $(document).on("click", "#btn_update", function () {
        var mode = $("#OPERATION").val();
        var VDATA = JSON.stringify({
            'NIDEXP': parseInt($("#FNIDEXP").val()),
            'TIPEXP': parseInt($("#FTIPEXP").val()),
            'LVLAPL': parseInt($("#FLVLAPL").val()),
            'VLRMIN': ToFloat($("#FVLRMIN").val()),
            'VLRMAX': ToFloat($("#FVLRMAX").val()),
            'VLRTAR': ToFloat($("#FVLRTAR").val()),
            'STAREC': $("#FSTAREC").is(':checked') ? 1 : 0
        });
        $.ajax({
            type: "POST",
            url: GetUrl("Tarifacao/AtualizarExpansao?modo=" + mode),
            data: VDATA,
            dataType: 'json',
            contentType: 'application/json',
            beforeSend: function () { },
            success: function (result) {
                $.alert({ class: 'danger', title: 'Expansão de Tarifa', message: result.MessageToUser, effect: 'zoom' });
            },
            error: function (data) {
                $.alert({ class: 'danger', title: 'Expansão de Tarifa', message: 'Ocorreu um erro inesperado, por favor tente novamente', effect: 'zoom' });
            }
        });
    });

    $(document).on('change', '#CTIPEXP', function () {
        $('#tbexpansaotarifa').DataTable().ajax.reload();
    });

    //$(document).on('hidden.bs.modal', '#modalexpansaotarifa', function () {
    //    window.location = GetUrl("/Tarifacao/ExpansaoTarifa");
    //});

});

function AtualizaDetalhe() {
    try {
        if (TbDetalhe == null) {
            TbDetalhe = $('#tbexpansaotarifa').DataTable(
                {
                    paging: true,
                    searching: false,
                    lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
                    processing: true,
                    serverSide: true,
                    orderMulti: false,
                    responsive: true,
                    ajax: {
                        url: GetUrl("Tarifacao/PesquisarExpansaoTarifa"),
                        type: "POST",
                        datatype: "json",
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                        data: {
                            pTIPEXP: function () { return $('#CTIPEXP').val(); }
                        }
                    },
                    columns: [
                        { data: "NIDEXP" },
                        { data: "DSCREC" },
                        { data: "LVLAPL" },
                        {
                            data: "VLRMIN",
                            render: $.fn.dataTable.render.number('.', ',', 2, '')
                        },
                        {
                            data: "VLRMAX",
                            render: $.fn.dataTable.render.number('.', ',', 2, '')
                        },
                        {
                            data: "VLRTAR",
                            render: $.fn.dataTable.render.number('.', ',', 2, '')
                        },

                        { data: "LGNUSU" },
                        {
                            "render": function (data, type, row) {
                                return "<button id='btn_call_edt' value='Editar' class='btn btn-primary btn-sm' onclick=\"CallForEdit('" + row.NIDEXP + "');\"> <i class='fas fa-edit'></i></button>&nbsp;"
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
                        { className: "text-right", "targets": [2,3,4,5], "visible": true, "searchable": false, "width": "4%" },
                        { className: "text-left", "targets": [6], "visible": true, "searchable": false, "width": "2%" },
                        { className: "text-left", "targets": [7], "visible": true, "searchable": false, "width": "2%" }
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
    $("#FNIDEXP").val("0");
    $('#modalexpansaotarifa').modal({ backdrop: 'static', Keyboard: 'true' });
    $('#modalexpansaotarifa').modal('toggle');
}
function CallForEdit(pNIDEXP) {
    $("#OPERATION").val("2");
    $.ajax({
        type: "GET",
        url: GetUrl("/Tarifacao/SelecionarExpansao"),
        data: { pNIDEXP: pnidexp },
        dataType: 'json',
        success: function (result) {
            $("#FNIDEXP").val(result.NIDEXP);
            $("#FTIPEXP").val(result.TIPEXP);
            $("#FLVLAPL").val(result.LVLAPL);
            $("#FVLRMIN").val(formatNumber(result.VLRMIN));
            $("#FVLRMAX").val(formatNumber(result.VLRMAX));
            $("#FVLRTAR").val(formatNumber(result.VLRTAR));
            $("#FSTAREC").prop('checked', (result.STAREC == 1));
        }
    });
    $('#modalexpansaotarifa').modal({ backdrop: 'static', Keyboard: 'true' });
    $('#modalexpansaotarifa').modal('toggle');
}