using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineWebPortal.Models
{
    public class Address
    {
        public string GetAddress()
        {
            string myAdd = this.StreetNumber + this.StreetName + this.Postcode + this.Town;
            return myAdd;
        }
        [Key]
        public int ID { get; set; }
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Street Name")]
        public string StreetName { get; set; }
        [Display(Name = "Street Number")]
        public string StreetNumber { get; set; }
        public string Postcode { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public string Town { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public string Country { get; set; }
        //public virtual RegUser RegUser { get; set; }
    }
}
