using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_Back_End.ViewModels
{
    public class LoginVM
    {
        [StringLength(maximumLength: 25)]
        public string Username { get; set; }

        [StringLength(maximumLength: 25), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
