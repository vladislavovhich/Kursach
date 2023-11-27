namespace Lab_5.ViewModels.Departments
{
    public class DepartmentFilterViewModel
    {
        public string SelectedDepartmentName { get; }

        public int SelectedPhone { get; }

        public DepartmentFilterViewModel(string selectedDepartmentName, int selectedPhone)
        {
            SelectedDepartmentName = selectedDepartmentName;
            SelectedPhone = selectedPhone;
        }
    }
}
