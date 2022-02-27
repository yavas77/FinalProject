using AutoMapper;
using Building.Application.Model.Common;
using Building.Domain.Entities.Authentications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Commands.Authentications.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, EntityResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        public RegisterUserCommandHandler(
                 IMapper mapper,
                 UserManager<User> userManager,
                 RoleManager<Role> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;

        }

        public async Task<EntityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            string defaultPassword = "123456";

            var userEntity = _mapper.Map<User>(request);

            userEntity.UserName = request.Email;

            var result = await _userManager.CreateAsync(userEntity, defaultPassword);

            if (result.Succeeded)
            {

                var roleResult = await _userManager.AddToRoleAsync(userEntity, "Member");

                //Kullanıcıya kayıt bilgisi için mail gönderilebilir.
                var entityResult = new EntityResult
                {
                    Success = true,
                    Message = $"Kullanıcı başarıyla oluştu! Varsayılan şifre: {defaultPassword}"
                };
                return entityResult;
            }
            return new EntityResult
            {
                Success = false,
                Message = "İşlem başarısız!",
                Errors = new List<string> { "Kullanıcı oluşturulamadı!" }
            };
        }


    }
}

