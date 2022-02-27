using MediatR;

namespace Building.Application.Features.Queries.Authentications.GetUsers
{
    public class GetUserByIdQuery : IRequest<UserListModel>
    {
        internal int Id { get; set; }
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
