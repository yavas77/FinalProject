using Building.Application.Model.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Application.Features.Commands.Buildings.UpdateBlock
{
    public class UpdateBlockCommand : IRequest<EntityResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        public UpdateBlockCommand()
        {
            IsActive = true;
            IsDelete = true;
        }
    }
}
