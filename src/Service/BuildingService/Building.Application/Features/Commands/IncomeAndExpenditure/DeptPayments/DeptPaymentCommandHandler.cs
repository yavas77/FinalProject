using AutoMapper;
using Building.Application.Features.Commands.IncomeAndExpenditure.AddCase;
using Building.Application.Model.Common;
using Building.Application.Services.IncomeAndExpenditure;
using Building.Domain.Entities.IncomeAndExpenditure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Commands.IncomeAndExpenditure.DeptPayments
{
    public class DeptPaymentCommandHandler : IRequestHandler<DeptPaymentCommand, EntityResult>
    {
        #region Properties

        private readonly IApartmentExpenseService _apartmentExpenseService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Constructor

        public DeptPaymentCommandHandler(IApartmentExpenseService apartmentExpenseService, IMapper mapper, IMediator mediator)
        {
            _apartmentExpenseService = apartmentExpenseService;
            _mapper = mapper;
            _mediator = mediator;
        }

        #endregion

        #region Methods

        public async Task<EntityResult> Handle(DeptPaymentCommand request, CancellationToken cancellationToken)
        {


            var userUpdateBallanceCommand = new UserUpdateBalanceCommand { Id = request.UserId, Amounth = request.Amount };
            var resultUpdateUser = await _mediator.Send(userUpdateBallanceCommand);


            //Ödenen tutarın kayasa işlenmesi
            var caseAddedCommand = _mapper.Map<AddCaseCommand>(request);          
            var resultCase = await _mediator.Send(caseAddedCommand);


            if (resultUpdateUser)
            {

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
