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
     public class JmInventoryMSupplierRepository : NHibernateRepositoryWithTypedId<JmInventoryMSupplier, string>, IJmInventoryMSupplierRepository
    {
       public IEnumerable<JmInventoryMSupplier> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryMSupplier));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           return criteria.List<JmInventoryMSupplier>();
       }


       public JmInventoryMSupplier GetLastCreatedSupplier()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryMSupplier));
           criteria.AddOrder(new Order("CreatedDate", false));
           criteria.SetMaxResults(1);
           return criteria.UniqueResult<JmInventoryMSupplier>();
       }
    }
}
