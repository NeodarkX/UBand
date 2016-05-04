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
    public class _AddUserBandViewModel
    {
        public Int32 BandaId { get; set; }

        public Int32 UsuarioId { get; set; }

        public Int32? Pagina { get; set; }

        public IPagedList<Usuario> LstUsuario { get; set; }

        public String Mensaje { get; set; }

        public _AddUserBandViewModel()
        {

        }

        public void CargarDatos(CargarDatosContext dataContext, Int32 bandaId)
        {
            Pagina = Pagina ?? 1;
            BandaId = bandaId;
            LstUsuario = dataContext.context.Usuario.AsQueryable().Where(x => x.Rol == ConstantHelpers.ROL_USUARIO).OrderBy(x => x.Nombres).ToPagedList(Pagina.Value, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}