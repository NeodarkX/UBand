$(function () {
    $("#CodigoMedicoBox").autocomplete({
        source: function (request, response) {
            var param = { Codigo: $('#CodigoMedicoBox').val() };
            $.ajax({
                url: "../Box/ObtenerCodigoMedicos",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },

                success: function (data) {
                    response(data.cods);
                },

                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        minLength: 2
    });
});

$(document).ready(function () {
    $('#NroRecetario').change(function () {
        var numero = $('#NroRecetario').val();
        if(numero != ""){
        var param = { NroRecetario: $('#NroRecetario').val() };
            $.ajax({
                url: "../Box/ValidarNroRecetario",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },

                success: function (data) {
                    $("#Supervisor").val(data.dato);
                    if (data.dato == "El nro de recetario no pertenece a ningun supervisor") {
                        $('#estadoSupervisor').attr('class', 'has-error has-feedback');
                    } else {
                        $('#estadoSupervisor').attr('class', '');
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
    });
});

$(function () {
    $("#CodigoPacienteBox").autocomplete({
        source: function (request, response) {  
            var param = { Codigo: $('#CodigoPacienteBox').val() };
            $.ajax({
                url: "../Box/ObtenerCodigoPacientes",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },

                success: function (data) {
                    if (data.cods.length == 0) {
                        $("#HistoriaClinica").val('');
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
            var paciente = ui.item.Id;
            $("#HistoriaClinica").val('');
            $('#HistoriaClinica').val('Nro. Historia Clinica: ' + ui.item.HistoriaClinica);
            $("#CodigoPacienteBox").val(ui.item.Nombre);
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
    $("#CodigoServicioBox").autocomplete({
        source: function (request, response) {
            var param = { Codigo: $('#CodigoServicioBox').val() };
            $.ajax({
                url: "../Box/ObtenerCodigoServicios",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },

                success: function (data) {
                    response(data.cods);
                },

                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        minLength: 2
    });
});

$(document).ready(function () {
    $('#bttnAccion1').click(function () {
        $('#Accion').val(1);
    });
});

$(document).ready(function () {
    $('#bttnAccion2').click(function () {
        $('#Accion').val(2);
    });
});

$(document).ready(function () {
    $('#bttnAccion3').click(function () {
        $('#Accion').val(3);
    });
});


var ListMaterial = [];

$('#btnAnadirMaterialBox').click(function() {
    var materialId = $('#CodigoMaterialAlmacenBox').val();
    var cantidad = $('#Cantidad').val();
    var UnidadId = $('#CodigoUnidad').val();

    var GestionarMaterialesSupervisorViewModel = {
        CodigoMaterial: materialId,
        CodigoUnidad: UnidadId,
        Cantidad: cantidad,
    };

    var Pos = jQuery.inArray(materialId, ListMaterial);
    if (Pos == -1) {

        if (cantidad.length > 0 && Number(cantidad) > 0) {
            $.ajax({

                type: "POST",
                url: "../Box/AnadirMaterial",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(GestionarMaterialesSupervisorViewModel),
                success: function (resultado) {

                    if (resultado.msg) {

                        $('#tablaSolicitud tr:last').after(' <tr> ' +
                                                           ' <td>' + '<input type="hidden" name="codigo[]" value="' + resultado.Id + '"/>' + resultado.codigo + '</td> ' +
                                                           ' <td>' + '<input type="hidden" name="nombre[]" value="' + resultado.nombre + '"/>' + resultado.nombre + '</td> ' +
                                                           ' <td>' + '<input type="hidden" name="cantidad[]" value="' + parseInt(cantidad) + '"/>' + parseInt(cantidad) + '</td> ' +
                                                           ' <td class="text-center">' + '<input type="hidden" name="unidadid[]" value="' + resultado.unidadId + '"/>' + resultado.unidad + ' </th> ' +
                                                           ' <td class="text-center"> <a id="' + materialId + '" href="#" class="btn btn-danger btnDelete"><i class="icon-cross3 position-left"></i>Elminar</a> </td> ' +
                                                           ' </tr> ');
                        ListMaterial.push(materialId);
                        $("#CodigoMaterialAlmacenBox").val('');
                        $("#Cantidad").val('1');
                        $("#CodigoUnidad").val('');
                        var $example = $("#CodigoUnidad").select2();
                        $example.empty();
                    } else {
                        swal({
                            title: "Error...!",
                            text: "Cantidad Incorrecta - Disponible : " + resultado.cantidad,
                            confirmButtonColor: "#EF5350",
                            type: "warning"
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
            text: "Material Existente!",
            confirmButtonColor: "#EF5350",
            type: "warning"
        });
    }
});


$('#btnAnadirMaterialRay').click(function() {
    var materialId = $('#CodigoMaterialAlmacenRay').val();
    var cantidad = $('#Cantidad').val();
    var UnidadId = $('#CodigoUnidad').val();

    var GestionarMaterialesSupervisorViewModel = {
        CodigoMaterial: materialId,
        CodigoUnidad: UnidadId,
        Cantidad: cantidad,
    };

    var Pos = jQuery.inArray(materialId, ListMaterial);
    if (Pos == -1) {

        if (cantidad.length > 0 && Number(cantidad) > 0) {
            $.ajax({

                type: "POST",
                url: "../Ray/AnadirMaterial",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(GestionarMaterialesSupervisorViewModel),
                success: function (resultado) {

                    if (resultado.msg) {

                        $('#tablaSolicitud tr:last').after(' <tr> ' +
                                                           ' <td>' + '<input type="hidden" name="codigo[]" value="' + resultado.Id + '"/>' + resultado.codigo + '</td> ' +
                                                           ' <td>' + '<input type="hidden" name="nombre[]" value="' + resultado.nombre + '"/>' + resultado.nombre + '</td> ' +
                                                           ' <td>' + '<input type="hidden" name="cantidad[]" value="' + parseInt(cantidad) + '"/>' + parseInt(cantidad) + '</td> ' +
                                                           ' <td class="text-center">' + '<input type="hidden" name="unidadid[]" value="' + resultado.unidadId + '"/>' + resultado.unidad + ' </th> ' +
                                                           ' <td class="text-center"> <a id="' + materialId + '" href="#" class="btn btn-danger btnDelete"><i class="icon-cross3 position-left"></i>Elminar</a> </td> ' +
                                                           ' </tr> ');
                        ListMaterial.push(materialId);
                        $("#CodigoMaterialAlmacenRay").val('');
                        $("#Cantidad").val('1');
                        $("#CodigoUnidad").val('');
                        var $example = $("#CodigoUnidad").select2();
                        $example.empty();
                    } else {
                        swal({
                            title: "Error...!",
                            text: "Cantidad Incorrecta - Disponible : " + resultado.cantidad,
                            confirmButtonColor: "#EF5350",
                            type: "warning"
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
            text: "Material Existente!",
            confirmButtonColor: "#EF5350",
            type: "warning"
        });
    }
});

$(document).ready(function () {

    $('#tablaSolicitud').on("click", ".btnDelete", function (e) {
        var idMat = $(this).attr("id");
        console.log(idMat);
        $($(this).closest('tr')).fadeTo(400, 0, function () {
            $(this).remove();
            ListMaterial.splice(jQuery.inArray(idMat, ListMaterial), 1);
        });
    })

});


$(function () {

    $("#CodigoMaterialAlmacenRay").autocomplete({
        source: function (request, response) {

            var param = { Codigo: $('#CodigoMaterialAlmacenRay').val() };
            $.ajax({
                url: "../Ray/ObtenerCodigoMaterial",
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
            $("#CodigoMaterialAlmacenRay").val(ui.item.Nombre);
            $("#CodigoUnidad").val('');
            var $example = $("#CodigoUnidad").select2();
            var valor = ui.item.Id; // El valor de este select/el primero. (Elemento)
            $.ajax({
                type: "POST", // o "GET"
                url: "../Ray/ObtenerUnidadAlmacen",
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

    $("#CodigoMaterialAlmacenBox").autocomplete({
        source: function (request, response) {

            var param = { Codigo: $('#CodigoMaterialAlmacenBox').val() };
            $.ajax({
                url: "../Box/ObtenerCodigoMaterial",
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
            $("#CodigoMaterialAlmacenBox").val(ui.item.Nombre);
            $("#CodigoUnidad").val('');
            var $example = $("#CodigoUnidad").select2();
            var valor = ui.item.Id; // El valor de este select/el primero. (Elemento)
            $.ajax({
                type: "POST", // o "GET"
                url: "../Box/ObtenerUnidadAlmacen",
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


var MontoTotal = 0;

$('#btnAnadirServicioBox').click(function () {
    var servicioId = $('#CodigoServicioBox').val();
    var cantidad = $('#Cantidad').val();    

    var RegistrarPagoViewModel = {
        ServicioId: servicioId,
        Cantidad: cantidad,
    };

    var Pos = jQuery.inArray(servicioId, ListMaterial);
    if (Pos == -1) {
        if (cantidad.length > 0 && Number(cantidad) > 0 && servicioId.length) {
            $.ajax({

                type: "POST",
                url: "../Box/AnadirServicio",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(RegistrarPagoViewModel),
                success: function (resultado) {

                    if (resultado.msg) {

                        $('#tablaSolicitud tbody tr:last').after(' <tr> ' +
                                                           ' <td>' + '<input type="hidden" name="codigo[]" value="' + resultado.Id + '"/>' + resultado.codigo + '</td> ' +
                                                           ' <td>' + '<input type="hidden" name="nombre[]" value="' + resultado.nombre + '"/>' + resultado.nombre + '</td> ' +
                                                           ' <td>' + '<input type="hidden" name="cantidad[]" value="' + parseInt(cantidad) + '"/>' + parseInt(cantidad) + '</td> ' +
                                                           ' <td>' + '<input type="hidden" name="precio[]" value="' + resultado.precio + '"/>S/ ' + resultado.precio + '</td> ' +
                                                           ' <td>' + '<input type="hidden" name="subtotal[]" value="' + resultado.subtotal + '"/>S/ ' + resultado.subtotal + '</td> ' +
                                                           ' <td class="text-center"> <a id="' + servicioId + '" name="' + resultado.subtotal + '" href="#" class="btn btn-danger btnDelete2"><i class="icon-cross3 position-left"></i>Elminar</a> </td> ' +
                                                           ' </tr> ');
                        ListMaterial.push(servicioId);
                        MontoTotal += resultado.subtotal;
                        $('#MontoTotal').text(MontoTotal);
                        $('#CodigoServicioBox').val('');
                        $('#Cantidad').val(1);
                    } else {
                        swal({
                            title: "Error...!",
                            text: "Cantidad Incorrecta - Disponible : " + resultado.cantidad,
                            confirmButtonColor: "#EF5350",
                            type: "warning"
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

    $('#tablaSolicitud').on("click", ".btnDelete2", function (e) {
        var idMat = $(this).attr("id");
        var sub = $(this).attr("name");
        console.log(idMat);
        $($(this).closest('tr')).fadeTo(400, 0, function () {
            $(this).remove();
            ListMaterial.splice(jQuery.inArray(idMat, ListMaterial), 1);
            MontoTotal -= sub;
            $('#MontoTotal').text(MontoTotal);
        });
    })

});



$('#btnRegistrarBoleta').click(function () {

    var param = { numSerie: $('#NroReciboSerie').val(), numSecuencial: $('#NroReciboSecuencial').val(), numRecetario: $('#NroRecetario').val(), paciente: $('#CodigoPacienteBox').val(), medico: $('#CodigoMedicoBox').val() };

            $.ajax({

                type: "POST",
                url: "../Box/validarRegistroPago",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(param),
                success: function (resultado) {

                    if (resultado.sub == 1) {
                        $("#formRegis").submit()
                    } else if (resultado.sub == 2) {
                        swal({
                            title: "Cuidado",
                            text: "Hay "+ resultado.cantBoletas +" boletas entre el ultimo numero secuencial registrado y el ingresado que no fueron utilizadas. \n ¿Desea anular esas boletas para continuar con la operacion?",
                            type: "warning",
                            showCancelButton: true,
                            confirmButtonColor: "#2196F3",
                            cancelButtonColor: '#BDBDBD',
                            confirmButtonText: "Si",
                            cancelButtonText: "No",
                            closeOnConfirm: false,
                            closeOnCancel: true

                        },
                        function (isConfirm) {
                            if (isConfirm) {
                                $("#formRegis").submit();
                            }
                        });
                    } else {
                        swal({
                            title: "Error...",
                            text: resultado.msg,
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

});

$('#bttnCerrarCaja').click(function () {
    swal({
        title: "Atencion!",
        text: "¿Esta seguro que desea cerrar caja? \nNo podra registrar boletas hasta el proximo turno.",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#2196F3",
        cancelButtonColor: '#BDBDBD',
        confirmButtonText: "Si",
        cancelButtonText: "No",
        closeOnConfirm: false,
        closeOnCancel: true

    },
                function (isConfirm) {
                    if (isConfirm) {
                        $("#formCerrarCaja").submit();
                    }
                });
});

$('#bttnCerrarCajaAnterior').click(function () {
    swal({
        title: "Atencion!",
        text: "¿Esta seguro que desea continuar?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#2196F3",
        cancelButtonColor: '#BDBDBD',
        confirmButtonText: "Si",
        cancelButtonText: "No",
        closeOnConfirm: false,
        closeOnCancel: true

    },
                function (isConfirm) {
                    if (isConfirm) {
                        $("#formCerrarCaja").submit();
                    }
                });
});

$('#bttnAnularBoleta').click(function () {
    swal({
        title: "Atencion!",
        text: "¿Esta seguro que desea anular esta boleta?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#2196F3",
        cancelButtonColor: '#BDBDBD',
        confirmButtonText: "Si",
        cancelButtonText: "No",
        closeOnConfirm: false,
        closeOnCancel: true

    },
                function (isConfirm) {
                    if (isConfirm) {
                        $("#formAnularBoleta").submit();
                    }
                });
});

$('#bttnAnularBoletas').click(function () {
    swal({
        title: "Atencion!",
        text: "¿Esta seguro que desea anular esta boleta?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#2196F3",
        cancelButtonColor: '#BDBDBD',
        confirmButtonText: "Si",
        cancelButtonText: "No",
        closeOnConfirm: false,
        closeOnCancel: true

    },
                function (isConfirm) {
                    if (isConfirm) {
                        $("#formAnularBoletas").submit();
                    }
                });
});

var maxLongitudObservaciones = 450;

$('#textAreaObservacion').keyup(function () {
    var length = $(this).val().length;
    var length = maxLongitudObservaciones - length;
    $('#lengtArea').text(length);
})