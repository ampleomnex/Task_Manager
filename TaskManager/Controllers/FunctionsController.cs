using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using TaskManager.Model;
using TaskManager.Repository;

namespace TaskManager.Controllers
{
    public class FunctionsController : Controller
    {
        private readonly CDBContext _context = new CDBContext();

        public FunctionsController(CDBContext context)
        {
            context = _context;
        }

        //GET: Functions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Functions.ToListAsync());
        }

        //GET: Functions/Details
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0 || _context.Functions == null)
            {
                return NotFound();
            }

            var functions = await _context.Functions.FirstOrDefaultAsync(m => m.Id == id);
            if (functions == null)
            {
                return NotFound();
            }
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "Id", "DepartmentName", functions.DepartmentID);
            return View(functions);

        }

        //GET: Functions/Create
        public IActionResult Create()
        {
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "Id", "DepartmentName");
            return View();
        }

        //POST: Functions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FunctionName,DepartmentID,CreatedBy,CreatedDate")] Function functions)
        {
            if (ModelState.IsValid)
            {
                _context.Functions.Add(functions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "Id", "DepartmentName", functions.DepartmentID);
            return View();
        }

        //GET: Functions/Edit
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0 || _context.Functions == null)
            {
                return NotFound();
            }

            var func = await _context.Functions.FindAsync(id);
            if (func == null)
            {
                return NotFound();
            }
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "Id", "DepartmentName", func.DepartmentID);
            return View(func);
        }

        //POST: Department/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FunctionName,DepartmentID,CreatedBy,CreatedDate")] Function functions)
        {
            if (id == 0 || _context.Functions == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(functions).State = System.Data.Entity.EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!FunctionExists(functions.Id))
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
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "Id", "DepartmentName", functions.DepartmentID);
            return View(functions);
        }

        //GET: Functions/Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0 || _context.Functions == null)
            {
                return NotFound();
            }

            var functions = await _context.Functions.FirstOrDefaultAsync(m => m.Id == id);
            if (functions == null)
            {
                return NotFound();
            }
            return View(functions);
        }

        //POST: Function/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == 0 || _context.Functions == null)
            {
                return NotFound();
            }

            var functions = await _context.Functions.FindAsync(id);
            if (functions != null)
            {
                _context.Functions.Remove(functions);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FunctionExists(int id)
        {
            return _context.Functions.Any(e => e.Id == id);
        }
    }
}