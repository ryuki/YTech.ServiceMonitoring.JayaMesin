using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts
{
    public interface IJmInventoryTReferenceRepository : INHibernateRepositoryWithTypedId<JmInventoryTReference, string>
    {
       IEnumerable<JmInventoryTReference> GetListNotDeleted();

       JmInventoryTReference GetByReferenceType(Enums.EnumInventoryReferenceType enumInventoryReferenceType);
    }
}
