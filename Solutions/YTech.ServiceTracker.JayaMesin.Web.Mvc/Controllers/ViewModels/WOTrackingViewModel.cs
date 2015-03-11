using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YTech.ServiceTracker.JayaMesin.Web.Mvc.Controllers.ViewModels
{
    public class WOTrackingViewModel
    {
        public WOTrackingViewModel()
        {
        }

        public string Search { get; set; }
        public string StatusMessage { get; set; }
        public IList<WOViewModel> WOs { get; set; }
        public bool FirstLoad { get; set; }
    }
}
