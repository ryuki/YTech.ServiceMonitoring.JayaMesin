using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface ITSmsTasks
    {
        IEnumerable<TSms> GetAllSms();
        TSms Insert(TSms cust);
        TSms Update(TSms cust);
        TSms Delete(TSms cust);
        TSms One(string custId);
        IEnumerable<TSms> GetListNotDeleted();

        IEnumerable<TSms> GetSmsByWoId(string woID);
    }
}
