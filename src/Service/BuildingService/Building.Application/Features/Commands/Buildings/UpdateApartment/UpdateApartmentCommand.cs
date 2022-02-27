using Building.Application.Model.Common;
using MediatR;

namespace Building.Application.Features.Commands.Buildings.UpdateApartment
{
    public class UpdateApartmentCommand : IRequest<EntityResult>
    {
        public int Id { get; set; }
        public string ApartmentType { get; set; }
        public int No { get; set; }
        public bool Status { get; set; }
        public int BlockId { get; set; }
        public bool IsActive { get; set; }       
        public bool IsDelete { get; set; }

        public UpdateApartmentCommand()
        {
            IsActive = true;
            IsDelete = true;
        }
    }
}
