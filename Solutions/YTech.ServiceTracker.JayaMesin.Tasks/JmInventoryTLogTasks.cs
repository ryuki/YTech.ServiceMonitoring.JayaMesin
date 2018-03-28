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
     public class JmInventoryTLogTasks : IJmInventoryTLogTasks
    {
      private readonly IJmInventoryTLogRepository _JmInventoryTLogRepository;

        public JmInventoryTLogTasks(IJmInventoryTLogRepository JmInventoryTLogRepository)
        {
            this._JmInventoryTLogRepository = JmInventoryTLogRepository;
        }

        public IEnumerable<Domain.JmInventoryTLog> GetAll()
        {
            var entitys = this._JmInventoryTLogRepository.GetAll(); ;
            return entitys;
        }
        
        public JmInventoryTLog Insert(Domain.JmInventoryTLog entity)
        {
            _JmInventoryTLogRepository.DbContext.BeginTransaction();
            _JmInventoryTLogRepository.Save(entity);
            _JmInventoryTLogRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTLog Update(Domain.JmInventoryTLog entity)
        {
            _JmInventoryTLogRepository.DbContext.BeginTransaction();
            _JmInventoryTLogRepository.Update(entity);
            _JmInventoryTLogRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTLog Delete(Domain.JmInventoryTLog entity)
        {
            _JmInventoryTLogRepository.DbContext.BeginTransaction();
            _JmInventoryTLogRepository.Delete(entity);
            _JmInventoryTLogRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTLog One(string id)
        {
            var entitys = this._JmInventoryTLogRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<JmInventoryTLog> GetListNotDeleted()
        {
            var entitys = this._JmInventoryTLogRepository.GetListNotDeleted(); ;
            return entitys;
        } 
    }
}
