using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Web_ECommerce.Controllers
{
    [Authorize]

    public class ProductsController : Controller
    {
        public readonly IProductApp _IProductApp;

        public ProductsController(IProductApp IProductApp)
        {
            _IProductApp = IProductApp;
        }

        // GET: ProductsController
        public async Task<IActionResult> Index()
        {
            return View(await _IProductApp.List());
        }

        // GET: ProductsController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _IProductApp.GetEntityById(id));
        }

        // GET: ProductsController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            try
            {
                await _IProductApp.AddProduct(product);
                if (product.Notifications.Any())
                {
                    foreach(var item in product.Notifications)
                    {
                        ModelState.AddModelError(item.NameProp, item.mensage);
                    }
                    return View("Edit", product);
                }
            }
            catch
            {
                return View("Edit", product); // not to be empty
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _IProductApp.GetEntityById(id));
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            try
            {
                await _IProductApp.UpdateProduct(product);
                if (product.Notifications.Any())
                {
                    foreach (var item in product.Notifications)
                    {
                        ModelState.AddModelError(item.NameProp, item.mensage);
                    }
                    return View("Edit", product);
                }
            }
            catch
            {
                return View("Edit", product); // not to be empty
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductsController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _IProductApp.GetEntityById(id));
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Product product)
        {
            try
            {
                var productDelete = await _IProductApp.GetEntityById(id);
                await _IProductApp.Delete(productDelete);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
