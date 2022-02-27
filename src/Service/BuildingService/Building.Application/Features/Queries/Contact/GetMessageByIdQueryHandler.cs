using AutoMapper;
using Building.Application.Features.Commands.Contact.UpadetMessage;
using Building.Application.Services.Messages;
using Building.Domain.Entities.Contact;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Queries.Contact
{
    public class GetMessageByIdQueryHandler : IRequestHandler<GetMessageByIdQuery, MessageListModel>
    {
        #region Properties

        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Constructor

        public GetMessageByIdQueryHandler(IMessageService messageService, IMapper mapper, IMediator mediator)
        {
            _messageService = messageService;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator;
        }


        #endregion

        #region Methods

        public async Task<MessageListModel> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
        {
            var message = await _messageService.GetAsync(x => x.Id == request.Id, tracking: true, "User");
            if (message is null)
            {
                return null;
            }

            var updateMessageCommand = _mapper.Map<UpdateMessageCommand>(message);
            updateMessageCommand.Status = MessageStatus.Okunmus;

            var result = await _mediator.Send(updateMessageCommand);

            return _mapper.Map<MessageListModel>(message);
        }

        #endregion
    }
}
