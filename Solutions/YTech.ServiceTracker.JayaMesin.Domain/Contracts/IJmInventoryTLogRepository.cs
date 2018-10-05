using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;
using YTech.ServiceTracker.JayaMesin.Enums;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts
{
    public interface IJmInventoryTLogRepository : INHibernateRepositoryWithTypedId<JmInventoryTLog, string>
    {
       IEnumerable<JmInventoryTLog> GetListNotDeleted();

       bool GetHaveBeenLogType(string refId, string userName, EnumLogType logType, EnumTransLog logStatus);
    }
}
