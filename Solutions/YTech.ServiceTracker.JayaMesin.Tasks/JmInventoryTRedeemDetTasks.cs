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
     public class JmInventoryTRedeemDetTasks : IJmInventoryTRedeemDetTasks
    {
      private readonly IJmInventoryTRedeemDetRepository _JmInventoryTRedeemDetRepository;

        public JmInventoryTRedeemDetTasks(IJmInventoryTRedeemDetRepository JmInventoryTRedeemDetRepository)
        {
            this._JmInventoryTRedeemDetRepository = JmInventoryTRedeemDetRepository;
        }

        public IEnumerable<Domain.JmInventoryTRedeemDet> GetAll()
        {
            var entitys = this._JmInventoryTRedeemDetRepository.GetAll(); ;
            return entitys;
        }
        
        public JmInventoryTRedeemDet Insert(Domain.JmInventoryTRedeemDet entity)
        {
            _JmInventoryTRedeemDetRepository.DbContext.BeginTransaction();
            _JmInventoryTRedeemDetRepository.Save(entity);
            _JmInventoryTRedeemDetRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTRedeemDet Update(Domain.JmInventoryTRedeemDet entity)
        {
            _JmInventoryTRedeemDetRepository.DbContext.BeginTransaction();
            _JmInventoryTRedeemDetRepository.Update(entity);
            _JmInventoryTRedeemDetRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTRedeemDet Delete(Domain.JmInventoryTRedeemDet entity)
        {
            _JmInventoryTRedeemDetRepository.DbContext.BeginTransaction();
            _JmInventoryTRedeemDetRepository.Delete(entity);
            _JmInventoryTRedeemDetRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTRedeemDet One(string id)
        {
            var entitys = this._JmInventoryTRedeemDetRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<JmInventoryTRedeemDet> GetListNotDeleted()
        {
            var entitys = this._JmInventoryTRedeemDetRepository.GetListNotDeleted(); ;
            return entitys;
        } 
    }
}
