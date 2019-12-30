using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineWebPortal.Models
{
    public class RegUser
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "First Name"), StringLength(60, MinimumLength = 3), Required]
        public string FirstName { get; set; }
        [Display(Name = "Last Name"), StringLength(60, MinimumLength = 3), Required]
        public string LastName { get; set; }
        [StringLength(60, MinimumLength = 3), Required]
        public string Username { get; set; }
        [StringLength(60, MinimumLength = 6), Required]
        public string Password { get; set; }
        public string Roles { get; set; }
        public string Sex { get; set; }
        [Display(Name = "Marital Status"), Required]
        public string MaritalStatus { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth"),Required]
        public DateTime? DateOFBirth { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number"), RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress), Required]
        public string Email { get; set; }
        [Display(Name = "Membership Type")]
        public string MemType { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Membership Type")]
        public DateTime DateOfMembership { get; set; }

        //related entities
        public virtual Address Address { get; set; }
        public virtual ICollection<RegUserChurchGroup> RegUserChurchGroups { get; set; }
        public virtual ICollection<Enquiry> Enquiries { get; set; }
        public IList<Payment> Payments { get; set; }
    }
}
