using NHibernate;
using NHibernate.Criterion;
using SharpArch.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts;
using YTech.ServiceTracker.JayaMesin.Enums;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.Repository
{
     public class JmInventoryTLogRepository : NHibernateRepositoryWithTypedId<JmInventoryTLog, string>, IJmInventoryTLogRepository
    {
       public IEnumerable<JmInventoryTLog> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryTLog));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           return criteria.List<JmInventoryTLog>();
       }


       public bool GetHaveBeenLogType(string refId, string userName, Enums.EnumLogType logType, EnumTransLog logStatus)
       {
           //get log last action, but not read
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryTLog));
           criteria.Add(Expression.Eq("LogRefId", refId));
           criteria.Add(Expression.Eq("LogType", logType.ToString()));
           criteria.Add(Expression.Not(Expression.Eq("LogStatus", logStatus.ToString())));
           criteria.AddOrder(new Order("LogDate", false));
           criteria.SetMaxResults(1);
           JmInventoryTLog logLastAction = criteria.UniqueResult<JmInventoryTLog>();

           if (logLastAction != null)
           {
               //if last action is current user, flag read to true
               if (logLastAction.LogUser == userName)
                   return true;
           }

           //get log last read by user
           criteria = Session.CreateCriteria(typeof(JmInventoryTLog));
           criteria.Add(Expression.Eq("LogRefId", refId));
           criteria.Add(Expression.Eq("LogUser", userName));
           criteria.Add(Expression.Eq("LogType", logType.ToString()));
           criteria.Add(Expression.Eq("LogStatus", logStatus.ToString()));
           criteria.AddOrder(new Order("LogDate", false));
           criteria.SetMaxResults(1);
           JmInventoryTLog woLogLastRead = criteria.UniqueResult<JmInventoryTLog>();

           if (logLastAction != null && woLogLastRead != null)
           {
               //if last action date less than read date, flag read to true
               if (logLastAction.LogDate < woLogLastRead.LogDate)
                   return true;
           }

           return false;
       }
    }
}
