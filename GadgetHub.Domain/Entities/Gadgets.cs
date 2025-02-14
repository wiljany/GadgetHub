using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetHub.Domain.Entities
{
	public class Gadgets
	{
		[Key] public int GadgetId { get; set; }
		//public int GadgetId { get; set; }
		public string Name { get; set; }
		public string Brand { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
		public string Category { get; set; }


	}
}
