using AutoMapper;
using Building.Application.Features.Commands.Buildings.UpdateApartment;
using Building.Application.Features.Queries.Buildings.GetApartments;
using Building.Application.Features.Queries.Buildings.GetBlocks;
using Building.Application.Model.Common;
using Building.Domain.Entities.Authentications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Commands.Authentications.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, EntityResult>
    {
        #region Properties

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Constructor

        public UpdateUserCommandHandler(
                IMapper mapper,
                UserManager<User> userManager,
                RoleManager<Role> roleManager,
                IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userManager = userManager;
            _roleManager = roleManager;

        }

        #endregion

        #region Methods

        public async Task<EntityResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            #region User_Checking_In_Database

            var userInDb = await _userManager.FindByIdAsync(request.Id.ToString());

            if (userInDb == null)
            {
                var entityResult = new EntityResult
                {
                    Success = false,
                    Message = "İşlem başarısız!",
                    Errors = new List<string>
                    {
                        "Kullanıcı bulunamadı!"
                    }
                };

                return entityResult;
            }

            #endregion

            #region New_Value

            userInDb.Email = request.Email != default ? request.Email : userInDb.Email;
            userInDb.UserName = request.Email != default ? request.Email : userInDb.Email;
            userInDb.FirstName = request.FirstName != default ? request.FirstName : userInDb.FirstName;
            userInDb.LastName = request.LastName != default ? request.LastName : userInDb.LastName;
            userInDb.PhoneNumber = request.PhoneNumber != default ? request.PhoneNumber : userInDb.PhoneNumber;
            userInDb.Plate = request.Plate != default ? request.Plate : userInDb.Plate;
            userInDb.ApartmentId = request.ApartmentId != default ? request.ApartmentId : userInDb.ApartmentId;
            userInDb.Balance = request.Balance != default ? request.Balance : userInDb.Balance;

            #endregion


            #region Updated_And_Return_Result

            var result = await _userManager.UpdateAsync(userInDb);

            if (result.Succeeded)
            {

                var entityResult = new EntityResult
                {
                    Success = true,
                    Message = "İşlem başarıyla gerçekleşti."
                };

                return entityResult;
            }

            return new EntityResult { Success = false, Message = "İşlem başarısız!", Errors = new List<string> { "Kullanıcı güncellenemedi!" } };

            #endregion
        }

        #endregion
    }
}
