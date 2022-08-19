namespace NorthwindModel;

[Table("Products")]
public class Product
{
    [Key]
    [Column("ProductID")]
    public int ProductId { get; set; }

    [Required]
    public string ProductName { get; set; }

    [Column("SupplierID")]
    public int? SupplierId { get; set; }

    [Column("CategoryID")]
    public int? CategoryId { get; set; }

    public string? QuantityPerUnit { get; set; }

    public decimal? UnitPrice { get; set; }

    public short? UnitsInStock { get; set; }

    public short? UnitsOnOrder { get; set; }

    public short? RecorderLevel { get; set; }

    public bool? Discontinued { get; set; }

    [NotMapped]
    public Category Category { get; set; }

    [NotMapped]
    public Supplier Supplier { get; set; }
}
