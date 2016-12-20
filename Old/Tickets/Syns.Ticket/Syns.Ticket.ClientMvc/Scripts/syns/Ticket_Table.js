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

var table = $("#tblTicket").dataTable({

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


    "sCookiePrefix": "SYNS_Ticket_Pesquisa_",
    "aoColumns": [
            {
                "bSortable": false,
                "bSearchable": false,
                "mData": "TicketId",
                "fnRender": function (oObj) {
                    return "<a href='" + selecionar + "/" + oObj.aData.TicketId + "'>Selecionar</a>";   
                }
            },
            { "@Url.Content("~/Images/details_open.png")"},
            { "mData": "Prioridade" },
            { "mData": "Categoria" },
            { "mData": "Assunto" },
            { "mData": "departamento" },
            { "mData": "usuario" },
            { "mData": "usuarioResponsavel" },

    ],

    "sAjaxSource": pesquisar,

    "fnServerData": function (sSource, aoData, fnCallback) {

     
        aoData.push({ "name": "prioridade", "value": $('#prioridade').val() });
        aoData.push({ "name": "categoria", "value": $('#categoria').val() });
        aoData.push({ "name": "assunto", "value": $('#assunto').val() });
        aoData.push({ "name": "departamento", "value": $('#departamento').val() });
        aoData.push({ "name": "usuario", "value": $('#usuario').val() });
        aoData.push({ "name": "usuarioResponsavel", "value": $('#usuarioResponsavel').val() });


        $.getJSON(sSource, aoData, function (json) {
            fnCallback(json);
        });
    }
});

$('#parametros').change(function () {
    table.fnFilter();
});

$(".dataTables_filter").hide();
