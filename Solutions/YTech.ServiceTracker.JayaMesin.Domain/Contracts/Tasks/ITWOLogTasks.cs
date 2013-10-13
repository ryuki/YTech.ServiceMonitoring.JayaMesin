using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface ITWOLogTasks
    {
        IEnumerable<TWOLog> GetAllWOLogs();
        TWOLog Insert(TWOLog woLog);
        TWOLog Update(TWOLog woLog);
        TWOLog Delete(TWOLog woLog);
        TWOLog One(string woLogId);

        bool HaveBeenRead(TWO wo, string userName);
        bool HaveBeenPrint(TWO wo, string userName);
    }
}
