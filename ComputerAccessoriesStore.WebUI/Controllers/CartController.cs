using ComputerAccessoriesStore.Domain.Abstract;
using ComputerAccessoriesStore.Domain.Entities;
using ComputerAccessoriesStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComputerAccessoriesStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IProductRepository rep, IOrderProcessor proc)
        {
            repository = rep;
            orderProcessor = proc;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel()
            {
                Cart = cart,
                ResultUrl = returnUrl
            });
        }
        // GET: Cart
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUri)
        {
            Product product = repository.Products
                .FirstOrDefault(x => x.ProductID == productId);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUri });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUri)
        {
            Product product = repository.Products
                .FirstOrDefault(x => x.ProductID == productId);

            if (product != null)
            {
                cart.RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUri });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        [HttpPost]
        public ViewResult Checkout (Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        //private Cart GetCart()
        //{
        //    Cart cart = (Cart)Session["Cart"];

        //    if (cart == null)
        //    {
        //        cart = new Cart();
        //        Session["Cart"] = cart;
        //    }

        //    return cart;
        //}
    }
}