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
using Lab_5.ViewModels.Employees;
using Microsoft.Data.SqlClient;
using System.Drawing.Printing;

namespace Lab_5.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin,MainAdmin,Employee")]
        [AllowAnonymous]
        // GET: Employees
        public async Task<IActionResult> Index(int page = 1, int postId = 0, string name = "", SortState sortOrder = SortState.EmployeeNameAsc)
        {
            int pageSize = 10;

            IQueryable<Employee> source = _context.Employees.Include(e => e.Department).Include(e => e.Post);

            if (postId != 0)
            {
                source = source.Where(emp => emp.PostId == postId);
            }
            if (name != null && name.Trim() != "")
            {
                source = source.Where(emp => emp.EmployeeName.Contains(name));
            }

            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);

            switch (sortOrder)
            {
                case SortState.EmployeeNameDesc:
                    items = items.OrderByDescending(s => s.EmployeeName);
                    break;
                case SortState.EmployeeMiddleNameDesc:
                    items = items.OrderByDescending(s => s.EmployeeMiddlename);
                    break;
                case SortState.EmployeeSurnameDesc:
                    items = items.OrderByDescending(s => s.EmployeeSurname);
                    break;
                case SortState.EmployeeDepartmentDesc:
                    items = items.OrderByDescending(s => s.Department.DepartmentName);
                    break;
                case SortState.EmployeePostDesc:
                    items = items.OrderByDescending(s => s.Post.PostName);
                    break;
            }

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PaginationViewModel<Employee, EmployeesFilterViewModel, EmployeeSortViewModel> viewModel = new 
                (items, pageViewModel, new EmployeesFilterViewModel(_context.Posts.ToList(), name, postId), new EmployeeSortViewModel(sortOrder));


            return items != null ?
                          View(viewModel) :
                          Problem("Entity set 'ApplicationDbContext.Employees'  is null.");
        }

        // GET: Employees/Details/5
        [Authorize(Roles = "Admin,MainAdmin,Employee")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Post)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        [Authorize(Roles = "Admin,MainAdmin")]
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,MainAdmin")]
        public async Task<IActionResult> Create([Bind("EmployeeId,EmployeeName,EmployeeSurname,EmployeeMiddlename,PostId,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostName", employee.PostId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        [Authorize(Roles = "Admin,MainAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostName", employee.PostId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,MainAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,EmployeeName,EmployeeSurname,EmployeeMiddlename,PostId,DepartmentId")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostName", employee.PostId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        [Authorize(Roles = "Admin,MainAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Post)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,MainAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
          return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
