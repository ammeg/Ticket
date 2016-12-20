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

var table = $("#tblDepartamento").dataTable({

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


    "sCookiePrefix": "SYNS_EquipamentoProntos_Pesquisa_",
    "aoColumns": [
            {
                "bSortable": false,
                "bSearchable": false,
                "mData": "DepartamentoId",
                "fnRender": function (oObj) {
                    return "<a href='" + selecionar + "/" + oObj.aData.DepartamentoId + "'>Selecionar</a>";

                }
            },
            { "mData": "DescricaoCompleta" },
            { "mData": "Sigla" },
            { "mData": "Email" },
          
          
    ],

    "sAjaxSource": pesquisar,

    "fnServerData": function (sSource, aoData, fnCallback) {

        aoData.push({ "name": "descricao", "value": $('#descricao').val() });

        $.getJSON(sSource, aoData, function (json) {
            fnCallback(json);
        });
    }

}).columnFilter({
    aoColumns: [
                { sSelector: "#PesquisarProntosUniluz_NumeroSerie" }
    ]
});


$('#descricao').change(function () {
    table.fnFilter();
});


$(".dataTables_filter").hide();
