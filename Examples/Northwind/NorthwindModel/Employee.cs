namespace NorthwindModel;

[Table("Employees")]
public class Employee
{
    [Key]
    [Column("EmployeeID")]
    public int EmployeeId { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public string? Title { get; set; }

    [Column("TitleOfCourtesy")]
    public string? Courtesy { get; set; }

    public DateTime? BirthDate { get; set; }

    public DateTime? HireDate { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Region { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    [Column("HomePhone")]
    public string? Phone { get; set; }

    [Column("Extension")]
    public string? PhoneExtension { get; set; }

    [IgnoreSelect]
    public byte[]? Photo { get; set; }

    public string? Notes { get; set; }

    public int? ReportsTo { get; set; }

    public string? PhotoPath { get; set; }

    [NotMapped]
    public IList<Territory> Territories { get; set; }
}
