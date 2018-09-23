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
     public class JmInventoryMBrandTasks : IJmInventoryMBrandTasks
    {
      private readonly IJmInventoryMBrandRepository _JmInventoryMBrandRepository;

        public JmInventoryMBrandTasks(IJmInventoryMBrandRepository JmInventoryMBrandRepository)
        {
            this._JmInventoryMBrandRepository = JmInventoryMBrandRepository;
        }

        public IEnumerable<Domain.JmInventoryMBrand> GetAll()
        {
            var entitys = this._JmInventoryMBrandRepository.GetAll(); ;
            return entitys;
        }
        
        public JmInventoryMBrand Insert(Domain.JmInventoryMBrand entity)
        {
            _JmInventoryMBrandRepository.DbContext.BeginTransaction();
            _JmInventoryMBrandRepository.Save(entity);
            _JmInventoryMBrandRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryMBrand Update(Domain.JmInventoryMBrand entity)
        {
            _JmInventoryMBrandRepository.DbContext.BeginTransaction();
            _JmInventoryMBrandRepository.Update(entity);
            _JmInventoryMBrandRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryMBrand Delete(Domain.JmInventoryMBrand entity)
        {
            _JmInventoryMBrandRepository.DbContext.BeginTransaction();
            _JmInventoryMBrandRepository.Delete(entity);
            _JmInventoryMBrandRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryMBrand One(string id)
        {
            var entitys = this._JmInventoryMBrandRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<JmInventoryMBrand> GetListNotDeleted()
        {
            var entitys = this._JmInventoryMBrandRepository.GetListNotDeleted(); ;
            return entitys;
        }


        public JmInventoryMBrand GetLastCreatedBrand()
        {
            JmInventoryMBrand ent = this._JmInventoryMBrandRepository.GetLastCreatedBrand();
            return ent;
        }
    }
}
