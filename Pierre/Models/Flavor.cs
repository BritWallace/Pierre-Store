using System;
using System.Collections.Generic;
using System.Linq;

namespace Pierre.Models
{
  public class Flavor
  {
    public Flavor()
    {
      this.JoinTreatFlavor = new HashSet<TreatFlavor>();
    }

    public int FlavorId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<TreatFlavor> JoinTreatFlavor { get; set; }

    public bool isDuplicateTreat(PierreContext _db, int treatId)
    {
      var treats =  _db.TreatFlavors.Where(treat => treat.FlavorId == this.FlavorId).ToList();
      bool isDuplicate = false;
      foreach (var treat in treats)
      {
        if (treatId == treat.TreatId)
        {
          isDuplicate = true;
        }
      }
      return isDuplicate;
    }
  }
}
