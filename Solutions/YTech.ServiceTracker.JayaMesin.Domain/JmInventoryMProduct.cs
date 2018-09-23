using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace YTech.ServiceTracker.JayaMesin.Domain
{
    public class JmInventoryMProduct : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        #region Properties
    
        [DomainSignature]
        public virtual JmInventoryMBrand BrandId { get; set; }
        public virtual JmInventoryMCat CatId { get; set; }
        
        public virtual string ProductName { get; set; }
        public virtual string ProductType { get; set; }
        public virtual string ProductMerk { get; set; }
        public virtual string ProductStatus { get; set; }
        public virtual string ProductDesc { get; set; }
        public virtual string DataStatus { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual System.DateTime? CreatedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual System.DateTime? ModifiedDate { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual byte[] ProductImg { get; set; }
        public virtual string ProductImgUrl { get; set; }
        public virtual decimal? ProductLastPrice { get; set; }
        public virtual decimal? ProductEstStock { get; set; }
        public virtual System.DateTime? ProductEstStockDate { get; set; }
        public virtual decimal? ProductMinStock { get; set; }
        public virtual decimal? ProductPriceSales { get; set; }
        public virtual System.DateTime? ProductLastPriceDate { get; set; }

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
