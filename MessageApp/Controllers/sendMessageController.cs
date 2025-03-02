using Microsoft.AspNetCore.Mvc;

namespace MessageApp.Controllers;

public class sendMessageController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
}