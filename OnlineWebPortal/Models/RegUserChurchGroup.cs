using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnlineWebPortal.Models
{
    public class RegUserChurchGroup
    {
        public int RegUserID { get; set; }
        public int ChurchGroupID { get; set; }
        public virtual RegUser RegUsers { get; set; }
        public virtual ChurchGroup ChurchGroups { get; set; }
    }
}
