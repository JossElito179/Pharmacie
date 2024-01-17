using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Dokotera.Models;
using Models.connection;
using Models;
using System.Numerics;

namespace Dokotera.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        BaseConnection connex=new();
        List<Patient> patients=Patient.GetAll(connex);
        return View(patients);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Traitement(int patient,string priorite)
    {
        BaseConnection connex=new();
        Patient patient1=Patient.FindById(connex, patient);
        patient1.GetMaladies(connex);
        patient1.GetAllNecessary(connex,priorite);
        return View("AllSick",patient1);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
