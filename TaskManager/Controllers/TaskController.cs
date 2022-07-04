using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TaskController(CDBContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context = _context;
            _userManager = userManager;
            _roleManager = roleManager; 
        }

        //GET: Task
        public async Task<IActionResult> Index()
        {
            await getEmployees();
            return View(await _context.Tasks.ToListAsync());
        }

        //GET: Task/Details
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
            await getEmployees();
            return View(tasks);

        }

        public async Task getEmployees()
        {
            bool employeeRoleExists = await _roleManager.RoleExistsAsync("employee");
            if(employeeRoleExists)
            {
                var resUsers = await _userManager.GetUsersInRoleAsync("employee");
                List<SelectListItem> Employeeid = resUsers
                              .Select(a => new SelectListItem()
                              {
                                  Value = a.Id,
                                  Text = a.UserName
                              }).ToList();

                ViewData["EmployeeID"] = Employeeid;
            }
        }



        //GET: Task/Create
        public async Task<IActionResult> Create()
        {
            await getEmployees();
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
            
            return View();
        }

        //POST: Task/Create
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

        //GET: Task/Edit
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
            await getEmployees();
            return View(tasks);
        }

        //POST: Task/Edit
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
                    if (!TaskExists(tasks.Id))
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

        //GET: Task/Delete
        public async Task<IActionResult> Delete(int id)
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
            return View(tasks);
        }

        //POST: Task/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == 0 || _context.Tasks == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks != null)
            {
                _context.Tasks.Remove(tasks);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}