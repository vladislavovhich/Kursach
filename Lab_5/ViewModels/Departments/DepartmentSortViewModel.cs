namespace Lab_5.ViewModels.Departments
{
    public enum SortState
    {
        DepartmentNameAsc, DepartmentNameDesc,
        DepartmentPhoneAsc, DepartmentPhoneDesc
    }

    public class DepartmentSortViewModel
    {
        public SortState NameSort { get; }

        public SortState PhoneSort { get; }

        public SortState Current { get; }

        public DepartmentSortViewModel(SortState state)
        {
            NameSort = state == SortState.DepartmentNameAsc ? SortState.DepartmentNameDesc : SortState.DepartmentNameAsc;
            PhoneSort = state == SortState.DepartmentPhoneAsc ? SortState.DepartmentPhoneDesc : SortState.DepartmentPhoneAsc;

            Current = state;
        }
    }
}
