using System;
using System.Collections.Generic;

namespace Kursovaia;

public partial class Post
{
    public int PostId { get; set; }

    public string PostName { get; set; } = null!;

    public decimal PostSalary { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
