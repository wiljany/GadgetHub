using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.WebUI.Models;

namespace GadgetHub.WebUI.Controllers
{
    public class GadgetController : Controller
    {
		private IGadgetRepository myrepository;

		public GadgetController(IGadgetRepository gadgetRepository)
		{
			this.myrepository = gadgetRepository;
		}

		public int PageSize = 4;
		public ViewResult List(int page = 1)
		{
			ProductsListViewModel model = new ProductsListViewModel
			{
				Gadgets = myrepository.Gadgets.OrderBy(p => p.GadgetId)
				.Skip((page - 1) * PageSize)
				.Take(PageSize),

				PagingInfo = new PagingInfo
				{
					CurrentPage = page,
					ItemsPerPage = PageSize,
					TotalItems = myrepository.Gadgets.Count()
				}
			};

			return View(model);
			//return View(myrepository.Gadgets.OrderBy(p => p.GadgetId)
			//	.Skip((page -1) * PageSize)
			//	.Take(PageSize));
		}
	}
}