using System;
using System.Collections.Generic;
using System.Linq;

namespace Pierre.Models
{
  public class Treat
  {
    public Treat()
    {
      this.JoinTreatFlavor = new HashSet<TreatFlavor>();
    }

    public int TreatId { get; set; }
    public string Name { get; set; }
    
    public virtual ICollection<TreatFlavor> JoinTreatFlavor { get; set; }

    public bool isDuplicateFlavor(PierreContext _db, int flavorId)
    {
      var flavors =  _db.TreatFlavors.Where(flavor => flavor.TreatId == this.TreatId).ToList();
      bool isDuplicate = false;
      foreach (var treat in flavors)
      {
        if (flavorId == TreatId)
        {
          isDuplicate = true;
        }
      }
      return isDuplicate;
    }
  }
}