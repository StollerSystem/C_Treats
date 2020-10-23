using System.Collections.Generic;

namespace Treats.Models
{
  public class Treat
  {
    public Treat()
    {      
      this.Flavors = new HashSet<TreatFlavor>();
              
    }
    public int TreatId { get; set; }

    public string TreatName { get; set; }   

    public string TreatDescription { get; set; }     

    public decimal TreatPrice { get; set; }       
    
    public virtual ApplicationUser User { get; set; } 

    public virtual ICollection<TreatFlavor> Flavors { get; set; }
    
  }
}