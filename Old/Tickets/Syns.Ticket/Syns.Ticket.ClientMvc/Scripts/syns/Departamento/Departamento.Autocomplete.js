jQuery.fn.extend({
    departamento: function (options, callbackFnk) {

		var settings = $.extend({
			// These are the defaults.
			urlDescricao: $("#DepartamentoDescricaoUrl").val(),
			urlId: $("#DepartamentoIdUrl").val(),
			departamentoId: null,
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
					dataFilter: function (data) {					    
					    return data;
					},
					success: function (data) {
						response($.map(data, function (item) {
							return {
								value: item.Descricao,

								DepartamentoId: item.DepartamentoId,
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

					$(settings.departamentoId).val(item.DepartamentoId);

					$(elemento).change();

					if (typeof callbackFnk == 'function')
						callbackFnk.call(this, item);
				}
			}

		});

		$(settings.departamentoId).ready(function () { $(settings.departamentoId).change(); });

		$(settings.departamentoId).on("change",
            function (request, response) {
            	$.ajax({
            		url: settings.urlId,
            		data: "{ 'departamentoId': '" + this.value + "' }",
            		dataType: "json",
            		type: "POST",
            		contentType: "application/json; charset=utf-8",
            		dataFilter: function (data) { return data; },
            		success: function (data) {
            			var item = data;

            			if (item != null) {
            				$(elemento).val(item.Descricao).change();
            				$(settings.departamentoId).val(item.DepartamentoId);

            				if (typeof callbackFnk == 'function')
            					callbackFnk.call(this, item);
            			}
            			return;
            		}
            	});
            });
	}
});
