using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab18_WebApp.Models
{
    public class RegisterUser
    {
        [Required]
        [MaxLength(25, ErrorMessage = "Your user name length must be between 3 and 25 characters"), MinLength(3, ErrorMessage = "Your user name length must be between 3 and 25 characters")]
        public string UserName { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required, MaxLength(20, ErrorMessage = "Password must contain atleast 5 letters and 1 Number"), MinLength(6, ErrorMessage = "Password must contain atleast 5 letters and 1 Number"), RegularExpression(@".*[0-9].*", ErrorMessage = "Password must contain atleast 5 letters and 1 Number")]
        public string UserPass { get; set; }

    }
}
