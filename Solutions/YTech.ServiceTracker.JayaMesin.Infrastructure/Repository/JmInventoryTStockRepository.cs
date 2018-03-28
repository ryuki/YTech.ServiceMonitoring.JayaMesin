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
     public class JmInventoryTStockRepository : NHibernateRepositoryWithTypedId<JmInventoryTStock, string>, IJmInventoryTStockRepository
    {
       public IEnumerable<JmInventoryTStock> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryTStock));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           return criteria.List<JmInventoryTStock>();
       }


       public IEnumerable<JmInventoryTStock> GetListByTransDetId(string TransDetId)
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryTStock));
           criteria.Add(Expression.Eq("StockInTransId", TransDetId));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           criteria.AddOrder(new Order("StockOrderNo", true));
           return criteria.List<JmInventoryTStock>();
       }
    }
}
