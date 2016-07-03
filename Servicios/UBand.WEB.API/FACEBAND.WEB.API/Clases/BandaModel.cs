using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FACEBAND.WEB.API.Clases
{
    public class BandaModel
    {
        public Int32 id { get; set; }
        public String nombre { get; set; }
        public String descripcion { get; set; }
        public String foto { get; set; }
        public Int32? seguidores { get; set; }
        public BandaModel() { }
    }
}