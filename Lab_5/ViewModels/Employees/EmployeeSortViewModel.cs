namespace Lab_5.ViewModels.Employees
{
    public enum SortState
    {
        EmployeeNameAsc, EmployeeNameDesc,
        EmployeeSurnameAsc, EmployeeSurnameDesc,
        EmployeeMiddleNameAsc, EmployeeMiddleNameDesc,
        EmployeePostAsc, EmployeePostDesc,
        EmployeeDepartmentAsc, EmployeeDepartmentDesc,
    }

    public class EmployeeSortViewModel
    {
        public SortState NameSort { get; }

        public SortState SurnameSort { get; }

        public SortState MiddleNameSort { get; }

        public SortState PostSort { get; }

        public SortState DepartmentSort { get; }

        public SortState Current { get; }

        public EmployeeSortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.EmployeeNameAsc ? SortState.EmployeeNameDesc : SortState.EmployeeNameAsc;
            SurnameSort = sortOrder == SortState.EmployeeSurnameAsc ? SortState.EmployeeSurnameDesc : SortState.EmployeeSurnameAsc;
            MiddleNameSort = sortOrder == SortState.EmployeeMiddleNameAsc ? SortState.EmployeeMiddleNameDesc : SortState.EmployeeMiddleNameAsc;
            PostSort = sortOrder == SortState.EmployeePostAsc ? SortState.EmployeePostDesc : SortState.EmployeePostAsc;
            DepartmentSort = sortOrder == SortState.EmployeeDepartmentAsc ? SortState.EmployeeDepartmentDesc : SortState.EmployeeDepartmentAsc;

            Current = sortOrder;
        }
    }
}
