using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using FACEBAND.WEB.API.Models;

namespace FACEBAND.WEB.API.Controllers
{
    public class BaseApiController : ApiController
    {
        public FacebandDBEntities context;
        private CargarDatosContext cargarDatosContext;

        public BaseApiController()
        {
            context = new FacebandDBEntities();
        }

        public void InvalidarContext()
        {
            context = new FacebandDBEntities();
        }

        public CargarDatosContext CargarDatosContext()
        {
            if (cargarDatosContext == null)
            {
                cargarDatosContext = new CargarDatosContext { context = context };
            }

            return cargarDatosContext;
        }

        public string Get(int id)
        {
            return "value";
        }
    }
    public class CargarDatosContext
    {
        public FacebandDBEntities context { get; set; }
        public HttpSessionStateBase session { get; set; }
    }
}
