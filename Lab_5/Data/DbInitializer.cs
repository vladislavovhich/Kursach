using Microsoft.EntityFrameworkCore;

namespace Lab_5.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext db)
        {
                db.Database.EnsureCreated();

                if (db.Projects.Any())
                {
                    return;
                }

                for (int postId = 1; postId <= 500; postId++)
                {
                    db.Posts.Add(new Post { PostName = Faker.Company.Name(), PostSalary = Faker.RandomNumber.Next(1000, 10000) });
                }

                db.SaveChanges();

                for (int departmentId = 1; departmentId <= 500; departmentId++)
                {
                    db.Departments.Add(new Department { DepartmentName = Faker.Company.Name(), DepartmentPhone = Faker.RandomNumber.Next(30000000, 35725777) });
                }

                db.SaveChanges();

                for (int employeeId = 1; employeeId <= 10000; employeeId++)
                {
                    db.Employees.Add(new Employee
                    {
                        EmployeeName = Faker.Name.First(),
                        EmployeeMiddlename = Faker.Name.Last(),
                        EmployeeSurname = Faker.Name.Last(),
                        PostId = Faker.RandomNumber.Next(1, 500),
                        DepartmentId = Faker.RandomNumber.Next(1, 500)
                    });
                }

                db.SaveChanges();

                for (int projectId = 1; projectId <= 500; projectId++)
                {
                    db.Projects.Add(new Project
                    {

                        ProjectName = Faker.Internet.DomainWord(),
                        IsStopped = Faker.RandomNumber.Next(0, 1) == 1 ? true : false
                    });
                }

                db.SaveChanges();

                DateTime beginDate, endDate, checkDate;
                bool isCompleted;

                for (int taskId = 1; taskId <= 20000; taskId++)
                {
                    beginDate = Faker.Identification.DateOfBirth();

                    endDate = new DateTime(beginDate.Ticks);

                    endDate = endDate.AddDays(Faker.RandomNumber.Next(1, 365));
                    endDate = endDate.AddYears(Faker.RandomNumber.Next(1, 25));

                    checkDate = new DateTime(beginDate.Ticks);

                    checkDate = checkDate.AddTicks(Faker.RandomNumber.Next(1, endDate.Ticks - beginDate.Ticks));

                    isCompleted = Faker.RandomNumber.Next(0, 1) == 1 ? true : false;

                    db.Tasks.Add(new Task
                    {
                        BeginDate = beginDate,
                        EndDate = endDate,
                        CheckDate = checkDate,
                        IsCompleted = isCompleted,
                        FailedReason = !isCompleted ? Faker.Internet.UserName() : "",
                        EmployeeId = Faker.RandomNumber.Next(1, 10000),
                        ProjectId = Faker.RandomNumber.Next(1, 500)
                    });
                }

                db.SaveChanges();
         
        }
    }
}
