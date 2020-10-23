using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;

namespace Factory.Controllers
{
  public class HomeController : Controller
  {
    private readonly FactoryContext _db;
    public HomeController(FactoryContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      List<Engineer> engineers = _db.Engineers.OrderBy(x => x.EngineerName).ToList();
      List<Machine> machines = _db.Machines.OrderBy(x => x.MachineName).ToList();
      ViewBag.engineers = engineers;
      ViewBag.machines = machines;
      return View();
    }
  }
}