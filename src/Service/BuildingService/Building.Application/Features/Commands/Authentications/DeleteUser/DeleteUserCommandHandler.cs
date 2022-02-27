using AutoMapper;
using Building.Application.Features.Commands.Buildings.UpdateApartment;
using Building.Application.Features.Commands.IncomeAndExpenditure.DeleteApartmentExpense;
using Building.Application.Features.Queries.Buildings.GetApartments;
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

namespace Building.Application.Features.Commands.Authentications.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, EntityResult>
    {
        #region Properties

        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public DeleteUserCommandHandler(
                 IMapper mapper,
                 IMediator mediator,
                 UserManager<User> userManager)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userManager = userManager;

        }

        #endregion

        #region Methods

        public async Task<EntityResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {

            //Veritabanında kayıt olup olmadığının kontrol edilmesi
            var userInDb = _userManager.Users.FirstOrDefault(x => x.Id == request.Id && x.IsDelete == true); 

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

            userInDb.IsDelete = false;
            var result = await _userManager.UpdateAsync(userInDb);

            //Kullanıcı silinirse ve bir apartmanda tanımlıysa tanımlı olduğu apartman durumunu boş yapma işlemi
            if (result.Succeeded)
            {
                if (userInDb.Apartment != null)
                {
                    var query = new GetApartmentByIdQuery(userInDb.ApartmentId.Value);
                    var apartment = await _mediator.Send(query);

                    apartment.Status = false;
                    var updateUpartmentCommand = _mapper.Map<UpdateApartmentCommand>(apartment);

                    await _mediator.Send(updateUpartmentCommand);
                }

                var entityResult = new EntityResult
                {
                    Success = true,
                    Message = "İşlem başarıyla gerçekleşti."
                };

                return entityResult;
            }

            return new EntityResult { Success = false, Message = "İşlem başarısız!", Errors = new List<string> { "Kullanıcı silinemedi!" } };

        }

        #endregion
    }
}