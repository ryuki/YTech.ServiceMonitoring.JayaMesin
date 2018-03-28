using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace YTech.ServiceTracker.JayaMesin.Domain
{
    public class JmInventoryTTran : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        #region Properties
    
        [DomainSignature]
        public virtual JmInventoryTTran TransRefId { get; set; }
        
        public virtual string TransNo { get; set; }
        public virtual System.DateTime? TransDate { get; set; }
        public virtual string TransType { get; set; }
        public virtual string TransStatus { get; set; }
        public virtual string TransApprovedBy { get; set; }
        public virtual System.DateTime? TransApprovedDate { get; set; }
        public virtual string TransInputBy { get; set; }
        public virtual System.DateTime? TransInputDate { get; set; }
        public virtual string InvoiceNo { get; set; }
        public virtual string TransDesc { get; set; }
        public virtual string DataStatus { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual System.DateTime? CreatedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual System.DateTime? ModifiedDate { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual decimal? TransSubTotal { get; set; }
        public virtual decimal? TransDisc { get; set; }
        public virtual decimal? TransPpn { get; set; }
        public virtual decimal? TransCost { get; set; }
        public virtual decimal? TransGrandTotal { get; set; }
        public virtual JmInventoryMSupplier SupplierId { get; set; }

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
