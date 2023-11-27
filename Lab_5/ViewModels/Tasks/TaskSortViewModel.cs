namespace Lab_5.ViewModels.Tasks
{
    public enum SortState
    {
        BeginDateAsc, BeginDateDesc,
        EndDateAsc, EndDateDesc,
        IsCompletedAsc, IsCompletedDesc,
        FailedReasonAsc, FailedReasonDesc,
        CheckDateAsc, CheckDateDesc,
        ProjectAsc, ProjectDesc,
        EmployeeAsc, EmployeeDesc
    }

    public class TaskSortViewModel
    {
        public SortState BeginDateSort { get; }

        public SortState EndDateSort { get; }

        public SortState IsCompletedSort { get; }

        public SortState FailedReasonSort { get; }

        public SortState ProjectSort { get; }

        public SortState EmployeeSort { get; }

        public SortState CheckDateSort { get; }

        public SortState Current { get; }

        public TaskSortViewModel(SortState state)
        {
            BeginDateSort = state == SortState.BeginDateAsc ? SortState.BeginDateDesc : SortState.BeginDateAsc;
            EndDateSort = state == SortState.EndDateAsc ? SortState.EndDateDesc : SortState.EndDateAsc;
            IsCompletedSort = state == SortState.IsCompletedAsc ? SortState.IsCompletedDesc : SortState.IsCompletedAsc;
            FailedReasonSort = state == SortState.FailedReasonAsc ? SortState.FailedReasonDesc : SortState.FailedReasonAsc;
            CheckDateSort = state == SortState.CheckDateAsc ? SortState.CheckDateDesc : SortState.CheckDateAsc;
            EmployeeSort = state == SortState.EmployeeAsc ? SortState.EmployeeDesc : SortState.EmployeeAsc;
            ProjectSort = state == SortState.ProjectAsc ? SortState.ProjectDesc : SortState.ProjectAsc;

            Current = state;
        }
    }
}
