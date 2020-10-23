using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Treats.Models;

namespace Treats.Controllers
{
  public class HomeController : Controller
  {
    private readonly TreatsContext _db;
    public HomeController(TreatsContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      List<Treat> Treats = _db.Treats.OrderBy(x => x.TreatName).ToList();
      List<Flavor> Flavors = _db.Flavors.OrderBy(x => x.FlavorName).ToList();
      ViewBag.Treats = Treats;
      ViewBag.Flavors = Flavors;
      return View();
    }
  }
}