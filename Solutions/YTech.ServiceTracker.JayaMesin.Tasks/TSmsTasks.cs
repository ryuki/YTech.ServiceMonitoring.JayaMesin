using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks;
using SharpArch.Domain;
using YTech.ServiceTracker.JayaMesin.Infrastructure.Repository;
using YTech.ServiceTracker.JayaMesin.Domain;

namespace YTech.ServiceTracker.JayaMesin.Tasks
{
    public class TSmsTasks : ITSmsTasks
    {
        private readonly ITSmsRepository _smsRepository;

        public TSmsTasks(ITSmsRepository smsRepository)
        {
            this._smsRepository = smsRepository;
        }

        public IEnumerable<Domain.TSms> GetAllSms()
        {
            var smss = this._smsRepository.GetAll(); ;
            return smss;
        }
        
        public TSms Insert(Domain.TSms cust)
        {
            _smsRepository.DbContext.BeginTransaction();
            _smsRepository.Save(cust);
            _smsRepository.DbContext.CommitTransaction();
            return cust;
        }

        public TSms Update(Domain.TSms cust)
        {
            _smsRepository.DbContext.BeginTransaction();
            _smsRepository.Update(cust);
            _smsRepository.DbContext.CommitTransaction();
            return cust;
        }

        public TSms Delete(Domain.TSms cust)
        {
            _smsRepository.DbContext.BeginTransaction();
            _smsRepository.Delete(cust);
            _smsRepository.DbContext.CommitTransaction();
            return cust;
        }

        public TSms One(string custId)
        {
            var smss = this._smsRepository.Get(custId); ;
            return smss;
        }

        public IEnumerable<TSms> GetListNotDeleted()
        {
            var smss = this._smsRepository.GetListNotDeleted(); ;
            return smss;
        }


        public IEnumerable<TSms> GetSmsByWoId(string woID)
        {
            var smss = this._smsRepository.GetSmsByWoId(woID); ;
            return smss;
        }
    }
}
