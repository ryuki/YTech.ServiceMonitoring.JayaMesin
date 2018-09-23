using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace YTech.Inventory.JayaMesin.Web.Mvc.Controllers.ViewModels
{
    public class JmIntrackTInvoiceLogViewModel
    {
        #region Properties
    
        [ScaffoldColumn(false)]
        public string InvoiceId
        {
            get;
            set;
        }
        
        
        [DisplayName("InvoiceNo")]
        public virtual string InvoiceNo { get; set; }
        
        [DisplayName("InvoicePacketNo")]
        public virtual string InvoicePacketNo { get; set; }
        
        [DisplayName("InvoiceDate")]
        public virtual System.DateTime? InvoiceDate { get; set; }
        
        [DisplayName("CustomerId")]
        public virtual string CustomerId { get; set; }
        
        [DisplayName("InvoiceAmmount")]
        public virtual decimal? InvoiceAmmount { get; set; }
        
        [DisplayName("InvoicePaid")]
        public virtual decimal? InvoicePaid { get; set; }
        
        [DisplayName("InvoicePacketAdmin")]
        public virtual string InvoicePacketAdmin { get; set; }
        
        [DisplayName("SalesmanId")]
        public virtual string SalesmanId { get; set; }
        
        [DisplayName("InvoiceAge")]
        public virtual decimal? InvoiceAge { get; set; }
        
        [DisplayName("InvoiceDueDate")]
        public virtual System.DateTime? InvoiceDueDate { get; set; }
        
        [DisplayName("InvoiceDelivery")]
        public virtual string InvoiceDelivery { get; set; }
        
        [DisplayName("InvoicePaidOffDate")]
        public virtual System.DateTime? InvoicePaidOffDate { get; set; }
        
        [DisplayName("InvoiceStatus")]
        public virtual string InvoiceStatus { get; set; }
        
        [DisplayName("InvoiceDesc")]
        public virtual string InvoiceDesc { get; set; }
        
        [DisplayName("InvoiceDocLetter")]
        public virtual string InvoiceDocLetter { get; set; }
        
        [DisplayName("InvoiceDocSspPpn")]
        public virtual string InvoiceDocSspPpn { get; set; }
        
        [DisplayName("InvoiceDocSspPph")]
        public virtual string InvoiceDocSspPph { get; set; }
        

        #endregion
    }
}
