using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Treats.Models;
// /new using directives
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Treats.Controllers
{
  [Authorize]
  public class FlavorsController : Controller
  {
    private readonly TreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public FlavorsController(UserManager<ApplicationUser> userManager,TreatsContext db)
    {
      _userManager = userManager;
      _db = db;
    }
    public async Task <ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      List<Flavor> model = _db.Flavors.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(model);
    }
    public ActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public async Task<ActionResult> Create(Flavor Flavor)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      Flavor.User = currentUser; 
      _db.Flavors.Add(Flavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
      Flavor model = _db.Flavors.FirstOrDefault(Flavor => Flavor.FlavorId == id);
      return View(model);
    }

    public ActionResult Edit(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(Flavor => Flavor.FlavorId == id);
      return View(thisFlavor);
    }

    [HttpPost]
    public ActionResult Edit(Flavor Flavor)
    {
      _db.Entry(Flavor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
    public ActionResult Delete(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(x => x.FlavorId == id);
      return View(thisFlavor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(x => x.FlavorId == id);
      _db.Flavors.Remove(thisFlavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    // ENGINEER
    public ActionResult AddEngineer(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(Flavors => Flavors.FlavorId == id);
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "EngineerName");
      return View(thisFlavor);
    }
    [HttpPost]
    public ActionResult AddEngineer(Flavor Flavor, int EngineerId)
    {
      if (EngineerId != 0)
      {
        _db.FlavorEngineers.Add(new FlavorEngineer() { EngineerId = EngineerId, FlavorId = Flavor.FlavorId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = Flavor.FlavorId });
    }

    [HttpPost]
    public ActionResult DeleteEngineer(int FlavorId, int joinId)
    {
      var joinEntry = _db.FlavorEngineers.FirstOrDefault(entry => entry.FlavorEngineerId == joinId);
      _db.FlavorEngineers.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = FlavorId });
    }

    [HttpPost] // SEARCH
    public ActionResult Index(string name)
    {
      List<Flavor> model = _db.Flavors.Where(x => x.FlavorName.Contains(name)).ToList();      
      List<Flavor> sortedList = model.OrderBy(o => o.FlavorName).ToList();
      ViewBag.filterName = "Filtering by: "+name;
      return View("Index", sortedList);
    }
  }
}