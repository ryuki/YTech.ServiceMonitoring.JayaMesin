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
    public class MSmsTemplateRepository : NHibernateRepositoryWithTypedId<MSmsTemplate, string>, IMSmsTemplateRepository
    {
       IEnumerable<MSmsTemplate> IMSmsTemplateRepository.GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(MSmsTemplate));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           criteria.AddOrder(new Order("WOStatus", true));
           return criteria.List<MSmsTemplate>();
       }


       public MSmsTemplate GetByWOStatus(string woStatus)
       {
           ICriteria criteria = Session.CreateCriteria(typeof(MSmsTemplate));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           criteria.Add(Expression.Eq("WOStatus", woStatus));
           return criteria.UniqueResult<MSmsTemplate>();
       }
    }
}
