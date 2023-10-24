using AES.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AES.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(SecEntity secDetails)
        {
            return View(secDetails);
        }

        public IActionResult GenerateKey()
        {
            SecEntity detailsEntity = new SecEntity();

            detailsEntity.AESKey = InfoSec.GenerateKey();

            return View("Index", detailsEntity);
        }

        public IActionResult Encrypt(SecEntity details)
        {
            string IVKey = "";
            details.CipherText = InfoSec.Encrypt(details.PlainText, details.AESKey, out IVKey);
            details.AESIVKey = IVKey;

            return RedirectToAction("Index", details);

        }
        public IActionResult Decrypt(SecEntity details)
        {
            string plainText = "";
            details.CipherToPlainText = InfoSec.Decrypt(details.CipherText, details.AESKey, details.AESIVKey);



            return RedirectToAction("Index", details);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}