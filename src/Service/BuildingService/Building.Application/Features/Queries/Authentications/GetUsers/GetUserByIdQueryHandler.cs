using AutoMapper;
using Building.Domain.Entities.Authentications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Queries.Authentications.GetUsers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserListModel>
    {

        #region Properties

        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public GetUserByIdQueryHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        #endregion


        #region Methods

        public async Task<UserListModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {

            var userList = await _userManager.Users.Include(x => x.Apartment).Include(x => x.Apartment.Block).FirstOrDefaultAsync(x => x.Id == request.Id && x.IsDelete == true);

            if (userList is null)
            {
                return null;
            }


            return _mapper.Map<UserListModel>(userList);
        }

        #endregion
    }
}
