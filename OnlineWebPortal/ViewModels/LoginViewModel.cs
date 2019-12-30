using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineWebPortal.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Username"), Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Display(Name = "Remember me"), Required]
        public bool RememberMe { get; set; }
    }
}
