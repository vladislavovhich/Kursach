namespace Lab_5.ViewModels.Posts
{
    public enum SortState
    {
        NameAsc, NameDesc,
        SalaryAsc, SalaryDesc
    }

    public class PostSortViewModel
    {
        public SortState NameSort { get; }

        public SortState SalarySort { get; }

        public SortState Current { get; }

        public PostSortViewModel(SortState state)
        {
            NameSort = state == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            SalarySort = state == SortState.SalaryAsc ? SortState.SalaryDesc : SortState.SalaryAsc;

            Current = state;
        }
    }
}
