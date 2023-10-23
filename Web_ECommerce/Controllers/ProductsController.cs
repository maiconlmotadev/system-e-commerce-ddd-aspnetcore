using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System.Security.Permissions;
using System.Security;

namespace Web_ECommerce.Controllers
{
    [Authorize]

    public class ProductsController : Controller
    {
        public readonly UserManager<ApplicationUser> _userManager;
        public readonly IProductApp _IProductApp;
        public readonly IUserBuyApp _IUserBuyApp;
        private IWebHostEnvironment _WebHostEnvironment;

        public ProductsController(IProductApp IProductApp, UserManager<ApplicationUser> userManager, IUserBuyApp iUserBuyApp, IWebHostEnvironment webHostEnvironment)
        {
            _IProductApp = IProductApp;
            _userManager = userManager;
            _IUserBuyApp = iUserBuyApp;
            _WebHostEnvironment = webHostEnvironment;
        }

        // GET: ProductsController
        public async Task<IActionResult> Index()
        {
            var userId = await ReturnLoggedUserId();
            return View(await _IProductApp.ListUserPoduct(userId));
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
                var userId = await ReturnLoggedUserId();
                product.UserId = userId;

                await _IProductApp.AddProduct(product);
                if (product.Notifications.Any())
                {
                    foreach(var item in product.Notifications)
                    {
                        ModelState.AddModelError(item.NameProp, item.mensage);
                    }
                    return View("Create", product);
                }
                await SaveProductImage(product);
            }
            catch
            {
                return View("Create", product); // not to be empty
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

                    ViewBag.Alert = true;
                    ViewBag.Message = "Verify, Error! Unable to verify product!";

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

        private async Task<string> ReturnLoggedUserId()
        {
            var userId = await _userManager.GetUserAsync(User);
            return userId.Id;
        }



        [AllowAnonymous]
        [HttpGet("/api/ListProductsWithStock")]
        public async Task<JsonResult> ListProductsWithStock(string description)
        {
            return Json(await _IProductApp.ListProductsWithStock(description));
        }

        public async Task<IActionResult> ListProductsUserCart()
        {
            var userId = await ReturnLoggedUserId();
            return View( await _IProductApp.ListProductsUserCart(userId));
        }



        // GET: ProductsController/Delete/5
        public async Task<IActionResult> RemoveCart(int id)
        {
            return View(await _IProductApp.GetProductsCart(id));
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveCart(int id, Product product)
        {
            try
            {
                var productDelete = await _IUserBuyApp.GetEntityById(id);
                await _IUserBuyApp.Delete(productDelete);

                return RedirectToAction(nameof(ListProductsUserCart));
            }
            catch
            {
                return View();
            }
        }



        public async Task SaveProductImage(Product viewProduct)
        {
            try
            {       
                var product = await _IProductApp.GetEntityById(viewProduct.Id);

                if(viewProduct.Image != null)
                {
                    var webRoot = _WebHostEnvironment.WebRootPath;
                    var permissionSet = new PermissionSet(PermissionState.Unrestricted);
                    var writePermission = new FileIOPermission(FileIOPermissionAccess.Append, string.Concat(webRoot, "/imgProducts"));
                    permissionSet.AddPermission(writePermission);

                    // pega a extensão
                    var Extension = System.IO.Path.GetExtension(viewProduct.Image.FileName);
                    // pega o nome do arquivo
                    var FileName = string.Concat(product.Id.ToString(), Extension);
                    // diretório para salvar
                    var DirectorySaveFile = string.Concat(webRoot, "\\imgProducts\\", FileName); 
                    // salvar
                    viewProduct.Image.CopyTo(new FileStream(DirectorySaveFile, FileMode.Create));
                    // url da imagem para salvar no DB
                    product.Url = string.Concat("https://localhost:44349", "/imgProducts/", FileName);
                    // atualizando o produto
                    await _IProductApp.UpdateProduct(product);
                }
            }
            catch (Exception error)
            {
            }

        }

    }
}     
