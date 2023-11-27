using Faker;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab_5.ViewModels.Tasks
{
    public class TaskFilterViewModel
    {
        public TaskFilterViewModel(List<Project> projects, List<Employee> employees, int projectId, int employeesId)
        {
            projects.Insert(0, new Project { ProjectName = "Все", ProjectId = 0 });

            Projects = new SelectList(projects, "ProjectId", "ProjectName");
            SelectedProjectId = projectId;

            employees.Insert(0, new Employee { EmployeeName = "Все", EmployeeId = 0 });

            Employees = new SelectList(employees, "EmployeeId", "EmployeeName");
            SelectedEmployeeId = employeesId;
        }

        public SelectList Projects { get; }
        public SelectList Employees { get; }

        public int SelectedProjectId { get; }

        public int SelectedEmployeeId { get; }
    }
}
