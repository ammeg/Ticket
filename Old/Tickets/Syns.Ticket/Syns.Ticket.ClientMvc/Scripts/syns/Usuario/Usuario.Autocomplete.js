jQuery.fn.extend({
    usuario: function (options, callbackFnk) {

        var settings = $.extend({
            // These are the defaults.
            urlUsuarioNome: $("#UsuarioNomeUrl").val(),
            urlId: $("#UsuarioIdUrl").val(),
            usuarioId: null,
            select: null,
            situacao: 0
        }, options);

        if (settings.urlUsuarioNome == null)
            alert("O parâmetro settings.urlUsuarioNome não foi encontrado");

        if (settings.urlId == null)
            alert("O parâmetro settings.urlId não foi encontrado");

        var elemento = this;

        this.autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: settings.urlUsuarioNome,
                    data: "{ 'nome': '" + request.term + "' }",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) {
                        return data;
                    },
                    success: function (data) {
                        response($.map(data, function (item) {
                            return {
                                value: item.Nome,

                                UsuarioId: item.UsuarioId,
                                Nome: item.Nome,
                            }
                        }))
                    }
                });
            },

            select: function (event, ui) {
                var item = ui.item;
                if (item) {

                    $(elemento).val(item.Nome);

                    $(settings.usuarioId).val(item.UsuarioId);

                    $(elemento).change();

                    if (typeof callbackFnk == 'function')
                        callbackFnk.call(this, item);
                }
            }

        });

        $(settings.usuarioId).ready(function () { $(settings.usuarioId).change(); });

        $(settings.usuarioId).on("change",
            function (request, response) {
                $.ajax({
                    url: settings.urlId,
                    data: "{ 'usuarioId': '" + this.value + "' }",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        var item = data;

                        if (item != null) {
                            $(elemento).val(item.Nome).change();
                            $(settings.usuarioId).val(item.UsuarioId);

                            if (typeof callbackFnk == 'function')
                                callbackFnk.call(this, item);
                        }
                        return;
                    }
                });
            });
    }
});
