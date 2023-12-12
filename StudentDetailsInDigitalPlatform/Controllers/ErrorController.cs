using Microsoft.AspNetCore.Mvc;

namespace StudentDetailsInDigitalPlatform.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "sorry, the page not found";
                    break;
            }
            return View("NotFound");
        }
    }
}
