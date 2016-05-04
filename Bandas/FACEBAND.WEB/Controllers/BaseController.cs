using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FACEBAND.WEB.Helpers;
using FACEBAND.WEB.Models;

namespace FACEBAND.WEB.Controllers
{
    public class BaseController : Controller
    {
        public FaceBandEntities context;
        private CargarDatosContext cargarDatosContext;

        public BaseController()
        {
            context = new FaceBandEntities();
        }

        public void InvalidarContext()
        {
            context = new FaceBandEntities();
        }

        public CargarDatosContext CargarDatosContext()
        {
            if (cargarDatosContext == null)
            {
                cargarDatosContext = new CargarDatosContext { context = context, session = Session };
            }

            return cargarDatosContext;
        }

        public void PostMessage(FlashMessage Message)
        {
            if (TempData["FlashMessages"] == null)
                TempData["FlashMessages"] = new List<FlashMessage>();

            ((List<FlashMessage>)TempData["FlashMessages"]).Add(Message);
        }

        public void SweetMessage(MessageType Type)
        {
            switch (Type)
            {
                case MessageType.Success: TempData["SweetAlert"] = "<script>swal({title: 'Confirmado', text: 'Los datos se guardaron exitosamente!', confirmButtonColor: '#66BB6A',type: 'success'});</script>"; break;
                case MessageType.Error: TempData["SweetAlert"] = "<script>swal({title: 'Error!',text: 'Ha ocurrido un error al procesar la solicitud', confirmButtonColor: '#EF5350',type: 'error'});</script>"; ; break;
            }
        }

        public void SweetMessage(MessageType Type, string Title, string Body)
        {    
            switch (Type)
            {
                case MessageType.Success: TempData["SweetAlert"] = "<script>swal({title: '" + Title + "', text: '" + Body + "', confirmButtonColor: '#66BB6A',type: 'success'});</script>"; break;
                case MessageType.Error: TempData["SweetAlert"] = "<script>swal({title: '" + Title + " !',text: '" + Body + "', confirmButtonColor: '#EF5350',type: 'error'});</script>"; ; break;
                case MessageType.Info: TempData["SweetAlert"] = "<script>swal({title: '"+ Title + " !',text: '"+ Body + "', confirmButtonColor: '#2196F3',type: 'info'});</script>"; ; break;
                case MessageType.Warning: TempData["SweetAlert"] = "<script>swal({title: '" + Title + " !',text: '" + Body + "', confirmButtonColor: '#ff5722',type: 'warning'});</script>"; ; break;

            }
        }

        public void SweetMessage(MessageType Type, string Title, string Body, string Link)
        {
            switch (Type)
            {
                case MessageType.Success: TempData["SweetAlert"] = "<script>swal({title: '" + Title + "', text: '" + Body + "', confirmButtonColor: '#66BB6A',type: 'success'}, function () { window.location.href = '" + Link + "'});</script>"; break;
                case MessageType.Error: TempData["SweetAlert"] = "<script>swal({title: '" + Title + "',text: '" + Body + "', confirmButtonColor: '#EF5350',type: 'error'  }, function () { window.location.href = '" + Link + "'});</script>"; ; break;
            }
        }

        public void SweetMessage(MessageType Type,string Link)
        {    
            switch (Type)
            {
                case MessageType.Success: TempData["SweetAlert"] = "<script>swal({title: 'Confirmado', text: 'Los datos se guardaron exitosamente!', confirmButtonColor: '#66BB6A',type: 'success'}, function () { window.location.href = '"+ Link + "'});</script>"; break;
                case MessageType.Error: TempData["SweetAlert"] = "<script>swal({title: 'Error!',text: 'Ha ocurrido un error al procesar la solicitud', confirmButtonColor: '#EF5350',type: 'error'  }, function () { window.location.href = '" + Link + "'});</script>"; ; break;
            }
        }

        public void PostMessage(MessageType Type)
        {
            PostMessage(Type, null,null,null);
        }

        public void PostMessage(MessageType Type,Exception Ex)
        {
            String Body = "";

            switch (Type)
            {
                case MessageType.Error: Body = "Ha ocurrido un error al procesar la solicitud."; break;
                case MessageType.Info: Body = ""; break;
                case MessageType.Success: Body = "Los datos se guardaron exitosamente."; break;
                case MessageType.Warning: Body = "."; break;
            }
            PostMessage(Type, Body);
        }

        public void PostMessage(MessageType Type, String Title, String Body)
        {
            PostMessage(Type, Title,Body, null);
        }

        public void PostMessage(MessageType Type, String Body)
        {
            PostMessage(Type,null,Body,null);
        }

        public void PostMessage(MessageType Type, String Title, String Body, Exception ex)
        {
            //Guardar EX en algun lado...

            PostMessage(new FlashMessage { Title = Title, Body = Body, Type = Type });
        }

        public void PostMessage(MessageType Type, String Body, Exception ex)
        {
            String Title = "";

            switch (Type)
            {
                case MessageType.Error: Title = "¡Error!"; break;
                case MessageType.Info: Title = "Ojo."; break;
                case MessageType.Success: Title = "¡Éxito!"; break;
                case MessageType.Warning: Title = "¡Atención!"; break;
            }

            PostMessage(Type,Title,Body,ex);
        }

        public void LimpiarSession()
        {
            Session.Clear();
            RedirectToAction("Login", "Home");
        }

    }

    public class CargarDatosContext
    {
        public FaceBandEntities context { get; set; }
        public HttpSessionStateBase session { get; set; }
    }
}
