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
     public class JmInventoryMExpeditionRepository : NHibernateRepositoryWithTypedId<JmInventoryMExpedition, string>, IJmInventoryMExpeditionRepository
    {
       public IEnumerable<JmInventoryMExpedition> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryMExpedition));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           return criteria.List<JmInventoryMExpedition>();
       }


       public JmInventoryMExpedition GetLastCreatedExpedition()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryMExpedition));
           criteria.AddOrder(new Order("CreatedDate", false));
           criteria.SetMaxResults(1);
           return criteria.UniqueResult<JmInventoryMExpedition>();
       }
    }
}
