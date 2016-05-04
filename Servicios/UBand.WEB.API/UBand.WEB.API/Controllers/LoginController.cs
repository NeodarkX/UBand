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
using UBand.WEB.API.Helpers;

namespace UBand.WEB.API.Controllers
{
    public class LoginController : BaseApiController
    {
        private string EncodePwd(string salt, string pwd)
        {
            byte[] bytePassword = Encoding.UTF8.GetBytes(salt + pwd);
            byte[] byteHashedPassword;
            using (MD5 md5 = MD5CryptoServiceProvider.Create())
            {
                byteHashedPassword = md5.ComputeHash(bytePassword);
            }

            return BitConverter.ToString(byteHashedPassword).Replace("-", "");
        }

        [HttpGet]
        public HttpResponseMessage Get(String u, String p)
        {
            try
            {
                var objUsuario = context.Usuario.FirstOrDefault(x => x.Codigo == u && x.Estado == ConstantHelpers.ESTADO_ACTIVO);

                string hashPassword = null;

                if (objUsuario != null)
                {
                    hashPassword = EncodePwd(objUsuario.Salt, p);
                }

                if (objUsuario == null || String.IsNullOrEmpty(objUsuario.Password) || objUsuario.Password.ToUpper() != hashPassword)
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict, new { results = "nulo" });
                }
                else
                {
                    Token t = objUsuario.Token;
                    if(objUsuario.TokenId == null)
                    {
                        t = new Token();
                        t.Codigo = Guid.NewGuid().ToString().Replace("-", "");
                        t.FechaCreacion = DateTime.Now;
                        t.FechaExpiracion = DateTime.Now.AddHours(2);
                        context.Token.Add(t);
                        context.SaveChanges();
                        objUsuario.TokenId = context.Token.FirstOrDefault(x => x.Codigo == t.Codigo).TokenId;
                        context.SaveChanges();
                    }
                    else
                    {
                        t = context.Token.Find(objUsuario.TokenId);
                        t.Codigo = Guid.NewGuid().ToString().Replace("-", "");
                        t.FechaCreacion = DateTime.Now;
                        t.FechaExpiracion = DateTime.Now.AddHours(2);
                        context.SaveChanges();
                    }
                    var tt = new tok();
                    tt.Code = t.Codigo;
                    tt.fechaC = t.FechaCreacion;
                    tt.FechaE = t.FechaExpiracion;
                    return Request.CreateResponse(HttpStatusCode.Accepted, new { results = tt });
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, new { results = "nulo" });
            }
        }
    }
}