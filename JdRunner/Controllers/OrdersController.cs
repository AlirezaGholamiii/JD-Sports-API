using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JdRunner.Filters;
using JdRunner.Services.Packages.Interfaces;
using JdRunner.Extentions;

using System.Threading.Tasks;
using JdRunner.Models.Customs;
using JdRunner.Database;
using JdRunner.Models.Custom.RequestResponse;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;


namespace JdRunner.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ValidateSessionAsyncActionFilter))]

    public class OrdersController
    {

        private readonly IProductExclusionPKService dataService;
        private readonly ILogger<OrdersController> _logger;
        ModelContext _context;

        public OrdersController(ILogger<OrdersController> logger, IProductExclusionPKService productsExclusionPK, ModelContext context)
        {
            dataService = productsExclusionPK;
            _logger = logger;
            _context = context;
        }

        [HttpGet("ListOfProduct")]
        public async Task<ListOfProducts> SimpleSearch(string searchCondition, HttpContext httpContext)
        {
            _logger.LogInformation("ListOfProduct");
            string potentialAuthToken = httpContext.Request.GetHeader(HttpRequestExtension.HeaderNameAuthToken);
            _logger.LogInformation("SimpleSearch Token{0} Condition{1} ", potentialAuthToken, searchCondition);

            return await dataService.SimpleSearch(potentialAuthToken, searchCondition);
        }
        
   

    }
}
