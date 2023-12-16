using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab_5;
using Lab_5.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Faker;
using Lab_5.ViewModels.Departments;
using SortState = Lab_5.ViewModels.Projects.SortState;
using Microsoft.AspNetCore.Authorization;
using Lab_5.ViewModels;
using Lab_5.ViewModels.Projects;
using System.Drawing.Printing;
using Faker.Resources;
using Microsoft.Data.SqlClient;
using Lab_5.ViewModels.Tasks;

namespace Lab_5.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Projects
        [Authorize(Roles = "Admin,MainAdmin,Employee")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1, string name = "", SortState sortOrder = SortState.ProjectNameAsc)
        {
            IQueryable<Project> projects = _context.Projects;

            ViewBag.ActionName = "Index";
            ViewBag.Header = "Проекты";

            int pageSize = 10;

            if (name != null && name.Trim() != "")
            {
                projects = projects.Where(project => project.ProjectName.Contains(name));
            }

            var count = projects.Count();
            var items = projects.Skip((page - 1) * pageSize).Take(pageSize);

            switch (sortOrder)
            {
                case SortState.ProjectNameDesc:
                    items = items.OrderByDescending(item => item.ProjectName);
                    break;
                case SortState.ProjectIsStoppedDesc:
                    items = items.OrderByDescending(item => item.IsStopped);
                    break;
            }

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PaginationViewModel<Project, ProjectFilterViewModel, ProjectSortViewModel> viewModel = new(items, pageViewModel, new ProjectFilterViewModel(name), new ProjectSortViewModel(sortOrder));

            return _context.Projects != null ? 
                          View(viewModel) :
                          Problem("Entity set 'ApplicationDbContext.Projects'  is null.");
        }

        [AllowAnonymous]
        public async Task<IActionResult> ProjectTasks(int? id, int page = 1)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Tasks)
                .ThenInclude(t => t.Employee)
                    .ThenInclude(t => t.Department)
                .FirstOrDefaultAsync(m => m.ProjectId == id);

            var tasks = project.Tasks.Where(t => t.IsCompleted == false);

            int pageSize = 10;

            var count = tasks.Count();
            var items = tasks.Skip((page - 1) * pageSize).Take(pageSize);

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PaginationViewModel<Task, TaskFilterViewModel, TaskSortViewModel> viewModel = new(items, pageViewModel, new TaskFilterViewModel(null, null, 0, 0), new TaskSortViewModel(ViewModels.Tasks.SortState.IsCompletedAsc));

            ViewBag.Project = project;

            return View(viewModel);
        }

        [AllowAnonymous]
        public async Task<IActionResult> RecentProjects(int page = 1, string name = "", SortState sortOrder = SortState.ProjectNameAsc)
        {
            DateTime time = DateTime.Now;

            IQueryable<Project> projects = _context.Projects.Include(p => p.Tasks)
                .Where(p => !p.IsStopped && 
                p.Tasks.Where(t => !t.IsCompleted
                && t.BeginDate.Year == time.Year && t.BeginDate.Month <= time.Month - 1).Count() != 0);

            int pageSize = 10;

            if (name != null && name.Trim() != "")
            {
                projects = projects.Where(project => project.ProjectName.Contains(name));
            }

            var count = projects.Count();
            var items = projects.Skip((page - 1) * pageSize).Take(pageSize);

            switch (sortOrder)
            {
                case SortState.ProjectNameDesc:
                    items = items.OrderByDescending(item => item.ProjectName);
                    break;
                case SortState.ProjectIsStoppedDesc:
                    items = items.OrderByDescending(item => item.IsStopped);
                    break;
            }

            ViewBag.ActionName = "RecentProjects";
            ViewBag.Header = "Недавние проекты";

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PaginationViewModel<Project, ProjectFilterViewModel, ProjectSortViewModel> viewModel = new(items, pageViewModel, new ProjectFilterViewModel(name), new ProjectSortViewModel(sortOrder));

            return _context.Projects != null ?
                          View("Index", viewModel) :
                          Problem("Entity set 'ApplicationDbContext.Projects'  is null.");
        }

        // GET: Projects/Details/5
        [Authorize(Roles = "Admin,MainAdmin,Employee")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        [Authorize(Roles = "Admin,MainAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,MainAdmin")]
        public async Task<IActionResult> Create([Bind("ProjectId,ProjectName,IsStopped")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "Admin,MainAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,MainAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,ProjectName,IsStopped")] Project project)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        [Authorize(Roles = "Admin,MainAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,MainAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Projects'  is null.");
            }
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
          return (_context.Projects?.Any(e => e.ProjectId == id)).GetValueOrDefault();
        }
    }
}
