using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UNICA.DENT.Helpers;
using UNICA.DENT.Logic;
namespace UNICA.DENT.Filters
{
    public class PermisoPacienteAttribute : ActionFilterAttribute
    {
        private readonly String[] _parametro;

        public PermisoPacienteAttribute(params String[] parametro)
        {
            _parametro = parametro;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!TieneAcceso(filterContext))
            {
                filterContext.Result = new ViewResult() { ViewName = filterContext.HttpContext.Request.IsAjaxRequest() ? "_PermisoInsuficienteModal" : "PermisoInsuficiente" };
            }
        }
        public virtual bool TieneAcceso(ActionExecutingContext context)
        {

            try
            {
                var usuarioId = context.HttpContext.Session.GetUsuarioId();
                var permisoLogic = new PermisoLogic();
                var pacienteId = context.HttpContext.Request[_parametro[0]].ToInteger();
                /*
                if (permisoLogic.GetPermisoPaciente(usuarioId,pacienteId))
                    return true;
                else
                {
                    foreach (var parametro in _parametro)
                    {
                        var identificador = context.HttpContext.Request[parametro].ToInteger();
                        return permisoLogic.GetPermiso(UsuarioId: usuarioId,Identificador:identificador, Parametro: parametro);
                    }
                }*/
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }
    }
}