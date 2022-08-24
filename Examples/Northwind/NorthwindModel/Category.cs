namespace NorthwindModel;

[Table("Categories", alias: "category")]
public class Category
{
    [Key]
    [Column("CategoryID")]
    public int CategoryId{ get; set; }

    [Required]
    public string CategoryName { get; set; }

    public string? Description { get; set; }

    [Column("Picture")]
    [IgnoreSelect]
    public byte[]? Image { get; set; }
}
