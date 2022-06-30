using Exam_Back_End.DAL;
using Exam_Back_End.Models;
using Exam_Back_End.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_Back_End.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Team> teams = await _context.Teams.ToListAsync();
            List<Slider> sliders = await _context.Sliders.ToListAsync();
            HomeVM model = new HomeVM
            {
                Teams = teams,
                Sliders = sliders
            };
            return View(model);
        }
    }
}
