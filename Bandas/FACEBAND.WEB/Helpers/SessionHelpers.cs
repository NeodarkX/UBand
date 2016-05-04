using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

using FACEBAND.WEB.Models;

namespace FACEBAND.WEB.Helpers
{
    public enum AppRol
    {
        Administrador,
        Usuario
    }

    public enum SessionKey
    {
        Usuario,
        UsuarioId,
        Codigo,
        NombreCompleto,
        Pregunta,
        Email,
        Rol,
        RolCompleto,
        Foto
    }

    public static class SessionHelpers
    {
        #region TieneRol
        public static Boolean TieneRol(this HttpSessionState Session, AppRol Rol)
        {
            return Session.GetRol() == Rol;
        }

        public static Boolean TieneRol(this HttpSessionStateBase Session, AppRol Rol)
        {
            return Session.GetRol() == Rol;
        }

        public static Boolean TieneRol(this HttpSessionState Session, String Rol)
        {
            return Get(Session, SessionKey.RolCompleto).ToString() == Rol;
        }

        public static Boolean TieneRol(this HttpSessionStateBase Session, String Rol)
        {
            return Get(Session, SessionKey.RolCompleto).ToString() == Rol;
        }

        #endregion

        #region GetobjUsuario

        public static Usuario GetUsuario(this HttpSessionState Session)
        {
            return (Usuario)Get(Session, SessionKey.Usuario);
        }

        public static Usuario GetUsuario(this HttpSessionStateBase Session)
        {
            return (Usuario)Get(Session, SessionKey.Usuario);
        }

        #endregion GetobjUsuario

        #region GetUsuarioId

        public static Int32 GetUsuarioId(this HttpSessionState Session)
        {
            return Get(Session, SessionKey.UsuarioId).ToInteger();
        }

        public static Int32 GetUsuarioId(this HttpSessionStateBase Session)
        {
            return Get(Session, SessionKey.UsuarioId).ToInteger();
        }
        #endregion

        #region GetCodigo
        public static String GetCodigo(this HttpSessionState Session)
        {
            return Get(Session, SessionKey.Codigo).ToString();
        }

        public static String GetCodigo(this HttpSessionStateBase Session)
        {
            return Get(Session, SessionKey.Codigo).ToString();
        }
        #endregion

        #region GetNombreCompleto
        public static String GetNombreCompleto(this HttpSessionState Session)
        {
            return Get(Session, SessionKey.NombreCompleto).ToString();
        }

        public static String GetNombreCompleto(this HttpSessionStateBase Session)
        {
            return Get(Session, SessionKey.NombreCompleto).ToString();
        }
        #endregion   

        #region Pregunta
        public static String GetPregunta(this HttpSessionState Session)
        {
            return Get(Session, SessionKey.Pregunta).ToString();
        }

        public static String GetPregunta(this HttpSessionStateBase Session)
        {
            return Get(Session, SessionKey.Pregunta).ToString();
        }
        #endregion Pregunta

        #region GetEmail
        public static String GetEmail(this HttpSessionState Session)
        {
            return Get(Session, SessionKey.Email).ToString();
        }

        public static String GetEmail(this HttpSessionStateBase Session)
        {
            return Get(Session, SessionKey.Email).ToString();
        }
        #endregion

        #region GetRol
        public static AppRol? GetRol(this HttpSessionState Session)
        {
            return (AppRol?)Get(Session, SessionKey.Rol);
        }

        public static AppRol? GetRol(this HttpSessionStateBase Session)
        {
            return (AppRol?)Get(Session, SessionKey.Rol);
        }
        #endregion

        #region GetRolCompleto
        public static String GetRolCompleto(this HttpSessionState Session)
        {
            return (String)Get(Session, SessionKey.RolCompleto);
        }

        public static String GetRolCompleto(this HttpSessionStateBase Session)
        {
            return (String)Get(Session, SessionKey.RolCompleto);
        }
        #endregion

        #region GetFoto
        public static String GetFoto(this HttpSessionState Session)
        {
            return (String)Get(Session, SessionKey.Foto);
        }

        public static String GetFoto(this HttpSessionStateBase Session)
        {
            return (String)Get(Session, SessionKey.Foto);
        }
        #endregion
        
        #region Private

        private static object Get(HttpSessionState Session, String Key)
        {
            return Session[Key];
        }

        private static void Set(HttpSessionState Session, String Key, object Value)
        {
            Session[Key] = Value;
        }

        private static bool Exists(HttpSessionState Session, String Key)
        {
            return Session[Key] != null;
        }

        private static object Get(HttpSessionStateBase Session, String Key)
        {
            return Session[Key];
        }

        private static void Set(HttpSessionStateBase Session, String Key, object Value)
        {
            Session[Key] = Value;
        }

        private static bool Exists(HttpSessionStateBase Session, String Key)
        {
            return Session[Key] != null;
        }

        #endregion

        #region Getters setters GlobalKey
        //HttpSessionState
        public static object Get(this HttpSessionState Session, SessionKey Key)
        {
            return Get(Session, Key.ToString());
        }

        public static void Set(this HttpSessionState Session, SessionKey Key, object Value)
        {
            Set(Session, Key.ToString(), Value);
        }

        public static bool Exists(this HttpSessionState Session, SessionKey Key)
        {
            return Exists(Session, Key.ToString());
        }

        //HttpSessionStateBase
        public static object Get(this HttpSessionStateBase Session, SessionKey Key)
        {
            return Get(Session, Key.ToString());
        }

        public static void Set(this HttpSessionStateBase Session, SessionKey Key, object Value)
        {
            Set(Session, Key.ToString(), Value);
        }

        public static bool Exists(this HttpSessionStateBase Session, SessionKey Key)
        {
            return Exists(Session, Key.ToString());
        }
        #endregion
    }
}