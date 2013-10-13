using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks;

namespace YTech.ServiceTracker.JayaMesin.Tasks
{
   public class TReferenceTasks : ITReferenceTasks
    {
       private readonly ITReferenceRepository _refRepository;

       public TReferenceTasks(ITReferenceRepository refRepository)
       {
           _refRepository = refRepository;
        }

        public Domain.TReference GetByReferenceType(YTech.ServiceTracker.JayaMesin.Enums.EnumReferenceType referenceType)
        {
            var reference = this._refRepository.GetByReferenceType(referenceType); ;
            return reference;
        }

        public Domain.TReference Update(Domain.TReference reference)
        {
            _refRepository.DbContext.BeginTransaction();
            _refRepository.Update(reference);
            _refRepository.DbContext.CommitTransaction();
            return reference;
        }
    }
}
