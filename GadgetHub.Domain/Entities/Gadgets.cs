using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GadgetHub.Domain.Entities
{
	public class Gadgets
	{
		[HiddenInput(DisplayValue = false)]
		[Key] public int GadgetId { get; set; }
		//public int GadgetId { get; set; }

		[Required(ErrorMessage = "Please enter a gadget name")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please enter a gadget brand")]
		public string Brand { get; set; }

		[Required]
		[Range(0.01, double.MaxValue, ErrorMessage = "Please enter a price")]
		public decimal Price { get; set; }

		[DataType(DataType.MultilineText)]
		[Required(ErrorMessage = "Please enter a gadget description")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Please specify a gadget category")]
		public string Category { get; set; }

		public byte[] ImageData { get; set; }
		public string ImageMimeType { get; set; }
	}
}
