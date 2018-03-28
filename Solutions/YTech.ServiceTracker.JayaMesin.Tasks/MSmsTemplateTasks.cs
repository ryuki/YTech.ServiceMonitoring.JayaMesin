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
    public class MSmsTemplateTasks : IMSmsTemplateTasks
    {
        private readonly IMSmsTemplateRepository _smsTemplateRepository;

        public MSmsTemplateTasks(IMSmsTemplateRepository smsTemplateRepository)
        {
            this._smsTemplateRepository = smsTemplateRepository;
        }

        public IEnumerable<Domain.MSmsTemplate> GetAllSmsTemplates()
        {
            var smsTemplates = this._smsTemplateRepository.GetAll(); ;
            return smsTemplates;
        }
        
        public MSmsTemplate Insert(Domain.MSmsTemplate cust)
        {
            _smsTemplateRepository.DbContext.BeginTransaction();
            _smsTemplateRepository.Save(cust);
            _smsTemplateRepository.DbContext.CommitTransaction();
            return cust;
        }

        public MSmsTemplate Update(Domain.MSmsTemplate cust)
        {
            _smsTemplateRepository.DbContext.BeginTransaction();
            _smsTemplateRepository.Update(cust);
            _smsTemplateRepository.DbContext.CommitTransaction();
            return cust;
        }

        public MSmsTemplate Delete(Domain.MSmsTemplate cust)
        {
            _smsTemplateRepository.DbContext.BeginTransaction();
            _smsTemplateRepository.Delete(cust);
            _smsTemplateRepository.DbContext.CommitTransaction();
            return cust;
        }

        public MSmsTemplate One(string custId)
        {
            var smsTemplates = this._smsTemplateRepository.Get(custId); ;
            return smsTemplates;
        }

        public IEnumerable<MSmsTemplate> GetListNotDeleted()
        {
            var smsTemplates = this._smsTemplateRepository.GetListNotDeleted(); ;
            return smsTemplates;
        }


        public MSmsTemplate GetByWOStatus(string woStatus)
        {
            var smsTemplates = this._smsTemplateRepository.GetByWOStatus(woStatus); ;
            return smsTemplates;
        }
    }
}
