using Pierre.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pierre.Controllers
{
  public class TreatsController: Controller
  {
    private readonly PierreContext _db;

    public TreatsController(PierreContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Treats.ToList());
    }
    public ActionResult Create()
    {
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      return View();
    }
    [HttpPost]
    public ActionResult Create(Treat treat, int FlavorId)
    {
      _db.Treats.Add(treat);
      _db.SaveChanges();
      if (FlavorId != 0)
      {
        _db.TreatFlavors.Add(new TreatFlavor() {FlavorId = FlavorId, TreatId = treat.TreatId});
        _db.SaveChanges();
      }
      
      return RedirectToAction("Index");
    }
      public ActionResult Details(int id)
    {
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      Treat foundTreat = _db.Treats
        .Include(treat => treat.JoinTreatFlavor)
        .ThenInclude(joinFlavor => joinFlavor.Flavor)
        .FirstOrDefault(model => model.TreatId == id);
      return View(foundTreat);
    }
    public ActionResult Edit(int id)
    {
      Treat foundTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(foundTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat)
    {
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
      var foundTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(foundTreat);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var foundTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      _db.Treats.Remove(foundTreat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteTreat(int joinId)
    {
      var joinEntry = _db.TreatFlavors.FirstOrDefault(entry => entry.TreatFlavorId == joinId);
      int savedTreat = joinEntry.TreatId;
      _db.TreatFlavors.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = savedTreat});
    }
    [HttpPost]
    public ActionResult AddFlavor(Treat treat, int FlavorId)
    {
      bool isDuplicate = treat.isDuplicateFlavor(_db, FlavorId);
      if (FlavorId !=0 && isDuplicate == false)
      {
        _db.TreatFlavors.Add(new TreatFlavor() {FlavorId = FlavorId, TreatId = treat.TreatId});
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = treat.TreatId});
    }



  }
}