using MediatR;
using System.Collections.Generic;

namespace Building.Application.Features.Queries.Buildings.GetApartments
{
    public class GetApartmentListQuery : IRequest<List<ApartmentListModel>>
    {
        public GetApartmentListQuery()
        {

        }
    }
}