// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).on('change', '#itemSelect', function () {
    var item=$(this),
        id_item=parseInt(item.val()),
        subitem=$('#SubItensId');

    if(id_item > 0) {
        var url = url_listar_subitens,
            param = {idItem: id_item};

        subitem.empty();
        $.post(url, param, function (response)
        {
            if (response && response.length > 0) {
                for (var i = 0; i < response.length; i++) {
                    subitem.append('<option value=' + response[i].subItensId + '>' + response[i].subItem + '</option>');
                }
            }
        });
    }
});



$(document).ready(function () {
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
        $('#conteudo').toggleClass('conteudo-expandido'); // Adicione ou remova a classe 'conteudo-expandido'
    });
});



