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
    public class TSmsRepository : NHibernateRepositoryWithTypedId<TSms, string>, ITSmsRepository
    {
       IEnumerable<TSms> ITSmsRepository.GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(TSms));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           criteria.AddOrder(new Order("SmsDate", true));
           return criteria.List<TSms>();
       }


       public IEnumerable<TSms> GetSmsByWoId(string woID)
       {
           ICriteria criteria = Session.CreateCriteria(typeof(TSms));
           criteria.Add(Expression.Eq("WOId.Id", woID));
           criteria.AddOrder(new Order("SmsDate", false));
           return criteria.List<TSms>();
       }
    }
}
