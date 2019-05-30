using ComputerAccessoriesStore.Domain.Abstract;
using ComputerAccessoriesStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComputerAccessoriesStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;


        public int PageSize { get; set; }

        public ProductController(IProductRepository productRepository)
        {
            repository = productRepository;
            PageSize = 4;
        }

        public ViewResult List(int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel()
            {
                Products = repository.Products
                                        .OrderBy(x => x.ProductID)
                                        .Skip((page - 1) * PageSize)
                                        .Take(PageSize),

                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                }
            };

            return View(model);
        }
    }
}