using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using TaskManager.Model;
using TaskManager.Repository;

namespace TaskManager.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly CDBContext _context = new CDBContext();

        public EmployeesController(CDBContext context)
        {
             _context = context;
        }

        //GET: Employee
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employee.ToListAsync());
        }

        //GET: Employees/Details
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0 || _context.Employee == null)
            {
                return NotFound();
            }

            var employees = await _context.Employee.FirstOrDefaultAsync(m => m.Id == id);
            if (employees == null)
            {
                return NotFound();
            }
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "Id", "DepartmentName", employees.DepartmentID);
            return View(employees);

        }

        //GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "Id", "DepartmentName");
            return View();
        }

        //POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, FirstName, LastName, Email, Phone, DepartmentID, Reportingto, Role, Function, Team, CreatedBy, CreatedDate")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                _context.Employee.Add(employees);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "Id", "DepartmentName", employees.DepartmentID);
            return View();
        }

        //GET: Employees/Edit
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0 || _context.Employee == null)
            {
                return NotFound();
            }

            var employees = await _context.Employee.FindAsync(id);
            if (employees == null)
            {
                return NotFound();
            }
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "Id", "DepartmentName", employees.DepartmentID);
            return View(employees);
        }

        //POST: Employees/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, FirstName, LastName, Email, Phone, DepartmentID, Reportingto, Role, Function, Team, CreatedBy, CreatedDate")] Employees employees)
        {
            if (id == 0 || _context.Employee == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(employees).State = System.Data.Entity.EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employees.Id))
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
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "Id", "DepartmentName", employees.DepartmentID);
            return View(employees);
        }

        //GET: Employees/Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0 || _context.Employee == null)
            {
                return NotFound();
            }

            var employees = await _context.Employee.FirstOrDefaultAsync(m => m.Id == id);
            if (employees == null)
            {
                return NotFound();
            }
            return View(employees);
        }

        //POST: Employees/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == 0 || _context.Employee == null)
            {
                return NotFound();
            }

            var employees = await _context.Employee.FindAsync(id);
            if (employees != null)
            {
                _context.Employee.Remove(employees);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}

