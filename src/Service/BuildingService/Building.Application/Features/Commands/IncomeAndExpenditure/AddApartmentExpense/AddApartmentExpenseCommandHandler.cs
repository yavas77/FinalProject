using AutoMapper;
using Building.Application.Features.Commands.Authentications.UpdateUser;
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
    public class AddApartmentExpenseCommandHandler : IRequestHandler<AddApartmentExpenseCommand, EntityResult>
    {
        #region Properties

        private readonly UserManager<User> _userManager;
        private readonly IApartmentExpenseService _apartmentExpenseService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AddApartmentExpenseCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Constructor

        public AddApartmentExpenseCommandHandler(UserManager<User> userManager, IApartmentExpenseService apartmentExpenseService, IMapper mapper, IMediator mediator)
        {
            _userManager = userManager;
            _apartmentExpenseService = apartmentExpenseService;
            _mapper = mapper;
            _mediator = mediator;
        }

        #endregion

        #region Methods

        public async Task<EntityResult> Handle(AddApartmentExpenseCommand request, CancellationToken cancellationToken)
        {
            var entityApartmentExpense = _mapper.Map<ApartmentExpense>(request);
            //decimal oldAmonut = entityApartmentExpense.Amount;

            var result = await _apartmentExpenseService.AddAsync(entityApartmentExpense);

            if (result > 0)
            {

                //Eklenen fatura tutarının kullanıyca borç olarak eklenme işlemi
                var userInDb = await _userManager.FindByIdAsync(request.UserId.ToString());
                decimal oldAmonut = userInDb.Balance;
                if (userInDb != null)
                {

                    var updateUserCommand = _mapper.Map<UpdateUserCommand>(userInDb);
                    updateUserCommand.Balance = oldAmonut - request.Amount;

                    await _mediator.Send(updateUserCommand);
                }


                var entityResult = new EntityResult
                {
                    Message = "İşlem başarıyla gerçekleşti!",
                    Success = true
                };
                return entityResult;
            }
            else
            {
                var entityResult = new EntityResult
                {
                    Message = "Hata oluştu!",
                    Success = false,
                    Errors = new List<string> { "İşlem esnansında hata oluştu! Lütfen yeniden deneyiniz." }
                };
                return entityResult;

            }
        }

        #endregion
    }
}
