using ApartmentManagment.MvcHelper.Enums;
using ApartmentManagment.MvcHelper.Extensions;
using ApartmentManagmentWebUI.Filters;
using ApartmentManagmentWebUI.Models.ApartmentModels;
using ApartmentManagmentWebUI.Models.BlockModels;
using ApartmentManagmentWebUI.Models.IncomeAndExpenditure;
using ApartmentManagmentWebUI.Models.UserModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Controllers
{
    [ControlLogin(Roles = "Admin")]
    public class ApartmentExpenseController : CustomController
    {


        //Kullanımı dolu olan daire listesini çekmek için kullanılı 
        async Task<bool> FillApartments()
        {
            var result = await MyHttpGet("GET", "Users/GetUsersWhoseApartmentIsNotEmpty");
            if (result.Success)
            {
                var jsondata = result.Message;
                var apartmentList = JsonConvert.DeserializeObject<List<UserListModel>>(jsondata)
                    .Select(x => new SelectListItem
                    {
                        Text = x.Apartment + " - " + x.FirstName + " " + x.LastName,
                        Value = x.Id.ToString()
                    }).ToList(); ;

                ViewBag.Users = new SelectList(apartmentList, "Value", "Text");
                return true;
            }
            return false;

        }



        #region Get

        public async Task<IActionResult> Index()
        {
            string jsonData = "";

            var result = await MyHttpGet("Get", "ApartmentExpenses");
            if (result.Success)
            {
                jsonData = result.Message;
                var ApartmentExpenseList = JsonConvert.DeserializeObject<List<ApartmentExpenseListModel>>(jsonData);
                return View(ApartmentExpenseList);
            }


            return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", result.Message);


        }

        #endregion


        #region Add

        public async Task<IActionResult> Add()
        {
            await FillApartments();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddApartmentExpenseModel addApartmentExpenseModel)
        {
            if (!ModelState.IsValid)
            {
                await FillApartments();
                return View(addApartmentExpenseModel).ShowMessage(Status.Error, "Error", "Eksik veya hatalı bilgiler mevcut!");
            }

            string jsonData = "";
            if (addApartmentExpenseModel.UserId == 0)
            {
                jsonData = JsonConvert.SerializeObject(addApartmentExpenseModel);
            }
            else
            {
                jsonData = JsonConvert.SerializeObject(addApartmentExpenseModel);
            }




            var result = await MyHttpCommand("POST", jsonData, $"ApartmentExpenses/Add");

            if (result.Success)
            {
                return RedirectToAction("Index").ShowMessage(Status.Ok, "Başarılı", result.Message);
            }
            else
            {
                //Hata oluşması durumunda hataların listelenmesi işlemi

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


            await FillApartments();

            var result = await MyHttpGet("GET", $"ApartmentExpenses/GetById/{id.Value}");
            var block = JsonConvert.DeserializeObject<UpdateApartmentExpenseModel>(result.Message);

            return View(block);
        }


        [HttpPost]
        public async Task<IActionResult> Update(UpdateApartmentExpenseModel updateApartmentModel)
        {

            if (updateApartmentModel == null)
                return RedirectToAction("Index").ShowMessage(Status.Error, "Uyarı", "Güncellenmek istenen blok bulunamadı!");

            if (!ModelState.IsValid)
            {
                await FillApartments();
                return View(updateApartmentModel).ShowMessage(Status.Error, "Error", "Eksik veya hatalı bilgiler mevcut!");
            }

            string jsonData = JsonConvert.SerializeObject(updateApartmentModel);
            var result = await MyHttpCommand("PUT", jsonData, $"ApartmentExpenses");
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
        public async Task<IActionResult> Delete(DeleteApartmentExpenseModel deleteApartmentExpenseModel)
        {

            if (deleteApartmentExpenseModel == null)
            {
                return Json(new JResult
                {
                    Status = Status.BadRequest,
                    Message = "Silinmek istenen fatura bulunamadı!"
                });
            }



            var result = await MyHttpGet("DELETE", $"ApartmentExpenses/{deleteApartmentExpenseModel.Id}");
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
