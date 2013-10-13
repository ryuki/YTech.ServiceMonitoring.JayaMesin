using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks;

namespace YTech.ServiceTracker.JayaMesin.Tasks
{
    public class TWOLogTasks : ITWOLogTasks
    {
        private readonly ITWORepository _woRepository;
        private readonly ITWOLogRepository _woLogRepository;
        private readonly ITWOStatusRepository _woStatusRepository;

        public TWOLogTasks(ITWORepository woRepository, ITWOLogRepository woLogRepository, ITWOStatusRepository woStatusRepository)
        {
            this._woRepository = woRepository;
            this._woLogRepository = woLogRepository;
            this._woStatusRepository = woStatusRepository;
        }

        public IEnumerable<Domain.TWOLog> GetAllWOLogs()
        {
            IEnumerable<Domain.TWOLog> wos = this._woLogRepository.GetAll().OrderByDescending(wo => wo.LogDate);
            return wos;
        }

        public Domain.TWOLog Insert(Domain.TWOLog woLog)
        {
            _woLogRepository.DbContext.BeginTransaction();
            _woLogRepository.Save(woLog);
            _woLogRepository.DbContext.CommitTransaction();
            return woLog;
        }

        public Domain.TWOLog Update(Domain.TWOLog woLog)
        {
            _woLogRepository.DbContext.BeginTransaction();
            _woLogRepository.Update(woLog);
            _woLogRepository.DbContext.CommitTransaction();
            return woLog;
        }

        public Domain.TWOLog Delete(Domain.TWOLog woLog)
        {
            _woLogRepository.DbContext.BeginTransaction();
            _woLogRepository.Delete(woLog);
            _woLogRepository.DbContext.CommitTransaction();
            return woLog;
        }

        public Domain.TWOLog One(string woLogId)
        {
            var woLog = this._woLogRepository.Get(woLogId); ;
            return woLog;
        }


        public bool HaveBeenRead(Domain.TWO wo, string userName)
        {
            return this._woLogRepository.HaveBeenRead(wo, userName); 
        }


        public bool HaveBeenPrint(Domain.TWO wo, string userName)
        {
            return this._woLogRepository.HaveBeenPrint(wo, userName); 
        }
    }
}
