using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_Back_End.ViewModels
{
    public class EditUserVM
    {
        [StringLength(maximumLength: 20)]

        public string FirstName { get; set; }
        [StringLength(maximumLength: 20)]
        public string LastName { get; set; }
        [StringLength(maximumLength: 25)]
        public string Username { get; set; }
        [StringLength(maximumLength: 25), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(maximumLength: 25), DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [StringLength(maximumLength: 25), DataType(DataType.Password)]
        public string Password { get; set; }
        [StringLength(maximumLength: 25), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
