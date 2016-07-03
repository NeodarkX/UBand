using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FACEBAND.WEB.API.Models;
using FACEBAND.WEB.API.Helpers;
using FACEBAND.WEB.API.Utils;
using FACEBAND.WEB.API.ViewModels.Authentication;
using FACEBAND.WEB.API.Clases;
using System.Web.Http.Cors;

namespace FACEBAND.WEB.API.Controllers
{
    public class AuthenticationController : BaseApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {

            return Ok(new List<int>() { 1, 2, 3 });
        }

        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "get,post")]
        public HttpResponseMessage Login([FromBody] LoginViewModel model)
        {
            try
            {
                var objUsuario = context.Usuario.FirstOrDefault(x => x.Codigo == model.user && x.Estado == ConstantHelpers.ESTADO_ACTIVO);

                string hashPassword = null;

                if (objUsuario != null)
                {
                    hashPassword = AuthenticationUtil.EncodePwd(objUsuario.Salt, model.pass);
                }

                if (objUsuario == null || String.IsNullOrEmpty(objUsuario.Password) || objUsuario.Password.ToUpper() != hashPassword)
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict, new { results = "" });
                }
                else
                {
                    UsuarioModel temp = new UsuarioModel();
                    temp.id = objUsuario.UsuarioId;
                    temp.codigo = objUsuario.Codigo;
                    temp.nombres = objUsuario.Nombres;
                    temp.apellidoPaterno = objUsuario.ApellidoPaterno;
                    temp.apellidoMaterno = objUsuario.ApellidoMaterno;
                    temp.foto = objUsuario.Foto;
                    return Request.CreateResponse(HttpStatusCode.Accepted, new { results = temp });
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, new { results = "nulo" });
            }
        }
    }
}
