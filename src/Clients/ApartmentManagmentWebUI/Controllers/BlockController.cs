using ApartmentManagment.MvcHelper.Enums;
using ApartmentManagment.MvcHelper.Extensions;
using ApartmentManagmentWebUI.Filters;
using ApartmentManagmentWebUI.Models.BlockModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Controllers
{
    [ControlLogin(Roles = "Admin")]
    public class BlockController : CustomController
    {
        #region Get
       
        public async Task<IActionResult> Index()
        {


            var result = await MyHttpGet("GET", "Blocks");
            if (result.Success)
            {
                var jsondata = result.Message;
                var blockList = JsonConvert.DeserializeObject<List<BlockListModel>>(jsondata);


                return View(blockList);
            }


            return null;


        }

        #endregion

        #region Add

        public IActionResult Add()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlockModel addBlockModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addBlockModel).ShowMessage(Status.Error, "Error", "Eksik veya hatalı bilgiler mevcut!");

            }

            string jsonData = JsonConvert.SerializeObject(addBlockModel);
            var result = await MyHttpCommand("POST", jsonData, $"Blocks/");
            if (result.Success)
            {
                return RedirectToAction("Index").ShowMessage(Status.Ok, "Başarılı", result.Message);
            }
            else
            {
                StringBuilder errors = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    errors.AppendLine(error);
                }
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", errors.ToString());
            }
        }

        #endregion

        #region Update


        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return RedirectToAction().ShowMessage(Status.Error, "Uyarı", "Talep hatalı lütfen menüleri kullanarak yeniden deneyiniz!");

            var result = await MyHttpGet("Get", $"Blocks/{id.Value}");
            var block = JsonConvert.DeserializeObject<UpdateBlockModel>(result.Message);

            return View(block);
        }


        [HttpPost]
        public async Task<IActionResult> Update(UpdateBlockModel updateBlockModel)
        {

            if (updateBlockModel == null)
                return RedirectToAction("Index").ShowMessage(Status.Error, "Uyarı", "Güncellenmek istenen blok bulunamadı!");

            if (!ModelState.IsValid)
            {
                return View(updateBlockModel).ShowMessage(Status.Error, "Error", "Eksik veya hatalı bilgiler mevcut!");
            }

            string jsonData = JsonConvert.SerializeObject(updateBlockModel);
            var result = await MyHttpCommand("PUT", jsonData, $"Blocks/");
            if (result.Success)
            {
                return RedirectToAction("Index").ShowMessage(Status.Ok, "Başarılı", result.Message);
            }
            else
            {
                StringBuilder errors = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    errors.AppendLine(error);
                }
                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", errors.ToString());

            }


            #endregion

        }
    }
}