using System.Collections.Generic;

namespace Orders.Models
{
  public class Order
  {
    public Order()
    {      
      this.Flavors = new HashSet<OrderFlavor>();
      this.Orders = new HashSet<OrderFlavor>();
              
    }
    public int OrderId { get; set; }

    public string OrderName { get; set; }   

    public string OrderDescription { get; set; }      
    
    public virtual ApplicationUser User { get; set; } 
    
    public virtual ICollection<OrderTreat> Treats { get; set; }    
  }
}