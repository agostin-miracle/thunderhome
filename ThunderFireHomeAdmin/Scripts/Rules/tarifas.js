var TbDetalhe;

$(document).ready(function () {
    $('.money').mask('000.000,00', { reverse: true });
    $('.money2').mask("#.##0,00", { reverse: true });
    AtualizaaDetalhe();

    $(document).on("click", "#btn_update", function () {

        if (PrimaryValidation()) {
            var mode = $("#OPERATION").val();
            var VDATA = JSON.stringify({
                'NIDTAR': parseInt($("#FNIDTAR").val()),
                'NIVCFG': parseInt($("#FNIVCFG").val()),
                'USUCFG': parseInt($("#FUSUCFG").val()),
                'CODTAR': parseInt($("#FCODTAR").val()),
                'CODBND': parseInt($("#FCODBND").val()),
                'TIPLIN': parseInt($("#FTIPLIN").val()),
                'MODCRT': parseInt($("#FMODCRT").val()),
                'CODEXP': parseInt($("#FCODEXP").val()),
                'CODMOE': 1,
                'FMTCOB': parseInt($("#FFMTCOB").val()),
                'TARBAS': ToFloat($("#FTARBAS").val()),
                'PCTTAR': ToFloat($("#FPCTTAR").val()),
                'VLRTAR': ToFloat($("#FVLRTAR").val()),
                'VLRINF': ToFloat($("#FVLRINF").val()),
                'VLRMAX': ToFloat($("#FVLRMAX").val()),
                'TARMAX': ToFloat($("#FTARMAX").val()),
                'RSPTAR': parseInt($("#FRSPTAR").val()),
                'STAREC': $("#FSTAREC").is(':checked') ? 1 : 0
            });
            $.ajax({
                type: "POST",
                url: GetUrl("Tarifacao/AtualizarTarifa?modo=" + mode),
                data: VDATA,
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function () { },
                success: function (result) {
                    console.log(result);
                    $.alert({ class: 'danger', title: 'Tarifa', message: result.MessageToUser, effect: 'zoom' });
                },
                error: function (data) {
                    console.log(data);
                    $.alert({ class: 'danger', title: 'Tarifa', message: 'Ocorreu um erro inesperado, por favor tente novamente', effect: 'zoom' });
                }
            });
        }

        //$('#CUSUCFG').on('change', function () {
        //    TbDetalhe.DataTable().ajax.reload();
        //});
        //$('#CNIVCFG').on('change', function () {
        //    TbDetalhe.DataTable().ajax.reload();
        //});


    });


    //$(document).on('hidden.bs.modal', '#modaltarifas', function () {
    //    window.location = GetUrl("/Tarifacao/ExpansaoTarifa");
    //});

});

function AtualizaDetalhe() {
    try {

        if ($.fn.DataTable.isDataTable('#rtbtarifa')) {
            $('#tbtarifa').DataTable().destroy();
        }
        if (TbDetalhe == null) {

            TbDetalhe = $('#tbtarifa').DataTable(
                {
                    paging: true,
                    searching: false,
                    lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
                    processing: true,
                    serverSide: true,
                    //orderMulti: false,
                    responsive: true,
                    ajax: {
                        url: GetUrl("Tarifacao/PesquisarTarifa"),
                        type: "POST",
                        datatype: "json",
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                        data: {
                            pUSUCFG: function () { return $('#CUSUCFG').val(); },
                            pNIVCFG: function () { return $('#CNIVCFG').val(); }
                        }
                    },
                    columns: [
                        { data: "NIDTAR" },
                        { data: "DSCREC" },
                        { data: "DSCTAR" }
                        //{ data: "DSCCOB" },
                        //{ data: "DSCEXP" },
                        //{ data: "TARBAS" },
                        //{ data: "TARMAX" }
                        //{ data: "PCTTAR" },
                        //{ data: "VLRTAR" },
                        //{ data: "VLRINF" },
                        //{ data: "VLRMAX" },
                        //{ data: "LGNUSU" },
                        //{
                        //    "render": function (data, type, row) {
                        //        return "<button id='btn_call_edt' value='Editar' class='btn btn-primary btn-sm' onclick=\"CallForEdit('" + row.NIDEXP + "');\"> <i class='fas fa-edit'></i></button>&nbsp;"
                        //    }
                        //}
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
                        { className: "text-left", "targets": [1], "visible": true, "searchable": false, "width": "20%" },
                        { className: "text-left", "targets": [2], "visible": true, "searchable": false, "width": "20%" }
                        //{ className: "text-right", "targets": [3], "visible": true, "searchable": false, "width": "15px" },
                        //{ className: "text-right", "targets": [4], "visible": true, "searchable": false, "width": "15px" },
                        //{ className: "text-right", "targets": [5], "visible": true, "searchable": false, "width": "15px" },
                        //{ className: "text-right", "targets": [6], "visible": true, "searchable": false, "width": "15px" }
                        //////{ className: "text-left", "targets": [7], "visible": true, "searchable": false, "width": "15px" },
                        //////{ className: "text-left", "targets": [8], "visible": true, "searchable": false, "width": "15px" },
                        //////{ className: "text-left", "targets": [9], "visible": true, "searchable": false, "width": "15px" },
                        //////{ className: "text-left", "targets": [10], "visible": true, "searchable": false, "width": "15px" },
                        //////{ className: "text-left", "targets": [11], "visible": true, "searchable": false, "width": "15px" },
                        //////{ className: "text-left", "targets": [12], "visible": true, "searchable": false, "width": "15px" }
                    ]
                });
        }
        else {
            TbDetalhe.DataTable().ajax.reload();
        }
    }
    catch (e) {
        alert(e);
    }
}

function AtualizaDetalhex() {
    //try {
//        if (TbDetalhe == null) {
            var TbDetalhe = $('#tbtarifa').DataTable(
                {
                    paging: true,
                    searching: false,
                    lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
                    processing: true,
                    serverSide: true,
                    orderMulti: false,
                    responsive: true,
                    ajax: {
                        url: GetUrl("Tarifacao/PesquisarTarifa"),
                        type: "POST",
                        datatype: "json",
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                        data: {
                            pUSUCFG: function () { return $('#CUSUCFG').val(); },
                            pNIVCFG: function () { return $('#CNIVCFG').val(); }
                        }
                    },
                    columns: [
                        { data: "NIDTAR" },
                        { data: "DSCREC" },
                        { data: "DSCTAR" },
                        { data: "DSCCOB" },
                        { data: "DSCEXP" },
                        { data: "TARBAS" },
                        { data: "TARMAX" },
                        { data: "PCTTAR" },
                        { data: "VLRTAR" },
                        { data: "VLRINF" },
                        { data: "VLRMAX" },
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
                        { className: "text-left", "targets": [1], "visible": true, "searchable": false, "width": "15px" },
                        { className: "text-left", "targets": [2], "visible": true, "searchable": false, "width": "15px" },
                        { className: "text-right", "targets": [3], "visible": true, "searchable": false, "width": "15px" },
                        { className: "text-right", "targets": [4], "visible": true, "searchable": false, "width": "15px" },
                        { className: "text-right", "targets": [5], "visible": true, "searchable": false, "width": "15px" },
                        { className: "text-right", "targets": [6], "visible": true, "searchable": false, "width": "15px" },
                        { className: "text-left", "targets": [7], "visible": true, "searchable": false, "width": "15px" },
                        { className: "text-left", "targets": [8], "visible": true, "searchable": false, "width": "15px" },
                        { className: "text-left", "targets": [9], "visible": true, "searchable": false, "width": "15px" },
                        { className: "text-left", "targets": [10], "visible": true, "searchable": false, "width": "15px" },
                        { className: "text-left", "targets": [11], "visible": true, "searchable": false, "width": "15px" },
                        { className: "text-left", "targets": [12], "visible": true, "searchable": false, "width": "15px" }
                    ]
                });
        //}
        //else {
        //    TbDetalhe.DataTable().ajax.reload();
        //}
    ////}
    ////catch (e) {
    ////    alert( 'x' + e);
    ////}
}

/*
 * 
 * 
                        {
                            data: "TARBAS", render: $.fn.dataTable.render.number('.', ',', 2, '')
                        },
                        {
                            data: "TARMAX",
                            render: $.fn.dataTable.render.number('.', ',', 2, '')
                        },
                        {
                            data: "PCTTAR",
                            render: $.fn.dataTable.render.number('.', ',', 2, '')
                        },

                        {
                            data: "VLRTAR",
                            render: $.fn.dataTable.render.number('.', ',', 2, '')
                        },

                        {
                            data: "VLRINF",
                            render: $.fn.dataTable.render.number('.', ',', 2, '')
                        },

                        {
                            data: "VLRMAX",
                            render: $.fn.dataTable.render.number('.', ',', 2, '')
                        },

  
   */

function PrimaryValidation() {

    var aval;
    var msg = '';
    var ret = true;

    var modo = parseInt($("#FOPERATION").val());
    if (modo == 2) {
        aval = parseInt($("#FNIDTAR").val());
        if (aval <= 0) {
            msg = 'Identificacao do Registro Inválida';
            ret = false;
        }
    }
    if (ret === true) {
        aval = parseInt($("#FNIVCFG").val());
        if (aval <= 0) {
            msg = 'Nível de Configuração Inválido';
            ret = false;
        }
    }
    if (ret === true) {
        aval = parseInt($("#FUSUCFG").val());
        if (aval <= 0) {
            msg = 'Selecione um usuário válido';
            ret = false;
        }
    }
    if (ret === true) {
        aval = parseInt($("#FCODTAR").val());
        if (aval <= 0) {
            msg = 'Selecione uma tarifa válida';
            ret = false;
        }
    }
    if (ret === true) {
        aval = parseInt($("#FCODBND").val());
        if (aval < 0) {
            $("#FCODBND").val("0");
        }
    }
    if (ret === true) {
        aval = parseInt($("#FTIPLIN").val());
        if (aval < 0) {
            $("#FTIPLIN").val("0");
        }
    }
    if (ret === true) {
        aval = parseInt($("#FMODCRT").val());
        if (aval < 0) {
            $("#FMODCRT").val("0");
        }
    }
    if (ret === true) {
        aval = parseInt($("#FCODEXP").val());
        if (aval < 0) {
            $("#FCODEXP").val("0");
        }
    }
    if (ret === true) {
        aval = parseInt($("#FFMTCOB").val());
        if (aval <= 0) {
            msg = 'Selecione um FORMATO de cobrança válido';
            ret = false;
        }
    }
    if (ret === true) {
        aval = parseInt($("#FRSPTAR").val());
        if (aval <= 0) {
            $("#FRSPTAR").val("2");
        }
    }
    var all0 = 0;
    aval = $("#FTARBAS").val();
    if ((aval == null) || (aval == '')) {
        $("#FTARBAS").val("0");
        all0++;
    }

    aval = $("#FPCTTAR").val();
    if ((aval == null) || (aval == '')) {
        $("#FPCTTAR").val("0");
        all0++;
    }

    aval = $("#FVLRTAR").val();
    if ((aval == null) || (aval == '')) {
        $("#FVLRTAR").val("0");
        all0++;
    }

    aval = $("#FVLRINF").val();
    if ((aval == null) || (aval == '')) {
        $("#FVLRINF").val("0");
        all0++;
    }

    aval = $("#FVLRMAX").val();
    if ((aval == null) || (aval == '')) {
        $("#FVLRMAX").val("0");
        all0++;
    }


    aval = $("#FTARMAX").val();
    if ((aval == null) || (aval == '')) {
        $("#FTARMAX").val("0");
        all0++;
    }

    if (all0 == 6) {
        ret = false;
        msg = "Informe os campos de valores";
    }

    if (ret == true) {
        var val = ToFloat($("#FVLRTAR").val());

        if (val <= 0) {
            ret = false;
            msg = "Informe o Valor da Tarifa";
        }
    }

    if (ret == true) {
        var val = ToFloat($("#FVLRINF").val());

        if (val <= 0) {
            ret = false;
            msg = "Informe o Limite Minimo de Operação da Tarifa";
        }
    }

    if (ret == true) {
        var val = ToFloat($("#FVLRMAX").val());

        if (val <= 0) {
            ret = false;
            msg = "Informe o Limite Máximo de Operação da Tarifa";
        }
    }

    if (ret == true) {
        var val1 = ToFloat($("#FVLRINF").val());
        var val2 = ToFloat($("#FVLRMAX").val());

        if (val1 > val2) {
            ret = false;
            msg = "o Limite Mínimo não pode ser maior que o Limite Máximo de Operação da Tarifa";
        }
    }

    if (ret === false) {
        $.alert({ class: 'danger', title: 'Tarifa', message: msg, effect: 'zoom' });
    }

    return ret;
}


function CallForAdd() {
    $("#OPERATION").val("1");
    $("#FSTAREC").prop('checked', "true");
    $("#FNIDTAR").val("0");
    $('#modaltarifas').modal({ backdrop: 'static', Keyboard: 'true' });
    $('#modaltarifas').modal('toggle');
}
function CallForEdit(pNIDTAR) {
    $("#OPERATION").val("2");
    $.ajax({
        type: "GET",
        url: GetUrl("/Tarifacao/SelecionarTarifa"),
        data: { pNIDTAR: pNIDTAR },
        dataType: 'json',
        success: function (result) {
            $("#FNIDTAR").val(result.NIDTAR);
            $("#FNIVCFG").val(result.NIVCFG);
            $("#FUSUCFG").val(result.USUCFG);
            $("#FCODTAR").val(result.CODTAR);
            $("#FCODBND").val(result.CODBND);
            $("#FTIPLIN").val(result.TIPLIN);
            $("#FMODCRT").val(result.MODCRT);
            $("#FCODEXP").val(result.CODEXP);
            $("#FFMTCOB").val(result.FMTCOB);
            $("#FTARBAS").val(result.TARBAS);
            $("#FPCTTAR").val(result.PCTTAR);
            $("#FVLRTAR").val(result.VLRTAR);
            $("#FVLRINF").val(result.VLRINF);
            $("#FVLRMAX").val(result.VLRMAX);
            $("#FTARMAX").val(result.TARMAX);
            $("#FRSPTAR").val(result.RSPTAR);
            $("#FSTAREC").prop('checked', (result.STAREC == 1));
        }
    });
    $('#modaltarifas').modal({ backdrop: 'static', Keyboard: 'true' });
    $('#modaltarifas').modal('toggle');
}