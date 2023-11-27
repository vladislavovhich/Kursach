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
using Microsoft.AspNetCore.Authorization;
using Lab_5.ViewModels;
using Lab_5.ViewModels.Tasks;

namespace Lab_5.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tasks
        [Authorize(Roles = "Admin,MainAdmin, Employee")]
        [AllowAnonymous]

        public async Task<IActionResult> Index(int page = 1, int projectId = 0, int employeeId = 0, SortState sortOrder = SortState.ProjectAsc)
        {
            IQueryable<Task> applicationDbContext = _context.Tasks.Include(t => t.Employee).Include(t => t.Project);

            int pageSize = 10;

            IQueryable<Task> source = applicationDbContext;

            if (projectId != 0)
            {
                source = source.Where(task => task.ProjectId == projectId);
            }

            if (employeeId != 0)
            {
                source = source.Where(task => task.Employee.EmployeeId == employeeId);
            }

            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);

            switch (sortOrder)
            {
                case SortState.BeginDateDesc:
                    items = items.OrderByDescending(item => item.BeginDate);
                    break;
                case SortState.CheckDateDesc:
                    items = items.OrderByDescending(item => item.CheckDate);
                    break;
                case SortState.EndDateDesc:
                    items = items.OrderByDescending(item => item.EndDate);
                    break;
                case SortState.EmployeeDesc:
                    items = items.OrderByDescending(item => item.Employee.EmployeeName);
                    break;
                case SortState.ProjectDesc:
                    items = items.OrderByDescending(item => item.Project.ProjectName);
                    break;
                case SortState.IsCompletedDesc:
                    items = items.OrderByDescending(item => item.IsCompleted);
                    break;
                case SortState.FailedReasonDesc:
                    items = items.OrderByDescending(item => item.FailedReason);
                    break;
            }

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PaginationViewModel<Task, TaskFilterViewModel, TaskSortViewModel> viewModel = new(items, pageViewModel, new TaskFilterViewModel(_context.Projects.ToList(), _context.Employees.ToList(), projectId, employeeId), new TaskSortViewModel(sortOrder));

            return View(viewModel);
        }

        // GET: Tasks/Details/5
        [Authorize(Roles = "Admin,MainAdmin, Employee")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.Employee)
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

       
        // GET: Tasks/Create
        [Authorize(Roles = "Admin,MainAdmin")]
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,MainAdmin")]
        public async Task<IActionResult> Create([Bind("TaskId,BeginDate,EndDate,IsCompleted,FailedReason,CheckDate,ProjectId,EmployeeId")] Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName", task.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName", task.ProjectId);
            return View(task);
        }

        // GET: Tasks/Edit/5
        [Authorize(Roles = "Admin,MainAdmin,Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName", task.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName", task.ProjectId);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,MainAdmin,Employee")]
        public async Task<IActionResult> Edit(int id, [Bind("TaskId,BeginDate,EndDate,IsCompleted,FailedReason,CheckDate,ProjectId,EmployeeId")] Task task)
        {
            if (id != task.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.TaskId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName", task.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName", task.ProjectId);
            return View(task);
        }

        // GET: Tasks/Delete/5
        [Authorize(Roles = "Admin,MainAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.Employee)
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,MainAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tasks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tasks'  is null.");
            }
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
          return (_context.Tasks?.Any(e => e.TaskId == id)).GetValueOrDefault();
        }
    }
}
