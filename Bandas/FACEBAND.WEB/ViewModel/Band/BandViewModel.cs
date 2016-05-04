using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PagedList;

using FACEBAND.WEB.Models;
using FACEBAND.WEB.Helpers;
using FACEBAND.WEB.Controllers;

namespace FACEBAND.WEB.ViewModel.Band
{
    public class BandViewModel
    {
        public UsuarioBanda ObjBanda { get; set; }

        public List<UsuarioBanda> LstUsuarioBanda { get; set; }

        public List<Actividad> LstActividad { get; set; }

        public IPagedList<MensajeBanda> LstMensajes { get; set; }

        public BandViewModel() {}

        public void CargarDatos(CargarDatosContext dataContext, Int32 usuarioId)
        {
            ObjBanda = dataContext.context.UsuarioBanda.FirstOrDefault(x => x.UsuarioId == usuarioId);
            LstUsuarioBanda = dataContext.context.UsuarioBanda.Where(x => x.Estado == ConstantHelpers.ESTADO_ACTIVO).ToList();
            LstActividad = dataContext.context.Actividad.Where(x => x.BandaDestinatarioId == ObjBanda.BandaId && x.Estado != ConstantHelpers.ESTADO_INACTIVO).ToList();
            LstMensajes = dataContext.context.MensajeBanda.AsQueryable().Where(x => x.BandaId == ObjBanda.BandaId && x.Estado == ConstantHelpers.ESTADO_ACTIVO).OrderByDescending(x => x.Fecha).ToPagedList(1, ConstantHelpers.DEFAULT_MESSAGE_BAND_PAGE_SIZE);
        }
    }
}