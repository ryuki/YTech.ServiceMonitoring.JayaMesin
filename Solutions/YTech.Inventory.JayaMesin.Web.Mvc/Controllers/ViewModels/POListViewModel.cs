using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.Inventory.JayaMesin.Web.Mvc.Controllers.ViewModels
{
    public class POListViewModel
    {
        public bool UserCanUpdateStatus { get; set; }

        public bool UserCanEdit { get; set; }
        public bool UserCanDelete { get; set; }
    }
}
