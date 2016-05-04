using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using FACEBAND.WEB.Models;

namespace FACEBAND.WEB.ViewModel.Home
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceName = "CampoUsuario", ErrorMessageResourceType = typeof(i18n.ValidationStrings))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "CampoEmail", ErrorMessageResourceType = typeof(i18n.ValidationStrings))]
        public String Codigo { get; set; }

        [Required(ErrorMessageResourceName = "CampoPassword", ErrorMessageResourceType = typeof(i18n.ValidationStrings))]
        public String Password { get; set; }

        public LoginViewModel() { }
    }
}