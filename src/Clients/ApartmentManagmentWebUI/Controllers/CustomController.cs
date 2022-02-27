using ApartmentManagment.MvcHelper.Extensions;
using ApartmentManagmentWebUI.Models;
using ApartmentManagmentWebUI.Models.UserModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Controllers
{
    public class CustomController : Controller
    {
        //Web Api'dan Get işlemleri yapmak için kullanılan metot
        #region Query

        public async Task<EntityResult> MyHttpGet(string method, string action)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod(method), $"https://localhost:44390/api/{action}"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");
                    if (HttpContext.HasCookie("Authorization"))
                    {
                        var myCookie = JsonConvert.DeserializeObject<LoginSessionModel>(HttpContext.GetCookie("Authorization"));
                        request.Headers.TryAddWithoutValidation("Authorization", myCookie.JwtToken);
                    }

                    var response = await httpClient.SendAsync(request);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return new EntityResult { Message = await response.Content.ReadAsStringAsync(), Success = true };
                    }

                    return new EntityResult { Message = await response.Content.ReadAsStringAsync(), Success = false };
                }

            }
        }

        #endregion


        //Web Api'dan Command işlemleri yapmak için kullanılan metot

        #region Command

        public async Task<EntityResult> MyHttpCommand(string method, string content, string action)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod(method), $"https://localhost:44390/api/{action}"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");
                    if (HttpContext.HasCookie("Authorization"))
                    {
                        var myCookie = JsonConvert.DeserializeObject<LoginSessionModel>(HttpContext.GetCookie("Authorization"));
                        request.Headers.TryAddWithoutValidation("Authorization", myCookie.JwtToken);
                    }


                    request.Content = new StringContent(content);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);

                    try
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            return JsonConvert.DeserializeObject<EntityResult>(await response.Content.ReadAsStringAsync());
                        }
                        return JsonConvert.DeserializeObject<EntityResult>(await response.Content.ReadAsStringAsync());
                    }
                    catch (System.Exception)
                    {

                        return new EntityResult { Message = await response.Content.ReadAsStringAsync(), Success = false, Errors = new List<string> { "İşlem başarısız oldu!" } };
                    }
                }

            }
        }

        #endregion
    }
}
