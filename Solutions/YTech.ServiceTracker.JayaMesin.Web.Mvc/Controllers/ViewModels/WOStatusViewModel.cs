using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YTech.ServiceTracker.JayaMesin.Web.Mvc.Controllers.ViewModels
{
    public class WOStatusViewModel
    {
        [ScaffoldColumn(false)]
        public string WOStatusId
        {
            get;
            set;
        }

        [DisplayName("User")]
        public string WOStatusUser
        {
            get;
            set;
        }

        [DisplayName("Tanggal Update")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy HH:mm}")]
        public DateTime? WOStatusDate
        {
            get;
            set;
        }

        [DisplayName("Status")]
        public string WOStatus
        {
            get;
            set;
        }

        [DisplayName("Update")]
        public string WOStatusBrokenDesc
        {
            get;
            set;
        }

        [DisplayName("Tanggal Mulai Dikerjakan")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy HH:mm}")]
        public DateTime? WOStatusStartDate
        {
            get;
            set;
        }

        [DisplayName("Tanggal Selesai Dikerjakan")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy HH:mm}")]
        public DateTime? WOStatusFinishDate
        {
            get;
            set;
        }

        [DisplayName("Keterangan Status")]
        public string WOStatusDesc
        {
            get;
            set;
        }
    }
}