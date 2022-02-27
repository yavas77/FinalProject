using AutoMapper;
using Building.Domain.Entities.Authentications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using System.Threading.Tasks;

namespace Building.Application.Features.Queries.Authentications.GetUsers
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, List<UserListModel>>
    {

        #region Properties

        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public GetUserListQueryHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        #endregion


        #region Methods

        public async Task<List<UserListModel>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {

            var userList = await _userManager.Users.Include(x => x.Apartment).Include(x => x.Apartment.Block).Where(x => x.IsDelete == true && x.Id > 1).ToListAsync();

            if (userList is null)
            {
                return null;
            }


            return _mapper.Map<List<UserListModel>>(userList);
        }

        #endregion
    }
}
