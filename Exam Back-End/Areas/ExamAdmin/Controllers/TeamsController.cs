using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exam_Back_End.DAL;
using Exam_Back_End.Models;
using Exam_Back_End.Extensions;
using Exam_Back_End.Utilities;
using Microsoft.AspNetCore.Hosting;

namespace Exam_Back_End.Areas.ExamAdmin.Controllers
{
    [Area("ExamAdmin")]
    public class TeamsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamsController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: ExamAdmin/Teams
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teams.ToListAsync());
        }

        // GET: ExamAdmin/Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: ExamAdmin/Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExamAdmin/Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team team)
        {
            if (ModelState.IsValid)
            {
                if(team.Photo != null)
                {
                    if (team.Photo.IsOkay(1))
                    {
                        team.Image = await team.Photo.FileCreate(_env.WebRootPath, @"assets\Image\Clients");
                    }

                }
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: ExamAdmin/Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: ExamAdmin/Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }
            Team existed = await _context.Teams.FirstOrDefaultAsync(p => p.Id == id);

            if (ModelState.IsValid)
            {
                try
                {
                    if (team.Photo.IsMoreMb(1))
                    {
                        string path = _env.WebRootPath + @"assets\Image\Clients" + existed.Image;

                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                        existed.Image = await team.Photo.FileCreate(_env.WebRootPath, @"assets\Image\Clients");

                    }
                    existed.FullName = team.FullName;
                    existed.Proffession = team.Proffession;
                    existed.About = team.About;
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
            return View(team);
        }

        // GET: ExamAdmin/Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: ExamAdmin/Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            string path = _env.WebRootPath + @"assets\Image\Clients" + team.Image;

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}
