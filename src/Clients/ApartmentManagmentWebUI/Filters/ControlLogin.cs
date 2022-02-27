using ApartmentManagmentWebUI.Models.UserModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Filters
{
    public class ControlLogin : ActionFilterAttribute, IActionFilter
    {
        public string Roles { get; set; }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (!string.IsNullOrEmpty(filterContext.HttpContext.Request.Cookies["Authorization"]))
                {
                    var loginSession = JsonConvert.DeserializeObject<LoginSessionModel>(filterContext.HttpContext.Request.Cookies["Authorization"]);

                    if (!Roles.Contains(loginSession.Role))
                    {
                        filterContext.HttpContext.Response.Redirect("/Account/SignIn");
                    }
                   
                }
                else
                {
                    filterContext.HttpContext.Response.Redirect("/Account/SignIn");
                }

            }
            catch (Exception)
            {

                filterContext.HttpContext.Response.Redirect("/Account/SignIn");
            }
        }
    }

}

