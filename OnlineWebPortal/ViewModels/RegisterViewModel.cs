using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineWebPortal.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "First name"), StringLength(60, MinimumLength = 3), Required]
        public string FirstName { get; set; }
        [Display(Name = "Last name"), StringLength(60, MinimumLength = 3), Required]
        public string LastName { get; set; }
        [StringLength(60, MinimumLength = 3), Required]
        public string Username { get; set; }
        [StringLength(60, MinimumLength = 6), Required]
        public string Password { get; set; }
        [Compare("Password")]
        [Display(Name = "Confirm password"), Required]
        public string ConfirmPassword { get; set; }
        public string Roles { get; set; }
        public string Sex { get; set; }
        [Display(Name = "Marital status"), Required]
        public string MaritalStatus { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true), Required]
        public DateTime? DateOFBirth { get; set; }
        [DataType(DataType.PhoneNumber)]
        //[Display(Name = "Phone number"), RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress), Required]
        public string Email { get; set; }
        [Display(Name = "Membership type")]
        public string MemType { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of membership"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfMembership { get; set; }
    }
}
