using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetHub.Domain.Entities
{

	public class Cartline
	{
		public Gadgets Gadgets { get; set; }
		public int Quantity { get; set; }
	}
	public class Cart
	{
		private List<Cartline> lineCollection = new List<Cartline> ();

		public IEnumerable<Cartline> Lines
		{
			get { return lineCollection; }
		}

		public void AddItem(Gadgets myproduct, int myquantity)
		{
			Cartline line = lineCollection
								.Where(g => g.Gadgets.GadgetId == myproduct.GadgetId)
								.FirstOrDefault();
			if (line == null)
			{
				lineCollection.Add(new Cartline
				{
					Gadgets = myproduct,
					Quantity = myquantity
				});
			}
			else
			{
				line.Quantity += myquantity;
			}
		}

		public void RemoveLine(Gadgets mygadgets)
		{
			lineCollection.RemoveAll(l => l.Gadgets.GadgetId == mygadgets.GadgetId);
		}

		public decimal ComputeTotalValue()
		{
			return lineCollection.Sum(e => e.Gadgets.Price * e.Quantity);
		}

		public void Clear()
		{
			lineCollection.Clear();
		}
	}
}
