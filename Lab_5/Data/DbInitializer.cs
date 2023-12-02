using Microsoft.EntityFrameworkCore;
using System;

namespace Lab_5.Data
{
    public static class DbInitializer
    {
        private static Random random = new Random();
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
            bool isCompleted, isFailed;

            DateTime startDateGen = new DateTime(2020, 1, 1);

           
            for (int taskId = 1; taskId <= 20000; taskId++)
            {
                beginDate = GenerateRandomDate(startDateGen, DateTime.Now);
                endDate = GenerateRandomDate(beginDate, DateTime.Now);

                checkDate = GenerateRandomDate(beginDate, DateTime.Now);

                isCompleted = Faker.RandomNumber.Next(0, 1) == 1 ? true : false;
                isFailed = Faker.RandomNumber.Next(0, 1) == 1 ? true : false;

                if (isFailed)
                {
                    db.Tasks.Add(new Task
                    {
                        BeginDate = beginDate,
                        EndDate = endDate,
                        CheckDate = checkDate,
                        IsCompleted = true,
                        FailedReason = Faker.Internet.UserName(),
                        EmployeeId = Faker.RandomNumber.Next(1, 10000),
                        ProjectId = Faker.RandomNumber.Next(1, 500)
                    });
                } 
                else if (isCompleted)
                {
                    db.Tasks.Add(new Task
                    {
                        BeginDate = beginDate,
                        EndDate = endDate,
                        CheckDate = checkDate,
                        IsCompleted = true,
                        FailedReason = "",
                        EmployeeId = Faker.RandomNumber.Next(1, 10000),
                        ProjectId = Faker.RandomNumber.Next(1, 500)
                    });
                }
                else
                {
                    db.Tasks.Add(new Task
                    {
                        BeginDate = beginDate,
                        EndDate = endDate,
                        CheckDate = null,
                        IsCompleted = false,
                        FailedReason = "",
                        EmployeeId = Faker.RandomNumber.Next(1, 10000),
                        ProjectId = Faker.RandomNumber.Next(1, 500)
                    });
                } 
            }

            db.SaveChanges();
        }

        static DateTime GenerateRandomDate(DateTime startDate, DateTime endDate)
        {

            Random random = new Random();
            int range = (endDate - startDate).Days;
            int randomDays = random.Next(range + 1);

            return startDate.AddDays(randomDays);
        }
    }
}
