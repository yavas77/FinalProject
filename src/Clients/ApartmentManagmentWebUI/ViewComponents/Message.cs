using ApartmentManagment.MvcHelper.Extensions;
using ApartmentManagmentWebUI.Filters;
using ApartmentManagmentWebUI.Models.Messages;
using ApartmentManagmentWebUI.Models.UserModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.ViewComponents
{
    [ControlLogin]
    public class Message : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {


            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://localhost:44390/api/Messages"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");
                    if (HttpContext.HasCookie("Authorization"))
                    {
                        if (HttpContext.HasCookie("Authorization"))
                        {
                            var myCookie = JsonConvert.DeserializeObject<LoginSessionModel>(HttpContext.GetCookie("Authorization"));
                            request.Headers.TryAddWithoutValidation("Authorization", myCookie.JwtToken);
                        }
                    }

                    var response = await httpClient.SendAsync(request);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string jsonData = "";
                        jsonData = await response.Content.ReadAsStringAsync();
                        var messageList = JsonConvert.DeserializeObject<List<MessageListModel>>(jsonData).Where(x => x.Status == MessageStatus.Okunmamis).ToList();

                        return View(messageList);
                    }
                    return View(new List<MessageListModel> { });
                }

            }
        }
    }
}
