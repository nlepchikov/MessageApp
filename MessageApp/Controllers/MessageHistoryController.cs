using Microsoft.AspNetCore.Mvc;

namespace MessageApp.Controllers;

public class MessageHistoryController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
}