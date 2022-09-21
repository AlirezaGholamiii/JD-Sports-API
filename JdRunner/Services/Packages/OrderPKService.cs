using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using JdRunner.Database;
using JdRunner.Extentions;
using JdRunner.Models.Custom.RequestResponse;
using JdRunner.Models.Customs;
using JdRunner.Services.Packages.Interfaces;
using JdRunner.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace JdRunner.Services.Packages
{
    public class OrderPKService : IOrderPKService
    {

        private readonly ILogger<OrderPKService> _logger;

        public OrderPKService(ILogger<OrderPKService> logger)
        {
            _logger = logger;
        }

        async Task<ListOfOrders> IOrderPKService.SimpleSearch(string sessionToken)
        {
            _logger.LogInformation("SimpleSearch search");

            ListOfOrders response = new ListOfOrders();

            try
            {
                //OracleDataReader odrPromotionsItems = null;
                using OracleConnection conn = await ConnectionManager.GetPosConnectionAsync();
                using OracleCommand cmd = new OracleCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = $"{ConnectionManager.GetPosSchemaName()}.mobile_common_pk.search_runner"

                };
                cmd.Parameters.Add("p_sessionToken", OracleDbType.Varchar2, ParameterDirection.Input).Value = sessionToken;
                cmd.Parameters.Add("p_Cursor", OracleDbType.RefCursor, ParameterDirection.Output);
                cmd.Parameters.Add("p_returnCode", OracleDbType.Int32, ParameterDirection.Output, 1);
                cmd.Parameters.Add("p_returnMessage", OracleDbType.Varchar2, ParameterDirection.Output, 4000);
                int numOfAffectedRows = await cmd.ExecuteNonQueryAsync();

                Order currentItem;

                if (!((OracleRefCursor)cmd.Parameters["p_Cursor"].Value).IsNull)
                {
                    List<Order> searchResponsesList = new List<Order>();

                    OracleDataReader odrResponseItems = ((OracleRefCursor)cmd.Parameters["p_Cursor"].Value).GetDataReader();
                    while (odrResponseItems.Read())
                    {
                        currentItem = new Order();

                        currentItem.Ticket = odrResponseItems.GetStringByColumnName(nameof(Resource.TICKET));
                        currentItem.Status = odrResponseItems.GetStringByColumnName(nameof(Resource.STATUS));
                        currentItem.Requester = odrResponseItems.GetStringByColumnName(nameof(Resource.REQUESTER));
                        currentItem.Runner = odrResponseItems.GetStringByColumnName(nameof(Resource.RUNNER));
                        currentItem.Description = odrResponseItems.GetStringByColumnName(nameof(Resource.DESCRIPTION));
                        currentItem.Color = odrResponseItems.GetStringByColumnName(nameof(Resource.COLOR));
                        currentItem.Size = odrResponseItems.GetStringByColumnName(nameof(Resource.SIZE_));
                        currentItem.ReqTime =DateTime.Parse(odrResponseItems.GetStringByColumnName(nameof(Resource.REQTIME)));


                        searchResponsesList.Add(currentItem);
                    }
                    response.listOforders = searchResponsesList;
                }

                response.Code = cmd.Parameters["p_returnCode"].Value.NullToEpmptyString();
                response.Message = cmd.Parameters["p_returnMessage"].Value.NullToEpmptyString();
            }
            catch (Exception e)
            {
                _logger.LogError("Simple_Search Exception:{0}", e);
                response.Code = "-1";
                response.Message = e.Message;
            }

            return response;
        }
       
       
    }

}
