using SharpArch.NHibernate.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts
{
    public interface ITWORepository : INHibernateRepositoryWithTypedId<TWO, string>
    {
        IEnumerable<TWO> GetWOByDate(DateTime? dateFrom, DateTime? dateTo);
        IEnumerable<TWOHaveRead> GetListNotDeleted(string userName);
        TWO GetWOByWONo(string woNo);
    }
}
