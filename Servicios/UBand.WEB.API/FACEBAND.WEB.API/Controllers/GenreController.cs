using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FACEBAND.WEB.API.Models;
using FACEBAND.WEB.API.Helpers;
using FACEBAND.WEB.API.Utils;
using FACEBAND.WEB.API.Clases;
using System.Web.Http.Cors;

namespace FACEBAND.WEB.API.Controllers
{
    public class GenreController : BaseApiController
    {
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "get,post")]
        public HttpResponseMessage Get()
        {
            try
            {
                List<Genero> genres = context.Genero.Where(x => x.Estado == ConstantHelpers.ESTADO_ACTIVO).ToList();
                List<GeneroModel> lista = new List<GeneroModel>();
                foreach(var item in genres)
                {
                    GeneroModel temp = new GeneroModel();
                    temp.id = item.GeneroId;
                    temp.nombre = item.Nombre;
                    lista.Add(temp);
                }
                lista = lista.OrderBy(x => x.nombre).ToList();
                return Request.CreateResponse(HttpStatusCode.Accepted, new { results = lista });
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, new { results = "nulo" });
            }
        }

        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "get,post")]
        public HttpResponseMessage Detail(Int32 id)
        {
            try
            {
                Genero genre = context.Genero.Find(id);
                GeneroModel temp = new GeneroModel();
                temp.id = genre.GeneroId;
                temp.nombre = genre.Nombre;
                return Request.CreateResponse(HttpStatusCode.Accepted, new { results = temp });
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, new { results = "nulo" });
            }
        }
    }
}
