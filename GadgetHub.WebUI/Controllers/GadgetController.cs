using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.WebUI.Models;
using GadgetHub.Domain.Entities;

namespace GadgetHub.WebUI.Controllers
{
    public class GadgetController : Controller
    {
		private IGadgetRepository myrepository;

		public GadgetController(IGadgetRepository gadgetRepository)
		{
			this.myrepository = gadgetRepository;
		}

		public int PageSize = 2;
		public ViewResult List(string category, int page = 1)
		{
			ProductsListViewModel model = new ProductsListViewModel
			{
				Gadgets = myrepository.Gadgets
				.Where(p => category == null || p.Category == category)
				.OrderBy(p => p.GadgetId)
				.Skip((page - 1) * PageSize)
				.Take(PageSize),

				PagingInfo = new PagingInfo
				{
					CurrentPage = page,
					ItemsPerPage = PageSize,
					// TotalItems = myrepository.Gadgets.Count()
					TotalItems = category == null ?
					myrepository.Gadgets.Count() : myrepository.Gadgets.Where(e => e.Category == category).Count()
				},
				CurrentCategory = category
			};

			return View(model);
			//return View(myrepository.Gadgets.OrderBy(p => p.GadgetId)
			//	.Skip((page -1) * PageSize)
			//	.Take(PageSize));
		}

		public FileContentResult GetImage(int gadgetId)
		{
			Gadgets gad = myrepository.Gadgets.FirstOrDefault(g => g.GadgetId == gadgetId);

			if (gad != null)
			{
				return File(gad.ImageData, gad.ImageMimeType);
			}

			else
			{
				return null;
			}
		}
	}
}