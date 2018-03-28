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
    public class TStockInputViewModel
    {
        #region Properties

        public List<JmInventoryTStockViewModel> Stocks { get; set; }

        [HiddenInput]
        public string TransDetId
        {
            get;
            set;
        }

        [ScaffoldColumn(true)]
        [HiddenInput(DisplayValue = false)]
        [DisplayName(" ")]
        public string TransId
        {
            get;
            set;
        }

        [DisplayName("Supplier")]
        [UIHint("Supplier")]
        [Required]
        public string SupplierId { get; set; }

        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string SupplierName { get; set; }

        //[DisplayName("Supplier")]
        //[UIHint("Supplier")]
        //public JmInventoryMSupplierViewModel SupplierIdForEdit { get; set; }

        [DisplayName("No PO")]
        [Required]
        public virtual string TransNo { get; set; }

        [DisplayName("Tgl PO")]
        [UIHint("Date")]
        [Required]
        public virtual DateTime? TransDate { get; set; }

        [DisplayName("Status")]
        [UIHint("TransStatus")]
        public virtual string TransStatus { get; set; }

        [DisplayName("No Invoice")]
        public virtual string InvoiceNo { get; set; }

        [DisplayName("Keterangan")]
        [UIHint("TextArea")]
        public virtual string TransDesc { get; set; }

        [DisplayName("TransSubTotal")]
        public virtual decimal? TransSubTotal { get; set; }

        [DisplayName("TransDisc")]
        public virtual decimal? TransDisc { get; set; }

        [DisplayName("TransPpn")]
        public virtual decimal? TransPpn { get; set; }

        [DisplayName("TransCost")]
        public virtual decimal? TransCost { get; set; }

        [DisplayName("TransGrandTotal")]
        public virtual decimal? TransGrandTotal { get; set; }

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


        public bool UserCanUpdateStatus { get; set; }
        public bool UserCanEdit { get; set; }
        public bool UserCanDelete { get; set; }
        public bool UserCanView { get; set; }
        public bool UserCanInputSN { get; set; }


        #endregion
    }
}