using Building.Application.Model.Common;
using MediatR;

namespace Building.Application.Features.Commands.Buildings.SetUserApartment
{
	public class SetUserApartmentCommand : IRequest<EntityResult>
    {
        public int UserId { get; set; }
        public int ApartmentId { get; set; }
    }
}
