using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;

namespace GadgetHub.Domain.Concrete
{
	public class EFGadgetRepository : IGadgetRepository
	{
		private EFDbContext context = new EFDbContext();
		public IEnumerable<Gadgets> Gadgets
		{
			get { return context.Gadgets; }
		}

		void IGadgetRepository.Savegadget(Gadgets gadgets)
		{
			if (gadgets.GadgetId == 0)
			{
				context.Gadgets.Add(gadgets);
			}
			else
			{
				Gadgets dbEntry = context.Gadgets.Find(gadgets.GadgetId);
				if (dbEntry != null)
				{
					dbEntry.Name = gadgets.Name;
					dbEntry.Description = gadgets.Description;
					dbEntry.Category = gadgets.Category;
					dbEntry.Brand = gadgets.Brand;
					dbEntry.Price = gadgets.Price;
				}
			}
			context.SaveChanges();
		}

		public Gadgets DeleteGadget(int gadgetId)
		{
			Gadgets dbEntry = context.Gadgets.Find(gadgetId);
			if (dbEntry != null)
			{
				context.Gadgets.Remove(dbEntry);
				context.SaveChanges();
			}
			return dbEntry;
		}
	}
}
