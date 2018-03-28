using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts
{
    public interface IJmInventoryTTransDetRepository : INHibernateRepositoryWithTypedId<JmInventoryTTransDet, string>
    {
       IEnumerable<JmInventoryTTransDet> GetListNotDeleted();

       IEnumerable<JmInventoryTTransDet> GetListByTransId(string TransId);

       IEnumerable<JmInventoryTTransDet> GetListByTransDetId(string TransDetId);
    }
}
