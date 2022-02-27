using Building.Application.Contracts.Persistence.Repositories.Messages;
using Building.Domain.Entities.Contact;
using Building.Infrastructure.Contracts.Persistence.DbSetting;
using Building.Infrastructure.Contracts.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Infrastructure.Contracts.Persistence.Repositories.Messages
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
