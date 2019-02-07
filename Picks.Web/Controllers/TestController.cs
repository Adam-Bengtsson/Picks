using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Picks.Infrastructure.Extensions;

namespace Picks.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly IDistributedCache _cache;
        public TestController(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task<IActionResult> Index(string name)
        {
            var value = await _cache.GetValueAsync<string>("the_cache_key");
            if (value == null)
            {
                value = $"{DateTime.Now.ToString(CultureInfo.CurrentCulture)}";
                await _cache.SetValueAsync("the_cache_key", value);
            }

            //ViewData vs ViewBag: https://www.c-sharpcorner.com/blogs/viewdata-vs-viewbag-vs-tempdata-in-mvc1
            ViewData["CacheTime"] = $"Cached time: {value}";
            ViewData["CurrentTime"] = $"Current time: {DateTime.Now.ToString(CultureInfo.CurrentCulture)}";

            var theNameFromSession = HttpContext.Session.Get<string>("name");
            if (!string.IsNullOrEmpty(name))
            {
                HttpContext.Session.Set("name", name);
                theNameFromSession = name;
            }

            ViewData["TheName"] = $"The name from session: {theNameFromSession}";

            return View();
        }
    }
}