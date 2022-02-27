using AutoMapper;
using Building.Application.Features.Commands.Buildings.UpdateApartment;
using Building.Application.Model.Common;
using Building.Application.Services.Buildings;
using Building.Domain.Entities.Authentications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Commands.Buildings.SetUserApartment
{
    public class SetUserApartmentCommandHandler : IRequestHandler<SetUserApartmentCommand, EntityResult>
    {
        #region Properties

        private readonly UserManager<User> _userManager;
        private readonly IApartmentService _apartmentService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Constructor

        public SetUserApartmentCommandHandler(UserManager<User> userManager, IMediator mediator, IApartmentService apartmentService, IMapper mapper)
        {
            _userManager = userManager;
            _apartmentService = apartmentService;
            _mapper = mapper;
            _mediator = mediator;
        }

        #endregion

        #region Methods

        public async Task<EntityResult> Handle(SetUserApartmentCommand request, CancellationToken cancellationToken)
        {


            //Apartman durumunun boş olup olmadığının kontrol işlemi
            #region Is_Apartment_Empty_Cheking

            var apartmentInDb = await _apartmentService.GetAsync(x => x.Id == request.ApartmentId && x.IsDelete == true);

            if (apartmentInDb != null)
            {
                //Daire dolu ise yapılacak işlemler
                if (apartmentInDb.Status == false)
                {
                    var entityResult = new EntityResult
                    {
                        Success = false,
                        Message = "İşlem başarısız!",
                        Errors = new List<string>
                        {
                            "Daire zaten dolu! Lütfen kontrol ediniz.",
                        }
                    };
                    return entityResult;
                }
            }
            else
            {
                //Daire veritabanında bulunamadı ise yapılacak işlemler
                var entityResult = new EntityResult
                {
                    Success = false,
                    Message = "İşlem başarısız!",
                    Errors = new List<string>
                    {
                      "Daire bulunamadı! Lütfen kontrol ediniz.",
                    }
                };
                return entityResult;
            }
            #endregion

            //Daire durumu boş ise Kullanıcıya daire atama işlemlerinin yapılması
            #region Updated_And_Return_Result

            var userInDb = await _userManager.FindByIdAsync(request.UserId.ToString());
            int? oldApartmentId = userInDb.ApartmentId;


            userInDb.ApartmentId = request.ApartmentId;

            var result = await _userManager.UpdateAsync(userInDb);

            if (result.Succeeded)
            {

                //Daire Durum bilgisininin değiştirilme işlemleri
                #region Changed_Apartment_Status

                //Yeni tanımlanan apartman durumunu dolu yapma işlemi
                apartmentInDb.Status = false;
                var updateApartmentCommand = _mapper.Map<UpdateApartmentCommand>(apartmentInDb);
                await _mediator.Send(updateApartmentCommand);


                //Kullanıcının mevcut daire durumu null değilse eski dairesinin durumunu boş olarak düzeltem işlemi
                if (oldApartmentId != null)
                {                   
                    var oldUserApertment = await _apartmentService.GetByIdAsync(oldApartmentId.Value);
                    oldUserApertment.Status = true;
                    updateApartmentCommand = _mapper.Map<UpdateApartmentCommand>(oldUserApertment);
                    await _mediator.Send(updateApartmentCommand);
                }
               


                #endregion


                var entityResult = new EntityResult
                {
                    Success = true,
                    Message = "İşlem başarıyla gerçekleşti."
                };

                return entityResult;
            }



            //Kullanıcı bilgileri güncellenemez ise yapılacak işlemler

            return new EntityResult
            {
                Success = false,
                Message = "Hata oluştu!",
                Errors = new List<string> { "Kullanıcıya daire tanımlaması yapılamadı!" }

            };

            #endregion
        }

        #endregion

    }
}
