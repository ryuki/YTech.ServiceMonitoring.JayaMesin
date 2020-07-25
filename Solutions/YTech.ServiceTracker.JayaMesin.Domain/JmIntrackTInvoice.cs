using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace YTech.ServiceTracker.JayaMesin.Domain
{
    public class JmIntrackTInvoice : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        #region Properties
    
        [DomainSignature]
        
        public virtual string InvoiceNo { get; set; }
        public virtual string InvoicePacketNo { get; set; }
        public virtual System.DateTime? InvoiceDate { get; set; }
        public virtual string CustomerId { get; set; }
        public virtual decimal? InvoiceAmmount { get; set; }
        public virtual decimal? InvoicePaid { get; set; }
        public virtual string InvoicePacketAdmin { get; set; }
        public virtual string SalesmanId { get; set; }
        public virtual decimal? InvoiceAge { get; set; }
        public virtual System.DateTime? InvoiceDueDate { get; set; }
        public virtual string InvoiceDelivery { get; set; }
        public virtual System.DateTime? InvoicePaidOffDate { get; set; }
        public virtual string InvoiceStatus { get; set; }
        public virtual string InvoiceDesc { get; set; }
        public virtual string InvoiceDocLetter { get; set; }
        public virtual string InvoiceDocSspPpn { get; set; }
        public virtual string InvoiceDocSspPph { get; set; }
        public virtual string DataStatus { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual System.DateTime? CreatedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual System.DateTime? ModifiedDate { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual string InvoiceHelpdesk { get; set; }
        public virtual string InvoiceFundSource { get; set; }
        public virtual string LocationId { get; set; }
        public virtual string InvoiceLocationType { get; set; }
        public virtual string InvoiceBank { get; set; }
        public virtual string InvoiceTaxInvoiceNo { get; set; }
        public virtual string InvoiceDocDo { get; set; }
        public virtual string InvoiceDocInvoice { get; set; }
        public virtual string InvoiceDocSpm { get; set; }
        public virtual string InvoiceDocReceiptCopy { get; set; }
        public virtual string InvoiceDocSpk { get; set; }
        public virtual string InvoiceDocBast { get; set; }
        public virtual string InvoiceDocBaphp { get; set; }
        public virtual string InvoiceDocBap { get; set; }
        public virtual decimal? InvoiceDocSspPpnValue { get; set; }
        public virtual string InvoiceDocSspPpnNtpn { get; set; }
        public virtual System.DateTime? InvoiceDocSspPpnDate { get; set; }
        public virtual string InvoiceDocSspPpnDesc { get; set; }
        public virtual decimal? InvoiceDocSspPphValue { get; set; }
        public virtual string InvoiceDocSspPphNtpn { get; set; }
        public virtual System.DateTime? InvoiceDocSspPphDate { get; set; }
        public virtual string InvoiceDocSspPphDesc { get; set; }
        #endregion
        
        #region Implementation of IHasAssignedId<string>
        public virtual void SetAssignedIdTo(string assignedId)
        {
            Check.Require(!string.IsNullOrEmpty(assignedId), "Assigned Id may not be null or empty");
            Id = assignedId.Trim();
        }    
        #endregion
    }
}
