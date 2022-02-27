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

namespace Building.Application.Features.Commands.IncomeAndExpenditure.UpdateApartmentExpense
{
    public class UpdateApartmentExpenseCommandHandler : IRequestHandler<UpdateApartmentExpenseCommand, EntityResult>
    {
        #region Properties
        private readonly UserManager<User> _userManager;
        private readonly IApartmentExpenseService _apartmentExpenseService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Constructor

        public UpdateApartmentExpenseCommandHandler(UserManager<User> userManager, IApartmentExpenseService apartmentExpenseService, IMapper mapper, IMediator mediator)
        {
            _apartmentExpenseService = apartmentExpenseService;
            _mapper = mapper;
            _mediator = mediator;
            _userManager = userManager;
        }

        #endregion

        #region Methods

        public async Task<EntityResult> Handle(UpdateApartmentExpenseCommand request, CancellationToken cancellationToken)
        {

            //Veritabanında kayıt olup olmadığının kontrol edilmesi
            var entityInDb = await _apartmentExpenseService.GetAsync(x => x.Id == request.Id && x.IsDelete == true);
            decimal oldAmonut = entityInDb.Amount;

            if (entityInDb == null)
            {
                var entityResult = new EntityResult { Message = "Güncellenmek istenen {Fatura} bulunamadı!", Success = true };
                return entityResult;
            }


            var entityApartmentExpense = _mapper.Map<ApartmentExpense>(request);

            var result = await _apartmentExpenseService.UpdateAsync(entityApartmentExpense);


            if (result > 0)
            {

                //Eklenen fatura tutarının kullanıyca borç olarak eklenme işlemi
                var userInDb = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (userInDb != null)
                {

                    var updateUserCommand = _mapper.Map<UpdateUserCommand>(userInDb);
                    updateUserCommand.Balance -=request.Amount- oldAmonut;

                    await _mediator.Send(updateUserCommand);
                }

                var entityResult = new EntityResult { Message = "İşlem başarıyla gerçekleşti.", Success = true };
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