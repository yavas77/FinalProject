using ApartmentManagment.MvcHelper.Enums;
using ApartmentManagment.MvcHelper.Extensions;
using ApartmentManagmentWebUI.Filters;
using ApartmentManagmentWebUI.Models.Messages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Controllers
{
    [ControlLogin(Roles = "Admin")]
    public class MessageController : CustomController
    {
        #region Get


        public async Task<IActionResult> Index()
        {
            string jsonData = "";

            var result = await MyHttpGet("GET", "Messages");
            if (result.Success)
            {
                jsonData = result.Message;
                var messageList = JsonConvert.DeserializeObject<List<MessageListModel>>(jsonData);
                return View(messageList);
            }


            return RedirectToAction("Index", "Home").ShowMessage(Status.Error, "Hata", result.Message);


        }




        #endregion


        #region Get


        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return RedirectToAction().ShowMessage(Status.Error, "Uyarı", "Talep hatalı lütfen menüleri kullanarak yeniden deneyiniz!");

            var result = await MyHttpGet("Get", $"Messages/{id.Value}");
            if (result.Success)
            {
                var jsondata = result.Message;
                var message = JsonConvert.DeserializeObject<MessageListModel>(jsondata);


                return View(message);
            }


            return RedirectToAction("Index", "Home").ShowMessage(Status.Error, "Hata", result.Message);




        }


    }

    #endregion
}
