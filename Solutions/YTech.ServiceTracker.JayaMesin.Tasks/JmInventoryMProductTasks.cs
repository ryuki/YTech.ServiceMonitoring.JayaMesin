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
     public class JmInventoryMProductTasks : IJmInventoryMProductTasks
    {
      private readonly IJmInventoryMProductRepository _JmInventoryMProductRepository;

        public JmInventoryMProductTasks(IJmInventoryMProductRepository JmInventoryMProductRepository)
        {
            this._JmInventoryMProductRepository = JmInventoryMProductRepository;
        }

        public IEnumerable<Domain.JmInventoryMProduct> GetAll()
        {
            var entitys = this._JmInventoryMProductRepository.GetAll(); ;
            return entitys;
        }
        
        public JmInventoryMProduct Insert(Domain.JmInventoryMProduct entity)
        {
            _JmInventoryMProductRepository.DbContext.BeginTransaction();
            _JmInventoryMProductRepository.Save(entity);
            _JmInventoryMProductRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryMProduct Update(Domain.JmInventoryMProduct entity)
        {
            _JmInventoryMProductRepository.DbContext.BeginTransaction();
            _JmInventoryMProductRepository.Update(entity);
            _JmInventoryMProductRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryMProduct Delete(Domain.JmInventoryMProduct entity)
        {
            _JmInventoryMProductRepository.DbContext.BeginTransaction();
            _JmInventoryMProductRepository.Delete(entity);
            _JmInventoryMProductRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryMProduct One(string id)
        {
            var entitys = this._JmInventoryMProductRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<JmInventoryMProduct> GetListNotDeleted()
        {
            var entitys = this._JmInventoryMProductRepository.GetListNotDeleted(); ;
            return entitys;
        }


        public IEnumerable<JmInventoryMProduct> GetListNotDeletedAndNotDisc()
        {
            var entitys = this._JmInventoryMProductRepository.GetListNotDeletedAndNotDisc(); ;
            return entitys;
        }
    }
}
