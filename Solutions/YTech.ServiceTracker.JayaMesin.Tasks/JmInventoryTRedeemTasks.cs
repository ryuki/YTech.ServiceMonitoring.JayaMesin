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
     public class JmInventoryTRedeemTasks : IJmInventoryTRedeemTasks
    {
      private readonly IJmInventoryTRedeemRepository _JmInventoryTRedeemRepository;

        public JmInventoryTRedeemTasks(IJmInventoryTRedeemRepository JmInventoryTRedeemRepository)
        {
            this._JmInventoryTRedeemRepository = JmInventoryTRedeemRepository;
        }

        public IEnumerable<Domain.JmInventoryTRedeem> GetAll()
        {
            var entitys = this._JmInventoryTRedeemRepository.GetAll(); ;
            return entitys;
        }
        
        public JmInventoryTRedeem Insert(Domain.JmInventoryTRedeem entity)
        {
            _JmInventoryTRedeemRepository.DbContext.BeginTransaction();
            _JmInventoryTRedeemRepository.Save(entity);
            _JmInventoryTRedeemRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTRedeem Update(Domain.JmInventoryTRedeem entity)
        {
            _JmInventoryTRedeemRepository.DbContext.BeginTransaction();
            _JmInventoryTRedeemRepository.Update(entity);
            _JmInventoryTRedeemRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTRedeem Delete(Domain.JmInventoryTRedeem entity)
        {
            _JmInventoryTRedeemRepository.DbContext.BeginTransaction();
            _JmInventoryTRedeemRepository.Delete(entity);
            _JmInventoryTRedeemRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTRedeem One(string id)
        {
            var entitys = this._JmInventoryTRedeemRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<JmInventoryTRedeem> GetListNotDeleted()
        {
            var entitys = this._JmInventoryTRedeemRepository.GetListNotDeleted(); ;
            return entitys;
        } 
    }
}
