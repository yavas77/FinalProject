using Building.Application.Model.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Application.Features.Commands.Buildings.DeleteApartment
{
    public class DeleteApartmentCommand : IRequest<EntityResult>
    {
        public int Id { get; set; }
    }
}
