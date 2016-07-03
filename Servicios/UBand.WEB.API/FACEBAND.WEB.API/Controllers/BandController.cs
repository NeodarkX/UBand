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
    public class BandController : BaseApiController
    {
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "get,post")]
        public HttpResponseMessage Get()
        {
            try
            {
                List<Banda> bands = context.Banda.Where(x => x.Estado == ConstantHelpers.ESTADO_ACTIVO).ToList();
                List<BandaModel> lista = new List<BandaModel>();
                foreach (var item in bands)
                {
                    BandaModel temp = new BandaModel();
                    temp.id = item.BandaId;
                    temp.nombre = item.Nombre;
                    temp.descripcion = item.Descripcion;
                    temp.foto = item.Foto;
                    temp.seguidores = item.Seguidores;
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
                Banda band = context.Banda.Find(id);
                BandaModel temp = new BandaModel();
                temp.id = band.BandaId;
                temp.nombre = band.Nombre;
                temp.descripcion = band.Descripcion;
                temp.foto = band.Foto;
                temp.seguidores = band.Seguidores.Value;

                return Request.CreateResponse(HttpStatusCode.Accepted, new { results = temp });
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, new { results = "nulo" });
            }
        }
    }
}
