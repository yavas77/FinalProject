using Building.Application.Features.Commands.IncomeAndExpenditure.AddApartmentExpense;
using Building.Application.Features.Commands.IncomeAndExpenditure.DeleteApartmentExpense;
using Building.Application.Features.Commands.IncomeAndExpenditure.DeptPayments;
using Building.Application.Features.Commands.IncomeAndExpenditure.UpdateApartmentExpense;
using Building.Application.Features.Queries.IncomeAndExpenditure;
using Building.Application.Model.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Building.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApartmentExpensesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApartmentExpensesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #region Get
        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetApartmentExpenseByIdQuery(id);
            var apartmentExpense = await _mediator.Send(query);
            return Ok(apartmentExpense);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var query = new GetApartmentExpenseListByUserIdQuery(userId);
            var apartmentExpenseList = await _mediator.Send(query);
            return Ok(apartmentExpenseList);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetApartmentExpenseGetAllQuery();
            var apartmentExpenseList = await _mediator.Send(query);
            return Ok(apartmentExpenseList);
        }

        #endregion

        #region Post

        [HttpPost("Add")]
        public async Task<IActionResult> Add(AddApartmentExpenseCommand registerUserCommand)
        {

            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(registerUserCommand);

                if (result != null)
                {
                    return Created("", result);
                }

                return BadRequest(result);
            }

            return BadRequest("Tüm alanlar eksiksiz ve tam doldurulmalıdır!");
        }

        #region Update

        [HttpPut]
        public async Task<IActionResult> Update(UpdateApartmentExpenseCommand updateApartmentExpenseCommand)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(updateApartmentExpenseCommand);
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }

            }

            return BadRequest("Tüm alanları eksiksiz ve tam doldurunuz!");
        }

        #endregion

        [HttpPost("Payment")]
        public async Task<IActionResult> Payment(DeptPaymentCommand deptPaymentCommand)
        {

            if (ModelState.IsValid)
            {

                //Banka servisi üzerkinden kard ve bakiye bilgilerinin konrol edilmesi
                #region BankCardInfoControl

                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://localhost:44376/api/Payments"))
                    {
                        request.Headers.TryAddWithoutValidation("accept", "*/*");
                        var jsonData = JsonConvert.SerializeObject(deptPaymentCommand);
                        request.Content = new StringContent(jsonData);
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                        var response = await httpClient.SendAsync(request);

                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            return BadRequest(
                                new EntityResult
                                {
                                    Success = false,
                                    Message = await response.Content.ReadAsStringAsync(),
                                    Errors = new List<string> { "Ödeme işlemi başarısız oldu!"}

                                });
                        }
                    }
                }

                #endregion


                var result = await _mediator.Send(deptPaymentCommand);

                if (result != null)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Tüm alanlar eksiksiz ve tam doldurulmalıdır!");
        }

        #endregion

        #region Delete

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteApartmentExpenseCommand { Id = id });

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("Silme işlemi esnasında hata oluştu!");
        }

        #endregion
    }
}