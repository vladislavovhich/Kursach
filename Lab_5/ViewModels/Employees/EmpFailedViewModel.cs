using System.ComponentModel.DataAnnotations;

namespace Lab_5.ViewModels.Employees
{
    public class EmpFailedViewModel
    {
        [Display(Name = "Сотрудник")]
        public Employee Employee { get; set; }

        [Display(Name = "Проект")]
        public Project Project { get; set; }

        [Display(Name = "Задание")]
        public Task Task { get; set; }

        [Display(Name = "Просрочка")]
        public int Delayed { get; set; }
    }
}
