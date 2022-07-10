var TbDetalhe = null;

$(document).ready(function () {
    //$('.cpf').mask('000.000.000-00', { reverse: true });
    //$('.cnpj').mask('00.000.000/0000-00', { reverse: true });
    //$('.other').mask('00000000000000', { reverse: true });

    $('.datepicker').datepicker({
        format: "dd/mm/yyyy",
        language: "pt-BR",
        minViewMode: 0,
        orientation: 'auto',
        startDate: new Date('1930-1-1'),
        autoclose: true,
        daysOfWeekDisabled: ['0']
    });

    $(document).on('change', '#CSTAREC', function () {
        $('#tbusuarios').DataTable().ajax.reload();
    });

    $(document).on('change', '#CSTAUSU', function () {
        $('#tbusuarios').DataTable().ajax.reload();
    });

    $(document).on('change', '#CSRCUSU', function () {
        $('#tbusuarios').DataTable().ajax.reload();
    });

    $(document).on('change', '#CCODATR', function () {
        $('#tbusuarios').DataTable().ajax.reload();
    });

    $(document).on('change', '#TNOMUSU', function () {
        $('#tbusuarios').DataTable().ajax.reload();
    });

    $(document).on('focus', '#FCODCMF', function () {
        var pju = $("input[name='FCODPJU']:checked").val();

        if (pju == "F") {
            $(this).mask('000.000.000-00', { reverse: true });
        }
        else {
            $(this).mask('00.000.000/0000-00', { reverse: true });
        }
    });

    $(document).on("click", "#btn_update_lgn", function () {
        var mode = $("#OPERATION").val();
        var VDATA = JSON.stringify({
            'LGNNUM': 0,
            'CODUSU': parseInt($("#LCODUSU").val()),
            'LGNTYP': $("#FLGNTYP").is(':checked') ? 1 : 0,
            'LGNUSU': $("#FLGNUSU").val(),
            'PSWUSU': $("#FPSWUSU").val(),
            'RSTPSW': $("#FRSTPSW").is(':checked') ? 1 : 0,
            'STAREC': 1
        });
        $.ajax({
            type: "POST",
            url: GetUrl("Cadastros/ChangeLoginUser"),
            data: VDATA,
            dataType: 'json',
            contentType: 'application/json',
            beforeSend: function () { },
            success: function (result) {
                console.log(result);
                var ok = parseInt(result.ReturnValue);
                if (ok > 0) {
                    $('#modalloginuser').modal('toggle');
                }
                else {

                    $.alert({ class: 'danger', title: 'Login', message: result.MessageToUser, effect: 'zoom' });
                }
            },
            error: function (data) {
                $.alert({ class: 'danger', title: 'Login', message: 'Ocorreu um erro inesperado, por favor tente novamente', effect: 'zoom' });
            }
        });
    });

    $(document).on("click", "#btn_update", function () {
        var mode = $("#OPMODE").val();
        var vCODCMF = $("#FCODCMF").val();
        var vDATNAC = $("#FDATNAC").val();
        vCODCMF = vCODCMF.replace(/[^0-9]/gi, '')
        var go = 1;

        if (vCODCMF.length < 11) {
            $.alert({ class: 'danger', title: 'Usuário', message: 'CPF/CNPJ Inválido', effect: 'zoom' });
            go = 0;
        };

        if (FDATNAC == "" || FDATNAC ==null) {
            $.alert({ class: 'danger', title: 'Usuário', message: 'Data inválida', effect: 'zoom' });
            go = 0;
        };

        if (mode == "" || mode == null) {
            $.alert({ class: 'danger', title: 'Usuário', message: 'Operação Inválida', effect: 'zoom' });
            go = 0;
        };

        if (go == 1) {

            var vCODPJU = $("input[name='FCODPJU']:checked").val();
            var vNUMIRG = $("#FNUMIRG").val();
            if (vNUMIRG == "" || vNUMIRG == null)
                vNUMIRG = " ";
            var vNOMMAE = $("#FNOMMAE").val();
            if (vNOMMAE == "" || vNOMAME == null)
                vNOMMAE = " ";
            vDSCOCO = " ";
            var VDATA = JSON.stringify({
                'CODUSU': parseInt($("#FCODUSU").val()),
                'CODATR': parseInt($("#FCODATR").val()),
                'SRCUSU': parseInt($("#FSRCUSU").val()),
                'NIVCFA': parseInt($("#FNIVCFA").val()),
                'STAUSU': parseInt($("#FSTAUSU").val()),
                'TIPUSU': parseInt($("#FTIPUSU").val()),
                'CODPJU': vCODPJU,
                'NUMIRG': vNUMIRG,
                'CODCMF': vCODCMF,
                'NOMUSU': $("#FNOMUSU").val(),
                'DATNAC': toDate($("#FDATNAC").val()),
                'ATRPPE': $("#FATRPPE").is(':checked') ? 1 : 0,
                'DSCOCO': vDSCOCO,
                'CODNAC': parseInt($("#FCODNAC").val()),
                'STAREC': $("#FSTAREC").is(':checked') ? 1 : 0
            });
            $.ajax({
                type: "POST",
                url: GetUrl("Cadastros/AtualizarUsuarios?modo=" + mode ),
                data: VDATA,
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function () { },
                success: function (result) {
                    console.log(result);
                    var ok = parseInt(result.ReturnValue);
                    if (ok > 0) {
                        $('#modalusuarios').modal('toggle');
                    }
                    else {

                        $.alert({ class: 'danger', title: 'Usuário', message: result.MessageToUser, effect: 'zoom' });
                    }
                },
                error: function (data) {
                    $.alert({ class: 'danger', title: 'Usuário', message: 'Ocorreu um erro inesperado, por favor tente novamente', effect: 'zoom' });
                }
            });
        }
    });

    AtualizaDetalhe();
});


function CallForAdd() {
    $("#OPMODE").val("1");
    $("#FSTAREC").prop('checked', "true");

    var value = $("#CSRCUSU").val();
    if (value > 0) {
        $("#FSRCUSU").val(value);
        $("#FSRCUSU").prop("disabled", true);
    }

    value = $("#CSTAUSU").val();
    if (value > 0) {
        $("#FSTAUSU").val(value);
        $("#FSTAUSU").prop("disabled", true);
    }

    value = $("#CCODATR").val();
    if (value > 0) {
        $("#FCODATR").val(value);
        $("#FCODATR").prop("disabled", true);
    }
    $("#FTIPPES").val("M");
    $("#FTIPUSU").val("4");
    $('#modalusuarios').modal({ backdrop: 'static', Keyboard: 'true' });
    $('#modalusuarios').modal('toggle');
}

function CallForEdit(pCODUSU) {
    $("#OPMODE").val("2");
    $.ajax({
        type: "GET",
        url: GetUrl("Cadastros/SelecionarUsuario"),
        data: { id: pCODUSU },
        dataType: 'json',
        success: function (result) {
            $("#FCODUSU").val(result.CODUSU);
            $("#FCODATR").val(result.CODATR);
            var value = $("#CCODATR").val();
            if (value == result.CODATR) {
                $("#FCODATR").prop("disabled", true);
            }
            $("#FSTAUSU").val(result.STAUSU);
            value = $("#CSRCUSU").val();
            if (value == result.STAUSU) {
                $("#FSTAUSU").prop("disabled", true);
            }

            $("#FSRCUSU").val(result.SRCUSU);
            value = $("#CSRCUSU").val();
            if (value == result.SRCUSU) {
                $("#FSRCUSU").prop("disabled", true);
            }
            $("#FTIPPES").val(result.TIPPES);
            $("#FNIVCFA").val(result.NIVCFA);
            $("#FTIPUSU").val(result.TIPUSU);
            $("#FCODNAC").val(result.CODNAC);
            $("#FCODCMF").val(result.CODCMF);
            $("#FNUMIRG").val(result.NUMIRG);
            $("#FNOMUSU").val(result.NOMUSU);
            $("#FNOMAME").val(result.NOMMAE);
            $("#FDATNAC").val(fromDate(result.DATNAC));
            $("#FSTAREC").val(result.STAREC);
            $("#FATRPPE").val(result.ATRPPE);
            $("#FDSCOCO").val(result.DSCOCO);
            if (result.CODPJU == "F")
                $("#FCODPJU1").prop('checked', "true");
            if (result.CODPJU == "J")
                $("#FCODPJU2").prop('checked', "true");
            if (result.CODPJU == "I")
                $("#FCODPJU3").prop('checked', "true");
        }
    })
    $('#modalusuarios').modal({ backdrop: 'static', Keyboard: 'true' });
    $('#modalusuarios').modal('toggle');
}

//function CallAddress(pCODUSU) {
//    $.ajax({
//        type: "GET",
//        url: GetUrl('Cadastros/Enderecos?uc=' +pCODUSU),
//        dataType: 'json',
//        success: function (result) {
//            window.Location = GetUrl('Cadastros/Enderecos');
//        }
//    });
//}

function CallForAddLogin(pCODUSU) {
    $.ajax({
        type: "GET",
        url: GetUrl("Cadastros/LoginPadrao"),
        data: { pCODUSU: pCODUSU },
        dataType: 'json',
        success: function (result) {
            console.log(result);

            $("#LCODUSU").val(pCODUSU);
            $("#FLGNUSU").val(result.NOMUSU);
            $("#FSTAREC").prop('checked', "true");
            $('#modalloginuser').modal({ backdrop: 'static', Keyboard: 'true' });
            $('#modalloginuser').modal('toggle');
        }
    })
}


function toDate(dateStr) {
    var parts = dateStr.split("/");
    return new Date(parts[2], parts[1] - 1, parts[0])
}

function fromDate(dateStr) {
    if (dateStr.length >= 10) {
        var parts = dateStr.substring(0,10).split("-")
         return parts[2] + '/' + parts[1] + '/' + parts[0].substring(0, 4);
    }
    return '';
}

function AtualizaDetalhe() {
    try {
        if (TbDetalhe == null) {
            TbDetalhe = $('#tbusuarios').DataTable(
                {
                    paging: true,
                    searching: false,
                    lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
                    processing: true,
                    serverSide: true,
                    orderMulti: false,
                    responsive: true,
                    ajax: {
                        url: GetUrl("Cadastros/PesquisarUsuarios"),
                        type: "POST",
                        datatype: "json",
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                        data: {
                            pCODATR: function () { return $('#CCODATR').val() },
                            pSTAUSU: function () { return $('#CSTAUSU').val() },
                            pSRCUSU: function () { return $('#CSRCUSU').val() },
                            pNOMUSU: function () { return $('#TNOMUSU').val() },
                            pSTAREC: function () { return $('#CSTAREC').val() }
                        }
                    },
                    columns: [
                        { data: "CODUSU" },
                        { data: "DSCREC" },
                        { data: "CNTCTA" },
                        { data: "CNTMAL" },
                        { data: "CNTCEL" },
                        { data: "DSCSTA" },
                        { data: "NOMUSU" },
                        { data: "DSCTUS" },
                        { data: "CODCMF" },
                        {
                            "render": function (data, type, row) {
                                var botoes = "";
                                if (row.STAREC == 1) {
                                    botoes += "<button title='Editar o Registro' id='btn_call_edt' value='Editar'  onclick=\"CallForEdit('" + row.CODUSU + "');\"> <i class='fa fa-edit' style='font-size:12px;border:none'></i></button>&nbsp;"
                                }

                                if (row.USELGN == 1 && row.STAREC == 1) {
                                    botoes += "<button title='Criar Login de Acesso' id='btn_call_lgn' value='Criar Logon'  onclick=\"CallForAddLogin('" + row.CODUSU + "');\"><i class='fa fa-key'  style='font-size:12px;border:none'></i> </button>&nbsp;"
                                }
                                botoes += "<a title='Endereços' id='href_call_address' value='Endereços'  href=\"/Cadastros/Enderecos?uc=" + row.CODUSU + "\"> <i class='fa fa-map-marker' style='font-size: 12px;border:none''></i></a>&nbsp;"

                                if (row.STAREC == 1 && row.USETAR == 1) {
                                    botoes += "<a title='Tarifas' id='href_call_tarif' value='Tarifas'  href=\"/Tarifacao/Tarifas?lvl=2&usrcode=" + row.CODUSU + "\"> <i class='fa fa-percent' style='font-size: 12px;border:none''></i></a>&nbsp;"
                                }
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
                        { className: "text-left", "targets": [1], "visible": true, "searchable": false, "width": "2%" },
                        { className: "text-center", "targets": [2], "visible": true, "searchable": false, "width": "3%" },
                        { className: "text-center", "targets": [3], "visible": true, "searchable": false, "width": "3%" },
                        { className: "text-center", "targets": [4], "visible": true, "searchable": false, "width": "3%" },
                        { className: "text-left", "targets": [5], "visible": true, "searchable": false, "width": "3%" },
                        { className: "text-left", "targets": [6], "visible": true, "searchable": false, "width": "30%x" },
                        { className: "text-left", "targets": [7], "visible": true, "searchable": false, "width": "14%" },
                        { className: "text-left", "targets": [8], "visible": true, "searchable": false, "width": "14%" },
                        { className: "text-left", "targets": [9], "visible": true, "searchable": false, "width": "15%" }
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