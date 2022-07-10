var TbDetalhe = null;

$(document).ready(function () {
    $('.cep').mask('00000-000');

    $(document).on("click", "#btn_update_end", function () {
        var mode = $("#OPERATION").val();
        var vNUMEND = $('#FNUMEND').val();
        var vCODPAI = $('#FCODPAI').val();
        if (vNUMEND == "")
            vNUMEND = 0;
        else
            vNUMEND = parseInt(vNUMEND);

        if (vCODPAI === "" || vCODPAI == null)
            vCODPAI = 55;
        else
            vCODPAI = parseInt(vCODPAI);
        var vDSCCID = $("#FDSCCID").val();
        if (vDSCCID === "" || vDSCCID == null)
            vDSCCID = " ";
        var vDSCBAI = $("#FDSCBAI").val();
        if (vDSCBAI === "" || vDSCBAI == null)
            vDSCBAI = " ";
        var vCODUFE = $("#FCODUFE").val();
        if (vCODUFE === "" || vCODUFE == null)
            vCODUFE = "00";

        var vCODCEP = $("#FCODCEP").val().replace('.', '').replace(',', '').replace('-', '');
        var VDATA = JSON.stringify({
            'CODEND': parseInt($("#FCODEND").val()),
            'REGATV': $("#FREGATV").is(':checked') ? 1 : 0,
            'CODUSU': parseInt($("#FCODUSU").val()),
            'TIPEND': parseInt($("#FTIPEND").val()),
            'TIPLOG': parseInt($("#FTIPLOG").val()),
            'CODUFE': vCODUFE,
            'DSCEND': $("#FDSCEND").val(),
            'NUMEND': vNUMEND,
            'DSCCID': vDSCCID,
            'DSCBAI': vDSCBAI,
            'CODCEP': vCODCEP,
            'CODPAI': vCODPAI,
            'STAREC': $("#FSTAREC").is(':checked') ? 1 : 0
        });
        $.ajax({
            type: "POST",
            url: GetUrl("Cadastros/AtualizarEnderecos?modo=" + mode),
            data: VDATA,
            dataType: 'json',
            contentType: 'application/json',
            beforeSend: function () { },
            success: function (result) {
                console.log(result);
                var ok = parseInt(result.ReturnValue);
                if (ok > 0) {
                    $('#modaladdressbook').modal('toggle');
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


    $('#FTIPEND').on('change', function () {
        var value = $(this).val();
        if ((value == 3) || (value == 4)) {
            $('#FCODCEP').val("00000000");
            $('#FTIPLOG').val("0");
            $('#FNUMEND').val("0");
            $('.addr1').attr("disabled", "disabled");
        }
        else {
            $('.addr1').removeAttr("disabled");
        }
    });

    $('#CCODUSU').on('change', function () {
        $('#tbenderecos').DataTable().ajax.reload();
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
    $("#FCODUFE").val('SP');
    $('#modaladdressbook').modal('toggle');
}

function CallForEdit(pCODEND) {
    $("#OPERATION").val("2");
    $.ajax({
        type: "GET",
        url: GetUrl("Cadastros/SelecionarEndereco"),
        data: { pCODEND: pCODEND },
        dataType: 'json',
        success: function (result) {
            $("#FCODEND").val(result.CODEND);
            $("#FREGATV").prop('checked', (result.REGATV == 1));
            $("#FCODUSU").val(result.CODUSU);
            $("#FTIPEND").val(result.TIPEND);
            $("#FTIPLOG").val(result.TIPLOG);
            $("#FCODUFE").val(result.CODUFE);
            $("#FDSCEND").val(result.DSCEND);
            $("#FDSCCPL").val(result.DSCCPL);
            $("#FNUMEND").val(result.NUMEND);
            $("#FDSCCID").val(result.DSCCID);
            $("#FDSCBAI").val(result.DSCBAI);
            $("#FCODCEP").val(result.CODCEP);
            $("#FCODPAI").val(result.CODPAI);
            $("#FSTAREC").prop('checked', (result.STAREC == 1));


            if ((result.TIPEND == 3) || (result.TIPEND == 4)) {
                $('#FCODCEP').val("00000000");
                $('#FTIPLOG').val("0");
                $('#FNUMEND').val("0");
                $('.addr1').attr("disabled", "disabled");
            }
            else {
                $('.addr1').removeAttr("disabled");
            }

        }
    })
    $('#modaladdressbook').modal({ backdrop: 'static', Keyboard: 'true' });
    $('#modaladdressbook').modal('toggle');
}

function CallForContact(pCODUSU, pCODEND) {
    $.ajax({
        type: "GET",
        url: '/Cadastros/Contatos',
        data: { pCODUSU: pCODUSU },
        data: { pCODEND: pCODEND },
        dataType: 'json',
        success: function (result) {
            window.Location = '/Cadastros/Contatos';
        }
    });
}



function AtualizaDetalhe() {
    try {
        if (TbDetalhe == null) {

            TbDetalhe = $('#tbenderecos').DataTable(
                {
                    paging: true,
                    searching: false,
                    lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
                    processing: true,
                    serverSide: true,
                    orderMulti: false,
                    responsive: true,
                    ajax: {
                        url: GetUrl("Cadastros/PesquisarEnderecos"),
                        type: "POST",
                        datatype: "json",
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                        data: {
                            pCODUSU: function () { return $('#CCODUSU').val() },
                            pTIPEND: function () { return -1 },
                            pREGATV: function () { return -1 },
                            pSTAREC: function () { return -1 }
                        }
                    },
                    columns: [
                        { data: "CODEND" },
                        { data: "DSCREC" },
                        { data: "DSCATV" },
                        { data: "DSCTEN" },
                        { data: "FULEND" },
                        { data: "DATUPD" },
                        { data: "LGNUSU" },
                        {
                            "render": function (data, type, row) {
                                var botoes = "";
                                botoes += "<button id='btn_call_edt' value='Editar' class='btn-sm' onclick=\"CallForEdit(" + row.CODEND + ");\"> <i class='fa fa-edit'></i></button>&nbsp;"
                                if (!(row.TIPEND == 3 || row.TIPEND == 4)) {
                                    botoes += "<a id='href_call_contact' value='Contato' class='btn-sm' href=\"/Cadastros/contatos?uc=" + row.CODUSU + "&ce=" + row.CODEND + "\"> <i class='fa fa-phone' style='font-size:16px'></i></a>&nbsp;"
                                }

                                //if (row.REFCTO) {

                                //}
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
                        { className: "text-left", "targets": [4], "visible": true, "searchable": false, "width": "80px" },
                        { className: "text-left", "targets": [5], "visible": true, "searchable": false, "width": "12%" },
                        { className: "text-left", "targets": [6], "visible": true, "searchable": false, "width": "8%" },
                        { className: "text-left", "targets": [7], "visible": true, "searchable": false, "width": "10%" }
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