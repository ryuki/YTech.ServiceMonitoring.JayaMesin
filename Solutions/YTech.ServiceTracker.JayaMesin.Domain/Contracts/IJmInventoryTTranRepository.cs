using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts
{
    public interface IJmInventoryTTranRepository : INHibernateRepositoryWithTypedId<JmInventoryTTran, string>
    {
       IEnumerable<JmInventoryTTran> GetListNotDeleted();

       IEnumerable<JmInventoryTTran> GetListByTransType(string TransType);

       IEnumerable<JmInventoryTTran> GetListByTransTypeAndStatus(string TransType, string TransStatus);
    }
}
