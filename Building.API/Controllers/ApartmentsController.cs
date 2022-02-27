using Building.Application.Features.Commands.Buildings.AddApartment;
using Building.Application.Features.Commands.Buildings.DeleteApartment;
using Building.Application.Features.Commands.Buildings.SetUserApartment;
using Building.Application.Features.Commands.Buildings.UpdateApartment;
using Building.Application.Features.Queries.Buildings.GetApartments;
using Building.Application.Model.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Building.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ApartmentsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #region Get

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetApartmentListQuery();
            var apartmentList = await _mediator.Send(query);
            return Ok(apartmentList);
        }

        [HttpGet("GetByState")]
        public async Task<IActionResult> GetByState()
        {
            var query = new GetApartmentListByStatusQuery();
            var apartmentList = await _mediator.Send(query);
            return Ok(apartmentList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetApartmentByIdQuery(id);
            var apartment = await _mediator.Send(query);
            return Ok(apartment);
        }
        #endregion

        #region Add

        [HttpPost]
        public async Task<IActionResult> Add(AddApartmentCommand addApartmentCommand)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(addApartmentCommand);
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

        #region Update

        [HttpPut]
        public async Task<IActionResult> Update(UpdateApartmentCommand updateApartmentCommand)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(updateApartmentCommand);
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

        #region Delete

        /// <summary>
        /// Kullanıcı silme işlemi.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            EntityResult result = null;
            if (ModelState.IsValid)
            {
                result = await _mediator.Send(new DeleteApartmentCommand { Id = id });

                if (result != null)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest(new EntityResult
            {
                Success = false,
                Message = "İşlem başarısız!",
                Errors = new List<string>
                {
                    "Beklenmedik bir hata oluştu!"
                }
            });

        }

        #endregion

        #region SetUserApartment

        [HttpPut("SetUserApartment")]
        public async Task<IActionResult> SetUserApartment(SetUserApartmentCommand setUserApartmentCommand)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(setUserApartmentCommand);
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
    }
}