namespace CompositionDAL.Entities;

[Table("JOBRUN")]
public class JobRun
{
    [Key]
    [Column("JOBRUNID")]
    public int JobRunId { get; set; }

    [Column("JOBID")]
    public int JobId { get; set; }

    [Column("JOBDATE")]
    public DateTime? JobDate { get; set; }

    [Column("REQQUEUE")]
    public string? RequestQueue { get; set; }

    [Column("SERVERCODE")]
    public string? ServerCode { get; set; }

    [Column("PRIORITY")]
    public string? Priority { get; set; }

    [Column("SERVERJOBSTATUS")]
    public string? Status { get; set; }

    [Column("ERRORCODE")]
    public string? ErrorCode { get; set; }

    [Column("RUNSTART")]
    public DateTime? RunStart { get; set; }

    [Column("RUNEND")]
    public DateTime? RunEnd { get; set; }

    [Column("FILESCOPIED")]
    public int FilesCopied { get; set; }

    #region JobRunStatus
    public readonly struct JobRunStatus
    {
        public static readonly string New = "NEW";
        public static readonly string Run = "RUN";
        public static readonly string Copy = "COPY";
        public static readonly string Complete = "COMP";
        public static readonly string Error = "ERR";
        public static readonly string ErrorComplete = "ERRC";
        public static readonly string ReSceduled = "RSCD";
        public static readonly string Closed = "CLSD";
        public static readonly string Claimed = "CLMD";
    }
    #endregion
}
