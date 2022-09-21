using Microsoft.AspNetCore.Mvc;
using JdRunner.Filters;
using JdRunner.Services.Packages.Interfaces;
using JdRunner.Database;
using JdRunner.Models.Customs;
using JdRunner.Extentions;

namespace JdRunner.Controllers.Packages
{

    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ValidateSessionAsyncActionFilter))]

    public class OrdersController:ControllerBase
    {

        private readonly IOrderPKService dataService;
        private readonly ILogger<OrdersController> _logger;
        ModelContext _context;

        public OrdersController(ILogger<OrdersController> logger, IOrderPKService orderPK, ModelContext context)
        {
            dataService = orderPK;
            _logger = logger;
            _context = context;
        }

        [HttpGet("ListOfProduct")]
        public async Task<ListOfOrders> SimpleSearch()
        {
            _logger.LogInformation("ListOfProduct");
            string potentialAuthToken = HttpContext.Request.GetHeader(HttpRequestExtension.HeaderNameAuthToken);
            _logger.LogInformation("SimpleSearch Token{0} ", potentialAuthToken);
            
            return await dataService.SimpleSearch(potentialAuthToken);
        }
        
   

    }
}
