using Building.Application.Model.Common;
using MediatR;

namespace Building.Application.Features.Commands.Buildings.AddApartment
{
    public class AddApartmentCommand : IRequest<EntityResult>
    {
        public string ApartmentType { get; set; }
        public int No { get; set; }
        public bool Status { get; set; }
        public int BlockId { get; set; }
        public AddApartmentCommand()
        {
            Status = true;
        }
    }
}
