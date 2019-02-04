using Microsoft.AspNetCore.Mvc;
using Picks.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picks.Web.ViewComponents
{

    public class BasketWidgetViewComponent : ViewComponent
    {
        private readonly Basket _basket;
        public BasketWidgetViewComponent(Basket basketService)
        {
            _basket = basketService;
        }

        public IViewComponentResult Invoke()
        {
            return View(_basket);
        }
    }
}
