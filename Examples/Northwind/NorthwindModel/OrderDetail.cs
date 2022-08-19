namespace NorthwindModel;

[Table("Order Details")]
public class OrderDetail
{
    [Required]
    [Column("OrderID")]
    public int OrderId { get; set; }

    [Required]
    [Column("ProductID")]
    public int ProductId { get; set; }

    [Required]
    public decimal UnitPrice { get; set; }

    [Required]
    public short Quantity { get; set; }

    [Required]
    public float Discount { get; set; }

    [NotMapped]
    public Product Product { get; set; }
}
