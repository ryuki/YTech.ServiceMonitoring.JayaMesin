using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace YTech.ServiceTracker.JayaMesin.Domain
{
    public class JmInventoryMCat : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        #region Properties
    
        [DomainSignature]
        
        public virtual string CatParentId { get; set; }
        public virtual string CatName { get; set; }
        public virtual string CatDesc { get; set; }
        public virtual byte[] CatImg { get; set; }
        public virtual string CatImgUrl { get; set; }
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
