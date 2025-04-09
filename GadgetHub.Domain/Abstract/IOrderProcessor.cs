﻿using GadgetHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetHub.Domain.Abstract
{
	public interface IOrderProcessor
	{
		void processOrder(Cart cart, ShippingDetails shippingDetails); 
	}
}
