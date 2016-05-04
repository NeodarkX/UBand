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
    public class _AddMensajeBandaViewModel
    {
        public Int32 Usuario { get; set; }

        public Int32 BandaId { get; set; }

        [Required(ErrorMessageResourceName = "CampoRequerido", ErrorMessageResourceType = typeof(i18n.ValidationStrings))]
        public String Mensaje { get; set; } 

        public _AddMensajeBandaViewModel()
        {

        }

        public void CargarDatos(Int32 usuarioId, Int32 bandaId)
        {
            Usuario = usuarioId;
            BandaId = bandaId;
        }

    }
}