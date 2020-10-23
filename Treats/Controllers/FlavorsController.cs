using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Treats.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Treats.Controllers
{
  
  public class FlavorsController : Controller
  {
    private readonly TreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public FlavorsController(UserManager<ApplicationUser> userManager,TreatsContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {      
      List<Flavor> model = _db.Flavors.OrderBy(x => x.FlavorName).ToList();
      return View(model);
    }
    [Authorize(Roles = "Administrator")]
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
    [Authorize(Roles = "Administrator")]
    public ActionResult Edit(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(Flavor => Flavor.FlavorId == id);
      return View(thisFlavor);
    }
    [Authorize(Roles = "Administrator")]
    [HttpPost]
    public ActionResult Edit(Flavor Flavor)
    {
      _db.Entry(Flavor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [Authorize(Roles = "Administrator")]
    public ActionResult Delete(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(x => x.FlavorId == id);
      return View(thisFlavor);
    }
    [Authorize(Roles = "Administrator")]
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(x => x.FlavorId == id);
      _db.Flavors.Remove(thisFlavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [Authorize(Roles = "Administrator")]
    
    public ActionResult AddTreat(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(Flavors => Flavors.FlavorId == id);
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "TreatName");
      return View(thisFlavor);
    }
    [Authorize(Roles = "Administrator")]
    [HttpPost]
    public ActionResult AddTreat(Flavor Flavor, int TreatId)
    {
      if (TreatId != 0)
      {
        _db.TreatFlavors.Add(new TreatFlavor() { TreatId = TreatId, FlavorId = Flavor.FlavorId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = Flavor.FlavorId });
    }
    [Authorize(Roles = "Administrator")]
    [HttpPost]
    public ActionResult DeleteTreat(int FlavorId, int joinId)
    {
      var joinEntry = _db.TreatFlavors.FirstOrDefault(entry => entry.TreatFlavorId == joinId);
      _db.TreatFlavors.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = FlavorId });
    }

    [HttpPost] 
    public ActionResult Index(string name)
    {
      List<Flavor> model = _db.Flavors.Where(x => x.FlavorName.Contains(name)).ToList();      
      List<Flavor> sortedList = model.OrderBy(o => o.FlavorName).ToList();
      ViewBag.filterName = "Filtering by: "+name;
      return View("Index", sortedList);
    }
  }
}