using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FACEBAND.WEB.API.ViewModels.Authentication
{
    public class LoginViewModel
    {
        public String user { get; set; }
        public String pass { get; set; }

        public LoginViewModel()
        {

        }
    }
}