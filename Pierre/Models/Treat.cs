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

    // public bool isDuplicateTreat(PierreContext _db, int TreatId)
    // {
    //   var  =  _db.TreatFlavor.Where(doctor => treat.flavorId == this.FlavorId).ToList();
    //   bool isDuplicate = false;
    //   foreach (var treat in treats)
    //   {
    //     if (treatId == .TreatId)
    //     {
    //       isDuplicate = true;
    //     }
    //   }
    //   return isDuplicate;
    // }
  }
}