using System;
using System.Collections.Generic;

public class Order {
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public int TotalPrice { get; set; }


    public Customer Customer { get; set; }
    public ICollection<OrderDetails> OrderDetails { get; set; }
}