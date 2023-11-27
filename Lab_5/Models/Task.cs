
using FoolProof.Core;
using System;
using System.Collections.Generic;

namespace Lab_5;

public partial class Task
{
    public int TaskId { get; set; }

    public DateTime BeginDate { get; set; }

    [GreaterThanOrEqualTo("BeginDate")]
    public DateTime EndDate { get; set; }

    public bool IsCompleted { get; set; }

    public string? FailedReason { get; set; }

    [GreaterThanOrEqualTo("BeginDate")]
    [LessThanOrEqualTo("EndDate")]
    public DateTime CheckDate { get; set; }

    public int? ProjectId { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Project? Project { get; set; }

}
