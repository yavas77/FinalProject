using Building.Application.Features.Commands.Authentications.DeleteUser;
using Building.Application.Features.Commands.Authentications.RegisterUser;
using Building.Application.Features.Commands.Authentications.UpdateUser;
using Building.Application.Features.Queries.Authentications.GetUsers;
using Building.Application.Model.Authentications;
using Building.Application.Model.Common;
using Building.Application.Model.Settings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Building.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly JwtSetting _jwtSetting;
        public UsersController(IMediator mediator,
            IOptionsSnapshot<JwtSetting> jwtSettings)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _jwtSetting = jwtSettings.Value;
        }

        #region Get

        /// <summary>
        /// Sistemde kayıtlı kullanıcıların listesini getirir.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetUserListQuery();
            var userList = await _mediator.Send(query);
            return Ok(userList);
        }

        /// <summary>
        /// Id'ye göre kullanıcı getirir.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetUserByIdQuery(id);
            var user = await _mediator.Send(query);
            return Ok(user);
        }


        /// <summary>
        /// Apartman tanımlanması yapılmış kullanıcı listesini getirir
        /// </summary>
        [HttpGet("GetUsersWhoseApartmentIsNotEmpty")]
        public async Task<IActionResult> GetUsersWhoseApartmentIsNotEmpty()
        {
            var query = new GetUsersWhoseApartmentIsNotEmptyQuery();
            var userList = await _mediator.Send(query);
            return Ok(userList);
        }

        #endregion

        #region Add

        /// <summary>
        /// Yeni kullanıcı ekler
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserCommand registerUserCommand)
        {

            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(registerUserCommand);

                if (result != null)
                {
                    return Created("", result);
                }

                return BadRequest(result);
            }

            return BadRequest(new EntityResult { Success = false, Errors = new List<string> { "" } });
        }
        #endregion

        #region Update

        /// <summary>
        /// Kullanıcı bilgilerini günceller.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserCommand updateUserCommand)
        {

            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(updateUserCommand);

                if (result != null)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest(new EntityResult { Success = false, Errors = new List<string> { "" } });

        }

        #endregion

        #region Delete

        /// <summary>
        /// Kullanıcı silme işlemi.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new DeleteUserCommand { Id = id });

                if (result != null)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest(new EntityResult { Success = false, Errors = new List<string> { "" } });

        }

        #endregion

        #region SignIn_And_JwtTokenGnerate

        /// <summary>
        /// Giriş işlemlerini yapmak için bilgileri kotrol eder.
        /// </summary>
        /// <param name="signInModal"></param>
        /// <returns>Bilgiler doğru olması halinde Jwt Token ve kullanıcı bilgilerini döner.</returns>
        [HttpPost("SignIn")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(SignInModal signInModal)
        {
            var query = new GetUserByEmailAndPasswordQuery(signInModal.Email, signInModal.Password);
            var userModel = await _mediator.Send(query);
            if (userModel != null)
            {
                var signInModel = new SignInModelView
                {
                    UserListModel = userModel,
                    JwtToken = GenerateJwt(userModel)
                };

                if (userModel != null)
                {
                    return Ok(new EntityResult { Success = true, Message = JsonConvert.SerializeObject(signInModel) });
                }
            }


            return BadRequest(new EntityResult { Success = false, Message = "Kullanıcı adı veya şifre hatalı" });
        }


        private string GenerateJwt(UserListModel userListModel)
        {
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userListModel.Id.ToString()),
                    new Claim(ClaimTypes.Name, userListModel.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, userListModel.Id.ToString())
                };

            var roleClaims = userListModel.Roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSetting.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion
    }
}