using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace YTech.ServiceTracker.JayaMesin.Domain
{
    public class TWO : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        [DomainSignature]
        public virtual string WONo { get; set; }
        public virtual MCustomer CustomerId { get; set; }
        public virtual DateTime? WODate { get; set; }
        public virtual string WOItemType { get; set; }
        public virtual string WOItemSn { get; set; }
        public virtual bool WOIsGuarantee { get; set; }
        public virtual string WOEquipments { get; set; }
        public virtual string WOScStore { get; set; }
        public virtual string WOPriority { get; set; }
        public virtual DateTime? WOStartDate { get; set; }
        public virtual string WOLastStatus { get; set; }
        public virtual DateTime? WOEstFinishDate { get; set; }
        public virtual decimal? WOTotal { get; set; }
        public virtual decimal? WODp { get; set; }
        public virtual string WOInvoiceNo { get; set; }
        public virtual DateTime? WOTakenDate { get; set; }
        public virtual string WOBrokenDesc { get; set; }
        public virtual string WODesc { get; set; }
        public virtual string WOComplain { get; set; }
        public virtual string WORemarkStatus { get; set; }
        public virtual string WOReceivedBy { get; set; }
        public virtual string WORepairedBy { get; set; }

        /// <summary>
        /// 20170511 - new request, add new column wo type, 
        /// service, ganti sparepart, service dan ganti sparepart
        /// </summary>
        public virtual string WOType { get; set; }

        public virtual string DataStatus { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual byte[] RowVersion { get; set; }

        public virtual IList<TWOLog> WOLogs { get; protected set; }
        //public virtual TWOLog WOLog { get; protected set; }

        #region Implementation of IHasAssignedId<string>

        public virtual void SetAssignedIdTo(string assignedId)
        {
            Check.Require(!string.IsNullOrEmpty(assignedId), "Assigned Id may not be null or empty");
            Id = assignedId.Trim();
        }

        #endregion
    }
}
