using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace YTech.ServiceTracker.JayaMesin.Domain
{
    public class TSms : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        [DomainSignature]
        public virtual string SmsRecipient { get; set; }
        public virtual string SmsText { get; set; }
        public virtual string SmsReport { get; set; }
        public virtual string SmsStatus { get; set; }
        public virtual string SmsDesc { get; set; }
        public virtual DateTime? SmsDate { get; set; }
        public virtual TWO WOId { get; set; }

        public virtual string DataStatus { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual byte[] RowVersion { get; set; }

        #region Implementation of IHasAssignedId<string>

        public virtual void SetAssignedIdTo(string assignedId)
        {
            Check.Require(!string.IsNullOrEmpty(assignedId), "Assigned Id may not be null or empty");
            Id = assignedId.Trim();
        }

        #endregion
    }
}
