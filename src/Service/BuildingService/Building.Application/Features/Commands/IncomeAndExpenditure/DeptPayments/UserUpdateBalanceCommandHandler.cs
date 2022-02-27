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

namespace Building.Application.Features.Commands.IncomeAndExpenditure.DeptPayments
{
    public class UserUpdateBalanceCommandHandler : IRequestHandler<UserUpdateBalanceCommand, bool>
    {
        #region Properties

        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;



        #endregion

        #region Constructor

        public UserUpdateBalanceCommandHandler(UserManager<User> userManager, IMapper mapper, IMediator mediator)
        {
            _userManager = userManager;
            _mapper = mapper;
            _mediator = mediator;
        }

        #endregion



        #region Methods

        public async Task<bool> Handle(UserUpdateBalanceCommand request, CancellationToken cancellationToken)
        {
            var userInDb = await _userManager.FindByIdAsync(request.Id.ToString());
            userInDb.Balance += request.Amounth;

            var result = await _userManager.UpdateAsync(userInDb);

            return result.Succeeded;
        }

        #endregion
    }
}