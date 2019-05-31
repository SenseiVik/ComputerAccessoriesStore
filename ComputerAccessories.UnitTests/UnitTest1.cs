using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ComputerAccessoriesStore.Domain.Abstract;
using ComputerAccessoriesStore.Domain.Entities;
using System.Linq;
using ComputerAccessoriesStore.WebUI.Controllers;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using ComputerAccessoriesStore.WebUI.Models;
using ComputerAccessoriesStore.WebUI.HtmlHelpers;

namespace ComputerAccessories.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product() {ProductID = 1, Name = "P1"},
                new Product() {ProductID = 2, Name = "P2"},
                new Product() {ProductID = 3, Name = "P3"},
                new Product() {ProductID = 4, Name = "P4"},
                new Product() {ProductID = 5, Name = "P5"}
            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object)
            {
                PageSize = 3
            };

            //act
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;

            //assert
            Product[] prodArray = result.Products.ToArray();

            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //arrange
            HtmlHelper helper = null;

            PagingInfo pagingInfo = new PagingInfo()
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = x => "Page" + x;

            //act

            MvcHtmlString result = helper.PageLinks(pagingInfo, pageUrlDelegate);

            //assert
            Assert.AreEqual(result.ToString(), @"<a href=""Page1"">1</a>" +
                                                @"<a class=""selected"" href=""Page2"">2</a>" +
                                                @"<a href=""Page3"">3</a>");
        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product() {ProductID = 1, Name = "P1", Category="C1"},
                new Product() {ProductID = 2, Name = "P2", Category="C2"},
                new Product() {ProductID = 3, Name = "P3", Category="C3"},
                new Product() {ProductID = 4, Name = "P4", Category="C2"},
                new Product() {ProductID = 5, Name = "P5", Category="C3"}
            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object)
            {
                PageSize = 3
            };

            //act
            var result = ((ProductsListViewModel)controller.List("C2").Model).Products.ToList();

            //assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category == "C2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category == "C2");
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product() {ProductID = 1, Name = "P1", Category="C1"},
                new Product() {ProductID = 2, Name = "P2", Category="C2"},
                new Product() {ProductID = 3, Name = "P3", Category="C3"},
                new Product() {ProductID = 4, Name = "P4", Category="C2"},
                new Product() {ProductID = 5, Name = "P5", Category="C3"}
            }.AsQueryable());

            NavigationController controller = new NavigationController(mock.Object);

            //act
            var res = ((IEnumerable<string>)controller.Menu().Model).ToArray();

            //assert
            Assert.AreEqual(3, res.Length);
            Assert.AreEqual("C1", res[0]);
            Assert.AreEqual("C2", res[1]);
            Assert.AreEqual("C3", res[2]);
        }

        [TestMethod]
        public void Selected_Category_Indication()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product() {ProductID = 1, Name = "P1", Category="C1"},
                new Product() {ProductID = 2, Name = "P2", Category="C2"},
                new Product() {ProductID = 3, Name = "P3", Category="C3"},
                new Product() {ProductID = 4, Name = "P4", Category="C2"},
                new Product() {ProductID = 5, Name = "P5", Category="C3"}
            }.AsQueryable());

            NavigationController controller = new NavigationController(mock.Object);
            string selectedCategory = "P2";

            //act
            string res = (string)controller.Menu(selectedCategory).ViewBag.SelectedCategory;

            //assert
            Assert.AreEqual(selectedCategory, res);
        }

        [TestMethod]
        public void Add_Item_To_Cart()
        {
            //arrange
            Product p1 = new Product() { ProductID = 1, Name = "P1" };
            Product p2 = new Product() { ProductID = 2, Name = "P2" };

            Cart target = new Cart();

            //act
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);

            var res = target.Lines.ToArray();

            //assert
            Assert.AreEqual(2, res.Length);
            Assert.IsTrue(res[0].Product.Name == "P1" && res[0].Quantity == 1);
            Assert.IsTrue(res[1].Product.Name == "P2" && res[1].Quantity == 3);
        }

        [TestMethod]
        public void Add_Exiting_Item_To_Cart()
        {
            //arrange
            Product p1 = new Product() { ProductID = 1, Name = "P1" };
            Product p2 = new Product() { ProductID = 2, Name = "P2" };

            Cart target = new Cart();

            //act
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p1, 10);

            var res = target.Lines.ToArray();

            //assert
            Assert.AreEqual(2, res.Length);
            Assert.IsTrue(res[0].Product.Name == "P1" && res[0].Quantity == 10);
            Assert.IsTrue(res[1].Product.Name == "P2" && res[1].Quantity == 3);
        }
    }
}
