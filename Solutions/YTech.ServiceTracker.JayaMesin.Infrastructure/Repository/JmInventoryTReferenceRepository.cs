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
     public class JmInventoryTReferenceRepository : NHibernateRepositoryWithTypedId<JmInventoryTReference, string>, IJmInventoryTReferenceRepository
    {
       public IEnumerable<JmInventoryTReference> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryTReference));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           return criteria.List<JmInventoryTReference>();
       }


       public JmInventoryTReference GetByReferenceType(Enums.EnumInventoryReferenceType enumInventoryReferenceType)
       {
           ICriteria criteria = Session.CreateCriteria(typeof(JmInventoryTReference));
           criteria.Add(Expression.Eq("ReferenceType", enumInventoryReferenceType.ToString()));
           criteria.SetCacheable(true);
           return criteria.UniqueResult() as JmInventoryTReference;
       }
    }
}
