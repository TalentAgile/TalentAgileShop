using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.ApplicationInsights;

namespace TalentAgileShop.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorHandler.AiHandleErrorAttribute());
        }
    }


}
