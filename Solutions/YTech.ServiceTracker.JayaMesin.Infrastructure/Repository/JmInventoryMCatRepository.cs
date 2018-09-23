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
     public class JmInventoryMCatRepository : NHibernateRepositoryWithTypedId<JmInventoryMCat, string>, IJmInventoryMCatRepository
    {
       public IEnumerable<JmInventoryMCat> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryMCat));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           return criteria.List<JmInventoryMCat>();
       }


       public JmInventoryMCat GetLastCreatedCat()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryMCat));
           criteria.AddOrder(new Order("CreatedDate", false));
           criteria.SetMaxResults(1);
           return criteria.UniqueResult<JmInventoryMCat>();
       }
    }
}
