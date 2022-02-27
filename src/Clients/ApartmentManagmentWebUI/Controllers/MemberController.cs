using ApartmentManagment.MvcHelper.Enums;
using ApartmentManagment.MvcHelper.Extensions;
using ApartmentManagmentWebUI.Filters;
using ApartmentManagmentWebUI.Models.CaseModels;
using ApartmentManagmentWebUI.Models.Messages;
using ApartmentManagmentWebUI.Models.UserModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Controllers
{
    [ControlLogin(Roles = "Member")]
    public class MemberController : CustomController
    {

        public MemberController()
        {

        }

        //Kullanıcı bilgilerini getiren metot
        public async Task<UserListModel> GetUser(int id)
        {
            var result = await MyHttpGet("GET", $"Users/{id}");
            if (result.Success)
            {
                var jsondata = result.Message;
                var user = JsonConvert.DeserializeObject<UserListModel>(jsondata);

                return user;
            }
            return null;
        }


        #region GetUser

        /// <summary>
        /// Kullanıcı bakiye bilgilerini getirerek sayfaya gönderme işlemi
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await GetUser(GetIdFromSession());
            if (user == null)
            {
                return View(new UserListModel { }).ShowMessage(Status.BadRequest, "Hata", "Bilgilerinize ulaşılamadı"); ;
            }

            return View(user);



        }

        #endregion

        #region GetPayments

        public async Task<IActionResult> Payments()
        {
            string jsonData = "";

            var result = await MyHttpGet("Get", $"Cases/{GetIdFromSession()}");
            if (result.Success)
            {
                jsonData = result.Message;
                var aparetmentList = JsonConvert.DeserializeObject<List<CaseListModel>>(jsonData);
                return View(aparetmentList);
            }


            return RedirectToAction("Index", "Home").ShowMessage(Status.Error, "Hata", result.Message);


        }

        #endregion

        #region DeptPayments

        [HttpGet]
        public async Task<IActionResult> DeptPayment()
        {

            var result = await MyHttpGet("GET", $"Users/{GetIdFromSession()}");
            if (result.Success)
            {
                var jsondata = result.Message;
                var user = JsonConvert.DeserializeObject<UserListModel>(jsondata);

                return View(new DeptPaymentModel { Balance = user.Balance });
            }

            return RedirectToAction("Payments").ShowMessage(Status.BadRequest, "Hata", "Bilgilerinize ulaşılamadı");
        }

        /// <summary>
        /// Kullanıcı borç ödeme işlemleri
        /// </summary>
        /// <param name="deptPaymentModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeptPayment(DeptPaymentModel deptPaymentModel)
        {
            if (!ModelState.IsValid)
            {
                return View(deptPaymentModel).ShowMessage(Status.Error, "Error", "Eksik veya hatalı bilgiler mevcut!");
            }

            deptPaymentModel.UserId = GetIdFromSession();
            deptPaymentModel.Type = PaymentType.Gelir;

            string jsonData = JsonConvert.SerializeObject(deptPaymentModel);

            var result = await MyHttpCommand("POST", jsonData, $"ApartmentExpenses/Payment");
            if (result.Success)
            {
                return RedirectToAction("Index").ShowMessage(Status.Ok, "Başarılı", result.Message);
            }
            else
            {

                return RedirectToAction("Index").ShowMessage(Status.Error, "Hata", result.Message);

            }
        }

        #endregion

        #region Messages

        [HttpGet]
        public IActionResult SendMessage()
        {
            return View();
        }

        /// <summary>
        /// Yönetime mesaj gönderme işlemleri
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> SendMessage(AddMessageModel addMessageModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addMessageModel).ShowMessage(Status.Error, "Error", "Eksik veya hatalı bilgiler mevcut!");
            }

            addMessageModel.UserId = GetIdFromSession();

            string jsonData = JsonConvert.SerializeObject(addMessageModel);

            var result = await MyHttpCommand("POST", jsonData, $"Messages");
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

        //Giriş yapan kullanıcının Id'sini getiren metot
        public int GetIdFromSession()
        {
            var model = JsonConvert.DeserializeObject<LoginSessionModel>(HttpContext.GetCookie("Authorization"));
            return model.Id;
        }
    }
}