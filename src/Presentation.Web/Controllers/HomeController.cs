using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Easy.Commerce.Presentation.Web.Models;

namespace Easy.Commerce.Presentation.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sandbox()
        {
            ViewData["Message"] = "Hora da diversão!";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Qualquer dúvida entre em contato.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
