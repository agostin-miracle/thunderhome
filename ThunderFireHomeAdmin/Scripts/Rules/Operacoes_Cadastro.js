var TbDetalhe = null;

$(document).ready(function () {
    $('.numero').mask('000000', { reverse: true });
    $('.numero3').mask('000', { reverse: true });

    $(document).on('change', '#CSUBSYS', function () {
        $('#tboperacoes').DataTable().ajax.reload();
    });

    
    $(document).on("click", "#btn_update", function () {
        var mode = $("#OPERATION").val();
        if (($("#FCODCAN").val() == "") || ($("#FCODCAN").val() ==null))
            $("#FCODCAN").val("0");
        if (($("#FCODEST").val() == "") || ($("#FCODEST").val() == null))
            $("#FCODEST").val("0");

        var VDATA = JSON.stringify({
            'CODMOV': parseInt($("#FCODMOV").val()),
            'SUBSYS': parseInt($("#FSUBSYS").val()),
            'DSCMOV': $("#FDSCMOV").val(),
            'DSCEXT': $("#FDSCEXT").val(),
            'SIGOPE': parseInt($("#FSIGOPE").val()),
            'CNDBLO': parseInt($("#FCNDBLO").val()),
            'CODEST': parseInt($("#FCODEST").val()),
            'CODCAN': parseInt($("#FCODCAN").val()),
            'DSCEXC': $("#FDSCEXC").val(),
            'STAREC': $("#FSTAREC").is(':checked') ? 1 : 0
        });
        $.ajax({
            type: "POST",
            url: GetUrl("Cadastros/AtualizarOperacao?modo=" + mode),
            data: VDATA,
            dataType: 'json',
            contentType: 'application/json',
            beforeSend: function () { },
            success: function (result) {

                var ok = parseInt(result.ReturnValue);
                if (ok > 0) {
                    $('#modaloperacoes').modal('toggle');
                    document.location.reload();
                }
                else {
                    $.alert({ class: 'danger', title: 'Operações', message: result.MessageToUser, effect: 'zoom' });
                };
            },
            error: function (data) {
                $.alert({ class: 'danger', title: 'Operações', message: 'Ocorreu um erro inesperado, por favor tente novamente', effect: 'zoom' });
            }
        });
    });
    AtualizaDetalhe();
});

function AtualizaDetalhe() {
    try {
        if (TbDetalhe == null) {
            TbDetalhe = $('#tboperacoes').DataTable(
                {
                    paging: true,
                    searching: false,
                    lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
                    processing: true,
                    serverSide: true,
                    orderMulti: false,
                    responsive: true,
                    ajax: {
                        url: GetUrl("Cadastros/ListarOperacoes"),
                        type: "POST",
                        datatype: "json",
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                        data: {
                            pSUBSYS: function () { return $('#CSUBSYS').val(); }
                        }
                    },
                    columns: [
                        { data: "CODMOV" },
                        { data: "DSCMOV" },
                        { data: "DSCEXT" },
                        { data: "DSCOPE" },
                        { data: "LGNUSU" },
                        {
                            "render": function (data, type, row) {
                                return "<button id='btn_call_edt' value='Editar' class='btn btn-primary btn-sm' onclick=\"CallForEdit(" + row.CODMOV + ");\"> <i class='fas fa-edit'></i></button>&nbsp;"
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
                        { className: "text-right", "targets": [0], "visible": true, "searchable": false, "width": "4%" },
                        { className: "text-left", "targets": [1], "visible": true, "searchable": false, "width": "4%" },
                        { className: "text-left", "targets": [2], "visible": true, "searchable": false, "width": "4%" },
                        { className: "text-left", "targets": [3], "visible": true, "searchable": false, "width": "4%" },
                        { className: "text-left", "targets": [4], "visible": true, "searchable": false, "width": "2%" },
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
    $("#FSUBSYS").val($("#CSUBSYS").val());
    $("#FSUBSYS").prop("disabled", true);
    $('#modaloperacoes').modal({ backdrop: 'static', Keyboard: 'true' });
    $('#modaloperacoes').modal('toggle');
}

function CallForEdit(pCODMOV) {
    $("#OPERATION").val("2");

    if (pCODMOV >= 0) {
        $.ajax({
            type: "GET",
            url: GetUrl("Cadastros/SelecionarOperacao"),
            data: { pCODMOV: pCODMOV },
            dataType: 'json',
            success: function (result) {
                $("#FCODMOV").val(result.CODMOV);
                $("#FCODMOV").prop("disabled", true);
                $("#FSUBSYS").val(result.SUBSYS);
                $("#FSUBSYS").prop("disabled", true);
                $("#FDSCMOV").val(result.DSCMOV);
                $("#FDSCEXT").val(result.DSCEXT);
                $("#FCNDBLO").val(result.CNDBLO);
                $("#FNUMDIA").val(result.NUMDIA);
                $("#FCODEST").val(result.CODEST);
                $("#FCODCAN").val(result.CODCAN);
                $("#FSIGOPE").val(result.SIGOPE);
                $("#FDSCEXC").val(result.DSCEXC);
                $("#FSTAREC").prop('checked', (result.STAREC == 1));
            }
        });
        $('#modaloperacoes').modal({ backdrop: 'static', Keyboard: 'true' });
        $('#modaloperacoes').modal('toggle');
    }
    else
        $.alert({ class: 'danger', title: 'Operações', message: "Selecione um registro válido", effect: 'zoom' });
}