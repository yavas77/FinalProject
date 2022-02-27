using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Api.Models
{
    public class IncomingPayment
    {
        public string FullName { get; set; }
        public string CardNumber { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int SecurityNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
