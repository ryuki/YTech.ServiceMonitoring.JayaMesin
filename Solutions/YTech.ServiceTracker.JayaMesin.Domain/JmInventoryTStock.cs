using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace YTech.ServiceTracker.JayaMesin.Domain
{
    public class JmInventoryTStock : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        #region Properties
    
        [DomainSignature]
        public virtual JmInventoryMProduct ProductId { get; set; }
        
        public virtual string StockSn { get; set; }
        public virtual string StockInTransId { get; set; }
        public virtual System.DateTime? StockInDate { get; set; }
        public virtual string StockOutTransId { get; set; }
        public virtual System.DateTime? StockOutDate { get; set; }
        public virtual string StockStatus { get; set; }
        public virtual string StockLocation { get; set; }
        public virtual string StockDesc { get; set; }
        public virtual string DataStatus { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual System.DateTime? CreatedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual System.DateTime? ModifiedDate { get; set; }
        public virtual byte[] RowVersion { get; set; }

        public virtual int? StockOrderNo { get; set; }
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
