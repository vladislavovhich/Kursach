using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_5;

public partial class Project
{
    [Display(Name = "Код")]
    public int ProjectId { get; set; }

    [Required]
    [Display(Name = "Название")]
    public string ProjectName { get; set; } = null!;

    [Display(Name = "Приостановлен")]
    public bool IsStopped { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
