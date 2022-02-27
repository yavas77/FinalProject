using AutoMapper;
using Building.Domain.Entities.Authentications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Queries.Authentications.GetUsers
{
    public class GetUsersWhoseApartmentIsNotEmptyQueryHandler : IRequestHandler<GetUsersWhoseApartmentIsNotEmptyQuery, List<UserListModel>>
    {

        #region Properties

        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public GetUsersWhoseApartmentIsNotEmptyQueryHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        #endregion


        #region Methods

        public async Task<List<UserListModel>> Handle(GetUsersWhoseApartmentIsNotEmptyQuery request, CancellationToken cancellationToken)
        {

            var userList = await _userManager.Users.Include(x => x.Apartment).Include(x => x.Apartment.Block).Where(x => x.ApartmentId != null && x.ApartmentId != 0 && x.IsDelete == true && x.IsActive == true && x.Id > 0).ToListAsync();

            if (userList is null)
            {
                return null;
            }


            return _mapper.Map<List<UserListModel>>(userList);
        }

        #endregion
    }
}
