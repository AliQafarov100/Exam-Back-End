using Exam_Back_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_Back_End.ViewModels
{
    public class HomeVM
    {
        public List<Team> Teams { get; set; }
        public List<Slider> Sliders { get; set; }
    }
}
