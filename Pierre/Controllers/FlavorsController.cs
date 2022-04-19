using Pierre.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;



namespace Pierre.Controllers
{
  [Authorize]
  public class FlavorsController : Controller
  {
    private readonly PierreContext _db;
    public FlavorsController(PierreContext db)
    {
      _db = db;
    }
    [AllowAnonymous]
    public ActionResult Index()
    {
      return View(_db.Flavors.ToList());
    }
    public ActionResult Create()
    {
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
      return View();
    }
    [HttpPost]
    public ActionResult Create(Flavor flavor, int TreatId)
    {
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      if (TreatId != 0)
      {
        _db.TreatFlavors.Add(new TreatFlavor() {TreatId = TreatId, FlavorId = flavor.FlavorId});
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }
    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      Flavor foundFlavor = _db.Flavors
        .Include(flavor => flavor.JoinTreatFlavor)
        .ThenInclude(join => join.Treat)
        .FirstOrDefault(model => model.FlavorId == id);
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
      return View(foundFlavor);
    }
    public ActionResult Edit(int id)
    {
      var foundFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
      return View(foundFlavor);
    }

    [HttpPost]
    public ActionResult Edit(Flavor flavor, int TreatId)
    {
      if (TreatId !=0)
      {
        _db.TreatFlavors.Add(new TreatFlavor() {TreatId = TreatId, FlavorId = flavor.FlavorId});        
      }
      _db.Entry(flavor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
      Flavor foundFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(foundFlavor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Flavor foundFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      _db.Flavors.Remove(foundFlavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult AddTreat(Flavor flavor, int TreatId)
    {
      bool isDuplicate = flavor.isDuplicateTreat(_db, TreatId);
      if (TreatId !=0 && isDuplicate == false)
      {
        _db.TreatFlavors.Add(new TreatFlavor() {TreatId = TreatId, FlavorId = flavor.FlavorId});
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = flavor.FlavorId});
    }
      [HttpPost]
    public ActionResult DeleteTreat(int joinId)
    {
      var joinEntry = _db.TreatFlavors.FirstOrDefault(entry => entry.TreatFlavorId == joinId);
      int savedFlavor = joinEntry.FlavorId;
      _db.TreatFlavors.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = savedFlavor});
    }
  }
}