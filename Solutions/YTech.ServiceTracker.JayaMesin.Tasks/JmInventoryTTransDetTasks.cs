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
     public class JmInventoryTTransDetTasks : IJmInventoryTTransDetTasks
    {
      private readonly IJmInventoryTTransDetRepository _JmInventoryTTransDetRepository;

        public JmInventoryTTransDetTasks(IJmInventoryTTransDetRepository JmInventoryTTransDetRepository)
        {
            this._JmInventoryTTransDetRepository = JmInventoryTTransDetRepository;
        }

        public IEnumerable<Domain.JmInventoryTTransDet> GetAll()
        {
            var entitys = this._JmInventoryTTransDetRepository.GetAll(); ;
            return entitys;
        }
        
        public JmInventoryTTransDet Insert(Domain.JmInventoryTTransDet entity)
        {
            _JmInventoryTTransDetRepository.DbContext.BeginTransaction();
            _JmInventoryTTransDetRepository.Save(entity);
            _JmInventoryTTransDetRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTTransDet Update(Domain.JmInventoryTTransDet entity)
        {
            _JmInventoryTTransDetRepository.DbContext.BeginTransaction();
            _JmInventoryTTransDetRepository.Update(entity);
            _JmInventoryTTransDetRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTTransDet Delete(Domain.JmInventoryTTransDet entity)
        {
            _JmInventoryTTransDetRepository.DbContext.BeginTransaction();
            _JmInventoryTTransDetRepository.Delete(entity);
            _JmInventoryTTransDetRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTTransDet One(string id)
        {
            var entitys = this._JmInventoryTTransDetRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<JmInventoryTTransDet> GetListNotDeleted()
        {
            var entitys = this._JmInventoryTTransDetRepository.GetListNotDeleted(); ;
            return entitys;
        }


        public IEnumerable<JmInventoryTTransDet> GetListByTransId(string TransId)
        {
            IEnumerable<JmInventoryTTransDet> entitys = this._JmInventoryTTransDetRepository.GetListByTransId(TransId); ;
            return entitys;
        }


        public IEnumerable<JmInventoryTTransDet> GetListByTransDetId(string TransDetId)
        {
            IEnumerable<JmInventoryTTransDet> entitys = this._JmInventoryTTransDetRepository.GetListByTransDetId(TransDetId); ;
            return entitys;
        }
    }
}
