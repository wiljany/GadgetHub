﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GadgetHub.Domain.Entities;

namespace GadgetHub.WebUI.Models
{
	public class ProductsListViewModel
	{
		public IEnumerable<Gadgets> Gadgets { get; set; }
		public PagingInfo PagingInfo { get; set; }
		public string CurrentCategory { get; set; }
	}
}