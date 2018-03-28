using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace YTech.ServiceTracker.JayaMesin.Web.Mvc.Controllers.ViewModels
{
    public class SmsViewModel
    {
        #region Properties
    
        [ScaffoldColumn(false)]
        public string SmsId
        {
            get;
            set;
        }
        
        
        [DisplayName("Penerima")]
        public virtual string SmsRecipient { get; set; }
        
        [DisplayName("Isi SMS")]
        public virtual string SmsText { get; set; }
        
        [DisplayName("Laporan SMS")]
        public virtual string SmsReport { get; set; }
        
        [DisplayName("Status")]
        public virtual string SmsStatus { get; set; }
        
        [DisplayName("Keterangan")]
        public virtual string SmsDesc { get; set; }
        
        [DisplayName("Tgl SMS")]
        public virtual System.DateTime? SmsDate { get; set; }
        
        [DisplayName("No WO")]
        public virtual string WoId { get; set; }
        

        #endregion
    }
}
