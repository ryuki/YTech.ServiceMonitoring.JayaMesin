using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace YTech.ServiceTracker.JayaMesin.Domain
{
    public class JmInventoryMSupplier : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        #region Properties
    
        [DomainSignature]
        
        public virtual string SupplierName { get; set; }
        public virtual string SupplierAddress { get; set; }
        public virtual string SupplierPhone { get; set; }
        public virtual string SupplierStatus { get; set; }
        public virtual string SupplierDesc { get; set; }
        public virtual string DataStatus { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual System.DateTime? CreatedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual System.DateTime? ModifiedDate { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual string SupplierNpwp { get; set; }
        public virtual byte[] SupplierNpwpImg { get; set; }
        public virtual string SupplierNpwpImgUrl { get; set; }
        public virtual decimal? SupplierDebtLimit { get; set; }
        public virtual decimal? SupplierDebtTermin { get; set; }

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
