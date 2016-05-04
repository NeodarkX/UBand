//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ PROVEEDURIA

$("#HoraI").change(function () {
    $('#CodigoArea').val('')
    $("#codigoAsiento").val('');
    var $example = $("#codigoAsiento").select2();
    $example.empty();
});

$("#HoraF").change(function () {
    $('#CodigoArea').val('')
    $("#codigoAsiento").val('');
    var $example = $("#codigoAsiento").select2();
    $example.empty();
});

$("#ProfesorId").change(function () {
    $("#Idprofesor").val('');
});

$('#btnEntregarMaterialSupervisor').click(function(){

        var param = {
            NumeroRecetario: $('#NumeroRecetario').val(),
        };
        if ($('#NombreSupervisor').val().length )
        {
            $.ajax({
                url: "../Provider/ValidarRecetarioSubmit",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },

                success: function (data) {
                    if (data.msg)
                        $('#frmEntregaSupervisor').submit();
                    else {
                        swal({
                            title: "Error...",
                            text: "Error de Conexion!",
                            confirmButtonColor: "#EF5350",
                            type: "error"
                        });
                    }
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
        }
        else
        {
            swal({
                title: "Por favor..",
                text: "Valide el Recetario !",
                confirmButtonColor: "#ff5722",
                type: "warning"
            });
        }
        
    
});

$(function () {

    $("#ProfesorId").autocomplete({
        source: function (request, response) {

            var param = { Codigo: $('#ProfesorId').val() };
            $.ajax({
                url: "../Provider/ObtenerCodigoSupervisor",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },

                success: function (data) {
                    //if (data.cods.length == 0) {
                    //    $("#ProfesorId").val('');
                    //}
                    response(data.cods)
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
        },
        select: function(event ,ui)
        {
            $("#ProfesorId").val(ui.item.Nombre);
            $("#Idprofesor").val(ui.item.Id);
            return false;
        },
        minLength: 2
    })
    .autocomplete("instance")._renderItem = function (ul, item) {
        return $("<li >")
        .append("<label>" + item.Nombre + "</label>")
        .appendTo(ul);
    };


});

$(function () {

    $("#CodigoArea").autocomplete({
        source: function (request, response) {

            var param = {
                Codigo: $('#CodigoArea').val(),
                
            };
            $.ajax({
                url: "../Doctor/ObtenerCodigoArea",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },

                success: function (data) {

                    if (data.cods.length == 0) {
                        $("#codigoAsiento").val('');
                        var $example = $("#codigoAsiento").select2();
                        $example.empty();
                        //console.log(data.cods.)
                    }
                    response(data.cods)
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
        },
        select: function (event, ui) {
            $("#codigoAsiento").empty();
            $("#CodigoArea").val(ui.item.Nombre);
            $("#codigoAsiento").val('');
            var $example = $("#codigoAsiento").select2();
            if ($('#HoraI').select2().val() < $('#HoraF').select2().val()) {
                var valor = ui.item.Id; // El valor de este select/el primero. (Elemento)
                $.ajax({
                    type: "POST", // o "GET"
                    url: "../Doctor/ObtenerAsiento",
                    data: {
                        Id: valor,
                        HoraI: $('#HoraI').select2().val(),
                        HoraF: $('#HoraF').select2().val(),
                    },
                    dataType: "json",
                    success: function (response) {
                        
                        $("#codigoAsiento").val($("#codigoAsiento option:first").val());
                        $.each(response.cods, function (idx, elm) {
                            $("#codigoAsiento").append($('<option></option>').attr("value", elm.Id).text(elm.Nombre));//incluyo las opciones en base al resultado obtenido
                        });
                        //console.log($('#codigoAsiento option:first-child').text());
                        $('#select2-chosen-40').text($('#codigoAsiento option:first-child').text());
                        
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
            }
            return false;
            
        },
        minLength: 0
    })
    .autocomplete("instance")._renderItem = function (ul, item) {
        return $("<li >")
        .append("<label>" + item.Nombre + "</label>")
        .appendTo(ul);
    };


});


$(function () {

    $("#CodigoMaterial").autocomplete({
        source: function (request, response) {
           
            var param = { Codigo: $('#CodigoMaterial').val() };
            $.ajax({
                url: "../Provider/ObtenerCodigoMaterial",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },

                success: function (data) {
                    
                    if (data.cods.length == 0)
                    {
                        $("#CodigoUnidad").val('');
                        var $example = $("#CodigoUnidad").select2();
                        $example.empty();
                        //console.log(data.cods.)
                    }
                    response(data.cods)
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
        },
        select: function(event ,ui)
        {
            $("#CodigoUnidad").empty();
            $("#CodigoMaterial").val(ui.item.Nombre);
            $("#CodigoUnidad").val('');
            var $example = $("#CodigoUnidad").select2();
            var valor = ui.item.Id; // El valor de este select/el primero. (Elemento)
            $.ajax({
                type: "POST", // o "GET"
                url: "../Provider/ObtenerUnidad",
                data: { Id: valor },
                dataType: "json",
                success: function (response) {
                    $.each(response.cods, function (idx, elm) {
                        $("#CodigoUnidad").append($('<option></option>').attr("value", elm.Id).text(elm.Nombre));//incluyo las opciones en base al resultado obtenido
                    });
                    $('.select2-chosen').text($('#CodigoUnidad option:first-child').text());
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

            return false;

        },
        minLength: 0
    })
    .autocomplete("instance")._renderItem = function (ul, item) {
        return $("<li >")
        .append("<label>" + item.Nombre + "</label>")
        .appendTo(ul);
    };
    

});

$(function () {

    $("#CodigoMaterialAlmacen").autocomplete({
        source: function (request, response) {

            var param = { Codigo: $('#CodigoMaterialAlmacen').val() };
            $.ajax({
                url: "../Provider/ObtenerCodigoMaterial",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },

                success: function (data) {

                    if (data.cods.length == 0) {
                        $("#CodigoUnidad").val('');
                        var $example = $("#CodigoUnidad").select2();
                        $example.empty();
                        //console.log(data.cods.)
                    }
                    response(data.cods)
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
        },
        select: function (event, ui) {
            $("#CodigoUnidad").empty();
            $("#CodigoMaterialAlmacen").val(ui.item.Nombre);
            $("#CodigoUnidad").val('');
            var $example = $("#CodigoUnidad").select2();
            var valor = ui.item.Id; // El valor de este select/el primero. (Elemento)
            $.ajax({
                type: "POST", // o "GET"
                url: "../Provider/ObtenerUnidadAlmacen",
                data: { Id: valor },
                dataType: "json",
                success: function (response) {
                    $.each(response.cods, function (idx, elm) {
                        $("#CodigoUnidad").append($('<option></option>').attr("value", elm.Id).text(elm.Nombre));//incluyo las opciones en base al resultado obtenido
                    });
                    $('.select2-chosen').text($('#CodigoUnidad option:first-child').text());
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

            return false;

        },
        minLength: 0
    })
    .autocomplete("instance")._renderItem = function (ul, item) {
        return $("<li >")
        .append("<label>" + item.Nombre + "</label>")
        .appendTo(ul);
    };


});

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~DOCTOR

$('#CodigoArea').change(function () {
    $('#ServicioId').empty();
    $.ajax({
        type: "POST",
        url: "../Patient/ObtenerServicio",
        data: { AreaId: $('#CodigoArea').val() },
        dataType: "json",
        success: function (response) {
            
            $.each(response.cods, function (idx, elm) {
                $("#ServicioId").append($('<option></option>').attr("value", elm.Id).text(elm.Nombre));
            });
            $('#select2-chosen-2').text($('#ServicioId option:first-child').text());
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

});

var ListMaterial = [];

var MontoTotal = 0;

$('#btnAddServicioDoctor').click(function () {
    var ServicioId = $('#ServicioId').val();
    var Cantidad = $('#Cantidad').val();
    
    var NuevaGestionViewModel = {
        ServicioId: ServicioId,
        Cantidad: Cantidad,
    };

    var Pos = jQuery.inArray(ServicioId, ListMaterial);
    if (Pos == -1) {
        if (Cantidad.length > 0 && Number(Cantidad) > 0 && ServicioId.length) {
            $.ajax({

                type: "POST",
                url: "../Patient/AddServicio",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(NuevaGestionViewModel),
                success: function (resultado) {

                    if (resultado.msg) {

                        $('#tablaPresupuesto tbody tr:last').after(' <tr> ' +
                                                           ' <td>' + '<input type="hidden" name="area[]" value="' + resultado.areaId + '"/>' + resultado.areaNombre + '</td> ' +
                                                           ' <td>' + '<input type="hidden" name="servicio[]" value="' + resultado.servicioId + '"/>' + resultado.servicioNombre + '</td> ' +
                                                           ' <td>' + '<input type="hidden" name="cantidad[]" value="' + parseInt(Cantidad) + '"/>' + parseInt(Cantidad) + '</td> ' +
                                                           ' <td>' + '<input type="hidden" name="precio[]" value="' + resultado.precio + '"/>S/ ' + resultado.precio + '</td> ' +
                                                           ' <td>' + '<input type="hidden" name="subtotal[]" value="' + resultado.subtotal + '"/>S/ ' + resultado.subtotal + '</td> ' +
                                                           ' <td> <a id="' + ServicioId + '" name="' + resultado.subtotal + '" href="#" class="btn btn-danger btnDelete2"><i class="icon-cross3 position-left"></i>Elminar</a> </td> ' +
                                                           ' </tr> ');
                        ListMaterial.push(ServicioId);
                        MontoTotal += resultado.subtotal;
                        $('#MontoTotal').text(MontoTotal);
                        $('#Cantidad').val(1);
                    } else {
                        swal({
                            title: "Error...",
                            text: "Error de Conexion!",
                            confirmButtonColor: "#EF5350",
                            type: "error"
                        });
                    }
                },
                error: function () {
                    swal({
                        title: "Error...",
                        text: "Error de Conexion!",
                        confirmButtonColor: "#EF5350",
                        type: "error"
                    });
                }
            });
        }
    }
    else {
        swal({
            title: "Cuidado!",
            text: "Servicio Existente!",
            confirmButtonColor: "#EF5350",
            type: "warning"
        });
    }
});

$(document).ready(function () {

    $('#tablaPresupuesto').on("click", ".btnDelete2", function (e) {
        var idMat = $(this).attr("id");
        var sub = $(this).attr("name");
        $($(this).closest('tr')).fadeTo(400, 0, function () {
            $(this).remove();
            ListMaterial.splice(jQuery.inArray(idMat, ListMaterial), 1);
            MontoTotal -= sub;
            $('#MontoTotal').text(MontoTotal);
        });
    })

});


//$('#GenerarPresupuestobtn').click(function () {
//    ServicioId = [];
//    Cantidad = [];
//    var servicioId = document.getElementsByName('servicio[]');
//    var cantidad = document.getElementsByName('cantidad[]');

//    $.each(document.getElementsByName('servicio[]'), function (idx, elm) {
//        ServicioId.push(document.getElementsByName('servicio[]')[idx].value);
//    });

//    $.each(document.getElementsByName('cantidad[]'), function (idx, elm) {
//        Cantidad.push(document.getElementsByName('cantidad[]')[idx].value);
//    });

//    $.ajax({
//        type: "POST",
//        url: "/Patient/GenerarPresupuesto",
//        data: { ServicioId: ServicioId, Cantidad: Cantidad },
//        traditional: true,
//        dataType: "json",
//        success: function (resultado) {
//            console.log(resultado);
//            console.log('hola');
//        }
        
//    });
//});


$('#GenerarPresupuestobtn').click(function () {
    $('#TypeSubmit').val('0');
    if (ListMaterial.length != 0) {
        $('#TypeSubmit').val('2');
    }
});


$('#SubmitFormbtn').click(function () {
    $('#TypeSubmit').val('1');
});


//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~  ALMACEN





//Autocompletar del Registro de Materiales para Almacen
$(function () {

    $("#CodigoRegistro").autocomplete({
        source: function (request, response) {

            var param = { Codigo: $('#CodigoRegistro').val() };
            $.ajax({
                url: "../Storage/ObtenerCodigoMaterial",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },

                success: function (data) {
                    if (data.cods.length == 0) {
                        $("#UnidadId").val('');
                        var $example = $("#UnidadId").select2();
                        $example.empty();
                        //console.log(data.cods.)
                    }
                    response(data.cods)
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
        },
        select: function (event, ui) {
            $("#UnidadId").empty();
            $("#MaterialId").val(ui.item.Id);
            var name = ui.item.Codigo + " - " + ui.item.Nombre;
            $("#CodigoRegistro").val(name);
            //$("#Descripcion").val(ui.item.Nombre);
            $("#UnidadId").val('');
            var $example = $("#UnidadId").select2();
            var valor = ui.item.Id; // El valor de este select/el primero. (Elemento)
            $.ajax({
                type: "POST", // o "GET"
                url: "../Provider/ObtenerUnidad",
                asyn: false,
                data: { Id: valor },
                dataType: "json",
                success: function (response) {
                    $.each(response.cods, function (idx, elm) {
                        $("#UnidadId").append($('<option></option>').attr("value", elm.Id).text(elm.Nombre));//incluyo las opciones en base al resultado obtenido
                    });
                    $('.select2-chosen').text($('#UnidadId option:first-child').text());
                    //$("#UnidadId option:first").attr('selected', 'selected');
                    //$("#UnidadId").val($("#UnidadId option:first").val());
                    
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

            return false;

        },
        minLength: 0
    })
    .autocomplete("instance")._renderItem = function (ul, item) {
        return $("<li >")
        .append("<label>" + item.Codigo + " - " + item.Nombre + "</label>")
        .appendTo(ul);
    };//fin del autocomplete


});//fin del function