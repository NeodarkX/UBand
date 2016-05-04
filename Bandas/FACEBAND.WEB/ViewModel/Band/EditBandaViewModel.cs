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
    public class EditBandaViewModel
    {
        public Int32 BandaId { get; set; }

        public UsuarioBanda ObjBanda { get; set; }

        public List<UsuarioBanda> LstUsuarioBanda { get; set; }

        public String Nombre { get; set; }

        public String Descripcion { get; set; }

        public List<Int32> LstSelect { get; set; }

        public List<Genero> LstGenero { get; set; }

        public HttpPostedFileBase Foto { get; set; }

        public EditBandaViewModel()
        {
            LstSelect = new List<Int32>();
        }

        public void CargarDatos(CargarDatosContext dataContext,Int32 bandaId)
        {
            BandaId = bandaId;
            LstUsuarioBanda = dataContext.context.UsuarioBanda.Where(x => x.Estado == ConstantHelpers.ESTADO_ACTIVO).ToList();
            LstGenero = dataContext.context.Genero.ToList();
            ObjBanda = dataContext.context.UsuarioBanda.FirstOrDefault(x => x.BandaId == BandaId && x.Estado == ConstantHelpers.ESTADO_ACTIVO);
            Nombre = ObjBanda.Banda.Nombre;
            Descripcion = ObjBanda.Banda.Descripcion;

        }
    }
}