using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using System.Globalization;

using FACEBAND.WEB.Helpers;
using FACEBAND.WEB.Models;
using FACEBAND.WEB.ViewModel.Band;
using FACEBAND.WEB.Filters;
using System.IO;

namespace FACEBAND.WEB.Controllers
{
    public class BandController : BaseController
    {
        [AppAuthorize(AppRol.Usuario)]
        public ActionResult Index()
        {
            var viewModel = new BandViewModel();
            viewModel.CargarDatos(CargarDatosContext(),Session.GetUsuarioId());
            return View(viewModel);
        }

        [AppAuthorize(AppRol.Usuario)]
        public ActionResult _AddMensajeBanda (int BandaId)
        {
            var viewModel = new _AddMensajeBandaViewModel();
            viewModel.CargarDatos(Session.GetUsuarioId(),BandaId);
            return PartialView(viewModel);
        }

        [HttpPost]
        [AppAuthorize(AppRol.Usuario)]
        public ActionResult _AddMensajeBanda(_AddMensajeBandaViewModel model)
        {
            try
            {
                using (var TransactionScope = new TransactionScope())
                {
                    var objMensaje = new MensajeBanda();

                    objMensaje.BandaId = model.BandaId;
                    objMensaje.Mensaje = model.Mensaje;
                    objMensaje.Fecha = DateTime.Now;
                    objMensaje.UsuarioId = Session.GetUsuarioId();
                    objMensaje.Estado = ConstantHelpers.ESTADO_ACTIVO;
                    context.MensajeBanda.Add(objMensaje);
                    context.SaveChanges();
                    TransactionScope.Complete();

                    return RedirectToAction("Index");
                }

            }catch(Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        [AppAuthorize(AppRol.Usuario)]
        public ActionResult EditBanda (int bandaId)
        {
            var viewModel = new EditBandaViewModel();
            viewModel.CargarDatos(CargarDatosContext(),bandaId);
            return View(viewModel);
        }

        [HttpPost]
        [AppAuthorize(AppRol.Usuario)]
        public ActionResult EditBanda(EditBandaViewModel model)
        {
            try
            {
                using (var TransactionScope = new TransactionScope())
                {
                    var objBanda = context.Banda.FirstOrDefault(x => x.BandaId == model.BandaId);

                    objBanda.Nombre = model.Nombre;
                    objBanda.Descripcion = model.Descripcion;

                    if (model.Foto != null)
                    {
                        String extension = Path.GetExtension(model.Foto.FileName);
                        String NombreFoto = "IMG" + "_" + model.BandaId + "_" + DateTime.Now.Ticks + extension;
                        String path = Path.Combine(Server.MapPath(ConstantHelpers.PATH_IMG_PERFIL), NombreFoto);
                        model.Foto.SaveAs(path);
                        objBanda.Foto = ConstantHelpers.PATH_IMG_PERFIL_db + "/" + NombreFoto;
                    }

                    if(model.LstSelect != null)
                    {
                        foreach (var item in model.LstSelect)
                        {
                            var objGeneroBanda = new GeneroBanda();

                            objGeneroBanda.BandaId = model.BandaId;
                            objGeneroBanda.GeneroId = item;
                            objGeneroBanda.Estado = ConstantHelpers.ESTADO_ACTIVO;
                            context.GeneroBanda.Add(objGeneroBanda);
                        }
                    }
                    

                    context.SaveChanges();
                    TransactionScope.Complete();

                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        [AppAuthorize(AppRol.Usuario)]
        public ActionResult DeleteUserBand(Int32 usuarioId)
        {
            try
            {
                using (var TransactionScope = new TransactionScope())
                {
                    var objusuario = context.UsuarioBanda.FirstOrDefault(x => x.UsuarioId == usuarioId);

                    //objusuario.Estado = ConstantHelpers.ESTADO_INACTIVO;
                    context.UsuarioBanda.Remove(objusuario);
                    context.SaveChanges();
                    TransactionScope.Complete();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [AppAuthorize(AppRol.Usuario)]
        public ActionResult _AddUserBand(int BandaId, Int32? usuarioId)
        {
            if(usuarioId.HasValue)
            {
                try
                {
                    using (var TransactionScope = new TransactionScope())
                    {
                        var objusuario = new UsuarioBanda();

                        objusuario.UsuarioId = usuarioId.Value;
                        objusuario.BandaId = BandaId;
                        objusuario.Rol = "BAT";
                        objusuario.FechaIngreso = DateTime.Now;
                        objusuario.Estado = ConstantHelpers.ESTADO_ACTIVO;
                        context.UsuarioBanda.Add(objusuario);
                        context.SaveChanges();
                        TransactionScope.Complete();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                var viewModel = new _AddUserBandViewModel();
                viewModel.CargarDatos(CargarDatosContext(), BandaId);
                return PartialView(viewModel);
            }
            
        }

    }
}
