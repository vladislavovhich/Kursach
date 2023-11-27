using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_5;

public partial class Employee
{
    public int EmployeeId { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3)]
    [MaxLength]
    public string EmployeeName { get; set; } = null!;

    [StringLength(50, MinimumLength = 3)]
    [MaxLength]
    public string EmployeeSurname { get; set; } = null!;

    [StringLength(50, MinimumLength = 3)]
    [MaxLength]

    public string EmployeeMiddlename { get; set; } = null!;

    public int? PostId { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Post? Post { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
