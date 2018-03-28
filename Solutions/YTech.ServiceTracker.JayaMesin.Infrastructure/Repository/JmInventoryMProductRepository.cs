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
     public class JmInventoryMProductRepository : NHibernateRepositoryWithTypedId<JmInventoryMProduct, string>, IJmInventoryMProductRepository
    {
       public IEnumerable<JmInventoryMProduct> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryMProduct));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           return criteria.List<JmInventoryMProduct>();
       }


       public IEnumerable<JmInventoryMProduct> GetListNotDeletedAndNotDisc()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryMProduct));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           criteria.Add(Expression.Not(Expression.Eq("ProductStatus", EnumProductStatus.Discontinued.ToString())));
           return criteria.List<JmInventoryMProduct>();
       }
    }
}
