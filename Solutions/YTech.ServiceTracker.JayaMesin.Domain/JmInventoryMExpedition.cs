using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace YTech.ServiceTracker.JayaMesin.Domain
{
    public class JmInventoryMExpedition : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        #region Properties
    
        [DomainSignature]
        
        public virtual string ExpeditionName { get; set; }
        public virtual string ExpeditionDesc { get; set; }
        public virtual byte[] ExpeditionImg { get; set; }
        public virtual string ExpeditionImgUrl { get; set; }
        public virtual string DataStatus { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual System.DateTime? CreatedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual System.DateTime? ModifiedDate { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual string ExpeditionAddress { get; set; }
        public virtual string ExpeditionPhone { get; set; }

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
