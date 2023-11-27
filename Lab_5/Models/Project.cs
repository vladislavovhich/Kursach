using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_5;

public partial class Project
{
    public int ProjectId { get; set; }

    [Required]
    public string ProjectName { get; set; } = null!;

    public bool IsStopped { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
