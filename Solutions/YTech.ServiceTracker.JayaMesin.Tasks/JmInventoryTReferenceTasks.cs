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
     public class JmInventoryTReferenceTasks : IJmInventoryTReferenceTasks
    {
      private readonly IJmInventoryTReferenceRepository _JmInventoryTReferenceRepository;

        public JmInventoryTReferenceTasks(IJmInventoryTReferenceRepository JmInventoryTReferenceRepository)
        {
            this._JmInventoryTReferenceRepository = JmInventoryTReferenceRepository;
        }

        public IEnumerable<Domain.JmInventoryTReference> GetAll()
        {
            var entitys = this._JmInventoryTReferenceRepository.GetAll(); ;
            return entitys;
        }
        
        public JmInventoryTReference Insert(Domain.JmInventoryTReference entity)
        {
            _JmInventoryTReferenceRepository.DbContext.BeginTransaction();
            _JmInventoryTReferenceRepository.Save(entity);
            _JmInventoryTReferenceRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTReference Update(Domain.JmInventoryTReference entity)
        {
            _JmInventoryTReferenceRepository.DbContext.BeginTransaction();
            _JmInventoryTReferenceRepository.Update(entity);
            _JmInventoryTReferenceRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTReference Delete(Domain.JmInventoryTReference entity)
        {
            _JmInventoryTReferenceRepository.DbContext.BeginTransaction();
            _JmInventoryTReferenceRepository.Delete(entity);
            _JmInventoryTReferenceRepository.DbContext.CommitTransaction();
            return entity;
        }

        public JmInventoryTReference One(string id)
        {
            var entitys = this._JmInventoryTReferenceRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<JmInventoryTReference> GetListNotDeleted()
        {
            var entitys = this._JmInventoryTReferenceRepository.GetListNotDeleted(); ;
            return entitys;
        }


        public JmInventoryTReference GetByReferenceType(Enums.EnumInventoryReferenceType enumInventoryReferenceType)
        {
            JmInventoryTReference entitys = this._JmInventoryTReferenceRepository.GetByReferenceType(enumInventoryReferenceType); ;
            return entitys;
        }
    }
}
