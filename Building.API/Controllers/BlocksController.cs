using Building.Application.Features.Commands.Buildings.AddBlock;
using Building.Application.Features.Commands.Buildings.UpdateBlock;
using Building.Application.Features.Queries.Buildings.GetBlocks;
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
    public class BlocksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlocksController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #region Get

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetBlockListQuery();
            var blockList = await _mediator.Send(query);
            return Ok(blockList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetBlockByIdQuery(id);
            var block = await _mediator.Send(query);
            return Ok(block);
        }

        #endregion


        #region Insert

        [HttpPost]
        public async Task<IActionResult> Add(AddBlockCommand addBlockCommand)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(addBlockCommand);
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
        public async Task<IActionResult> Update(UpdateBlockCommand updateBlockCommand)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(updateBlockCommand);
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
