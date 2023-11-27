namespace Lab_5.ViewModels.Projects
{
    public enum SortState
    {
        ProjectNameAsc, ProjectNameDesc,
        ProjectIsStoppedAsc, ProjectIsStoppedDesc,
    }

    public class ProjectSortViewModel
    {
        public SortState NameSort { get; }

        public SortState IsStoppedSort { get; }

        public SortState Current { get; }

        public ProjectSortViewModel(SortState state)
        {
            NameSort = state == SortState.ProjectNameAsc ? SortState.ProjectNameDesc : SortState.ProjectNameAsc;
            IsStoppedSort = state == SortState.ProjectIsStoppedAsc ? SortState.ProjectIsStoppedDesc : SortState.ProjectIsStoppedAsc;

            Current = state;
        }
    }
}
