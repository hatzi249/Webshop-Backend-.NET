using System.Collections.Generic;

public class CustomerDTO {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Phone { get; set; }

    public ICollection<OrderDTO> Orders { get; set; }
}