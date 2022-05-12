namespace CompositionDAL.Entities;

[Table("VARIABLEDATA")]
public class VariableData 
{
    [Column("JOBID")]
    public int JobId { get; set; }

    [Column("VARIABLEDATA", int.MaxValue)]
    public string? VariableDataValue { get; set; } 

    [Column("BILLOFMATERIAL", int.MaxValue)]
    public string? BillofMaterial { get; set; }

    [Column("BOMIND")]
    public string? BillofMaterialIndicator { get; set; }
}
