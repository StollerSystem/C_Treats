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

namespace Orders.Controllers
{
  // [Authorize] //new line
  public class OrdersController : Controller
  {
    private readonly TreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager; //new line
    public OrdersController(UserManager<ApplicationUser> userManager, TreatsContext db)
    {
      _userManager = userManager;
      _db = db;
    }
    public ActionResult Index()
    {
      // var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      // var currentUser = await _userManager.FindByIdAsync(userId);
      // List<Order> userOrders = _db.Orders.Where(entry => entry.User.Id == currentUser.Id).ToList();

      List<Order> model = _db.Orders.OrderBy(x => x.OrderName).ToList();
      return View(model);
    }
    public ActionResult Create()
    {
      return View();
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult> Create(Order Order)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      Order.User = currentUser; //"STAMP" USER ON THE OBJECT
      _db.Orders.Add(Order);      
      _db.SaveChanges();
      // _db.Orders.Add(Order);
      // _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Order model = _db.Orders.FirstOrDefault(Order => Order.OrderId == id);
      return View(model);
    }
    [Authorize]
    public ActionResult Edit(int id)
    {
      var thisOrder = _db.Orders.FirstOrDefault(Order => Order.OrderId == id);
      return View(thisOrder);
    }
    [Authorize]
    [HttpPost]
    public ActionResult Edit(Order Order)
    {
      _db.Entry(Order).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [Authorize]
    public ActionResult Delete(int id)
    {
      var thisOrder = _db.Orders.FirstOrDefault(x => x.OrderId == id);
      return View(thisOrder);
    }
    [Authorize]
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisOrder = _db.Orders.FirstOrDefault(x => x.OrderId == id);
      _db.Orders.Remove(thisOrder);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [Authorize]
    //  Treat
    public ActionResult AddTreat(int id)
    {
      var thisOrder = _db.Orders.FirstOrDefault(Orders => Orders.OrderId == id);
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "TreatName");
      return View(thisOrder);
    }
    [Authorize]
    [HttpPost]
    public ActionResult AddTreat(Order Order, int TreatId)
    {
      if (TreatId != 0)
      {
        _db.OrderTreats.Add(new OrderTreat() { TreatId = TreatId, OrderId = Order.OrderId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = Order.OrderId });
    }
    [Authorize]
    [HttpPost]
    public ActionResult DeleteTreat(int OrderId, int joinId)
    {
      var joinEntry = _db.OrderTreats.FirstOrDefault(entry => entry.OrderTreatId == joinId);
      _db.OrderTreats.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = OrderId });
    }

    [HttpPost] // SEARCH
    public ActionResult Index(string name)
    {
      List<Order> model = _db.Orders.Where(x => x.OrderName.Contains(name)).ToList();      
      List<Order> sortedList = model.OrderBy(o => o.OrderName).ToList();
      ViewBag.filterName = "Filtering by: "+name;
      return View("Index", sortedList);
    }
  }
}