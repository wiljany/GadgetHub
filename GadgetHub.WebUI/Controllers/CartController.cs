﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Entities;
using GadgetHub.Domain.Abstract;
using GadgetHub.WebUI.Models;

namespace GadgetHub.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IGadgetRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IGadgetRepository repo, IOrderProcessor processor)
        {
            this.repository = repo;
            orderProcessor = processor;
        }
        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public RedirectToRouteResult AddToCart(int gadgetId, string returnUrl)
        {
            Gadgets gadgets = repository.Gadgets.FirstOrDefault
                                                (g => g.GadgetId == gadgetId);
            if (gadgets != null)
            {
                GetCart().AddItem(gadgets, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

		public RedirectToRouteResult RemoveFromCart(int gadgetId, string returnUrl)
		{
			Gadgets gadgets = repository.Gadgets.FirstOrDefault
												(g => g.GadgetId == gadgetId);
			if (gadgets != null)
			{
				GetCart().RemoveLine(gadgets);
			}
			return RedirectToAction("Index", new { returnUrl });
		}

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

		public PartialViewResult Summary(Cart cart)
		{
			return PartialView(cart);
		}

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.processOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed", shippingDetails);
            }
            else
            {
                return View(shippingDetails);
            }
        }
	}
}