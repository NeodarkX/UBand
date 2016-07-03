using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FACEBAND.WEB.API.Clases
{
    public class UsuarioModel
    {
        public Int32 id { get; set; }
        public String codigo { get; set; }
        public String nombres { get; set; }
        public String apellidoPaterno { get; set; }
        public String apellidoMaterno { get; set; }
        public String foto { get; set; }
        public UsuarioModel() { }
    }
}