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
  // [Authorize] 
  public class TreatsController : Controller
  {
    private readonly TreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager; 
    public TreatsController(UserManager<ApplicationUser> userManager, TreatsContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {      

      List<Treat> model = _db.Treats.OrderBy(x => x.TreatName).ToList();
      return View(model);
    }
    public ActionResult Create()
    {
      return View();
    }

    [Authorize (Roles = "Administrator")]
    [HttpPost]
    public async Task<ActionResult> Create(Treat Treat)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      Treat.User = currentUser; //"STAMP" USER ON THE OBJECT
      _db.Treats.Add(Treat);      
      _db.SaveChanges();      
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Treat model = _db.Treats.FirstOrDefault(Treat => Treat.TreatId == id);
      return View(model);
    }
    [Authorize (Roles = "Administrator")]
    public ActionResult Edit(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(Treat => Treat.TreatId == id);
      return View(thisTreat);
    }

    [Authorize (Roles = "Administrator")]
    [HttpPost]
    public ActionResult Edit(Treat Treat)
    {
      _db.Entry(Treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [Authorize (Roles = "Administrator")]
    public ActionResult Delete(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(x => x.TreatId == id);
      return View(thisTreat);
    }

    [Authorize (Roles = "Administrator")]
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(x => x.TreatId == id);
      _db.Treats.Remove(thisTreat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize (Roles = "Administrator")]
    //  Add Flavor 
    public ActionResult AddFlavor(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(Treats => Treats.TreatId == id);
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");
      return View(thisTreat);
    }

    [Authorize (Roles = "Administrator")]
    [HttpPost]
    public ActionResult AddFlavor(Treat Treat, int FlavorId)
    {
      if (FlavorId != 0)
      {
        _db.TreatFlavors.Add(new TreatFlavor() { FlavorId = FlavorId, TreatId = Treat.TreatId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = Treat.TreatId });
    }

    [Authorize (Roles = "Administrator")]
    [HttpPost]
    public ActionResult DeleteFlavor(int TreatId, int joinId)
    {
      var joinEntry = _db.TreatFlavors.FirstOrDefault(entry => entry.TreatFlavorId == joinId);
      _db.TreatFlavors.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = TreatId });
    }

    [HttpPost] // SEARCH
    public ActionResult Index(string name)
    {
      List<Treat> model = _db.Treats.Where(x => x.TreatName.Contains(name)).ToList();      
      List<Treat> sortedList = model.OrderBy(o => o.TreatName).ToList();
      ViewBag.filterName = "Filtering by: "+name;
      return View("Index", sortedList);
    }

    // ADD TO ORDER
    [Authorize]
    public ActionResult AddOrder(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(Treats => Treats.TreatId == id);
      ViewBag.OrderId = new SelectList(_db.Orders, "OrderId", "OrderName");
      return View(thisTreat);
    }

    [Authorize]
    [HttpPost]
    public ActionResult AddOrder(Treat Treat, int OrderId)
    {
      if (OrderId != 0)
      {
        _db.OrderTreats.Add(new OrderTreat() { OrderId = OrderId, TreatId = Treat.TreatId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = Treat.TreatId });
    }
  }
}