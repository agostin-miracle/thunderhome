var TbDetalhe = null;
var TbResumo = null
//var ReloadPesquisa = false;

//$(document).ready(function () {

//    ReloadPesquisa = false;
//    $('#CUSUPRO').on('change', function () {
//        ReloadPesquisa = true;
//    });
//    $('#CSTACRT').on('change', function () {
//        ReloadPesquisa = true;
//    });
//    $('#CTIPMEN').on('change', function () {
//        ReloadPesquisa = true;
//    });
//    $('#CCODUSU').on('change', function () {
//        ReloadPesquisa = true;
//    });

//    if (ReloadPesquisa) {
//        AtualizaDetalhe();
//        AtualizaResumo();
//    }
//});




function ProcessDataOnCard(pCODCRT, pACTION, pCODACT) {
    $.ajax({
        type: "POST",
        url: GetUrl("Gestao/ChangeDataOnCard"),
        data: { pCODCRT: pCODCRT, pACTION: pACTION, pCODACT: pCODACT },
        dataType: 'json',
        success: function (result) {
            $.alert({ class: 'Warning', title: 'Cartões', message: result.MessageToUser, effect: 'newspaper' });
            $('#gridaddress').DataTable().ajax.reload();
        },
        error: function (data) {
            $.alert({ class: 'danger', title: 'Cartões', message: 'Ocorreu um erro inesperado, por favor tente novamente', effect: 'zoom' });
        }
    });
}


function AtualizaDetalhe() {
    try {
        if (TbDetalhe == null) {
            TbDetalhe = $('#tbdetailmen').DataTable(
                {
                    paging: true,
                    searching: false,
                    lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
                    processing: true,
                    serverSide: true,
                    orderMulti: false,
                    responsive: true,
                    ajax: {
                        url: GetUrl("Gestao/PesquisaMensalidades"),
                        type: "POST",
                        datatype: "json",
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                        data: {
                            pUSUPRO: function () {
                                var InputVal = $('#CUSUPRO').val();
                                if (InputVal > 0)
                                    return InputVal;
                                else
                                    return 0;
                            },
                            pTIPMEN: function () {
                                var InputVal = $('#CTIPMEN').val();
                                if (InputVal > 0)
                                    return InputVal;
                                else
                                    return 0;
                            },

                            pSTAMEN: function () { return $('#CSTAMEN').val(); },
                            pCODUSU: function () { return $('#CCODUSU').val(); }
                        }
                    },
                    columns: [
                        { data: "NIDMEN" },
                        { data: "NOMUSU" },
                        { data: "REFMEN" },
                        { data: "DSCMEN" },
                        { data: "NUMPCL" },
                        { data: "CNVMEN" },
                        { data: "CNVVCT" },
                        {
                            data: "VLRMOV",

                            render: $.fn.dataTable.render.number('.', ',', 2, '')
                        },
                        { data: "LGNUSU" }
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
                        { className: "text-right", "targets": [4], "visible": true, "searchable": false, "width": "2%" },
                        { className: "text-left", "targets": [5], "visible": true, "searchable": false, "width": "2%" },
                        { className: "text-left", "targets": [6], "visible": true, "searchable": false, "width": "2%" },
                        { className: "text-right", "targets": [7], "visible": true, "searchable": false, "width": "2%", "sType": "numeric-comma" },
                        { className: "text-left", "targets": [8], "visible": true, "searchable": false, "width": "8%" }
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

function AtualizaResumo() {
    try {
        if (TbResumo == null) {
            TbResumo = $('#tbresumomen').DataTable(
                {
                    paging: true,
                    searching: false,
                    lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
                    processing: true,
                    serverSide: true,
                    orderMulti: false,
                    responsive: true,
                    ajax: {
                        url: GetUrl("Gestao/ResumoMensalidades"),
                        type: "POST",
                        datatype: "json",
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                        data: {
                            pUSUPRO: function () {
                                var InputVal = $('#CUSUPRO').val();
                                if (InputVal > 0)
                                    return InputVal;
                                else
                                    return 0;
                            },
                            pTIPMEN: function () {
                                var InputVal = $('#CTIPMEN').val();
                                if (InputVal > 0)
                                    return InputVal;
                                else
                                    return 0;
                            },

                            pSTAMEN: function () { return $('#CSTAMEN').val(); },
                            pCODUSU: function () { return $('#CCODUSU').val(); }
                        }
                    },
                    columns: [
                        { data: "DSCREC" },
                        { data: "NOMGST" },
                        { data: "NOMUSU" },
                        { data: "DSCMEN" },
                        { data: "NUMPCL" },
                        { data: "CNVMEN" },
                        { data: "CNVVCT" },
                        {
                            data: "VLRMOV",

                            render: $.fn.dataTable.render.number('.', ',', 2, '')
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
                        { className: "text-right", "targets": [4], "visible": true, "searchable": false, "width": "2%" },
                        { className: "text-left", "targets": [5], "visible": true, "searchable": false, "width": "2%" },
                        { className: "text-left", "targets": [6], "visible": true, "searchable": false, "width": "2%" },
                        { className: "text-right", "targets": [7], "visible": true, "searchable": false, "width": "2%", "sType": "numeric-comma" }
                    ]
                });
        }
        else {
            TbResumo.DataTable().ajax.reload();
        }
    }
    catch (e) {

    }

}