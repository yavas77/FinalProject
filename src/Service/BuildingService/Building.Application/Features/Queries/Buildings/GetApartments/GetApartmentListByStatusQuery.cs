using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Application.Features.Queries.Buildings.GetApartments
{
    public class GetApartmentListByStatusQuery : IRequest<List<ApartmentListModel>>
    {
        public GetApartmentListByStatusQuery()
        {
        }
    }
}
