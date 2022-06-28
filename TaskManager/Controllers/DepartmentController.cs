using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using TaskManager.Model;
using TaskManager.Repository;

namespace TaskManager.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly CDBContext _context = new CDBContext();

        public DepartmentController(CDBContext context)
        {
             _context = context;
        }

        //GET: Department
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departments.ToListAsync());
        }

        //GET: Department/Details
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0 || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);

        }

        //GET: Department/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartmentName,CreatedBy,CreatedDate")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        //GET: Department/Edit
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0 || _context.Departments == null)
            {
                return NotFound();
            }

            var dept = await _context.Departments.FindAsync(id);
            if (dept == null)
            {
                return NotFound();
            }
            return View(dept);
        }

        //POST: Department/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartmentName,CreatedBy,CreatedDate")] Department department)
        {
            if (id == 0 || _context.Departments == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(department).State = System.Data.Entity.EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
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

        //GET: Department/Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0 || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        //POST: Department/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == 0 || _context.Departments == null)
            {
                return NotFound();
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
            return _context.Departments.Any(e => e.Id == id);
        }
    }
}