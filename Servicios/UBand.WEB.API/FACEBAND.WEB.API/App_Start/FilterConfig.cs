using FACEBAND.WEB.API.Infrastructure;

namespace FACEBAND.WEB.API.App_Start
{
    public class FilterConfig
    {
        public static void Configure(System.Web.Mvc.GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
