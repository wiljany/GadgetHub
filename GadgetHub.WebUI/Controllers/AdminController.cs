using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;

namespace GadgetHub.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IGadgetRepository repository;

        public AdminController(IGadgetRepository repo)
        {
            this.repository = repo;
        }
        public ActionResult Index()
        {
            return View(repository.Gadgets);
        }

        public ViewResult Edit(int gadgetId)
        {
            Gadgets gadgets = repository.Gadgets.FirstOrDefault(g => g.GadgetId == gadgetId);
            return View(gadgets);
        }
        [HttpPost]
        public ActionResult Edit(Gadgets gadgets, HttpPostedFileBase image=null)
        {
            if (ModelState.IsValid)
            {
				if (image != null)
				{
					gadgets.ImageMimeType = image.ContentType;
					gadgets.ImageData = new byte[image.ContentLength];
					image.InputStream.Read(gadgets.ImageData, 0, image.ContentLength);
				}

				repository.Savegadget(gadgets);
                TempData["message"] = string.Format("{0} has been saved", gadgets.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(gadgets);
            }
        }

        public ViewResult Create()
        {
            ViewBag.operation = "create";
            return View("Edit", new Gadgets());
        }

        [HttpPost]
        public ActionResult Delete(int gadgetId)
        {
            Gadgets deleteGadget = repository.DeleteGadget(gadgetId);
            if (deleteGadget != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deleteGadget.Name);
            }
            return RedirectToAction("Index");
        }
    }
}