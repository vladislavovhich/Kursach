namespace Lab_5.ViewModels.Projects
{
    public class ProjectFilterViewModel
    {
        public string SelectedName { get; }

        public ProjectFilterViewModel(string selectedName)
        {
            SelectedName = selectedName;
        }
    }
}
