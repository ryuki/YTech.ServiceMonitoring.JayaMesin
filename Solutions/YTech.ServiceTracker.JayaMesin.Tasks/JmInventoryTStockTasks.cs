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
     public class JmInventoryTStockTasks : IJmInventoryTStockTasks
    {
      private readonly IJmInventoryTStockRepository _JmInventoryTStockRepository;

        public JmInventoryTStockTasks(IJmInventoryTStockRepository JmInventoryTStockRepository)
        {
            this._JmInventoryTStockRepository = JmInventoryTStockRepository;
        }

        public IEnumerable<Domain.JmInventoryTStock> GetAll()
        {
            var entitys = this._JmInventoryTStockRepository.GetAll(); ;
            return entitys;
        }
        
        public JmInventoryTStock Insert(Domain.JmInventoryTStock entity)
        {
            _JmInventoryTStockRepository.DbContext.BeginTransaction();
            _JmInventoryTStockRepository.Save(entity);
            _JmInventoryTStockRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTStock Update(Domain.JmInventoryTStock entity)
        {
            _JmInventoryTStockRepository.DbContext.BeginTransaction();
            _JmInventoryTStockRepository.Update(entity);
            _JmInventoryTStockRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTStock Delete(Domain.JmInventoryTStock entity)
        {
            _JmInventoryTStockRepository.DbContext.BeginTransaction();
            _JmInventoryTStockRepository.Delete(entity);
            _JmInventoryTStockRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTStock One(string id)
        {
            var entitys = this._JmInventoryTStockRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<JmInventoryTStock> GetListNotDeleted()
        {
            var entitys = this._JmInventoryTStockRepository.GetListNotDeleted(); ;
            return entitys;
        }


        public IEnumerable<JmInventoryTStock> GetListByTransDetId(string TransDetId)
        {
            var entitys = this._JmInventoryTStockRepository.GetListByTransDetId(TransDetId); ;
            return entitys;
        }
    }
}
