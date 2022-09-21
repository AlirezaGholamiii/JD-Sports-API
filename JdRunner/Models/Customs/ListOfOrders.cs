using JdRunner.Models.Custom.RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JdRunner.Models.Customs
{
    public class ListOfOrders:ReturnResponse
    {
        public List<Order> listOforders { get; set; }
    }
}
