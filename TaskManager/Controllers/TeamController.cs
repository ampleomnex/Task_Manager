using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using TaskManager.Model;
using TaskManager.Repository;

namespace TaskManager.Controllers
{
    public class TeamController : Controller
    {
        private readonly CDBContext _context = new CDBContext();

        public TeamController(CDBContext context)
        {
             _context = context;
        }

        //GET: Team
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teams.ToListAsync());
        }

        //GET: Team/Details
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0 || _context.Teams == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "Id", "DepartmentName", team.DepartmentID);

            return View(team);

        }

        //GET: Team/Create
        public IActionResult Create()
        {
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "Id", "DepartmentName");

            return View();
        }

        //POST: Team/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TeamName,DepartmentID,CreatedBy,CreatedDate")] Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Teams.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "Id", "DepartmentName", team.DepartmentID);

            return View();
        }

        //GET: Team/Edit
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0 || _context.Teams == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "Id", "DepartmentName", team.DepartmentID);

            return View(team);
        }

        //POST: Department/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeamName,DepartmentID,CreatedBy,CreatedDate")] Team team)
        {
            if (id == 0 || _context.Teams == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(team).State = System.Data.Entity.EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.Id))
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
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "Id", "DepartmentName", team.DepartmentID);

            return View(team);
        }

        //GET: Department/Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0 || _context.Teams == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "Id", "DepartmentName", team.DepartmentID);

            return View(team);
        }

        //POST: Team/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == 0 || _context.Teams == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            if (team != null)
            {
                _context.Teams.Remove(team);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}