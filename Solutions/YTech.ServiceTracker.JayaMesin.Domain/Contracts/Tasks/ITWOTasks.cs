﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks
{
    public interface ITWOTasks
    {
        IEnumerable<TWO> GetAllWOs();
        TWO Insert(TWO wo);
        TWO Update(TWO wo);
        TWO Delete(TWO wo);
        TWO One(string woId);
        IEnumerable<TWO> GetWOByDate(DateTime? dateFrom, DateTime? dateTo);

        IEnumerable<TWOHaveRead> GetListNotDeleted(string userName, string WOStatus, string TextSearch);
        TWO GetWOByWONo(string woNo);

        IEnumerable<TWOHaveRead> GetListExpiredWO();
    }
}
