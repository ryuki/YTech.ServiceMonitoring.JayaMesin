using NHibernate;
using NHibernate.Criterion;
using SharpArch.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts;

namespace YTech.ServiceTracker.JayaMesin.Infrastructure.Repository
{
     public class JmInventoryTProductPriceLogRepository : NHibernateRepositoryWithTypedId<JmInventoryTProductPriceLog, string>, IJmInventoryTProductPriceLogRepository
    {
       public IEnumerable<JmInventoryTProductPriceLog> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryTProductPriceLog));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           return criteria.List<JmInventoryTProductPriceLog>();
       } 
    }
}
