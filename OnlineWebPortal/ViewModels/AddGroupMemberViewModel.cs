using OnlineWebPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineWebPortal.ViewModels
{
    public class AddGroupMemberViewModel
    {
        public int RegUserID { get; set; }
        public int ChurchGroupID { get; set; }
        public virtual RegUser RegUsers { get; set; }
        public virtual ChurchGroup ChurchGroups { get; set; }
    }
}
