using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks;
using YTech.ServiceTracker.JayaMesin.Enums;

namespace YTech.ServiceTracker.JayaMesin.Tasks
{
    public class TWOTasks : ITWOTasks
    {
        private readonly ITWORepository _woRepository;
        private readonly ITWOLogRepository _woLogRepository;
        private readonly ITWOStatusRepository _woStatusRepository;

        public TWOTasks(ITWORepository woRepository, ITWOLogRepository woLogRepository, ITWOStatusRepository woStatusRepository)
        {
            this._woRepository = woRepository;
            this._woLogRepository = woLogRepository;
            this._woStatusRepository = woStatusRepository;
        }

        public IEnumerable<Domain.TWO> GetAllWOs()
        {
            IEnumerable<Domain.TWO> wos = this._woRepository.GetAll();
            return wos;
        }


        public Domain.TWO Insert(Domain.TWO wo)
        {
            _woRepository.DbContext.BeginTransaction();
            _woRepository.Save(wo);
            //save log and status history
            SaveWOStatus(wo, wo.CreatedBy);
            SaveWOLog(wo, wo.CreatedBy, EnumWOLog.Save);

            _woRepository.DbContext.CommitTransaction();
            return wo;
        }

        public Domain.TWO Update(Domain.TWO wo)
        {
            _woRepository.DbContext.BeginTransaction();
            _woRepository.Update(wo);
            //save log and status history
            SaveWOStatus(wo, wo.ModifiedBy);
            SaveWOLog(wo, wo.ModifiedBy, EnumWOLog.Update);

            _woRepository.DbContext.CommitTransaction();
            return wo;
        }


        private void SaveWOLog(TWO wo, string woCreatedBy, EnumWOLog logStatus)
        {
            TWOLog woLog = new TWOLog();
            woLog.SetAssignedIdTo(Guid.NewGuid().ToString());
            woLog.WOId = wo;
            woLog.LogUser = woCreatedBy;
            woLog.LogDate = DateTime.Now;
            woLog.LogStatus = logStatus.ToString();                           

            woLog.CreatedDate = DateTime.Now;
            woLog.CreatedBy = woCreatedBy;
            woLog.DataStatus = "New";
            _woLogRepository.Save(woLog);
        }

        private void SaveWOStatus(TWO wo, string woCreatedBy)
        {
            TWOStatus woStatus = new TWOStatus();
            woStatus.SetAssignedIdTo(Guid.NewGuid().ToString());
            woStatus.WOId = wo;
            woStatus.WOStatusUser = woCreatedBy;
            woStatus.WOStatus = wo.WOLastStatus;
            woStatus.WOStatusBrokenDesc = wo.WOBrokenDesc;
            woStatus.WOStatusDate = DateTime.Now;
            woStatus.WOStatusStartDate = wo.WOStartDate;
            woStatus.WOStatusFinishDate = wo.WOEstFinishDate;


            woStatus.CreatedDate = DateTime.Now;
            woStatus.CreatedBy = woCreatedBy;
            woStatus.DataStatus = "New";
            _woStatusRepository.Save(woStatus);
        }

        public Domain.TWO Delete(Domain.TWO wo)
        {
            _woRepository.DbContext.BeginTransaction();
            _woRepository.Delete(wo);
            _woRepository.DbContext.CommitTransaction();
            return wo;
        }

        public Domain.TWO One(string woId)
        {
            var wo = this._woRepository.Get(woId); ;
            return wo;
        }


        public IEnumerable<Domain.TWO> GetWOByDate(DateTime? dateFrom, DateTime? dateTo)
        {
            IEnumerable<Domain.TWO> wos = this._woRepository.GetWOByDate(dateFrom, dateTo);
            return wos;
        }

        public IEnumerable<TWO> GetListNotDeleted()
        {
            IEnumerable<Domain.TWO> wos = this._woRepository.GetListNotDeleted();
            return wos;
        }
    }
}
