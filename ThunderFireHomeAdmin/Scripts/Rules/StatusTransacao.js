var TbDetalhe = null;

$(document).ready(function () {
    $('.numero').mask('000000', { reverse: true });

    $(document).on('change', '#CCODMOD', function () {
   
        $('#tbtransacoes').DataTable().ajax.reload();
    });

    $(document).on("click", "#btn_update", function () {
        var mode = $("#OPERATION").val();
        var VDATA = JSON.stringify({
            'CODSTA': parseInt($("#FCODSTA").val()),
            'DSCSTA': $("#FDSCSTA").val(),
            'CODMOD': parseInt($("#FCODMOD").val()),
            'NXTSTA': parseInt($("#FNXTSTA").val()),
            'SIGOPE': 0,
            'CANCHG': $("#FCANCHG").is(':checked') ? 1 : 0,
            'DELMEN': $("#FDELMEN").is(':checked') ? 1 : 0,
            'STAREC': $("#FSTAREC").is(':checked') ? 1 : 0
        });
        $.ajax({
            type: "POST",
            url: GetUrl("/Cadastros/AtualizarStatusTransacao?modo=" + mode),
            data: VDATA,
            dataType: 'json',
            contentType: 'application/json',
            beforeSend: function () { },
            success: function (result) {
                var ok = parseInt(result.ReturnValue);
                if (ok > 0) {
                    $('#modaltransacoes').modal('toggle');
                    document.location.reload();
                }
                else {
                    $.alert({ class: 'Warning', title: 'Status de Transação', message: result.MessageToUser, effect: 'newspaper' });
                };
            },
            error: function (data) {

                console.log(data);
                $.alert({ class: 'danger', title: 'Status de Transação', message: 'Ocorreu um erro inesperado, por favor tente novamente', effect: 'zoom' });
            }
        });
    });


    AtualizaDetalhe();

});


function CallForAdd() {
    var value = $('#CCODMOD').val();
    if (value > 0) {
        $("#OPERATION").val("1");
        $("#FSTAREC").prop('checked', "true");
        $("#FCODMOD").val(value);
        $("#FCODMOD").prop('disabled', true);
        $('#modaltransacoes').modal({ backdrop: 'static', Keyboard: 'true' });
        $('#modaltransacoes').modal('toggle');
    }
    else {
        $.alert({ class: 'danger', title: 'Transações', message: 'Selecione uma Módulo válido', effect: 'zoom' });
    }
}
function CallForEdit(pCODSTA) {
    $("#OPERATION").val("2");
    $.ajax({
        type: "GET",
        url: "/Cadastros/SelecionarStatusTransacao",
        data: { pCODSTA: pCODSTA },
        dataType: 'json',
        success: function (result) {
            $("#FCODSTA").val(result.CODSTA);
            $("#FDSCSTA").val(result.DSCSTA);
            $("#FCODMOD").val(result.CODMOD);
            $("#FCODMOD").prop('disabled', true);
            $("#FDELMEN").prop('checked', (result.DELMEN == 1));
            $("#CANCHG").prop('checked', (result.CANCHG == 1));
            $("#FSTAREC").prop('checked', (result.STAREC == 1));
        }
    });
    $('#modaltransacoes').modal({ backdrop: 'static', Keyboard: 'true' });
    $('#modaltransacoes').modal('toggle');
}


function AtualizaDetalhe() {
    try {
        if (TbDetalhe == null) {
            TbDetalhe = $('#tbtransacoes').DataTable({
                paging: true,
                searching: false,
                lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
                processing: true,
                serverSide: true,
                orderMulti: false,
                ajax: {
                    url: GetUrl("/Cadastros/PesquisarStatusTransacao"),
                    type: "POST",
                    datatype: "json",
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                    contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                    data: {
                        pCODMOD: function () { return $('#CCODMOD').val() }
                    }
                },

                columns: [
                    { data: "CODSTA" },
                    { data: "DSCREC" },
                    { data: "DSCMOD" },
                    { data: "DSCSTA" },
                    { data: "LGNUSU" },
                    {
                        "render": function (data, type, row) {
                            return "<button id='btn_call_edt' value='Editar' class='btn btn-primary btn-sm' onclick=\"CallForEdit('" + row.CODSTA + "');\"> <i class='fas fa-edit'></i></button>&nbsp;"
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
                    { "targets": [0], "visible": true, "searchable": false, "width": "2%"  },
                    { className: "text-left", "targets": [1], "visible": true, "searchable": false, "width": "2%" },
                    { className: "text-left", "targets": [2], "visible": true, "searchable": false, "width": "2%" },
                    { className: "text-left", "targets": [3], "visible": true, "searchable": false, "width": "5%" },
                    { className: "text-left", "targets": [4], "visible": true, "searchable": false, "width": "5%" },
                    { className: "text-left", "targets": [5], "visible": true, "searchable": false, "width": "3%" },
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
