using NHibernate;
using NHibernate.Criterion;
using SharpArch.NHibernate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts;
using YTech.ServiceTracker.JayaMesin.Enums;

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

        public IEnumerable<TWOHaveRead> GetListNotDeleted(string userName, string WOStatus, string TextSearch)
        {
            //ICriteria criteria = Session.CreateCriteria(typeof(TWO));
            //criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
            //return criteria.List<TWO>();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"   EXEC [dbo].[JAYAMESIN_SP_GET_LIST_WO_READ]
                        		@User_Name = :UserName, @WOStatus = :WOStatus, @TextSearch = :TextSearch ");
            IQuery q = Session.CreateSQLQuery(sql.ToString()).AddEntity(typeof(TWOHaveRead));
            q.SetString("UserName", userName);
            if (WOStatus == "")
                q.SetString("WOStatus", "%");
            else
                q.SetString("WOStatus", WOStatus);
            q.SetString("TextSearch", TextSearch);

            return q.List<TWOHaveRead>();
            //TWO
            //List<TWO> result = new List<TWO>(q.List());
            //return result;
        }

        public TWO GetWOByWONo(string woNo)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TWO));
            criteria.Add(Expression.Eq("WONo", woNo));
            criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
            return criteria.UniqueResult<TWO>();
        }

        /// <summary>
        /// 20170511 : add new feature, get expired wo list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TWOHaveRead> GetListExpiredWO()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"   select t.*, c.[CUSTOMER_NAME], c.[CUSTOMER_PHONE]
, c.[CUSTOMER_ADDRESS], 0 HaveBeenRead
from [dbo].[JAYAMESIN_T_WO] t
left join [dbo].[JAYAMESIN_M_CUSTOMER] c
on t.[CUSTOMER_ID] = c.[CUSTOMER_ID]
left join 
( select s.wo_id, s.[WO_STATUS], s.[WO_STATUS_DATE]
, dense_rank() over (partition by wo_id order by s.[WO_STATUS_DATE] desc) ranking
from [dbo].[JAYAMESIN_T_WO_STATUS] s
) ss
on t.wo_id = ss.wo_id  and ss.ranking = 1
where 
 case 
when (ss.[WO_STATUS] = 'Baru_Masuk' and ss.[WO_STATUS_DATE]+1 < getdate()) then 'expired'
when (ss.[WO_STATUS] = 'Sedang_Dikerjakan' and ss.[WO_STATUS_DATE]+3 < getdate()) then 'expired'
when (ss.[WO_STATUS] = 'Menunggu_Persetujuan' and ss.[WO_STATUS_DATE]+1 < getdate()) then 'expired'
when (ss.[WO_STATUS] = 'Menunggu_Part' and ss.[WO_STATUS_DATE]+3 < getdate()) then 'expired'
when (ss.[WO_STATUS] = 'Selesai' and ss.[WO_STATUS_DATE]+1 < getdate()) then 'expired'
else 'no expired' end = 'expired'
order by ss.[WO_STATUS_DATE] desc ");
            IQuery q = Session.CreateSQLQuery(sql.ToString()).AddEntity(typeof(TWOHaveRead));

            return q.List<TWOHaveRead>();
        }
    }
}
