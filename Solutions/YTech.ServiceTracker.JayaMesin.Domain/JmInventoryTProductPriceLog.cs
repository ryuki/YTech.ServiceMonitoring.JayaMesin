using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace YTech.ServiceTracker.JayaMesin.Domain
{
    public class JmInventoryTProductPriceLog : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        #region Properties
    
        [DomainSignature]
        public virtual JmInventoryMProduct ProductId { get; set; }
        public virtual JmInventoryMSupplier SupplierId { get; set; }
        
        public virtual decimal? ProductPrice { get; set; }
        public virtual string ProductPriceStatus { get; set; }
        public virtual string ProductPriceDesc { get; set; }
        public virtual string DataStatus { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual System.DateTime? CreatedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual System.DateTime? ModifiedDate { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual System.DateTime? ProductPriceDate { get; set; }
        public virtual byte[] ProductPriceImg { get; set; }
        public virtual string ProductPriceImgUrl { get; set; }
        public virtual decimal? ProductPriceQty { get; set; }
        public virtual decimal? ProductPriceOngkir { get; set; }

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
