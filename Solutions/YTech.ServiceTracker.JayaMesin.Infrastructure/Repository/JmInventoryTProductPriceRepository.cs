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
     public class JmInventoryTProductPriceRepository : NHibernateRepositoryWithTypedId<JmInventoryTProductPrice, string>, IJmInventoryTProductPriceRepository
    {
       public IEnumerable<JmInventoryTProductPrice> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryTProductPrice));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           return criteria.List<JmInventoryTProductPrice>();
       }


       public IEnumerable<JmInventoryTProductPrice> GetListByProductId(string ParentProductId)
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryTProductPrice));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           criteria.Add((Expression.Eq("ProductId.Id", ParentProductId)));

           criteria.AddOrder(new Order("ProductPriceDate", false));
           return criteria.List<JmInventoryTProductPrice>();
       }
    }
}
