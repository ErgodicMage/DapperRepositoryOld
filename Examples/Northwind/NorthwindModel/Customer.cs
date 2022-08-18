namespace NorthwindModel;

[Table("Customers")]
public class Customer
{
    [NonAutoKey]
    [Column("CustomerID", 5)]
    public string CustomerId { get; set; }

    [Required]
    public string CustomerName { get; set; }

    public string? ContactName { get; set; }

    public string? ContactTitle { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Region { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public string? Phone { get; set; }

    public string? Fax { get; set; }

}
