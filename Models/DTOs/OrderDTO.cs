using System;
using System.Collections.Generic;

public class OrderDTO {
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public int TotalPrice { get; set; }

    public ICollection<OrderDetailsDTO> OrderDetails { get; set; }
}