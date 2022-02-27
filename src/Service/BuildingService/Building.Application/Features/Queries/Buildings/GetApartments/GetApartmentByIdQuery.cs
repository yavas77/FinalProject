using MediatR;

namespace Building.Application.Features.Queries.Buildings.GetApartments
{
    public class GetApartmentByIdQuery : IRequest<ApartmentListModel>
    {
        public int ApartmentId { get; set; }
        public GetApartmentByIdQuery(int apartmentId)
        {
            ApartmentId = apartmentId;
        }
    }
}
