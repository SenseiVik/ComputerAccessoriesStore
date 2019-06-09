using ComputerAccessoriesStore.Domain.Abstract;
using ComputerAccessoriesStore.Domain.Concrete;
using ComputerAccessoriesStore.Domain.Entities;
using ComputerAccessoriesStore.WebUI.Infrastruture.Abstract;
using ComputerAccessoriesStore.WebUI.Infrastruture.Concrete;
using Ninject;
using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace ComputerAccessoriesStore.WebUI.Infrastruture
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            //constant objects

            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>
            //{
            //    new Product() {Name = "Intel Core i5 3370", Price = 2300},
            //    new Product() {Name = "Intel B75-M", Price = 1250},
            //    new Product() {Name = "Kingstone 8Gb 1600Mhz", Price = 800},
            //    new Product() {Name = "Western Digital Blue 1Tb", Price = 650}
            //}.AsQueryable());
            //ninjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);


            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();

            EmailSettings emailSettings = new EmailSettings();
            emailSettings.WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false");

            ninjectKernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);
            ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}