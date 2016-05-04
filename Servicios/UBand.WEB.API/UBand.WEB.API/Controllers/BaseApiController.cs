using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Xml;

using UBand.WEB.API.Models;

namespace UBand.WEB.API.Controllers
{
    public class BaseApiController : ApiController
    {
        public UBandEntities context;
        private CargarDatosContext cargarDatosContext;

        public BaseApiController()
        {
            context = new UBandEntities();
        }

        public void InvalidarContext()
        {
            context = new UBandEntities();
        }

        public CargarDatosContext CargarDatosContext()
        {
            if (cargarDatosContext == null)
            {
                cargarDatosContext = new CargarDatosContext { context = context };
            }

            return cargarDatosContext;
        }

        // GET: BaseApi
        public string Get(int id)
        {
            return "value";
        }
    }

    public class CargarDatosContext
    {
        public UBandEntities context { get; set; }
        public HttpSessionStateBase session { get; set; }
    }
}