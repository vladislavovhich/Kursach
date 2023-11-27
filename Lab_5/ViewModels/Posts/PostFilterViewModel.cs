namespace Lab_5.ViewModels.Posts
{
    public class PostFilterViewModel
    {
        public string SelectedPostName { get; }

        public decimal SelectedSalary { get; }

        public PostFilterViewModel(string post, decimal salary)
        {
            SelectedPostName = post;
            SelectedSalary = salary;
        }
    }
}
