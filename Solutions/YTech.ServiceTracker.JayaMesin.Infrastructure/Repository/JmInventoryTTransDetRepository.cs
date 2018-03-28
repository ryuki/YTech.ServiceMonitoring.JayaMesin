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
     public class JmInventoryTTransDetRepository : NHibernateRepositoryWithTypedId<JmInventoryTTransDet, string>, IJmInventoryTTransDetRepository
    {
       public IEnumerable<JmInventoryTTransDet> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryTTransDet));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           return criteria.List<JmInventoryTTransDet>();
       }


       public IEnumerable<JmInventoryTTransDet> GetListByTransId(string TransId)
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryTTransDet));
           criteria.Add(Expression.Eq("TransId.Id", TransId));
           criteria.AddOrder(new Order("CreatedDate", true));
           return criteria.List<JmInventoryTTransDet>();
       }


       public IEnumerable<JmInventoryTTransDet> GetListByTransDetId(string TransDetId)
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryTTransDet));
           criteria.Add(Expression.Eq("Id", TransDetId));
           criteria.AddOrder(new Order("CreatedDate", true));
           return criteria.List<JmInventoryTTransDet>();
       }
    }
}
