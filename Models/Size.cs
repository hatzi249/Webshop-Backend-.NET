public class Size {
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int SizeNumber { get; set; }

    public Product Product { get; set; }
}