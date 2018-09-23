using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace YTech.ServiceTracker.JayaMesin.Domain
{
    public class JmInventoryTTransDet : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        #region Properties
    
        [DomainSignature]
        public virtual JmInventoryMProduct ProductId { get; set; }
        public virtual JmInventoryTTran TransId { get; set; }
        
        public virtual decimal? TransDetQty { get; set; }
        public virtual string TransDetStatus { get; set; }
        public virtual string TransDetDesc { get; set; }
        public virtual string DataStatus { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual System.DateTime? CreatedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual System.DateTime? ModifiedDate { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual decimal? TransDetPrice { get; set; }
        public virtual decimal? TransDetDisc { get; set; }
        public virtual bool? TransDetIsTaxed { get; set; }
        public virtual decimal? TransDetTotal { get; set; }
        public virtual decimal? TransDetOngkir { get; set; }

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
