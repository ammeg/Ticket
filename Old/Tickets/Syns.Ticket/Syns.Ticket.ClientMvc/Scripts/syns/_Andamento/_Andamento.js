$.extend($.fn.dataTable.defaults, {
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

var table = $("#tblTicketAndamento").dataTable({

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
                "mData": "TicketAndamentoId",
                "fnRender": function (oObj) {
                    return "<a href='" + selecionar + "/" + oObj.aData.TicketAndamentoId + "'>Selecionar</a>";

                }
            },
            { "mData": "Status" },
            { "mData": "DataHora" },
            { "mData": "Descricao" },
            { "mData": "Ticket" },
            { "mData": "Visibilidade" },
            { "mData": "Departamento" },
            { "mData": "Data" },

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


$(".dataTables_filter").hide();
