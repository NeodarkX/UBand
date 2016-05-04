
function ValidaEdit() {
    var estado = $('#Estado').val();
    var edit = true;
    var date = new Date();
    var dia = date.getDate();
    var mes = date.getMonth() + 1;
    var year = date.getFullYear();
    var hoy = (dia < 10 ? '0' : '') + dia + '/' + (mes < 10 ? '0' : '') + mes + '/' + year;
    //pendiente la validacion de fecha
    var FI = $("#FechaInicio").val();
    if (estado == "ACT") {
        if (Date.parse(FI) > Date.parse(hoy)) {
            edit = true;
        } else {
            edit = true;
        }
    } else {
        edit = true;
    }
    return true;
}

$("#MaterialId").change(function (e) {
    var valor = $(this).val(); // El valor de este select/el primero. (Elemento)
    $("#MedidaId").empty(); // Limpiar las opciones
    var dd = document.getElementById("MedidaId");
    dd.options.length = 0;
    dd.options[0] = new Option("Espere...");
    dd.selectedIndex = 0;
    dd.disabled = true;
    $.ajax({
        type: "POST", // o "GET"
        url: "../AcademicPeriod/_SolicitarMedida",
        data: { MaterialId: valor },
        dataType: "json",
        success: function (response) {
            $("#MedidaId").empty(); // Limpiar las opciones

            $("#MedidaId").append($('<option></option>').attr("value", "").text("Selecciona la Medida"));
            $("#MedidaId").val($("#MedidaId option:first").val());
            $.each(response, function (idx, elm) {
                $("#MedidaId").append($('<option></option>').attr("value", elm.UnidadId).text(elm.Nombre));
            });
            dd.disabled = false;

        },
        error: function () {
            swal({
                title: "Error...!",
                text: "Problema con la conexión",
                confirmButtonColor: "#EF5350",
                type: "warning"
            });
            return false;
        }
    });

});

$('#NroSerie').keyup(function () {
    $('#Documento').val('');
    $('#PacienteId').val('');
});

$('#NroSecuencial').keyup(function () {
    $('#Documento').val('');
    $('#PacienteId').val('');
});

$('#NumeroRecetario').keyup(function () {
    $('#NombreSupervisor').val('');
});


$('#btnValidarRecetario').click(function () {
    var NumeroRecetario = $('#NumeroRecetario').val();
    if (NumeroRecetario.length) {
        var GestionarMaterialesSupervisorViewModel = {
            NumeroRecetario: NumeroRecetario,
        };

        $.ajax({
            type: "POST",
            url: "../Provider/ValidarRecetario",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(GestionarMaterialesSupervisorViewModel),
            success: function (resultado) {

                if (resultado.msg) {

                    $('#NombreSupervisor').val(resultado.nombrecompleto);
                } else {
                    swal({
                        title: "Error...",
                        text: "El recetario es inválido o ya fue usado !",
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
});

$('#btnValidar').click(function () {
    var NroSerie = $('#NroSerie').val();
    var NroSecuencial = $('#NroSecuencial').val();

    if (NroSerie.length && NroSecuencial.length) {
        var ReservaAsientoViewModel = {
            NroSerie: NroSerie,
            NroSecuencial: NroSecuencial,
        };

        $.ajax({
            type: "POST",
            url: "../Doctor/ValidarBoleta",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(ReservaAsientoViewModel),
            success: function (resultado) {

                if (resultado.msg) {

                    $('#Documento').val(resultado.Documento);
                    $('#PacienteId').val(resultado.nombrePaciente);

                } else {
                    swal({
                        title: "Error...",
                        text: "Boleta Inválidao o en uso!",
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
});

var ListMaterial = [];


$('#btnAnadirMaterial').click(function EnviarDatos2() {
    var materialId = $('#CodigoMaterial').val();
    var cantidad = $('#Cantidad').val();
    var UnidadId = $('#CodigoUnidad').val();

    var GestionarMaterialesSupervisorViewModel = {
        CodigoMaterial: materialId,
        CodigoUnidad: UnidadId,
        Cantidad: cantidad,
    };

    
    var Pos = jQuery.inArray(materialId, ListMaterial);
    if (Pos == -1) {
        if (cantidad.length > 0 && Number(cantidad) > 0 && materialId.length > 0 && UnidadId.length ) {
            $.ajax({

                type: "POST",
                url: "../Provider/AnadirMaterial",
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
                        $("#CodigoMaterial").val('');
                        $("#Cantidad").val('1');
                        $("#CodigoUnidad").val('');
                        var $example = $("#CodigoUnidad").select2();
                        $example.empty();
                    } else {
                        swal({
                            title: "Error...!",
                            text: "Cantidad Incorrecta - Disponible : "+resultado.cantidad,
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

$('#btnAnadirMaterialAlmacen').click(function EnviarDatos2() {
    var materialId = $('#CodigoMaterialAlmacen').val();
    var cantidad = $('#Cantidad').val();
    var UnidadId = $('#CodigoUnidad').val();

    var GestionarMaterialesSupervisorViewModel = {
        CodigoMaterial: materialId,
        CodigoUnidad: UnidadId,
        Cantidad: cantidad,
    };

    var Pos = jQuery.inArray(materialId, ListMaterial);
    if (Pos == -1) {

        if (cantidad.length > 0 && Number(cantidad) > 0 && materialId.length > 0 && UnidadId.length) {
            $.ajax({

                type: "POST",
                url: "../Provider/AnadirMaterialSolicitudMaterial",
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
                        $("#CodigoMaterialAlmacen").val('');
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

$(document).ready(function() {

    $('#tablaSolicitud').on("click", ".btnDelete", function (e) {
        var idMat = $(this).attr("id");
        $($(this).closest('tr')).fadeTo(400, 0, function () {
            $(this).remove();
            ListMaterial.splice(jQuery.inArray(idMat,ListMaterial), 1);
        });
    })

});