using AutoMapper;
using Building.Application.Features.Commands.Authentications.UpdateUser;
using Building.Application.Model.Common;
using Building.Application.Services.IncomeAndExpenditure;
using Building.Domain.Entities.Authentications;
using Building.Domain.Entities.IncomeAndExpenditure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Commands.IncomeAndExpenditure.DeleteApartmentExpense
{
    public class DeleteApartmentExpenseCommandHandler : IRequestHandler<DeleteApartmentExpenseCommand, EntityResult>
    {
        #region Properties

        private readonly UserManager<User> _userManager;
        private readonly IApartmentExpenseService _apartmentExpenseService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Constructor

        public DeleteApartmentExpenseCommandHandler(UserManager<User> userManager, IApartmentExpenseService apartmentExpenseService, IMapper mapper, IMediator mediator)
        {
            _apartmentExpenseService = apartmentExpenseService;
            _mapper = mapper;
            _mediator = mediator;
            _userManager = userManager;
        }

        #endregion

        #region Methods

        public async Task<EntityResult> Handle(DeleteApartmentExpenseCommand request, CancellationToken cancellationToken)
        {

            //Veritabanında kayıt olup olmadığının kontrol edilmesi
            var entityInDb = await _apartmentExpenseService.GetAsync(x => x.Id == request.Id && x.IsDelete == true);

            if (entityInDb == null)
            {
                var entityResult = new EntityResult { Message = "Silinmek istenen {Fatura} bulunamadı!", Success = true };
                return entityResult;
            }


            var entityApartmentExpense = _mapper.Map<ApartmentExpense>(request);


            var result = await _apartmentExpenseService.RemoveAsync(entityApartmentExpense);


            if (result > 0)
            {
                //Eklenen fatura tutarının kullanıyca borç olarak eklenme işlemi
                var userInDb = await _userManager.FindByIdAsync(entityInDb.UserId.ToString());
                if (userInDb != null)
                {

                    var updateUserCommand = _mapper.Map<UpdateUserCommand>(userInDb);
                    updateUserCommand.Balance += entityInDb.Amount;

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