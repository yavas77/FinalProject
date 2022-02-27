using Bank.Api.DbContext;
using Bank.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;

namespace Bank.Api.Controllers
{



    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        IMongoDatabase _database;
        public PaymentsController()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://oyavas:663763@sitemanagement.hp3mm.mongodb.net/SiteManagementDB?retryWrites=true&w=majority");
            var client = new MongoClient(settings);
            _database = client.GetDatabase("SiteManagementDB");
        }

        [HttpPost]
        public IActionResult Payment(IncomingPayment incomingPayment)
        {
            var cardInfoCollection = _database.GetCollection<CardInfo>("CardInfo");
            var paymentCollection = _database.GetCollection<Payments>("Payments");

            var card = cardInfoCollection.Find(x =>
                   x.CardNumber == incomingPayment.CardNumber
                   && x.Year == incomingPayment.Year
                   && x.Month == incomingPayment.Month
                   && x.FullName == incomingPayment.FullName
                   && x.SecurityNumber == incomingPayment.SecurityNumber
                                   ).FirstOrDefault();

            if (card == null)
                return BadRequest("Kart bilgileri hatalı!");

            if (incomingPayment.Amount > card.Balance)
                return BadRequest("Bakiye yetersiz!");


            try
            {
                var payment = new Payments
                {
                    Amount = incomingPayment.Amount,
                    Date = DateTime.Now,
                    incomingPayment = incomingPayment
                };


                paymentCollection.InsertOne(payment);

                return Ok("Ödeme başarıyla gerçekleşti.");

            }
            catch (Exception ex)
            {
                return BadRequest($"InsertOne {ex.Message}");

            }
        }

        [HttpPost("AddCard")]
        public IActionResult AddCard(CardInfo  cardInfo)
        {
            var cardInfoCollection = _database.GetCollection<CardInfo>("CardInfo");
        
            try
            {

                cardInfoCollection.InsertOne(cardInfo);

                return Ok("Cart başarıyla oluşturuldu!");

            }
            catch (Exception ex)
            {
                return BadRequest($"InsertOne {ex.Message}");

            }
        }




    }
}
