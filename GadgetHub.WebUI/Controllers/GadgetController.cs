using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;

namespace GadgetHub.WebUI.Controllers
{
    public class GadgetController : Controller
    {
		private IGadgetRepository myrepository;

		public GadgetController(IGadgetRepository gadgetRepository)
		{
			this.myrepository = gadgetRepository;
		}
		
		public ViewResult List()
		{
			return View(myrepository.Gadgets);
		}
	}
}