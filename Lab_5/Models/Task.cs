
using FoolProof.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Lab_5;

public partial class Task
{
    [Display(Name = "Код")]
    public int TaskId { get; set; }

    [Display(Name = "Начало")]
    public DateTime BeginDate { get; set; }

    [GreaterThanOrEqualTo("BeginDate")]
    [Display(Name = "Конец")]
    public DateTime EndDate { get; set; }

    [Display(Name = "Завершено")]
    public bool IsCompleted { get; set; } = false;

    [Display(Name = "Причина невыполнения")]
    public string? FailedReason { get; set; }

    [GreaterThanOrEqualTo("BeginDate")]
    [LessThanOrEqualTo("EndDate")]
    [Display(Name = "Проверено")]
    public DateTime? CheckDate { get; set; }

    [Display(Name = "Код проекта")]
    public int? ProjectId { get; set; }

    [Display(Name = "Код сотрудника")]
    public int? EmployeeId { get; set; }

    [Display(Name = "Сотрудник")]
    public virtual Employee? Employee { get; set; }

    [Display(Name = "Проект")]
    public virtual Project? Project { get; set; }

}
