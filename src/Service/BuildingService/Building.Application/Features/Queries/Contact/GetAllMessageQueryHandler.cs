using AutoMapper;
using Building.Application.Services.Messages;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Queries.Contact
{
    public class GetAllMessageQueryHandler : IRequestHandler<GetAllMessageQuery, List<MessageListModel>>
    {
        #region Properties

        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public GetAllMessageQueryHandler(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        public async Task<List<MessageListModel>> Handle(GetAllMessageQuery request, CancellationToken cancellationToken)
        {

            var messageList = await _messageService.GetAllAsync(null, tracking: true, "User");
            if (messageList is null)
            {
                return null;
            }

            return _mapper.Map<List<MessageListModel>>(messageList);
        }

        #endregion
    }
}
