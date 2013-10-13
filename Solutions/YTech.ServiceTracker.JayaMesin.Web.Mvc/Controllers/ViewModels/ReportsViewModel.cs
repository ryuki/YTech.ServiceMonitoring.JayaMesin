using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YTech.ServiceTracker.JayaMesin.Web.Mvc.Controllers.ViewModels
{
    public class ReportsViewModel
    {
        public ReportsViewModel()
        {
            RptDateFrom = DateTime.Today.AddDays(-7);
            RptDateTo = DateTime.Today;
        }

        [DisplayName("Dari Tanggal")]
        [UIHint("Date")]
        public DateTime? RptDateFrom
        {
            get;
            set;
        }

        [DisplayName("Sampai Tanggal")]
        [UIHint("Date")]
        public DateTime? RptDateTo
        {
            get;
            set;
        }
    }
}