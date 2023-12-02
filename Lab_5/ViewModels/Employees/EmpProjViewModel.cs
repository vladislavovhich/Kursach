using System.ComponentModel.DataAnnotations;

namespace Lab_5.ViewModels.Employees
{
    public class EmpProjViewModel
    {
        [Display(Name = "Сотрудник")]
        public Employee Employee { get; set; }

        [Display(Name = "Проект")]
        public Project Project { get; set; }

        [Display(Name = "Задание")]
        public Task Task { get; set; }

        [Display(Name = "Конец")]
        public DateTime EndDate { get; set; }
    }
}
