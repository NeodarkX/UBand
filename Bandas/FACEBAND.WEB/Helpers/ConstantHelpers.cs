using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FACEBAND.WEB.Controllers;

namespace FACEBAND.WEB.Helpers
{
    public static class ConstantHelpers
    {
        public static readonly Int32 DEFAULT_PAGE_SIZE = 18;
        public static readonly Int32 DEFAULT_MESSAGE_BAND_PAGE_SIZE = 3;
        public static readonly Int32 DEFAULT_AUTOCOMPLETE_SIZE = 5;
        public static readonly Decimal IGV = 18;

        public const String ROL_ADMINISTRADOR = "ADM";
        public const String ROL_USUARIO = "USU";

        public const String ROL_BATERISTA = "BAT";
        public const String ROL_GUITARRISTA = "GUI";
        public const String ROL_CANTANTE = "CNT";

        public const String ESTADO_ACTIVO = "ACT";
        public const String ESTADO_INACTIVO = "INA";


        public const String ESTADO_INICIAR_ATENCION = "IAT";
        public const String ESTADO_TERMINAR_ATENCION = "TAT";

        public const Boolean ESTADO_ACTUAL = true;
        public const Boolean ESTADO_DESACTUAL = false;

        public const String ESTADO_SERVICIO_APROBADO = "SER_APR";
        public const String ESTADO_SERVICIO_PENDIENTE = "SER_PEN";
        public const String ESTADO_SERVICIO_RECHAZADO = "SER_REC";
        public const String ESTADO_SERVICIO_FINALIZADO = "SER_FIN";

        public const String TURNO_MANANA = "Mañana";
        public const String TURNO_TARDE = "Tarde";

        public const String ESTADO_BOLETA_ANULADO = "ANU";
        public const String ESTADO_BOLETA_PAGADO = "PAG";

        public const String AREA_ALMACEN = "Almacen";
        public const String AREA_PROVEEDURIA = "Proveeduria";        

        public const String ESTADO_SOLICITUD_NUEVA_SOLICITUD = "SOL_NUE";
        public const String ESTADO_SOLICITUD_PARCIALEMENTE_APROBADA = "SOL_PAR";
        public const String ESTADO_SOLICITUD_APROBADO = "SOL_APR";
        public const String ESTADO_SOLICITUD_FINALIZADO = "SOL_FIN";

        public const String SOLICITUD_IMPRESION_CERTIFICADO_MEDICO = "IMP-CME";
        public const String SOLICITUD_IMPRESION_RECORD_ACADEMICO = "IMP-RAC";
        public const String SOLICITUD_RESERVA_ASIENTO_ODONTOLOGICO = "RES-AOD";

        public const String IMAGEN_ODONTOGRAMA = "IMODO";
        public const String IMAGEN_FOTOGRAFICAS = "IMFOT";
        public const String IMAGEN_RADIOGRAFICAS = "IMRAD";
        public const String IMAGEN_TOMOGRAFICAS = "IMTOM";
            
            
        public const String MOVIMIENTO_INGRESO = "ING";
        public const String MOVIMIENTO_EGRESO = "EGR";

        public const String SEXO_MASCULINO = "M";
        public const String SEXO_FEMENINO = "F";
        public const String SEXO_NEUTRO = "N"; 
        public const Int32 HistoriaClinica = 10000;

        public static readonly TimeSpan Hinicio = new TimeSpan(12, 0, 0);
        public static readonly TimeSpan HFin = new TimeSpan(20, 0, 0);
        public static readonly TimeSpan ReservaIntervalo = new TimeSpan(0, 30, 0);

         
        public const String TIPO_DOCUMENTO_DNI = "DNI";
        public const String TIPO_DOCUMENTO_CE = "CE";
        public const String TIPO_DOCUMENTO_PASAPORTE = "PAS";

        public const String TIPO_SEXO_MASCULINO = "M";
        public const String TIPO_SEXO_FEMENINO = "F";

        public const String PATH_IMG_PERFIL = "~/Content/images/bands";
        public const String PATH_IMG_PERFIL_db = "~/Content/images/bands";

        public static class Layout
        {
            public static readonly String LAYOUT_PATH = "~/Views/Shared/_Layout.cshtml";
            public static readonly String LOGINLAYOUT_PATH = "~/Views/Shared/_LoginLayout.cshtml";
            public static readonly String MODAL_LAYOUT_PATH = "~/Views/Shared/_ModalLayout.cshtml";
            public static readonly String MODAL_LAYOUT_PATH_SOLICITUD = "~/Views/Shared/_SolicitudModalLayout.cshtml";
            public static readonly String MODAL_GENERIC_LAYOUT_PATH = "~/Views/Shared/_GenericModalLayout.cshtml";
            public static readonly String MODAL_EMAIL_PATH = "~/Views/Shared/_MailLayout.cshtml";
        }

        public static PagedListRenderOptions Bootstrap3Pager
        {
            get
            {
                return new PagedListRenderOptions
                {
                    DisplayLinkToFirstPage = PagedListDisplayMode.IfNeeded,
                    DisplayLinkToLastPage = PagedListDisplayMode.IfNeeded,
                    DisplayLinkToPreviousPage = PagedListDisplayMode.IfNeeded,
                    DisplayLinkToNextPage = PagedListDisplayMode.IfNeeded,
                    DisplayLinkToIndividualPages = true,
                    DisplayPageCountAndCurrentLocation = false,
                    MaximumPageNumbersToDisplay = 10,
                    DisplayEllipsesWhenNotShowingAllPageNumbers = true,
                    EllipsesFormat = "&#8230;",
                    LinkToFirstPageFormat = "««",
                    LinkToPreviousPageFormat = "«",
                    LinkToIndividualPageFormat = "{0}",
                    LinkToNextPageFormat = "»",
                    LinkToLastPageFormat = "»»",
                    PageCountAndCurrentLocationFormat = "Page {0} of {1}.",
                    ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.",
                    FunctionToDisplayEachPageNumber = null,
                    ClassToApplyToFirstListItemInPager = null,
                    ClassToApplyToLastListItemInPager = null,
                    ContainerDivClasses = new[] { "pagination-container" },
                    UlElementClasses = new[] { "pagination" },
                    LiElementClasses = Enumerable.Empty<string>(),
                };
            }
        }

        public static String TextoAgregarEditar(Int32? id, String entidad)
        {
            if (id.HasValue)
                return "Editar " + entidad;
            return "Agregar " + entidad;
        }

        public static String ColorEstadoTarea(String estado)
        {
            switch (estado)
            {
                case ESTADO_SERVICIO_FINALIZADO: return "#48B558";
                case ESTADO_SERVICIO_APROBADO: return "#48B558";
                case ESTADO_SERVICIO_PENDIENTE: return "#48B558";
                case ESTADO_SERVICIO_RECHAZADO: return "#48B558";
            }
            return "#000";
        }

        public static String ObtenerDiaDeLaSemana(Int32 dia)
        {
            switch (dia)
            {
                case 1: return "Lunes";
                case 2: return "Martes";
                case 3: return "Miercoles";
                case 4: return "Jueves";
                case 5: return "Viernes";
                case 6: return "Sabado";
                case 7: return "Domingo";
            }
            return "";
        }

      
        

        public static String TextoRol(String rol)
        {
            switch (rol)
            {
                case ROL_ADMINISTRADOR: return "Administrador";
                case ROL_USUARIO: return "Usuario";
                case ROL_BATERISTA: return "Baterista";
                case ROL_GUITARRISTA: return "Guitarrista";
                case ROL_CANTANTE: return "Cantante";
            }
            return "Rol incorrecto";
        }


        public static String TextoSexo(String sexo)
        {
            switch(sexo)
            {
                case SEXO_MASCULINO: return "Masculino";
                case SEXO_FEMENINO: return "Femenino";
            }
            return "Sexo incorrecto";
        }

        public static String TextoSexoAcronimo(String sexo)
        {
            switch (sexo)
            {
                case "Masculino": return SEXO_MASCULINO;
                case "Femenino": return SEXO_FEMENINO;
            }
            return "Sexo incorrecto";
        }


        public static String TituloEstado(String estado)
        {
            switch (estado)
            {

                case ESTADO_SERVICIO_FINALIZADO: return "La solicitud ha finalizado exitosamente";
                case ESTADO_SERVICIO_APROBADO: return "La solicitud ha sido aprobada exitosamente";
                case ESTADO_SERVICIO_PENDIENTE: return "La solicitud está pendiente";
                case ESTADO_SERVICIO_RECHAZADO: return "La solicitud se ha rechazado";

            }

            return "Estado incorrecto";
        }

        public static String ObtenerRutaBaseSenaletica(String RutaBase, String Ruta)
        {
            return "~/Content/resources/" + RutaBase + "/" + Ruta;
        }

        public static List<String> ListEstadoSolicitud()
        {
            List<String> LstEstadoFactuacion = new List<String>();

            LstEstadoFactuacion.Add(ESTADO_SOLICITUD_NUEVA_SOLICITUD);
            LstEstadoFactuacion.Add(ESTADO_SOLICITUD_PARCIALEMENTE_APROBADA);
            LstEstadoFactuacion.Add(ESTADO_SOLICITUD_APROBADO);
            LstEstadoFactuacion.Add(ESTADO_SOLICITUD_FINALIZADO);
            return LstEstadoFactuacion;
        }

        public static List<String> ListSexos()
        {
            List<String> ListSexos = new List<String>();

            ListSexos.Add(TextoSexo(SEXO_MASCULINO));
            ListSexos.Add(TextoSexo(SEXO_FEMENINO));
            return ListSexos;
        }

    }
}