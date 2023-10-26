using System;
using ApplicationApp.Interfaces;
using Entities.Entities;
using Entities.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Threading.Tasks;
using Web_ECommerce.Models;
using Microsoft.AspNetCore.Hosting;

namespace Web_ECommerce.Controllers
{
    public class UserBuyController : HelpQrCode
    {
        public readonly UserManager<ApplicationUser> _userManager;
        public readonly IUserBuyApp _IUserBuyApp;
        private IWebHostEnvironment _IWebHostEnvironment;

        public UserBuyController(UserManager<ApplicationUser> userManager, IUserBuyApp IUserBuyApp, IWebHostEnvironment IWebHostEnvironment)
        {
            _userManager = userManager;
            _IUserBuyApp = IUserBuyApp;
            _IWebHostEnvironment = IWebHostEnvironment;

        }

        public async Task<IActionResult> FinalizeBuy()
        {
            var user = await _userManager.GetUserAsync(User);
            var userBuy = await _IUserBuyApp.BuyCart(user.Id);
            return View(userBuy);
        }

        public async Task<IActionResult> MyBuys(bool message = false)
        {
            var user = await _userManager.GetUserAsync(User);
            var userBuy = await _IUserBuyApp.MyPurchases(user.Id);

            if (message)
            {
                ViewBag.Success = true;
                ViewBag.Message = "Purchase made! Pay now and guarantee your purchase!";
            }
            return View(userBuy);
        }

        public async Task<IActionResult> ConfirmBuy()
        {
            var user = await _userManager.GetUserAsync(User);
            var success = await _IUserBuyApp.ConfirmPurchaseCartUser(user.Id);

            if (success)
            {
                return RedirectToAction("MyBuys", new { Message = true });
            }
            else
                return RedirectToAction("FinalizeBuy");

        }

        public async Task<IActionResult> Print(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var userBuy = await _IUserBuyApp.BuyProducts(user.Id, id);

            return await Download(userBuy, _IWebHostEnvironment);
        }


        [HttpPost("/api/AddProductCart")]
        public async Task<JsonResult> AddProductCart(string id, string name, string quant)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                await _IUserBuyApp.Add(new UserBuy
                {
                    IdProduct = Convert.ToInt32(id),
                    BuyQuantity = Convert.ToInt32(quant),
                    State = EnumBuyState.Product_Cart,
                    UserId = user.Id
                });
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpGet("/api/QuantProductsCart")]
        public async Task<JsonResult> QuantProductsCart()
        {
            var user = await _userManager.GetUserAsync(User);
            var quant = 0;

            if (user != null)
            {
               quant = await _IUserBuyApp.QuantProductUserCart(user.Id);

               return Json(new { success = true, quant = quant });
            }
            return Json(new { success = false, quant = quant });
        }
    }
}
