﻿$.extend($.fn.dataTable.defaults, {
    "bFilter": true,
    "aLengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
    "sPaginationType": "full_numbers",
    "bProcessing": true,
    "bServerSide": true,
    "bStateSave": true,
    "bSortMulti": false,
});



var pesquisar = $("#pesquisarUrl").val();
var selecionar = $("#selecionarId").val();

var table = $("#tblUsuario").dataTable({

    "oLanguage": {
        "oPaginate": {
            "sFirst": "Primeira",
            "sLast": "Última",
            "sNext": "Próxima",
            "sPrevious": "Anterior"
        },
        "sEmptyTable": "Nenhum registro encontrado",
        "sLengthMenu": "Mostrar _MENU_ registros",
        "sInfo": "Mostrando _START_ de _END_ de _TOTAL_ registros",
        "sInfoEmpty": "Mostrando 0 de _END_ de _TOTAL_ registros",
        "sLoadingRecords": "Aguarde...",
        "sProcessing": "Aguarde..."
    },


    "sCookiePrefix": "UNILUZ_EquipamentoProntos_Pesquisa_",
    "aoColumns": [
            {
                "bSortable": false,
                "bSearchable": false,
                "mData": "UsuarioId",
                "fnRender": function (oObj) {
                    return "<a href='" + selecionar + "/" + oObj.aData.UsuarioId + "'>Selecionar</a>";

                }
            },
            { "mData": "Nome" },
            { "mData": "Login" },
            { "mData": "UsuarioPerfil" },
       
    ],

    "sAjaxSource": pesquisar,

    "fnServerData": function (sSource, aoData, fnCallback) {

        aoData.push({ "name": "nome", "value": $('#nome').val() });

        $.getJSON(sSource, aoData, function (json) {
            fnCallback(json);
        });
    }

}).columnFilter({
    aoColumns: [
                { sSelector: "#PesquisarProntosUniluz_NumeroSerie" }
    ]
});

$(document).ready(function () {
    table.fnFilter();
})


$('#nome').change(function () {
    table.fnFilter();
});


function OnCompleteTeste2() {
    table.fnFilter();
}

$(".dataTables_filter").hide();