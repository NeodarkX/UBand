using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FACEBAND.WEB.Helpers;
using FACEBAND.WEB.Models;
using FACEBAND.WEB.ViewModel.User;
using FACEBAND.WEB.Filters;

namespace FACEBAND.WEB.Controllers
{
    public class UserController : BaseController
    {
        [AppAuthorize(AppRol.Usuario)]
        public ActionResult Index(IndexViewModel model)
        {
            model.CargarDatos(CargarDatosContext());
            return View(model);
        }
    }
}
