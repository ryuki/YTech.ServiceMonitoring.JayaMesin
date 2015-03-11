using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YTech.ServiceTracker.JayaMesin.Web.Mvc.Controllers.ViewModels
{
    public class WODetailViewModel
    {
        public WODetailViewModel()
        {
        }

        public IList<WOStatusViewModel> WOStatus { get; set; }
        public WOViewModel Wo { get; set; }
    }
}
