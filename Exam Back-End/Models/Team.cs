using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_Back_End.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string FullName { get; set; }
        public string Proffession { get; set; }
        public string About { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
