using Building.Application.Contracts.Persistence.Repositories.Commons;
using Building.Application.Services.Messages;
using Building.Domain.Entities.Contact;
using Building.Infrastructure.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Infrastructure.Services.Messages
{
    public class MessageService : BaseService<Message>, IMessageService
    {
        private readonly IBaseRepository<Message> _repository;

        public MessageService(IBaseRepository<Message> repository) : base(repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
    }
}
