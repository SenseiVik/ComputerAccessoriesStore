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

        public CartController(IProductRepository rep)
        {
            repository = rep;
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