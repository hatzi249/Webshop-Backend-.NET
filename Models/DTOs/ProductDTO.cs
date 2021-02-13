using System.Collections.Generic;

public class ProductDTO {
     public int Id { get; set; }
    public string ProductName { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public string Photo { get; set; }

    public ICollection<OrderDetailsDTO> Orders { get; set; }
    public ICollection<SizeDTO> Sizes { get; set;}
}