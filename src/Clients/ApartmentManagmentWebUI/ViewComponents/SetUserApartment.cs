using ApartmentManagment.MvcHelper.Extensions;
using ApartmentManagmentWebUI.Models.ApartmentModels;
using ApartmentManagmentWebUI.Models.UserModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.ViewComponents
{
    public class SetUserApartment : ViewComponent
    {

        public int UserId { get; set; }
        public async Task<IViewComponentResult> InvokeAsync()
        {


            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://localhost:44390/api/Apartments/GetByState"))
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
                        string jsonData = "";
                        jsonData = await response.Content.ReadAsStringAsync();
                        var aparetmentList = JsonConvert.DeserializeObject<List<ApartmentListModel>>(jsonData).Select(x => new SelectListItem
                        {
                            Text = x.Block + " - Daire No: " + x.No,
                            Value = x.Id.ToString()
                        }).ToList();
                        var model = new SetUserApartmentModel { ApartmentList = new SelectList(aparetmentList, "Value", "Text") };

                        return View(model);
                    }
                    return View(new SetUserApartmentModel { });
                }
            }

        }
    }
}

