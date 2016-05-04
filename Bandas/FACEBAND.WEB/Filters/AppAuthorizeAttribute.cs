using FACEBAND.WEB.Helpers;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace FACEBAND.WEB.Filters
{
    public class AppAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly AppRol[] _acceptedRoles;

        public AppAuthorizeAttribute(params AppRol[] acceptedRoles)
        {
            _acceptedRoles = acceptedRoles;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var unauthorized = false;

            try
            {
                var user = filterContext.HttpContext.Session.Get(SessionKey.UsuarioId).ToString();

                if (_acceptedRoles.Length > 0)
                {
                    var rol = (AppRol)filterContext.HttpContext.Session.Get(SessionKey.Rol);
                    if (!_acceptedRoles.Contains(rol))
                    {
                        unauthorized = true;
                    }
                }
            }
            catch (Exception ex)
            {
                unauthorized = true;
            }

            if (unauthorized)
            {


                if (filterContext.HttpContext.Session.Get(SessionKey.UsuarioId) == null)
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Login" }));
                else
                    filterContext.Result = new ViewResult() { ViewName = filterContext.HttpContext.Request.IsAjaxRequest() ? "_PermisoInsuficienteModal" : "PermisoInsuficiente" };
            }
        }
    }
}