using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_5;

public partial class Department
{
    [Display(Name = "Код")]
    public int DepartmentId { get; set; }

    [Required]
    [Display(Name = "Название")]
    public string DepartmentName { get; set; } = null!;

    [Required]
    [Range(30000000, 35725777, ErrorMessage = "Value must be between 30000000 to 35725777")]
    [Display(Name = "Телефон")]
    public int DepartmentPhone { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
