using Building.Application.Model.Common;
using MediatR;

namespace Building.Application.Features.Commands.Buildings.AddBlock
{
    public class AddBlockCommand:IRequest<EntityResult>
    {
        public string Name { get; set; }
        public int Floor { get; set; }
    }
}
