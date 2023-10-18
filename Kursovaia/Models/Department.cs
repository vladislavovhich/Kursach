using System;
using System.Collections.Generic;

namespace Kursovaia;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public int DepartmentPhone { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
