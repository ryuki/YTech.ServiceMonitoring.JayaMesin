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
    public class TransReceiptViewModel
    {
        #region Properties

        [ScaffoldColumn(true)]
        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string TransId
        {
            get;
            set;
        }

        [DisplayName("No PO")]
        [UIHint("POListSelect")]
        public string TransRefId { get; set; }
        
        [DisplayName("Supplier")]
        [UIHint("Supplier")]
        public string SupplierId { get; set; }
        
        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string SupplierName { get; set; }
                
        [DisplayName("No Penerimaan")]
        public virtual string TransNo { get; set; }

        [DisplayName("Tgl Penerimaan")]
        [UIHint("Date")]
        public virtual System.DateTime? TransDate { get; set; }

        [DisplayName("Status")]
        [UIHint("TransStatus")]
        public virtual string TransStatus { get; set; }

        [DisplayName("No Invoice")]
        public virtual string InvoiceNo { get; set; }

        [DisplayName("Keterangan")]
        [UIHint("TextArea")]
        public virtual string TransDesc { get; set; }

        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public virtual string TransInputBy { get; set; }

        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public virtual System.DateTime? TransInputDate { get; set; }

        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public virtual string TransApprovedBy { get; set; }

        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public virtual System.DateTime? TransApprovedDate { get; set; }        

        #endregion
    }
}
