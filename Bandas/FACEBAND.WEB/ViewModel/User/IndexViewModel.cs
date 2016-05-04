using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PagedList;

using FACEBAND.WEB.Models;
using FACEBAND.WEB.Helpers;
using FACEBAND.WEB.Controllers;

namespace FACEBAND.WEB.ViewModel.User
{
    public class IndexViewModel
    {
        public String Genero { get; set; }

        public Int32? GeneroId { get; set; }

        public Int32? Pagina { get; set; }

        public List<Genero> LstGenero { get; set; }

        public IPagedList<GeneroBanda> LstBanda { get; set; }

        public IndexViewModel() { }

        public void CargarDatos(CargarDatosContext dataContext, Int32? generoId, String genero, Int32? pagina)
        {
            Pagina = pagina ?? 1;
            this.Genero = genero;
            LstGenero = dataContext.context.Genero.Where(x => x.Estado == ConstantHelpers.ESTADO_ACTIVO).ToList();
            if (generoId.HasValue)
            {
                GeneroId = generoId.Value;
                LstBanda = dataContext.context.GeneroBanda.AsQueryable().Where(x => x.GeneroId == generoId.Value).OrderBy(x => x.Banda.Nombre).ToPagedList(Pagina.Value, ConstantHelpers.DEFAULT_PAGE_SIZE);
            }
            else
                LstBanda = dataContext.context.GeneroBanda.AsQueryable().OrderBy(x => x.Banda.Nombre).ToPagedList(Pagina.Value, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }

        public void CargarDatos(CargarDatosContext dataContext)
        {
            Pagina = Pagina ?? 1;
            LstGenero = dataContext.context.Genero.Where(x => x.Estado == ConstantHelpers.ESTADO_ACTIVO).ToList();
            if (GeneroId.HasValue)
            {
                GeneroId = GeneroId ?? null;
                LstBanda = dataContext.context.GeneroBanda.AsQueryable().Where(x => x.GeneroId == GeneroId.Value).OrderBy(x => x.Banda.Nombre).ToPagedList(Pagina.Value, ConstantHelpers.DEFAULT_PAGE_SIZE);
            }
            else
                LstBanda = dataContext.context.GeneroBanda.AsQueryable().OrderBy(x => x.Banda.Nombre).ToPagedList(Pagina.Value, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}