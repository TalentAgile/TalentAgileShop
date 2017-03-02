using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using Ninject.Activation;

namespace TalentAgileShop.Web.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Method)]
    public class CartCookieActionFilter: System.Web.Mvc.ActionFilterAttribute
    {

        private string GetCookieId(HttpRequestBase request)
        {
            var cookie = request.Cookies.Get("cart-id");

            if (cookie == null)
            {
                return Guid.NewGuid().ToString();
            }
            var value = cookie.Value;

            return value;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
          
            var id = GetCookieId(filterContext.HttpContext.Request);


            var updatedCookie = new HttpCookie("cart-id", id)
            {
                Domain = filterContext.HttpContext.Request.Url.Host,
                Path = "/",
                Expires = DateTime.Now.AddMinutes(40)

            };

            filterContext.HttpContext.Response.Cookies.Add(updatedCookie);
          
        }
    }
}