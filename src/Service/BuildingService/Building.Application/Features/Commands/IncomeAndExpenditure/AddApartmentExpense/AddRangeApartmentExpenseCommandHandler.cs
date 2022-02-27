using AutoMapper;
using Building.Application.Features.Commands.Authentications.UpdateUser;
using Building.Application.Features.Queries.Authentications.GetUsers;
using Building.Application.Model.Common;
using Building.Application.Services.IncomeAndExpenditure;
using Building.Domain.Entities.Authentications;
using Building.Domain.Entities.IncomeAndExpenditure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Commands.IncomeAndExpenditure.AddApartmentExpense
{
    public class AddRangeApartmentExpenseCommandHandler : IRequestHandler<AddRangeApartmentExpenseCommand, EntityResult>
    {
        #region Properties

        private readonly UserManager<User> _userManager;
        private readonly IApartmentExpenseService _apartmentExpenseService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Constructor

        public AddRangeApartmentExpenseCommandHandler(UserManager<User> userManager, IApartmentExpenseService apartmentExpenseService, IMapper mapper, IMediator mediator)
        {
            _userManager = userManager;
            _apartmentExpenseService = apartmentExpenseService;
            _mapper = mapper;
            _mediator = mediator;
        }

        #endregion

        #region Methods

        public async Task<EntityResult> Handle(AddRangeApartmentExpenseCommand request, CancellationToken cancellationToken)
        {
           
            var entityApartmentExpense = _mapper.Map<ApartmentExpense>(request);


            var result = await _apartmentExpenseService.AddAsync(entityApartmentExpense);

            if (result > 0)
            {

                //Eklenen fatura tutarının kullanıyca borç olarak eklenme işlemi
                var query = new GetUsersWhoseApartmentIsNotEmptyQuery();
                var userList = await _mediator.Send(query);

                //if (userInDb != null)
                //{

                //    var updateUserCommand = _mapper.Map<UpdateUserCommand>(userInDb);
                //    updateUserCommand.Balance -= request.Amount;

                //    await _mediator.Send(updateUserCommand);
                //}


                var entityResult = new EntityResult { Message = "İşlem başarıyla gerçekleşti!", Success = true };
                return entityResult;
            }
            else
            {
                var entityResult = new EntityResult { Message = "İşlem esnansında hata oluştu! Lütfen yeniden deneyiniz.", Success = false };
                return entityResult;

            }
        }

        #endregion
    }
}
