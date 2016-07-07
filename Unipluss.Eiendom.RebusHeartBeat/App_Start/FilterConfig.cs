using System.Web;
using System.Web.Mvc;

namespace Unipluss.Eiendom.RebusHeartBeat
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
