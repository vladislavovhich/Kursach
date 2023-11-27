using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab_5;
using Lab_5.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using Lab_5.ViewModels;
using Lab_5.ViewModels.Departments;

namespace Lab_5.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin,MainAdmin,Employee")]
        [AllowAnonymous]
        // GET: Departments
        public IActionResult Index(int page = 1, SortState sortOrder = SortState.DepartmentNameAsc, string name = "", int phone = 0)
        {
            IQueryable<Department> departments = _context.Departments;

            int pageSize = 10;

            if (name != null && name.Trim() != "")
            {
                departments = departments.Where(d => d.DepartmentName.Contains(name));
            }

            if (phone != null && phone != 0)
            {
                departments = departments.Where(d => d.DepartmentPhone.ToString().Contains(phone.ToString()));
            }

            var count = departments.Count();
            var items = departments.Skip((page - 1) * pageSize).Take(pageSize);

            switch (sortOrder)
            {
                case SortState.DepartmentNameDesc:
                    items = items.OrderByDescending(item => item.DepartmentName);
                    break;
                case SortState.DepartmentPhoneDesc:
                    items = items.OrderByDescending(item => item.DepartmentPhone);
                    break;
            }

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PaginationViewModel<Department, DepartmentFilterViewModel, DepartmentSortViewModel> viewModel = new(items, pageViewModel, new DepartmentFilterViewModel(name, phone), new DepartmentSortViewModel(sortOrder));

            return items != null ?
                          View(viewModel) :
                          Problem("Entity set 'ApplicationDbContext.Departments'  is null.");
        }

        // GET: Departments/Details/5
        [Authorize(Roles = "Admin,MainAdmin, Employee")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        [Authorize(Roles = "Admin,MainAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,MainAdmin")]
        public async Task<IActionResult> Create([Bind("DepartmentId,DepartmentName,DepartmentPhone")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        [Authorize(Roles = "Admin,MainAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,MainAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentId,DepartmentName,DepartmentPhone")] Department department)
        {
            if (id != department.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.DepartmentId))
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
            return View(department);
        }

        // GET: Departments/Delete/5
        [Authorize(Roles = "Admin,MainAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,MainAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Departments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Departments'  is null.");
            }
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
          return (_context.Departments?.Any(e => e.DepartmentId == id)).GetValueOrDefault();
        }
    }
}
