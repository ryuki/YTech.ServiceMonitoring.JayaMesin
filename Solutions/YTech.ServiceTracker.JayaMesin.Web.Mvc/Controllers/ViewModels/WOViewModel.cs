using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Web.Mvc.Controllers.ViewModels
{
    public class WOViewModel
    {
        [ScaffoldColumn(false)]
        //string _WOID;
        //public string WOID
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(_WOID))
        //            _WOID = Guid.NewGuid().ToString();
        //        return _WOID;
        //    }
        //    set { _WOID = value; }
        //}
        public string WOID
        {
            get;
            set;
        }

        [Required]
        [DisplayName("No WO")]
        public string WONo
        {
            get;
            set;
        }

        [DisplayName("Nama Konsumen")]
        public string CustomerName
        {
            get;
            set;
        }

        CustomerViewModel _Customer;
        [Required]
        [DisplayName("Konsumen")]
        [UIHint("Customer")]
        public CustomerViewModel Customer
        {
            get
            {
                //if (_Customer == null)
                //{
                //    _Customer = new CustomerViewModel();
                //    _Customer.CustomerID = string.Empty;
                //    _Customer.CustomerName = "Customer baru";
                //}
                return _Customer;
            }
            set { _Customer = value; }
        }
        //public CustomerViewModel Customer { get; set; }

        [Required]
        [DisplayName("Tgl WO")]
        [UIHint("Date")]
        public DateTime? WODate
        {
            get;
            set;
        }

        [DisplayName("Merk dan Tipe")]
        public string WOItemType
        {
            get;
            set;
        }

        [DisplayName("Serial Number")]
        public string WOItemSN
        {
            get;
            set;
        }

        [DisplayName("Garansi")]
        public bool WOIsGuarantee
        {
            get;
            set;
        }

        [DisplayName("Perlengkapan yg dibawa")]
        public string WOEquipments
        {
            get;
            set;
        }

        [DisplayName("SC / Toko")]
        public string WOScStore
        {
            get;
            set;
        }

        [DisplayName("Priority")]
        public string WOPriority
        {
            get;
            set;
        }

        //[Required]
        [DisplayName("Tgl Mulai Dikerjakan")]
        [UIHint("Date")]
        public DateTime? WOStartDate
        {
            get;
            set;
        }

        //[Required]
        [DisplayName("Status")]
        [UIHint("WOStatus")]
        public string WOLastStatus
        {
            get;
            set;
        }

        [DisplayName("Tgl Selesai Dikerjakan")]
        [UIHint("Date")]
        public DateTime? WOEstFinishDate
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Total")]
        public decimal? WOTotal
        {
            get;
            set;
        }

        [DisplayName("DP")]
        public decimal? WODp
        {
            get;
            set;
        }

        [DisplayName("No Invoice")]
        public string WOInvoiceNo
        {
            get;
            set;
        }

        [DisplayName("Tgl Diambil")]
        [UIHint("Date")]
        public DateTime? WOTakenDate
        {
            get;
            set;
        }

        [DisplayName("Keterangan Rusak")]
        [UIHint("TextArea")]
        public string WOBrokenDesc
        {
            get;
            set;
        }

        [DisplayName("Keterangan")]
        [UIHint("TextArea")]
        public string WODesc
        {
            get;
            set;
        }
    }
}