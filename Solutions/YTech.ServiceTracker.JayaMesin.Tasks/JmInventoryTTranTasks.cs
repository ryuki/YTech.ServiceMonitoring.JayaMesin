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
     public class JmInventoryTTranTasks : IJmInventoryTTranTasks
    {
      private readonly IJmInventoryTTranRepository _JmInventoryTTranRepository;

        public JmInventoryTTranTasks(IJmInventoryTTranRepository JmInventoryTTranRepository)
        {
            this._JmInventoryTTranRepository = JmInventoryTTranRepository;
        }

        public IEnumerable<Domain.JmInventoryTTran> GetAll()
        {
            var entitys = this._JmInventoryTTranRepository.GetAll(); ;
            return entitys;
        }
        
        public JmInventoryTTran Insert(Domain.JmInventoryTTran entity)
        {
            _JmInventoryTTranRepository.DbContext.BeginTransaction();
            _JmInventoryTTranRepository.Save(entity);
            _JmInventoryTTranRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTTran Update(Domain.JmInventoryTTran entity)
        {
            _JmInventoryTTranRepository.DbContext.BeginTransaction();
            _JmInventoryTTranRepository.Update(entity);
            _JmInventoryTTranRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTTran Delete(Domain.JmInventoryTTran entity)
        {
            _JmInventoryTTranRepository.DbContext.BeginTransaction();
            _JmInventoryTTranRepository.Delete(entity);
            _JmInventoryTTranRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTTran One(string id)
        {
            var entitys = this._JmInventoryTTranRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<JmInventoryTTran> GetListNotDeleted()
        {
            var entitys = this._JmInventoryTTranRepository.GetListNotDeleted(); ;
            return entitys;
        }


        public IEnumerable<JmInventoryTTran> GetListByTransType(string TransType)
        {
            var entitys = this._JmInventoryTTranRepository.GetListByTransType(TransType); ;
            return entitys;
        }


        public IEnumerable<JmInventoryTTran> GetListByTransTypeAndStatus(string TransType, string TransStatus)
        {
            var entitys = this._JmInventoryTTranRepository.GetListByTransTypeAndStatus(TransType, TransStatus); ;
            return entitys;
        }
    }
}
