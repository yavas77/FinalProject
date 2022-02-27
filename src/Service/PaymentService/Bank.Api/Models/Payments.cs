using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Api.Models
{
    public class Payments : BaseModel
    {

        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public IncomingPayment incomingPayment { get; set; }

    }
}
