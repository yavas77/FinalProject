using AutoMapper;
using Building.Application.Features.Commands.Authentications.RegisterUser;
using Building.Application.Features.Commands.Authentications.UpdateUser;
using Building.Application.Features.Commands.Buildings.AddApartment;
using Building.Application.Features.Commands.Buildings.AddBlock;
using Building.Application.Features.Commands.Buildings.UpdateApartment;
using Building.Application.Features.Commands.Buildings.UpdateBlock;
using Building.Application.Features.Commands.IncomeAndExpenditure.AddApartmentExpense;
using Building.Application.Features.Commands.IncomeAndExpenditure.AddCase;
using Building.Application.Features.Commands.IncomeAndExpenditure.DeptPayments;
using Building.Application.Features.Queries.Authentications.GetUsers;
using Building.Application.Features.Queries.Buildings.GetBlocks;
using Building.Application.Features.Queries.IncomeAndExpenditure;
using Building.Domain.Entities.Authentications;
using Building.Domain.Entities.Building;
using Building.Domain.Entities.IncomeAndExpenditure;
using Building.Application.Features.Queries.Buildings.GetApartments;
using Building.Application.Features.Commands.IncomeAndExpenditure.UpdateApartmentExpense;
using Building.Application.Features.Commands.IncomeAndExpenditure.DeleteApartmentExpense;
using Building.Application.Features.Queries.IncomeAndExpenditure.GetCase;
using Building.Application.Features.Commands.Buildings.DeleteApartment;
using Building.Domain.Entities.Contact;
using Building.Application.Features.Commands.Contact.AddMessage;
using Building.Application.Features.Queries.Contact;
using Building.Application.Features.Commands.Contact.UpadetMessage;

namespace Building.Application.Mapping.AutoMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Authentications

            CreateMap<User, RegisterUserCommand>().ReverseMap();
            CreateMap<User, UpdateUserCommand>().ReverseMap();
            CreateMap<User, UserListModel>().ForMember(dto => dto.Apartment,
                map => map.MapFrom(entity => entity.Apartment.Block.Name + " - Daire No : " + entity.Apartment.No)).ReverseMap();

            #endregion

            #region Blocks

            CreateMap<Block, AddBlockCommand>().ReverseMap();
            CreateMap<Block, UpdateBlockCommand>().ReverseMap();
            CreateMap<Block, BlockListModel>().ReverseMap();

            #endregion

            #region Apartments

            CreateMap<Apartment, AddApartmentCommand>().ReverseMap();
            CreateMap<Apartment, UpdateApartmentCommand>().ReverseMap();
            CreateMap<ApartmentListModel, UpdateApartmentCommand>().ReverseMap();
            CreateMap<Apartment, ApartmentListModel>().ForMember(dto => dto.Block,
                map => map.MapFrom(entity => entity.Block.Name))
                .ReverseMap();

            CreateMap<Apartment, DeleteApartmentCommand>().ReverseMap();
            #endregion

            #region ApartmentExpense

            CreateMap<ApartmentExpense, AddApartmentExpenseCommand>().ReverseMap();
            CreateMap<ApartmentExpense, ApartmentExpenseListModel>()
                .ForMember(dto => dto.Apartment,
                     map => map.MapFrom(entity => entity.User.Apartment.Block.Name + " - Daire No: " + entity.User.Apartment.No))
                .ForMember(dto => dto.User,
                     map => map.MapFrom(entity => entity.User.FirstName + " " + entity.User.LastName))
                .ForMember(dto => dto.Status, map => map.MapFrom(dto => dto.Status == PaymentStatus.Beklemede ? "Beklemede" : "Ödendi")).ReverseMap();
            CreateMap<ApartmentExpense, UpdateApartmentExpenseCommand>().ReverseMap();
            CreateMap<ApartmentExpense, DeleteApartmentExpenseCommand>().ReverseMap();

            #endregion

            #region Cases

            CreateMap<Case, AddCaseCommand>().ReverseMap();
            CreateMap<Case, CaseListModel>()
               .ForMember(dto => dto.User,
                     map => map.MapFrom(entity => entity.User.FirstName + " " + entity.User.LastName))
                .ForMember(dto => dto.Type, map => map.MapFrom(entity => entity.Type == PaymentType.Gelir ? PaymentType.Gelir.ToString() : PaymentType.Gider.ToString())).ReverseMap();
            #endregion

            #region DeptPayments

            CreateMap<DeptPaymentCommand, UserUpdateBalanceCommand>().ReverseMap();
            CreateMap<DeptPaymentCommand, AddCaseCommand>().ReverseMap();

            #endregion

            #region Message

            CreateMap<Message, AddMessageCommand>().ReverseMap();
            CreateMap<Message, UpdateMessageCommand>().ReverseMap();
            CreateMap<MessageListModel, UpdateMessageCommand>().ReverseMap();
            CreateMap<Message, MessageListModel>()
                .ForMember(dto => dto.User,
                     map => map.MapFrom(entity => entity.User.FirstName + " " + entity.User.LastName)).ReverseMap();

            #endregion




        }
    }
}
