jQuery.fn.extend({
    usuarioPerfil: function (options, callbackFnk) {

        var settings = $.extend({
            // These are the defaults.
            urlDescricao: $("#UsuarioPerfilDescricaoUrl").val(),
            urlId: $("#UsuarioPerfilIdUrl").val(),
            usuarioPerfilId: null,
            select: null,
            situacao: 0
        }, options);
        
        if (settings.urlDescricao == null)
            alert("O parâmetro settings.urlDescricao não foi encontrado");

        if (settings.urlId == null)
            alert("O parâmetro settings.urlId não foi encontrado");

        var elemento = this;

        this.autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: settings.urlDescricao,
                    data: "{ 'descricao': '" + request.term + "' }",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        response($.map(data, function (item) {
                            return {
                                value: item.Descricao,

                                UsuarioPerfilId: item.UsuarioPerfilId,
                                Descricao: item.Descricao,
                            }
                        }))
                    }
                });
            },

            select: function (event, ui) {
                var item = ui.item;
                if (item) {

                    $(elemento).val(item.Descricao);

                    $(settings.usuarioPerfilId).val(item.UsuarioPerfilId);

                    $(elemento).change();

                    if (typeof callbackFnk == 'function')
                        callbackFnk.call(this, item);
                }
            }

        });

        $(settings.usuarioPerfilId).ready(function () { $(settings.usuarioPerfilId).change(); });

        $(settings.usuarioPerfilId).on("change",
            function (request, response) {
                $.ajax({
                    url: settings.urlId,
                    data: "{ 'usuarioPerfilId': '" + this.value + "' }",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        var item = data;

                        if (item != null) {
                            $(elemento).val(item.Descricao).change();
                            $(settings.usuarioPerfilId).val(item.UsuarioPerfilId);

                            if (typeof callbackFnk == 'function')
                                callbackFnk.call(this, item);
                        }
                        return;
                    }
                });
            });
    }
});


