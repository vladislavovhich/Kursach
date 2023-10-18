using System;
using System.Collections.Generic;

namespace Kursovaia;

public partial class Employee
{
    public int EmployeeId { get; set; }


    public string EmployeeName { get; set; } = null!;

    public string EmployeeSurname { get; set; } = null!;

    public string EmployeeMiddlename { get; set; } = null!;

    public int? PostId { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Post? Post { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
