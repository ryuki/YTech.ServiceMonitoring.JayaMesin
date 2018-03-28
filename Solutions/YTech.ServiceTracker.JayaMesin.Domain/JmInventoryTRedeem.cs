using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace YTech.ServiceTracker.JayaMesin.Domain
{
    public class JmInventoryTRedeem : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        #region Properties
    
        [DomainSignature]
        public virtual JmInventoryMProgram ProgramId { get; set; }
        
        public virtual string RedeemType { get; set; }
        public virtual System.DateTime? RedeemDate { get; set; }
        public virtual decimal? RedeemValueTotal { get; set; }
        public virtual string RedeemStatus { get; set; }
        public virtual System.DateTime? RedeemDateOut { get; set; }
        public virtual System.DateTime? RedeemDateCair { get; set; }
        public virtual string CustomerId { get; set; }
        public virtual string RedeemDesc { get; set; }
        public virtual string DataStatus { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual System.DateTime? CreatedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual System.DateTime? ModifiedDate { get; set; }
        public virtual byte[] RowVersion { get; set; }

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
