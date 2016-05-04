using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

using FACEBAND.WEB.Helpers;
using FACEBAND.WEB.Models;
using FACEBAND.WEB.ViewModel.Home;
using FACEBAND.WEB.Filters;


namespace FACEBAND.WEB.Controllers
{
    public class HomeController : BaseController
    {
        [AppAuthorize(AppRol.Administrador, AppRol.Usuario)]
        public ActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }

        public ContentResult KeepAlive()
        {
            Session.Set(SessionKey.UsuarioId, Session.Get(SessionKey.UsuarioId));
            return Content("");
        }

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


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var objUsuario = context.Usuario.FirstOrDefault(x => x.Codigo == model.Codigo && x.Estado == ConstantHelpers.ESTADO_ACTIVO);

                String hashPassword = objUsuario != null ? EncodePwd(objUsuario.Salt, model.Password) : null;

                if (objUsuario == null || String.IsNullOrEmpty(objUsuario.Password) || objUsuario.Password.ToUpper() != hashPassword)
                {
                    PostMessage(MessageType.Error, "Usuario y/o Contraseña Incorrectos");
                }
                else
                {
                    Session.Clear();

                    AppRol rol = AppRol.Usuario;

                    switch (objUsuario.Rol)
                    {
                        case ConstantHelpers.ROL_ADMINISTRADOR: rol = AppRol.Administrador; break;
                        case ConstantHelpers.ROL_USUARIO : rol = AppRol.Usuario; break;
                    }

                    Session.Set(SessionKey.Usuario, objUsuario);
                    Session.Set(SessionKey.UsuarioId, objUsuario.UsuarioId);
                    Session.Set(SessionKey.Codigo, objUsuario.Codigo);
                    Session.Set(SessionKey.NombreCompleto, objUsuario.Nombres + " " + objUsuario.ApellidoPaterno + " " + objUsuario.ApellidoMaterno);
                    Session.Set(SessionKey.Rol, rol);
                    Session.Set(SessionKey.RolCompleto, objUsuario.Rol);
                    Session.Set(SessionKey.Foto, objUsuario.Foto);

                    return Dashboard();
                }
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error, "Ha ocurrido un error", ex);
            }

            return View();
        }


        [AppAuthorize(AppRol.Administrador,AppRol.Usuario)]
        public ActionResult Dashboard()
        {
            switch (Session.GetRol())
            {                
                case AppRol.Administrador: return RedirectToAction("AdministradorIndex");
                case AppRol.Usuario: return RedirectToAction("UsuarioIndex");

            }

            return RedirectToAction("Index");

        }

        [AppAuthorize(AppRol.Usuario)]
        public ActionResult UsuarioIndex()
        {
            return RedirectToAction("Index", "User");
        }


        [AppAuthorize(AppRol.Administrador, AppRol.Usuario)]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

    }
}
