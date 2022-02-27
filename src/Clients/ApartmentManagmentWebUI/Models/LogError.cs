using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Models
{
	public class LogError
	{
        public int StatusCode { get; set; }
        public string Url { get; set; }
        public string Message { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime LastUpdate { get; set; }
	}
}
