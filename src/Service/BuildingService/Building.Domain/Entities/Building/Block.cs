using Building.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Domain.Entities.Building
{
    public class Block : BaseEntity
    {
        public string Name { get; set; }
        public int Floor { get; set; }

        #region NavigationProperties
        public ICollection<Apartment> Apartments { get; set; }
        #endregion

        #region Constructor
        public Block()
        {
            Apartments = new HashSet<Apartment>();
        }
        #endregion
    }
}
