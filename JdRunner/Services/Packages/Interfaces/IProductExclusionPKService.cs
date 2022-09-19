using JdRunner.Models.Custom.RequestResponse;
using JdRunner.Models.Customs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JdRunner.Services.Packages.Interfaces
{
    public interface IProductExclusionPKService
    {
        public Task<ListOfProducts> SimpleSearch(string sessionToken, string searchCondition);
      

    }
}

