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

  }
}