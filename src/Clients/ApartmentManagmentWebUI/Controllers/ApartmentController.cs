using ApartmentManagment.MvcHelper.Enums;
using ApartmentManagment.MvcHelper.Extensions;
using ApartmentManagmentWebUI.Models.ApartmentModels;
using ApartmentManagmentWebUI.Models.BlockModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Controllers
{
    public class ApartmentController : CustomController
    {


        async Task<bool> FillBlocks()
        {
            var result = await MyHttpGet("GET", "Blocks");
            if (result.Success)
            {
                var jsondata = result.Message;
                var blockList = JsonConvert.DeserializeObject<List<BlockListModel>>(jsondata);

                ViewBag.Blocks = new SelectList(blockList, "Id", "Name");
                return true;
            }
            return false;

        }

        async Task<bool> FillApartments()
        {
            var result = await MyHttpGet("GET", "Apartments");
            if (result.Success)
            {
                var jsondata = result.Message;
                var apartmentList = JsonConvert.DeserializeObject<List<ApartmentListModel>>(jsondata);

                ViewBag.Apartments = new SelectList(apartmentList, "Id", "Name");
                return true;
            }
            return false;

        }

        #region Get

        public async Task<IActionResult> Index()
        {
            string jsonData = "";

            var result = await MyHttpGet("Get", "Apartments");
            if (result.Success)
            {
                jsonData = result.Message;
                var aparetmentList = JsonConvert.DeserializeObject<List<ApartmentListModel>>(jsonData);
                return View(aparetmentList);
            }


            return RedirectToAction("Index", "Home").ShowMessage(Status.Error, "Hata", result.Message);


        }

        #endregion

        #region Add

        public async Task<IActionResult> Add()
        {
            if (await FillBlocks() != true)
            {
                return RedirectToAction("Index").ShowMessage(Status.Error, "Error", "Blok Listesi Yüklenemedi!");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddApartmentModel addApartmentModel)
        {
            if (!ModelState.IsValid)
            {
                await FillBlocks();
                return View(addApartmentModel);
            }

            string jsonData = JsonConvert.SerializeObject(addApartmentModel);
            var result = await MyHttpCommand("POST", jsonData, $"Apartments/");
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

            await FillBlocks();

            var result = await MyHttpGet("Get", $"Apartments/{id.Value}");
            var block = JsonConvert.DeserializeObject<UpdateApartmentModel>(result.Message);

            return View(block);
        }


        [HttpPost]
        public async Task<IActionResult> Update(UpdateApartmentModel updateApartmentModel)
        {

            if (updateApartmentModel == null)
                return RedirectToAction("Index").ShowMessage(Status.Error, "Uyarı", "Güncellenmek istenen blok bulunamadı!");

            if (!ModelState.IsValid)
            {
                await FillBlocks();
                return View(updateApartmentModel).ShowMessage(Status.Error, "Error", "Eksik veya hatalı bilgiler mevcut!");
            }

            string jsonData = JsonConvert.SerializeObject(updateApartmentModel);
            var result = await MyHttpCommand("PUT", jsonData, $"Apartments");
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

        #region Delete

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteApartmentModel deleteApartmentModel)
        {

            if (deleteApartmentModel == null)
            {
                return Json(new JResult
                {
                    Status = Status.BadRequest,
                    Message = "Hatalı istekte bulundunuz! Lütfen yeniden deneyiniz."
                });
            }



            var result = await MyHttpGet("DELETE", $"Apartments/{deleteApartmentModel.Id}");
            if (result.Success)
            {
                return Json(new JResult
                {
                    Status = Status.Ok,
                    Message = "Silme işlemi başarıyla gerçekleşti."
                });
            }
            else
            {
                StringBuilder errors = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    errors.AppendLine(error);
                }
                return Json(new JResult
                {
                    Status = Status.BadRequest,
                    Message = errors.ToString()
                });

            }
        }

        #endregion




    }
}