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
    public class TWOLogRepository : NHibernateRepositoryWithTypedId<TWOLog, string>, ITWOLogRepository
    {
        public bool HaveBeenRead(TWO wo, string userName)
        {
            //get log last action, but not read
            ICriteria criteria = Session.CreateCriteria(typeof(TWOLog));
            criteria.Add(Expression.Eq("WOId", wo));
            criteria.Add(Expression.Not(Expression.Eq("LogStatus", EnumWOLog.Read.ToString())));
            criteria.AddOrder(new Order("LogDate", false));
            criteria.SetMaxResults(1);
            TWOLog woLogLastAction = criteria.UniqueResult<TWOLog>();

            if (woLogLastAction != null)
            {
                //if last action is current user, flag read to true
                if (woLogLastAction.LogUser == userName)
                    return true;
            }

            //get log last read by user
            criteria = Session.CreateCriteria(typeof(TWOLog));
            criteria.Add(Expression.Eq("WOId", wo));
            criteria.Add(Expression.Eq("LogUser", userName));
            criteria.Add(Expression.Eq("LogStatus", EnumWOLog.Read.ToString()));
            criteria.AddOrder(new Order("LogDate", false));
            criteria.SetMaxResults(1);
            TWOLog woLogLastRead = criteria.UniqueResult<TWOLog>();

            if (woLogLastAction != null && woLogLastRead != null)
            {
                //if last action date less than read date, flag read to true
                if (woLogLastAction.LogDate < woLogLastRead.LogDate)
                    return true;
            }

            return false;
        }


        public bool HaveBeenPrint(TWO wo, string userName)
        {
            //get log last print by user
            ICriteria criteria = Session.CreateCriteria(typeof(TWOLog));
            criteria.Add(Expression.Eq("WOId", wo));
            criteria.Add(Expression.Eq("LogUser", userName));
            criteria.Add(Expression.Eq("LogStatus", EnumWOLog.Print.ToString()));
            criteria.AddOrder(new Order("LogDate", false));
            criteria.SetMaxResults(1);
            TWOLog woLogLastPrint = criteria.UniqueResult<TWOLog>();
            if (woLogLastPrint != null)
                return true;

            return false;
        }
    }
}
