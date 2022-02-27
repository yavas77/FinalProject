using MediatR;
using System.Collections.Generic;

namespace Building.Application.Features.Queries.Authentications.GetUsers
{
    public class GetUserListQuery : IRequest<List<UserListModel>>
    {
        public GetUserListQuery()
        {

        }
    }
}
