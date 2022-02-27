using Building.Application.Features.Queries.IncomeAndExpenditure.GetCase;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Building.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    [Authorize]
    public class CasesController : ControllerBase
	{

		private readonly IMediator _mediator;

        public CasesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #region Get

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCaseQuery();
            var caseList = await _mediator.Send(query);
            return Ok(caseList);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(int userId)
        {
            var query = new GetCaseByUserIdQuery(userId);
            var caseList = await _mediator.Send(query);
            return Ok(caseList);
        }

        #endregion

    }
}
