using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks;

namespace YTech.ServiceTracker.JayaMesin.Tasks
{
    public class TWOTasks : ITWOTasks
    {
        private readonly ITWORepository _woRepository;

        public TWOTasks(ITWORepository woRepository)
        {
            this._woRepository = woRepository;
        }

        public IEnumerable<Domain.TWO> GetAllWOs()
        {
            var wos = this._woRepository.GetAll(); ;
            return wos;
        }


        public Domain.TWO Insert(Domain.TWO wo)
        {
            _woRepository.DbContext.BeginTransaction();
            _woRepository.Save(wo);
            _woRepository.DbContext.CommitTransaction();
            return wo;
        }

        public Domain.TWO Update(Domain.TWO wo)
        {
            _woRepository.DbContext.BeginTransaction();
            _woRepository.Update(wo);
            _woRepository.DbContext.CommitTransaction();
            return wo;
        }

        public Domain.TWO Delete(Domain.TWO wo)
        {
            _woRepository.DbContext.BeginTransaction();
            _woRepository.Delete(wo);
            _woRepository.DbContext.CommitTransaction();
            return wo;
        }

        public Domain.TWO One(string woId)
        {
            var wo = this._woRepository.Get(woId); ;
            return wo;
        }
    }
}
