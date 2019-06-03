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
    public class ProductController : Controller
    {
        private IProductRepository repository;

        public int PageSize { get; set; }

        public ProductController(IProductRepository productRepository)
        {
            repository = productRepository;
            PageSize = 4;
        }

        public ViewResult List(string category ,int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel()
            {
                Products = repository.Products
                                        .Where(p => category == null || p.Category == category)
                                        .OrderBy(x => x.ProductID)
                                        .Skip((page - 1) * PageSize)
                                        .Take(PageSize),

                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(x => x.Category == category).Count()
                },

                CurrentCategory = category
            };

            return View(model);
        }

        public FileContentResult GetImage(int productId)
        {
            Product product = repository.Products.FirstOrDefault(x => x.ProductID == productId);

            if (product != null)
            {
                return File(product.ImageData, product.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}