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
     public class JmInventoryTProductPriceLogTasks : IJmInventoryTProductPriceLogTasks
    {
      private readonly IJmInventoryTProductPriceLogRepository _JmInventoryTProductPriceLogRepository;

        public JmInventoryTProductPriceLogTasks(IJmInventoryTProductPriceLogRepository JmInventoryTProductPriceLogRepository)
        {
            this._JmInventoryTProductPriceLogRepository = JmInventoryTProductPriceLogRepository;
        }

        public IEnumerable<Domain.JmInventoryTProductPriceLog> GetAll()
        {
            var entitys = this._JmInventoryTProductPriceLogRepository.GetAll(); ;
            return entitys;
        }
        
        public JmInventoryTProductPriceLog Insert(Domain.JmInventoryTProductPriceLog entity)
        {
            _JmInventoryTProductPriceLogRepository.DbContext.BeginTransaction();
            _JmInventoryTProductPriceLogRepository.Save(entity);
            _JmInventoryTProductPriceLogRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTProductPriceLog Update(Domain.JmInventoryTProductPriceLog entity)
        {
            _JmInventoryTProductPriceLogRepository.DbContext.BeginTransaction();
            _JmInventoryTProductPriceLogRepository.Update(entity);
            _JmInventoryTProductPriceLogRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTProductPriceLog Delete(Domain.JmInventoryTProductPriceLog entity)
        {
            _JmInventoryTProductPriceLogRepository.DbContext.BeginTransaction();
            _JmInventoryTProductPriceLogRepository.Delete(entity);
            _JmInventoryTProductPriceLogRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTProductPriceLog One(string id)
        {
            var entitys = this._JmInventoryTProductPriceLogRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<JmInventoryTProductPriceLog> GetListNotDeleted()
        {
            var entitys = this._JmInventoryTProductPriceLogRepository.GetListNotDeleted(); ;
            return entitys;
        } 
    }
}
