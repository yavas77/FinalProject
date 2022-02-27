using ApartmentManagment.MvcHelper.Enums;
using ApartmentManagment.MvcHelper.Extensions;
using ApartmentManagmentWebUI.Models.CaseModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Controllers
{
    public class CaseController : CustomController
    {
        #region Get

        public async Task<IActionResult> Index()
        {
            string jsonData = "";

            var result = await MyHttpGet("Get", "Cases");
            if (result.Success)
            {
                jsonData = result.Message;
                var aparetmentList = JsonConvert.DeserializeObject<List<CaseListModel>>(jsonData);
                return View(aparetmentList);
            }


            return RedirectToAction("Index", "Home").ShowMessage(Status.Error, "Hata", result.Message);


        }

        #endregion
    }
}
