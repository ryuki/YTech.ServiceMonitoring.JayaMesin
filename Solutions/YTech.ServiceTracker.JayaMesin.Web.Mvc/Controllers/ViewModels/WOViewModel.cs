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
        public WOViewModel()
        {
            //set new customer to woviewmodel to prevent error in web when create new WO
            _Customer = new CustomerViewModel();
        }

        [ScaffoldColumn(false)]
        public string WOID
        {
            get;
            set;
        }

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

        [Required]
        [DisplayName("Konsumen")]
        public string HiddenCustomerId
        {
            get;
            set;
        }

        [DisplayName("HP Konsumen")]
        public string CustomerPhone
        {
            get;
            set;
        }

        [DisplayName("Alamat Konsumen")]
        public string CustomerAddress
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
                return _Customer;
            }
            set { _Customer = value; }
        }

        [Required]
        [DisplayName("Tgl Masuk")]
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
        [UIHint("SCToko")]
        public string WOScStore
        {
            get;
            set;
        }

        [DisplayName("Prioritas")]
        [UIHint("Priority")]
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

        [DisplayName("Status")]
        [UIHint("WOStatus")]
        [Required]
        public string WOLastStatus
        {
            get;
            set;
        }

        [DisplayName("Tgl Selesai Dikerjakan")]
        [UIHint("DateTime")]
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

        [DisplayName("Sisa")]
        [ReadOnly(true)]
        public string WOSisa
        {
            get
            {
                if (WOTotal.HasValue && WODp.HasValue)
                    return (WOTotal.Value - WODp.Value).ToString("N0");
                else if (WOTotal.HasValue)
                    return WOTotal.Value.ToString("N0");
                else if (WODp.HasValue)
                    return (WODp.Value*-1).ToString("N0");
                else
                    return "0";
            }
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

        [DisplayName("Update")]
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

        [DisplayName("Keluhan")]
        [UIHint("TextArea")]
        public string WOComplain
        {
            get;
            set;
        }
        
        [DisplayName("Sudah dibaca?")]
        public bool HaveBeenRead
        {
            get;
            set;
        }
    }
}