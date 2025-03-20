using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetHub.Domain.Entities
{
	public class ShippingDetails
	{
		[Required(ErrorMessage = "Please enter a name")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please enter an address")]
		public string Address { get; set; }
		[Display(Name = "Secondary Address")]
		public string SecondaryAddress { get; set; }
		[Display(Name = "Third Address")]
		public string ThirdAddress { get; set; }

		[Required(ErrorMessage = "Please enter a city")]
		public string City { get; set; }

		[Required(ErrorMessage = "Please enter a state")]
		public string State { get; set; }
		public string Zip { get; set; }

		[Required(ErrorMessage = "Please enter a country")]
		public string Country { get; set; }

		public bool GiftWrap { get; set; }
	}
}
