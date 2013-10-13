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
    public class TWORepository : NHibernateRepositoryWithTypedId<TWO, string>, ITWORepository
    {
        public IEnumerable<TWO> GetWOByDate(DateTime? dateFrom, DateTime? dateTo)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TWO));
            criteria.Add(Expression.Between("WODate", dateFrom, dateTo));
            criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
            criteria.AddOrder(Order.Asc("WODate"));
            return criteria.List<TWO>();
        }

        public IEnumerable<TWO> GetListNotDeleted()
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TWO));
            criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
            return criteria.List<TWO>();
        }
    }
}
