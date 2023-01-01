namespace DapperRepositoryTests;

[Table("People")]
public class Person
{
    [Key]
    public int Id { get; set; }

    [Column("First_Name")]
    [Required]
    public string? FirstName { get; set; }

    [Column("Middle_Name")]
    public string? MiddleName { get; set; }

    [Column("Last_Name")]
    [Required]
    public string? LastName { get; set; }

    public char? Gender { get; set; }

    public int? Age { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? ZipCode { get; set; }

    [Column("EmailAddress", 320)]
    public string? Email { get; set; }

    // Mother and Father is set by something else like a stored procedure
    // so should never be inserted or updated sames as [IgnoreInsert] and [IgnoreUpdate]
    [ReadOnly]
    public int? MotherId { get; set; }

    [ReadOnly]
    public int? FatherId { get; set; }

    // this should always be skipped because it is not a simple type
    public IList<int>? ChildrenId { get; set; }

    [NotMapped]
    public string? NotMapped { get; set; }

    [IgnoreSelect]
    public string? IgnoreSelect { get; set; }

    [IgnoreInsert]
    public string? IgnoreInsert { get; set; }

    [IgnoreUpdate]
    [Column("IgnoreUpdate", 5000)]
    public string? IgnoreUpdate { get; set; }
}

