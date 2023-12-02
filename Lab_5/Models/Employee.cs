using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_5;

public partial class Employee
{
    [Display(Name = "Код")]
    public int EmployeeId { get; set; }

    [Required]
    [Display(Name = "Имя")]
    public string EmployeeName { get; set; } = null!;

    [Required]
    [Display(Name = "Фамилия")]
    public string EmployeeSurname { get; set; } = null!;

    [Display(Name = "Отчество")]
    [Required]
    public string EmployeeMiddlename { get; set; } = null!;

    [Display(Name = "Код должности")]
    public int? PostId { get; set; }

    [Display(Name = "Код отделения")]
    public int? DepartmentId { get; set; }

    [Display(Name = "Отделение")]
    public virtual Department? Department { get; set; }

    [Display(Name = "Должность")]
    public virtual Post? Post { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public string FullName => EmployeeName + " " + EmployeeSurname;
}
