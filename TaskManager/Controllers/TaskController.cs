using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using TaskManager.Model;
using TaskManager.Repository;

namespace TaskManager.Controllers
{
    public class TaskController : Controller
    {
        private readonly CDBContext _context = new CDBContext();

        public TaskController(CDBContext context)
        {
            context = _context;
        }

        //GET: Functions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tasks.ToListAsync());
        }

        //GET: Functions/Details
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0 || _context.Tasks == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks.FirstOrDefaultAsync(m => m.Id == id);
            if (tasks == null)
            {
                return NotFound();
            }
            /*ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "Id", "DepartmentName", tasks.DepartmentID);*/
            return View(tasks);

        }

        //GET: Functions/Create
        public IActionResult Create()
        {
            List<SelectListItem> Priorityid = new List<SelectListItem>() {
                                    new SelectListItem {
                                        Text = "Immediate", Value = "1"
                                    },new SelectListItem {
                                        Text = "High", Value = "2"
                                    },
                                    new SelectListItem{
                                        Text = "Medium", Value = "3"
                                    },
                                    new SelectListItem{
                                        Text = "Low", Value = "4"
                                    }
                                };
            ViewBag.PriorityID = Priorityid;
            List<SelectListItem> Employeeid = new List<SelectListItem>() {
                                    new SelectListItem {
                                        Text = "E-One", Value = "1"
                                    },new SelectListItem {
                                        Text = "E-One", Value = "2"
                                    }
                                };
            ViewBag.EmployeeID = Employeeid;
            return View();
        }

        //POST: Functions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TaskName,PriorityID,EstTime,EmployeeID,DueDate,CreatedBy,CreatedDate")] Task_tbl tasks)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Tasks.Add(tasks);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {

                }
            }
            return View();
        }

        //GET: Functions/Edit
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0 || _context.Tasks == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return NotFound();
            }
            List<SelectListItem> Priorityid = new List<SelectListItem>() {
                                    new SelectListItem {
                                        Text = "Immediate", Value = "1"
                                    },new SelectListItem {
                                        Text = "High", Value = "2"
                                    },
                                    new SelectListItem{
                                        Text = "Medium", Value = "3"
                                    },
                                    new SelectListItem{
                                        Text = "Low", Value = "4"
                                    }
                                };
            ViewBag.PriorityID = Priorityid;
            List<SelectListItem> Employeeid = new List<SelectListItem>() {
                                    new SelectListItem {
                                        Text = "E-One", Value = "1"
                                    },new SelectListItem {
                                        Text = "E-One", Value = "2"
                                    }
                                };
            ViewBag.EmployeeID = Employeeid;
            return View(tasks);
        }

        //POST: Department/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TaskName,PriorityID,EstTime,EmployeeID,DueDate,CreatedBy,CreatedDate")] Task_tbl tasks)
        {
            if (id == 0 || _context.Tasks == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(tasks).State = System.Data.Entity.EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!FunctionExists(tasks.Id))
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
            
            return View(tasks);
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