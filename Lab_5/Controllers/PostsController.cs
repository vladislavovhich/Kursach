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
using Lab_5.ViewModels.Posts;

namespace Lab_5.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Posts
        [Authorize(Roles = "Admin,MainAdmin,Employee")]
        [AllowAnonymous]

        public async Task<IActionResult> Index(int page = 1, string postName = "", decimal salary = 0, SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<Post> posts = _context.Posts;

            int pageSize = 10;

            if (postName != null && postName.Trim() != "")
            {
                posts = posts.Where(p => p.PostName.Contains(postName));
            }

            if (salary != null && salary != 0)
            {
                posts = posts.Where(p => p.PostSalary >=  salary);
            }

            var count = posts.Count();
            var items = posts.Skip((page - 1) * pageSize).Take(pageSize);

            switch (sortOrder)
            {
                case SortState.SalaryDesc:
                    items = items.OrderByDescending(item => item.PostSalary);
                    break;
                case SortState.NameDesc:
                    items = items.OrderByDescending(item => item.PostName);
                    break;
            }

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PaginationViewModel<Post, PostFilterViewModel, PostSortViewModel> viewModel = new(items, pageViewModel, new PostFilterViewModel(postName, salary), new PostSortViewModel(sortOrder));

            return _context.Posts != null ? 
                          View(viewModel) :
                          Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
        }


        // GET: Posts/Details/5
        [Authorize(Roles = "Admin,MainAdmin,Employee")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        [Authorize(Roles = "Admin,MainAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,MainAdmin")]
        public async Task<IActionResult> Create([Bind("PostId,PostName,PostSalary")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize(Roles = "Admin,MainAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,MainAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,PostName,PostSalary")] Post post)
        {
            if (id != post.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
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
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Admin,MainAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,MainAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
            }
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
          return (_context.Posts?.Any(e => e.PostId == id)).GetValueOrDefault();
        }
    }
}
