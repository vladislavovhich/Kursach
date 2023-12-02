using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_5;

public partial class Post
{
    [Display(Name = "Код")]
    public int PostId { get; set; }

    [Required]
    [Display(Name = "Название")]
    public string PostName { get; set; } = null!;

    [Required]
    [DataType(DataType.Currency)]
    [Display(Name = "Оклад")]

    public decimal PostSalary { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
