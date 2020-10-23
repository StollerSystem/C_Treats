using System.Collections.Generic;

namespace Treats.Models
{
  public class Treat
  {
    public Treat()
    {      
      this.Flavors = new HashSet<TreatFlavors>();
              
    }
    public int TreatId { get; set; }
    public string TreatName { get; set; }    
    
    
    public virtual ApplicationUser User { get; set; } 
    public virtual ICollection<TreatFlavors> Flavors { get; set; }
    
  }
}