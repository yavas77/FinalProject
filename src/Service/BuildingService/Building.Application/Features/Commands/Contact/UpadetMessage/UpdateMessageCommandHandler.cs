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

namespace Building.Application.Features.Commands.Contact.UpadetMessage
{
    public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, EntityResult>
    {
        #region Properties

        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public UpdateMessageCommandHandler(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        public async Task<EntityResult> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {


            var entityInDb = await _messageService.GetAsync(x => x.Id == request.Id && x.IsDelete == true);

            if (entityInDb == null)
            {
                var entityResult = new EntityResult { Message = "Güncellenmek istenen Blok bulunamadı!", Success = false };
                return entityResult;
            }


            var entityMessage = _mapper.Map<Message>(request);



            var result = await _messageService.UpdateAsync(entityMessage);

            if (result > 0)
            {

                var entityResult = new EntityResult { Message = "İşlem başarıyla gerçekleşti.", Success = true };
                return entityResult;
            }
            else
            {
                var entityResult = new EntityResult { Message = "İşlem esnansında hata oluştu! Lütfen yeniden deneyiniz.", Success = false };
                return entityResult;

            }
        }

        #endregion
    }
}
