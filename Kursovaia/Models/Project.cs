using System;
using System.Collections.Generic;
namespace Kursovaia;

public partial class Project
{
    public int ProjectId { get; set; }

    public string ProjectName { get; set; } = null!;

    public bool IsStopped { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
