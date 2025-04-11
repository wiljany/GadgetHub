using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GadgetHub.Domain.Entities;

namespace GadgetHub.Domain.Abstract
{
	public interface IGadgetRepository
	{
		IEnumerable<Gadgets> Gadgets { get; }
		void Savegadget(Gadgets gadgets);

		Gadgets DeleteGadget(int gadgetId);
	}
}
