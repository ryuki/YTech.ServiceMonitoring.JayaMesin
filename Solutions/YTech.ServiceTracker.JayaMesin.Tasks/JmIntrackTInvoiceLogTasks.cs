using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain;
using YTech.ServiceTracker.JayaMesin.Infrastructure.Repository;
using YTech.ServiceTracker.JayaMesin.Domain;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks;

namespace YTech.ServiceTracker.JayaMesin.Tasks
{
     public class JmIntrackTInvoiceLogTasks : IJmIntrackTInvoiceLogTasks
    {
      private readonly IJmIntrackTInvoiceLogRepository _JmIntrackTInvoiceLogRepository;

        public JmIntrackTInvoiceLogTasks(IJmIntrackTInvoiceLogRepository JmIntrackTInvoiceLogRepository)
        {
            this._JmIntrackTInvoiceLogRepository = JmIntrackTInvoiceLogRepository;
        }

        public IEnumerable<Domain.JmIntrackTInvoiceLog> GetAll()
        {
            var entitys = this._JmIntrackTInvoiceLogRepository.GetAll(); ;
            return entitys;
        }
        
        public JmIntrackTInvoiceLog Insert(Domain.JmIntrackTInvoiceLog entity)
        {
            _JmIntrackTInvoiceLogRepository.DbContext.BeginTransaction();
            _JmIntrackTInvoiceLogRepository.Save(entity);
            _JmIntrackTInvoiceLogRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmIntrackTInvoiceLog Update(Domain.JmIntrackTInvoiceLog entity)
        {
            _JmIntrackTInvoiceLogRepository.DbContext.BeginTransaction();
            _JmIntrackTInvoiceLogRepository.Update(entity);
            _JmIntrackTInvoiceLogRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmIntrackTInvoiceLog Delete(Domain.JmIntrackTInvoiceLog entity)
        {
            _JmIntrackTInvoiceLogRepository.DbContext.BeginTransaction();
            _JmIntrackTInvoiceLogRepository.Delete(entity);
            _JmIntrackTInvoiceLogRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmIntrackTInvoiceLog One(string id)
        {
            var entitys = this._JmIntrackTInvoiceLogRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<JmIntrackTInvoiceLog> GetListNotDeleted()
        {
            var entitys = this._JmIntrackTInvoiceLogRepository.GetListNotDeleted(); ;
            return entitys;
        } 
    }
}
