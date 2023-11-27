using Faker;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab_5.ViewModels.Employees
{
    public class EmployeesFilterViewModel
    {
        public EmployeesFilterViewModel(List<Post> posts, string name, int postId)
        {
            posts.Insert(0, new Post { PostName = "Все", PostId = 0 });

            Posts = new SelectList(posts, "PostId", "PostName");
            SelectedName = name;
            SelectedPostId = postId;
        }

        public SelectList Posts { get; }

        public string SelectedName { get; }
        public int SelectedPostId { get; }
    }
}
