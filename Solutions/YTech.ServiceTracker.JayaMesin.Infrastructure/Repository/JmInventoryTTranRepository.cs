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
     public class JmInventoryTTranRepository : NHibernateRepositoryWithTypedId<JmInventoryTTran, string>, IJmInventoryTTranRepository
    {
       public IEnumerable<JmInventoryTTran> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryTTran));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           return criteria.List<JmInventoryTTran>();
       }


       public IEnumerable<JmInventoryTTran> GetListByTransType(string TransType)
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryTTran));
           criteria.Add(Expression.Eq("TransType", TransType));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           criteria.AddOrder(new Order("TransDate", true));
           return criteria.List<JmInventoryTTran>();
       }


       public IEnumerable<JmInventoryTTran> GetListByTransTypeAndStatus(string TransType, string TransStatus)
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryTTran));
           criteria.Add(Expression.Eq("TransType", TransType));
           criteria.Add(Expression.Eq("TransStatus", TransStatus));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           criteria.AddOrder(new Order("TransDate", true));
           return criteria.List<JmInventoryTTran>();
       }
    }
}
