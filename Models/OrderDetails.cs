public class OrderDetails {
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }   
    

    public Order Order { get; set; }
    public Product Product { get; set; }
}