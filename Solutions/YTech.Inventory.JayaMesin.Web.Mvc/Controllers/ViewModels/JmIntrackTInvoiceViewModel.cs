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
    public class JmIntrackTInvoiceViewModel
    {
        #region Properties
    
        [ScaffoldColumn(false)]
        public string InvoiceId
        {
            get;
            set;
        }
        
        
        [DisplayName("No Faktur")]
        public virtual string InvoiceNo { get; set; }
        
        [DisplayName("No Paket")]
        public virtual string InvoicePacketNo { get; set; }

        [DisplayName("Tgl Faktur")]
        public virtual System.DateTime? InvoiceDate { get; set; }
        
        [DisplayName("Nama Pelanggan")]
        public virtual string CustomerId { get; set; }

        [DisplayName("Nilai Faktur (Rp)")]
        public virtual decimal? InvoiceAmmount { get; set; }
        
        [DisplayName("Sudah Dibayar (Rp)")]
        public virtual decimal? InvoicePaid { get; set; }
        
        [DisplayName("Admin Paket")]
        public virtual string InvoicePacketAdmin { get; set; }
        
        [DisplayName("Nama Sales")]
        public virtual string SalesmanId { get; set; }

        [DisplayName("Umur Faktur")]
        public virtual decimal? InvoiceAge { get; set; }
        
        [DisplayName("Tgl Jatuh Tempo")]
        public virtual System.DateTime? InvoiceDueDate { get; set; }
        
        [DisplayName("Pengiriman")]
        public virtual string InvoiceDelivery { get; set; }
        
        [DisplayName("Tgl Lunas")]
        public virtual System.DateTime? InvoicePaidOffDate { get; set; }

        [DisplayName("Status Faktur")]
        [UIHint("InvoiceStatus")]
        public virtual string InvoiceStatus { get; set; }

        [DisplayName("Keterangan")]
        [UIHint("TextArea")]
        public virtual string InvoiceDesc { get; set; }

        [DisplayName("Surat Pesanan")]
        [UIHint("DocStatus")]
        public virtual string InvoiceDocLetter { get; set; }

        [DisplayName("SSP PPN")]
        [UIHint("DocStatus")]
        public virtual string InvoiceDocSspPpn { get; set; }

        [DisplayName("SSP PPH")]
        [UIHint("DocStatus")]
        public virtual string InvoiceDocSspPph { get; set; }

        [DisplayName("Helpdesk")]
        public virtual string InvoiceHelpdesk { get; set; }

        [DisplayName("Sumber Dana")]
        public virtual string InvoiceFundSource { get; set; }

        public virtual string InvoiceIsUrgent { get; set; }
        public virtual string InvoiceToCopy { get; set; }
        public virtual bool HaveBeenRead { get; set; }
        
        #endregion
    }

    public class DashboardInvoiceViewModel
    {
        public virtual string InvoiceStatus { get; set; }
        public virtual decimal? InvoiceTotal { get; set; }
        public virtual string Color { get; set; }
    }
}
