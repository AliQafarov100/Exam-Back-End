using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_Back_End.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(maximumLength: 20)]
        public string FirstName { get; set; }
        [StringLength(maximumLength: 20)]
        public string LastName { get; set; }
    }
}
