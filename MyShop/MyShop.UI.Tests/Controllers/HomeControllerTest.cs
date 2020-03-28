using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.UI.Controllers;
using System.Linq;
using System.Web.Mvc;

namespace MyShop.UI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexPageDoesReturnProducts()
        {
            IRepository<Product> prodContext = new Mocks.MockContext<Product>();
            IRepository<ProductCategory> prodCatContext = new Mocks.MockContext<ProductCategory>();
            prodContext.Insert( new Product());
            // Arrange
            HomeController controller = new HomeController(prodContext, prodCatContext);

            // Act
            var result = controller.Index() as ViewResult;
            var viewModel = (ProductListViewModel)result.ViewData.Model;
            // Assert
            Assert.AreEqual(1, viewModel.Products.Count());

        }


    }
}
