using ApartmentManagment.MvcHelper.Enums;
using ApartmentManagment.MvcHelper.Extensions;
using ApartmentManagmentWebUI.Filters;
using ApartmentManagmentWebUI.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Controllers
{


    public class AccountController : CustomController
    {

        #region Get

        [ControlLogin(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var result = await MyHttpGet("GET", "Users");
            if (result.Success)
            {
                var jsondata = result.Message;
                var userList = JsonConvert.DeserializeObject<List<UserListModel>>(jsondata);
                return View(userList);
            }


            return RedirectToAction().ShowMessage(Status.Error, "Hata", result.Message);
        }

        #endregion

        #region Add
        [ControlLogin(Roles = "Admin")]
        public IActionResult Add()
        {

            return View();
        }


        /// <summary>
        /// Yeni kullanıcı ekleme işlemlemleri
        /// </summary>
        [ControlLogin(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(UserAddModel userAddModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userAddModel).ShowMessage(Status.Error, "Error", "Eksik veya hatalı bilgiler mevcut!");

            }

            string jsonData = JsonConvert.SerializeObject(userAddModel);
            var result = await MyHttpCommand("POST", jsonData, $"Users");
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

        #region SignIn


        [HttpGet]
        public IActionResult SignIn()
        {
            if (HttpContext.HasCookie("Authorization"))
            {

                var loginSessionModel = JsonConvert.DeserializeObject<LoginSessionModel>(HttpContext.GetCookie("Authorization"));
                if (loginSessionModel.Role == "Admin")
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Payments", "Member");
            }

            return View();


        }


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModal signInModal)
        {

            if (ModelState.IsValid == false)
            {
                return View(signInModal).ShowMessage(Status.BadRequest, "Uyarı", "Eksik veya hatalı bilgiler mevcut!");
            }
            string jsonData = JsonConvert.SerializeObject(signInModal);
            var result = await MyHttpCommand("POST", jsonData, "Users/SignIn");
            if (result.Success)
            {

                var jsondata = result.Message;
                var signInViewModels = JsonConvert.DeserializeObject<SignInViewModel>(jsondata);

                var loginSessionModel = new LoginSessionModel
                {
                    Id = signInViewModels.UserListModel.Id,
                    JwtToken = "Bearer " + signInViewModels.JwtToken,
                    Role = signInViewModels.UserListModel.Roles.FirstOrDefault()
                };

                HttpContext.SetCookie("Authorization", JsonConvert.SerializeObject(loginSessionModel), TimeSpan.FromDays(1));

                if (loginSessionModel.Role == "Admin")
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Payments", "Member");
            }

            return View(signInModal).ShowMessage(Status.Error, "Hata", result.Message);
        }

        #endregion

        #region Update


        [ControlLogin(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return RedirectToAction().ShowMessage(Status.Error, "Uyarı", "Talep hatalı lütfen menüleri kullanarak yeniden deneyiniz!");

            var result = await MyHttpGet("Get", $"Users/{id.Value}");
            var user = JsonConvert.DeserializeObject<UserUpdateModel>(result.Message);

            return View(user);
        }


        /// <summary>
        /// Kullanıcı güncelleme işlemleri
        /// </summary>
        [ControlLogin(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateModel userUpdateModel)
        {

            if (userUpdateModel == null)
                return RedirectToAction("Index").ShowMessage(Status.Error, "Uyarı", "Güncellenmek istenen blok bulunamadı!");

            if (!ModelState.IsValid)
            {
                 return View(userUpdateModel).ShowMessage(Status.Error, "Error", "Eksik veya hatalı bilgiler mevcut!");
            }

            string jsonData = JsonConvert.SerializeObject(userUpdateModel);
            var result = await MyHttpCommand("PUT", jsonData, $"Users/");
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
        [ControlLogin(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteUserModel deleteUserModel)
        {

            if (deleteUserModel == null)
                return RedirectToAction("Index").ShowMessage(Status.Error, "Uyarı", "Hatalı istekte bulundunuz. Lütfen yeniden deneyiniz!");

            string jsonData = JsonConvert.SerializeObject(deleteUserModel);
            var result = await MyHttpGet("DELETE", $"Users/{deleteUserModel.Id}");
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

        #region SetUserAparatment

        /// <summary>
        /// Kullanıcıya Daire Tanımlaması yapar.
        /// </summary>       
        [ControlLogin(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> SetUserApartment(SetUserApartmentModel setUserApartmentModel)
        {

            if (setUserApartmentModel == null)
            {
                return Json(new JResult
                {
                    Status = Status.BadRequest,
                    Message = "Bilgileri kotnrol ediniz!"
                });

            }

            string jsonData = JsonConvert.SerializeObject(setUserApartmentModel);
            var result = await MyHttpCommand("PUT", jsonData, "Apartments/SetUserApartment");

            if (result.Success)
            {
                return Json(new JResult
                {
                    Status = Status.Ok,
                    Message = result.Message
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
                }); ;

            }
        }

        #endregion

        #region LogOut

        public IActionResult LogOut()
        {

            HttpContext.DeleteCookie("Authorization");
            return RedirectToAction("SignIn");
        }


        #endregion


    }
}
