var TbDetalhe = null;

$(document).ready(function () {
    $('.money').mask("#.##0,00");
    $('.datepicker').datepicker({
        format: "dd/mm/yyyy",
        language: "pt-BR",
        minViewMode: 0,
        orientation: 'auto',
        startDate: '+1d',
        autoclose: true,
        daysOfWeekDisabled: ['0']
    });

    $("#CNOMUSU").autocomplete({
        source: function (request, response) {
            var vDATA = JSON.stringify({ 'pNOMUSU': $("#CNOMUSU").val() });
            $.ajax({
                url: GetUrl('Home/UsuariosPorNome'),
                type: "POST",
                dataType: "json",
                data: vDATA,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    console.log(data);
                    response($.map(data, function (item) {
                        return { label: item.NOMUSU, value: item.KEYCOD };
                    }))
                }
            });
        },
        messages: {
            noResults: "", results: ""
        },
        select: function (event, ui) {
            $("#PCODUSU").val(ui.item.KEYCOD);
            $("#CNOMUSU").prop("disabled", true);
        },
        minLength: 1
    });

    $(document).on("click", "#btn_update", function () {
        var mode = $("#OPERATION").val();

        vDATVAL = ToDateValid($("#FDATVAL").val());
        var VDATA = JSON.stringify({
            'CODUSU': parseInt($("#FCODUSU").val()),
            'NUMAGE': $("#FNUMAGE").val(),
            'NUMBCO': $("#FNUMBCO").val(),
            'NUMCTA': $("#FNUMCTA").val(),
            'NUMDVE': $("#FNUMDVE").val(),
            'ORGCTA': parseInt($("#FORGCTA").val()),
            'TIPCTA': parseInt($("#FTIPCTA").val()),
            'STACTA': parseInt($("#FSTACTA").val()),
            'VLRLIM': ToFloat($('#FVLRLIM').val()),
            'DATTRF': vDATVAL,
            'APLLIM': $("#FAPLLIM").is(':checked') ? 1 : 0,
            'TIPBNF': parseInt($("#FTIPBNF").val()),
            'CODCMF': $("#FCODCMF").val(),
            'NOMBNF': $("#FNOMBNF").val(),
            'STAREC': $("#FSTAREC").is(':checked') ? 1 : 0
        });
        $.ajax({
            type: "POST",
            url: GetUrl("/Cadastros/AtualizarContaVirtual?modo=" + mode),
            data: VDATA,
            dataType: 'json',
            contentType: 'application/json',
            beforeSend: function () { },
            success: function (result) {
                console.log(result);
                var ok = parseInt(result.ReturnValue);
                if (ok > 0) {
                    $('#modalcontavirtual').modal('toggle');
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


    $('#FORGCTA').on('change', function () {
        var value = parseInt($(this).val());
        $('.key1').removeAttr("disabled");
        if (reset == 1) {
            $('.key1').attr("disabled", "disabled");
            $('#FTIPCTA').val("1");
            $('#FNUMBCO').val("000");
            $('#FTIPBNF').val("0");
            $('#FNUMAGE').val("0001");

        }
    });
    $('#CNOMUSU').on('change', function () {
        $('#tbcontas').DataTable().ajax.reload();
    });
    $('#CSTACTA').on('change', function () {
        $('#tbcontas').DataTable().ajax.reload();
    });

    AtualizaDetalhe();

});


function CallForAdd() {
    $("#OPERATION").val("1");
    var pCODUSU = $("#PCODUSU").val();
    if (pCODUSU > 0) {
        $("#FCODUSU").val(pCODUSU);
        $("#FCODUSU").attr('disabled', 'disabled');
        $("#FSTACTA").val(251);
        $("#FSTACTA").attr('disabled', 'disabled');
        $("#FSTAREC").prop('checked', "true");
        $("#FDATVAL").val(FormatDate(addDays(new Date(), 720)));
        $('#modalcontavirtual').modal({ backdrop: 'static', Keyboard: 'true' });
        $('#modalcontavirtual').modal('toggle');
    }
    else {
        $.alert({ class: 'danger', title: 'Conta Virtual', message: 'Selecione um usuário válido !!!', effect: 'zoom' });
    }
}

function CallForEdit(pNIDCTA) {
    $("#OPERATION").val("2");
    $.ajax({
        type: "GET",
        url: GetUrl("Cadastros/SelecionarContaVirtual"),
        data: { pNIDCTA: pNIDCTA },
        dataType: 'json',
        success: function (result) {
            $("#FNIDCTA").val(result.NIDCTA);
            $("#FCODUSU").val(result.CODUSU);
            $("#FNUMBCO").val(result.NUMBCO);
            $("#FNUMCTA").val(result.NUMCTA);
            $("#FNUMDVE").val(result.NUMDVE);
            $("#FORGCTA").val(result.ORGCTA);
            $("#FTIPCTA").val(result.TIPCTA);
            $("#FSTACTA").val(result.STACTA);
            $("#FAPLLIM").prop('checked', (result.APLLIM == 1));
            $("#FVLRLIM").val(result.VLRLIM);
            $("#FTIPBNF").val(result.TIPBNF);
            $("#FCODCMF").val(result.CODCMF);
            $("#FNOMBNF").val(result.NOMBNF);
            $("#FSTAREC").prop('checked', (result.STAREC == 1));
            $("#FSTAREC").prop('checked', (result.STAREC == 1));
            $("#FDATVAL").val(fromDate(result.DATVAL));


            if (result.ORGCTA == 1 || result.ORGCTA == 3 || result.ORGCTA == 4) {
                $('.key1').attr("disabled", "disabled");
                $('#FTIPCTA').val("1");
                $('#FTIPBNF').val("0");
                $('#FNUMBCO').val("000");
                $('#FNUMAGE').val("0001");
            }
        }
    })
    $('#modalcontavirtual').modal({ backdrop: 'static', Keyboard: 'true' });
    $('#modalcontavirtual').modal('toggle');
}

function CallForAproval(pNIDCTA) {
    $("#OPERATION").val("2");
    $.ajax({
        type: "GET",
        url: GetUrl("Cadastros/SelecionarContaVirtual"),
        data: { pNIDCTA: pNIDCTA },
        dataType: 'json',
        success: function (result) {
            $("#FNIDCTA").val(result.NIDCTA);
            $("#FCODUSU").val(result.CODUSU);
            $("#FNUMBCO").val(result.NUMBCO);
            $("#FNUMCTA").val(result.NUMCTA);
            $("#FNUMDVE").val(result.NUMDVE);
            $("#FORGCTA").val(result.ORGCTA);
            $("#FTIPCTA").val(result.TIPCTA);
            $("#FSTACTA").val(result.STACTA);
            $("#FAPLLIM").prop('checked', (result.APLLIM == 1));
            $("#FVLRLIM").val(result.VLRLIM);
            $("#FTIPBNF").val(result.TIPBNF);
            $("#FCODCMF").val(result.CODCMF);
            $("#FNOMBNF").val(result.NOMBNF);
            $("#FSTAREC").prop('checked', (result.STAREC == 1));
            $("#FSTAREC").prop('checked', (result.STAREC == 1));
            $("#FDATVAL").val(fromDate(result.DATVAL));
            if (result.ORGCTA == 1 || result.ORGCTA == 3 || result.ORGCTA == 4) {
                $('.key1').attr("disabled", "disabled");
                $('#FTIPCTA').val("1");
                $('#FTIPBNF').val("0");
                $('#FNUMBCO').val("000");
                $('#FNUMAGE').val("0001");
            }
        }
    })
    $('#modalcontavirtual').modal({ backdrop: 'static', Keyboard: 'true' });
    $('#modalcontavirtual').modal('toggle');
}


function addDays(date, days) {
    var result = new Date(date);
    result.setDate(result.getDate() + days);
    return result;
}

function FormatDate(date) {
    return Right('00' + date.getDate(), 2) + '/' + Right('00' + (date.getMonth() + 1), 2) + '/' + date.getFullYear();
}

function ToFloat(value) {
    var v = value.replace('.', '').replace(',', '');
    return parseInt(v) / 100;
}
function ToDateValid(value) {
    var d = value;
    if ((value === "") || (value == null)) {
        d = FormatDate(addDays(new Date(), 720));
    }
    d = d.substr(6, 4) + d.substr(3, 2) + d.substr(0, 2) + ' 23:59:59';
    return d;
}

function fromDate(dateStr) {
    if (dateStr.length >= 10) {
        var parts = dateStr.substring(0, 10).split("-")
        return parts[2] + '/' + parts[1] + '/' + parts[0].substring(0, 4);
    }
    return '';
}


function AtualizaDetalhe() {
    try {
        if (TbDetalhe == null) {

            TbDetalhe = $('#tbcontas').DataTable(
                {
                    paging: true,
                    searching: false,
                    lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
                    processing: true,
                    serverSide: true,
                    orderMulti: false,
                    responsive: true,
                    ajax: {
                        url: GetUrl("Cadastros/PesquisarContasVirtuais"),
                        type: "POST",
                        datatype: "json",
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                        data: {
                            pSTACTA: function () { return $('#CSTACTA').val() },
                            pNOMUSU: function () { return $('#CNOMUSU').val() }
                        }
                    },
                    columns: [
                        { data: "NIDCTA" },
                        { data: "DSCCTA" },
                        {
                            render: function (data, type, row) {
                                return row.NUMAGE + ' ' + row.NUMCTA + '-' + row.NUMDVE;
                            }
                        },
                        {
                            render: function (data, type, row) {
                                return row.NUMBCO + '-' + row.DSCBCO;
                            }

                        },
                        { data: "NOMUSU" },
                        {
                            render: function (data, type, row) {
                                if (row.ORGCTA == 1 || row.ORGCTA == 3 || row.ORGCTA == 4)
                                    return row.DSCBNF;
                                else
                                    return row.CODCMF + ' - ' + row.NOMBNF;
                            }
                        },
                        { data: "LGNUSU" },
                        {
                            "render": function (data, type, row) {
                                var botoes = "";
                                botoes += "<button id='btn_call_edt' value='Editar' onclick=\"CallForEdit('" + row.NIDCTA + "');\"> <i class='fa fa-edit style='font-size:12px;border:none'></i></button>&nbsp;"
                                if (row.STACTA == 253) {
                                    botoes += "<button id='btn_call_apr' value='Aprovar Conta' class='btn' onclick=\"CallForApproval('" + row.NIDCTA + "');\"> <i class='fa fa-edit' ></i></button>&nbsp;"
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
                        { className: "text-left", "targets": [1], "visible": true, "searchable": false, "width": "8%" },
                        { className: "text-left", "targets": [2], "visible": true, "searchable": false, "width": "8%" },
                        { className: "text-left", "targets": [3], "visible": true, "searchable": false, "width": "6%" },
                        { className: "text-left", "targets": [4], "visible": true, "searchable": false, "width": "12%" },
                        { className: "text-left", "targets": [5], "visible": true, "searchable": false, "width": "12%" },
                        { className: "text-left", "targets": [6], "visible": true, "searchable": false, "width": "8%" },
                        { className: "text-right", "targets": [7], "visible": true, "searchable": false, "width": "10%" }
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