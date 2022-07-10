var TbDetalhe = null;


$(document).ready(function () {
    $('.inteiro').mask('000', { reverse: true });
    AtualizaDetalhe();

    $(document).on("click", "#btn_update", function () {
        var mode = $("#OPERATION").val();
        var VDATA = JSON.stringify({
            'MODCRT': parseInt($("#FMODCRT").val()),
            'DSCMOD': $("#FDSCMOD").val(),
            'PARINI': parseInt($("#FPARINI").val()),
            'PARFIN': parseInt($("#FPARFIN").val()),
            'STAREC': $("#FSTAREC").is(':checked') ? 1 : 0
        });
        $.ajax({
            type: "POST",
            url: GetUrl("Tarifacao/AtualizarModalidade?modo=" + mode),
            data: VDATA,
            dataType: 'json',
            contentType: 'application/json',
            beforeSend: function () { },
            success: function (result) {
                $.alert({ class: 'danger', title: 'Modalidade de Tarifa', message: result.MessageToUser, effect: 'zoom' });
            },
            error: function (data) {
                $.alert({ class: 'danger', title: 'Modalidade de Tarifa', message: 'Ocorreu um erro inesperado, por favor tente novamente', effect: 'zoom' });
            }
        });
    });
});

function AtualizaDetalhe() {
    try {
        if (TbDetalhe == null) {
            TbDetalhe = $('#tbmodalidade').DataTable(
                {
                    paging: true,
                    searching: false,
                    lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
                    processing: true,
                    serverSide: true,
                    orderMulti: false,
                    responsive: true,
                    ajax: {
                        url: GetUrl("Tarifacao/PesquisarModalidades"),
                        type: "POST",
                        datatype: "json",
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                        contentType: 'application/x-www-form-urlencoded; charset=utf-8'
                    },
                    columns: [
                        { data: "MODCRT" },
                        { data: "DSCREC" },
                        { data: "DSCMOD" },
                        { data: "EXTMOD" },
                        { data: "LGNUSU" },
                        {
                            "render": function (data, type, row) {
                                return "<button id='btn_call_edt' value='Editar' class='btn btn-primary btn-sm' onclick=\"CallForEdit('" + row.MODCRT + "');\"> <i class='fas fa-edit'></i></button>&nbsp;"
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
                        { className: "text-left", "targets": [4], "visible": true, "searchable": false, "width": "4%" },
                        { className: "text-left", "targets": [5], "visible": true, "searchable": false, "width": "2%" }
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
    $("#FMODCRT").val("0");
    $("#FMODCRT").prop("disabled", true);
    $('#modalmodalidade').modal({ backdrop: 'static', Keyboard: 'true' });
    $('#modalmodalidade').modal('toggle');
}
function CallForEdit(pMODCRT) {
    $("#OPERATION").val("2");
    $.ajax({
        type: "GET",
        url: GetUrl("/Tarifacao/SelecionarModalidade"),
        data: { pMODCRT: pMODCRT },
        dataType: 'json',
        success: function (result) {
            $("#FMODCRT").val(result, MODCRT);
            $("#FMODCRT").prop("disabled", true);
            $("#FDSCMOD").val(result.DSCMOD);
            $("#FPARINI").val(result.PARINI);
            $("#FPARFIN").val(result.PARFIN);
            $("#FSTAREC").prop('checked', (result.STAREC == 1));
        }
    });
    $('#modalmodalidade').modal({ backdrop: 'static', Keyboard: 'true' });
    $('#modalmodalidade').modal('toggle');
}

