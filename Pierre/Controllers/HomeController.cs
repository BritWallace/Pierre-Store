using Microsoft.AspNetCore.Mvc;

namespace Pierre.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }
  }
}