using ApartmentManagmentWebUI.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Web.Http.ExceptionHandling;

namespace ApartmentManagmentWebUI.Controllers
{
	public class ErrorController : Controller
	{
        private readonly IExceptionLogger _logger;

        public ErrorController(IExceptionLogger logger)
        {
            _logger = logger;
        }

        public IActionResult HandleError(int statusCode)
        {
            var errorData = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var message = "";
            if (statusCode == (int)HttpStatusCode.InternalServerError) //Status 500
            {
                message = errorData.Error.Message;
            }
            else if (statusCode == (int)HttpStatusCode.NotFound) //Status 404
            {
                message = "Talep edilen sayfa bulunamadı.";
            }
            else if (statusCode == (int)HttpStatusCode.BadRequest)
            {
                message = "Geçersiz bir istekte bulundunuz.";
            }
            else
            {
                message = "Bilinmeyen bir hata tespit edildi.";
            }

            var errorModel = new LogError
            {
                Message = message,
                StatusCode = statusCode,
                CreateDate = DateTime.Now,
                LastUpdate = DateTime.Now
                //Url = HttpContext.Items["url"].ToString()
            };

            return View("Error", errorModel);
        }
    }
}
