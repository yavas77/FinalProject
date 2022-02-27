using AutoMapper;
using Building.Domain.Entities.Authentications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Queries.Authentications.GetUsers
{
    public class GetUserByEmailAndPasswordQueryHandler : IRequestHandler<GetUserByEmailAndPasswordQuery, UserListModel>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;

        public GetUserByEmailAndPasswordQueryHandler(UserManager<User> userManager,
                 RoleManager<Role> roleManager,
                 IMapper mapper
                 )
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;

        }
        public async Task<UserListModel> Handle(GetUserByEmailAndPasswordQuery request, CancellationToken cancellationToken)
        {
            var userInDb = _userManager.Users.SingleOrDefault(x => x.Email == request.Email && x.IsDelete == true && x.IsActive == true);
            if (userInDb is null)
            {
                return null;
            }

            var userModel = _mapper.Map<UserListModel>(userInDb);

            var userSigninResult = await _userManager.CheckPasswordAsync(userInDb, request.Password);

            if (userSigninResult)
            {
                userModel.Roles = await _userManager.GetRolesAsync(userInDb);
                return userModel;
            }

            return null;
        }
    }
}
