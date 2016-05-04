
$(function() {

    var LstImage = [];

    function GetImagenes() {
        $.ajax({
            url: "../Home/GetImagenPerfil",
            data: JSON.stringify(),
            dataType: "json",
            type: "POST",
            async: false,
            contentType: "application/json; charset=utf-8",
            dataFilter: function (data) { return data; },

            success: function (data) {
                LstImage = [];
                LstImage.push("<img src='.." + data.lstimagenes + "' class='file-preview-image' alt=''>");
            },

            error: function (XMLHttpRequest, textStatus, errorThrown) {
                swal({
                    title: "Error...",
                    text: "Error de Conexion!",
                    confirmButtonColor: "#EF5350",
                    type: "error"
                });
            }
        });
        return LstImage;
    }


    $('.file-input-imgperfil').fileinput({

        browseLabel: '',
        browseClass: 'btn btn-primary btn-icon',
        removeLabel: '',
        uploadLabel: '',
        uploadClass: 'btn btn-default btn-icon',
        browseIcon: '<i class="icon-plus22"></i> ',
        uploadIcon: '<i class="icon-file-upload"></i> ',
        removeClass: 'btn btn-danger btn-icon',
        removeIcon: '<i class="icon-cancel-square"></i> ',
        layoutTemplates: {
            caption: '<div tabindex="-1" class="form-control file-caption {class}">\n' + '<span class="icon-file-plus kv-caption-icon"></span><div class="file-caption-name"></div>\n' + '</div>'
        },
        initialCaption: "Ninguna imagen seleccionada",
        allowedFileExtensions: ["jpg", "png"],
        initialPreview: GetImagenes(),
        overwriteInitial: true

    });



});