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
     public class JmInventoryMProductSupplierRepository : NHibernateRepositoryWithTypedId<JmInventoryMProductSupplier, string>, IJmInventoryMProductSupplierRepository
    {
       public IEnumerable<JmInventoryMProductSupplier> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryMProductSupplier));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           return criteria.List<JmInventoryMProductSupplier>();
       } 
    }
}
