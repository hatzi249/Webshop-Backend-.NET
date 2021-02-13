using System.Collections.Generic;

public class Product {
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public string Photo { get; set;}

    public ICollection<OrderDetails> OrderDetails { get; set; }
    
    public ICollection<Size> Sizes { get; set; }
}