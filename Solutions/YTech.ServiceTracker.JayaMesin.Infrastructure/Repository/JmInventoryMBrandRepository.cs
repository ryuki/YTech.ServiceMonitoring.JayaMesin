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
     public class JmInventoryMBrandRepository : NHibernateRepositoryWithTypedId<JmInventoryMBrand, string>, IJmInventoryMBrandRepository
    {
       public IEnumerable<JmInventoryMBrand> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryMBrand));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           return criteria.List<JmInventoryMBrand>();
       }


       public JmInventoryMBrand GetLastCreatedBrand()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryMBrand));
           criteria.AddOrder(new Order("CreatedDate", false));
           criteria.SetMaxResults(1);
           return criteria.UniqueResult<JmInventoryMBrand>();
       }
    }
}
