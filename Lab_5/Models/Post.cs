using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_5;

public partial class Post
{
    public int PostId { get; set; }

    [Required]
    public string PostName { get; set; } = null!;

    [Required]
    [DataType(DataType.Currency)]

    public decimal PostSalary { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
