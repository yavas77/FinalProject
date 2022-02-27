using AutoMapper;
using Building.Application.Model.Common;
using Building.Application.Services.Messages;
using Building.Domain.Entities.Contact;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Commands.Contact.AddMessage
{
    public class AddMessageCommandHandler : IRequestHandler<AddMessageCommand, EntityResult>
    {
        #region Properties

        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public AddMessageCommandHandler(IMessageService messageervice, IMapper mapper)
        {
            _messageService = messageervice;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        public async Task<EntityResult> Handle(AddMessageCommand request, CancellationToken cancellationToken)
        {
            var entityMessage = _mapper.Map<Message>(request);



            entityMessage.Status = MessageStatus.Okunmamis;
            var result = await _messageService.AddAsync(entityMessage);

            if (result > 0)
            {

                var entityResult = new EntityResult { Message = "İşlem başarıyla gerçekleşti!", Success = true };
                return entityResult;
            }
            else
            {
                var entityResult = new EntityResult
                {
                    Message = "Hata oluştu",
                    Success = false,
                    Errors = new List<string> { "İşlem esnansında hata oluştu! Lütfen yeniden deneyiniz." }
                };
                return entityResult;

            }
        }

        #endregion

    }
}
