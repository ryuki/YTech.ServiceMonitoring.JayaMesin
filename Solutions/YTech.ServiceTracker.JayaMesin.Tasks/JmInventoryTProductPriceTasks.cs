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
     public class JmInventoryTProductPriceTasks : IJmInventoryTProductPriceTasks
    {
      private readonly IJmInventoryTProductPriceRepository _JmInventoryTProductPriceRepository;

        public JmInventoryTProductPriceTasks(IJmInventoryTProductPriceRepository JmInventoryTProductPriceRepository)
        {
            this._JmInventoryTProductPriceRepository = JmInventoryTProductPriceRepository;
        }

        public IEnumerable<Domain.JmInventoryTProductPrice> GetAll()
        {
            var entitys = this._JmInventoryTProductPriceRepository.GetAll(); ;
            return entitys;
        }
        
        public JmInventoryTProductPrice Insert(Domain.JmInventoryTProductPrice entity)
        {
            _JmInventoryTProductPriceRepository.DbContext.BeginTransaction();
            _JmInventoryTProductPriceRepository.Save(entity);
            _JmInventoryTProductPriceRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTProductPrice Update(Domain.JmInventoryTProductPrice entity)
        {
            _JmInventoryTProductPriceRepository.DbContext.BeginTransaction();
            _JmInventoryTProductPriceRepository.Update(entity);
            _JmInventoryTProductPriceRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTProductPrice Delete(Domain.JmInventoryTProductPrice entity)
        {
            _JmInventoryTProductPriceRepository.DbContext.BeginTransaction();
            _JmInventoryTProductPriceRepository.Delete(entity);
            _JmInventoryTProductPriceRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTProductPrice One(string id)
        {
            var entitys = this._JmInventoryTProductPriceRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<JmInventoryTProductPrice> GetListNotDeleted()
        {
            var entitys = this._JmInventoryTProductPriceRepository.GetListNotDeleted(); ;
            return entitys;
        }


        public IEnumerable<JmInventoryTProductPrice> GetListByProductId(string ParentProductId)
        {
            var entitys = this._JmInventoryTProductPriceRepository.GetListByProductId(ParentProductId);
            return entitys;
        }
    }
}
