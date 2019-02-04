using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Picks.Infrastructure.Models;
using Picks.Infrastructure.Repositories;

namespace Picks.Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly IPictureRepository _pictureRepository;
        private readonly Basket _basket;

        public BasketController(IPictureRepository pictureRepo, Basket basketService)
        {
            _pictureRepository = pictureRepo;
            _basket = basketService;

        }

        public IActionResult Basket()
        {
            return View(_basket);
        }

        public RedirectToActionResult RemoveFromBasket(Guid pictureId)
        {
            _basket.RemovePicture(pictureId);

            return RedirectToAction(nameof(Basket));
        }

        public RedirectToActionResult EmptyBasket()
        {
            _basket.EmptyBasket();

            return RedirectToAction(nameof(Basket));
        }
    }
}