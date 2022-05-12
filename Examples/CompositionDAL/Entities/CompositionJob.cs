namespace CompositionDAL.Entities;

[Table("COMPJOBS")]
public class CompositionJob
{
    [Column("JOBID")]
    public int JobId { get; set; }

    [Column("JOBNAME")]
    public string? JobName { get; set; }

    [Column("JOBITEM")]
    public string? JobItem { get; set; }

    [Column("PAGEDEFS", int.MaxValue)]
    public string? PageDefinitions { get; set; }

    [Column("DELIVERY")]
    public string? Delivery { get; set; }

    [Column("SOURCE")]
    public string? Source { get; set; }
}
