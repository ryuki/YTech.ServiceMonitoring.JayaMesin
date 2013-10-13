using SharpArch.NHibernate.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts
{
    public interface ITWOLogRepository : INHibernateRepositoryWithTypedId<TWOLog, string>
    {
        bool HaveBeenRead(TWO wo, string userName);
        bool HaveBeenPrint(TWO wo, string userName);
    }
}
