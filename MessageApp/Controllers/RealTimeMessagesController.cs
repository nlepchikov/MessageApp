using Microsoft.AspNetCore.Mvc;

namespace MessageApp.Controllers;

public class RealTimeMessagesController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
}