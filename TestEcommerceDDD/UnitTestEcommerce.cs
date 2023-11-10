

using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Domain.Services;
using Entities.Entities;
using Infrastructure.Repository.Repositories;

namespace UnitTestEcommerce
{
    [TestClass]
    public class UnitTestEcommerce
    {

        [TestMethod]
        public async Task AddProductSuccess()
        {
            try
            {
                // ver com o luciano: Injeção de dependência X instânciar

                IProduct _IProduct = new RepositoryProduct();
                IServiceProduct _IServiceProduct = new ServiceProduct(_IProduct);

                var product = new Product
                {
                    Description = string.Concat("Test: Product added successfully", DateTime.Now.ToString()),
                    StockQuantity = 10,
                    Name = string.Concat("Test: Name", DateTime.Now.ToString()),
                    Price = 1500,
                    UserId = "1c6a99a5-0a88-47b1-9ddf-e4b0ef39d67b", // USR_NAME Tercius Quintos // user4@gmail.com
                };

                await _IServiceProduct.AddProduct(product);

<<<<<<< HEAD
                // Caso dê "Erro" em alguma coisa no Domain, virá uma mensagem
=======
                // Caso dê "Error" em alguma coisa no Domain, virá uma mensagem
>>>>>>> master
                Assert.IsFalse(product.Notifications.Any());

            }
            catch (Exception exc)
            {
                Assert.Fail(exc.ToString());
            }


        }

        [TestMethod]
        public async Task AddProductMandatoryValidation()
        {
            try
            {

                IProduct _IProduct = new RepositoryProduct();
                IServiceProduct _IServiceProduct = new ServiceProduct(_IProduct);

                var product = new Product
                {

                };

                await _IServiceProduct.AddProduct(product);

                Assert.IsTrue(product.Notifications.Any());

            }
            catch (Exception exc)
            {
                Assert.Fail(exc.ToString());
            }
        }

        [TestMethod]
        public async Task ListProductsUser()
        {
            try
            {

                IProduct _IProduct = new RepositoryProduct();

                var productsList = await _IProduct.ListProductsUser("1c6a99a5-0a88-47b1-9ddf-e4b0ef39d67b"); // USR_NAME Tercius Quintos // user4@gmail.com

                Assert.IsTrue(productsList.Any());

            }
            catch (Exception exc)
            {
                Assert.Fail(exc.ToString());
            }
        }

        [TestMethod]
        public async Task GetEntityById()
        {
            try
            {

                IProduct _IProduct = new RepositoryProduct();
                var productList = await _IProduct.ListProductsUser("1c6a99a5-0a88-47b1-9ddf-e4b0ef39d67b"); // USR_NAME Tercius Quintos // user4@gmail.com
                var product = await _IProduct.GetEntityById(productList.LastOrDefault().Id);

                Assert.IsTrue(product != null);

            }
            catch (Exception exc)
            {
                Assert.Fail(exc.ToString());

            }      

        }

        [TestMethod]
        public async Task Delete()
        {
            try
            {

                IProduct _IProduct = new RepositoryProduct();

                var productList = await _IProduct.ListProductsUser("1c6a99a5-0a88-47b1-9ddf-e4b0ef39d67b"); // USR_NAME Tercius Quintos // user4@gmail.com
                var lastProduct = productList.LastOrDefault();
                await _IProduct.Delete(lastProduct);

                Assert.IsTrue(true);

            }
            catch (Exception exc)
            {
                Assert.Fail(exc.ToString());

            }
        }

    }
}