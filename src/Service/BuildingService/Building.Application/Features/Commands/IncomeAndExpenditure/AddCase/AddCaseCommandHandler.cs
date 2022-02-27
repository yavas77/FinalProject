using AutoMapper;
using Building.Application.Model.Common;
using Building.Application.Services.IncomeAndExpenditure;
using Building.Domain.Entities.IncomeAndExpenditure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Commands.IncomeAndExpenditure.AddCase
{
    public class AddCaseCommandHandler : IRequestHandler<AddCaseCommand, EntityResult>
    {
        #region Properties

        private readonly ICaseService _caseService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public AddCaseCommandHandler(ICaseService caseService, IMapper mapper)
        {
            _caseService = caseService;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        public async Task<EntityResult> Handle(AddCaseCommand request, CancellationToken cancellationToken)
        {
            var entityCase = _mapper.Map<Case>(request);


            var result = await _caseService.AddAsync(entityCase);

            if (result > 0)
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
