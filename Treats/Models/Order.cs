using System.Collections.Generic;

namespace Treats.Models
{
  public class Order
  {
    public Order()
    {            
      this.Treats = new HashSet<OrderTreat>();
              
    }
    public int OrderId { get; set; }

    public string OrderName { get; set; }   

    public string OrderDescription { get; set; }      
    
    public virtual ApplicationUser User { get; set; } 
    
    public virtual ICollection<OrderTreat> Treats { get; set; }    
  }
}