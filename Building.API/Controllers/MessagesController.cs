using Building.Application.Features.Commands.Contact.AddMessage;
using Building.Application.Features.Queries.Contact;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Building.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {

        private readonly IMediator _mediator;

        public MessagesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #region Insert

        [HttpPost]
        public async Task<IActionResult> Add(AddMessageCommand addMessageCommand)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(addMessageCommand);
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

        #region Get

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllMessageQuery();
            var messageList = await _mediator.Send(query);
            return Ok(messageList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetMessageByIdQuery(id);
            var message = await _mediator.Send(query);
            return Ok(message);
        }



        #endregion
    }
}
