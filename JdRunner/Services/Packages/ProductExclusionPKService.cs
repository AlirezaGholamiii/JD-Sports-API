using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using JdRunner.Database;
using JdRunner.Extentions;
using JdRunner.Models.Custom.RequestResponse;
using JdRunner.Models.Customs;
using JdRunner.Services.Packages.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace JdRunner.Services.Packages
{
    public class ProductExclusionPKService : IProductExclusionPKService
    {

        private readonly ILogger<ProductExclusionPKService> _logger;

        public ProductExclusionPKService(ILogger<ProductExclusionPKService> logger)
        {
            _logger = logger;
        }

        async Task<ListOfProducts> IProductExclusionPKService.SimpleSearch(string sessionToken, string searchCondition)
        {
            _logger.LogInformation("SimpleSearch searchCondition:{0} ", searchCondition);

            ListOfProducts response = new ListOfProducts();

            try
            {
                //OracleDataReader odrPromotionsItems = null;
                using OracleConnection conn = await ConnectionManager.GetPosConnectionAsync();
                using OracleCommand cmd = new OracleCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = $"{ConnectionManager.GetPosSchemaName()}.ITEM_EXCLUSION_PK.simpleSearch"

                };
                cmd.Parameters.Add("P_SESSIONTOKEN", OracleDbType.Varchar2, ParameterDirection.Input).Value = sessionToken;
                cmd.Parameters.Add("p_search", OracleDbType.Varchar2, ParameterDirection.Input).Value = searchCondition;
                cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output);
                cmd.Parameters.Add("P_RETURNCODE", OracleDbType.Int32, ParameterDirection.Output, 1);
                cmd.Parameters.Add("P_RETURNMESSAGE", OracleDbType.Varchar2, ParameterDirection.Output, 4000);

                int numOfAffectedRows = await cmd.ExecuteNonQueryAsync();
                _logger.LogDebug("Simple_Search numOfAffectedRows:{0}", numOfAffectedRows);

                Product currentItem;

                if (!((OracleRefCursor)cmd.Parameters["p_cursor"].Value).IsNull)
                {
                    List<Product> searchResponsesList = new List<Product>();

                    OracleDataReader odrResponseItems = ((OracleRefCursor)cmd.Parameters["p_cursor"].Value).GetDataReader();
                    while (odrResponseItems.Read())
                    {
                        currentItem = new Product();

                      //  currentItem.Requester = odrResponseItems.GetStringByColumnName(nameof(DataDictionary.))






                        currentItem.Departmentdesc = odrResponseItems.GetStringByColumnName(nameof(DataDictionary.departmentDesc));
                        currentItem.Classdesc = odrResponseItems.GetStringByColumnName(nameof(DataDictionary.classDesc));
                        currentItem.Subclassdesc = odrResponseItems.GetStringByColumnName(nameof(DataDictionary.subclassDesc));
                        currentItem.Styleno = odrResponseItems.GetStringByColumnName(nameof(DataDictionary.styleno));
                        currentItem.Styledesc = odrResponseItems.GetStringByColumnName(nameof(DataDictionary.styleDesc));
                        currentItem.Barcode = odrResponseItems.GetStringByColumnName(nameof(DataDictionary.barcode));
                        currentItem.Colordesc = odrResponseItems.GetStringByColumnName(nameof(DataDictionary.colorDesc));
                        currentItem.Sizedesc = odrResponseItems.GetStringByColumnName(nameof(DataDictionary.sizeDesc));
                        currentItem.Dimensiondesc = odrResponseItems.GetStringByColumnName(nameof(DataDictionary.dimensionDesc));
                        currentItem.Earnloyalty = odrResponseItems.GetStringByColumnName(nameof(DataDictionary.earnLoyalty));
                        currentItem.Redeemloyalty = odrResponseItems.GetStringByColumnName(nameof(DataDictionary.redeemLoyalty));
                        currentItem.Promotions = odrResponseItems.GetStringByColumnName(nameof(DataDictionary.promotions));
                        currentItem.Discounts = odrResponseItems.GetStringByColumnName(nameof(DataDictionary.discounts));
                        currentItem.Createdby = odrResponseItems.GetStringByColumnName(nameof(DataDictionary.createdBy));
                        currentItem.Createddate = DateTime.Parse(odrResponseItems.GetStringByColumnName(nameof(DataDictionary.createdDate)));
                        currentItem.Modifiedby = odrResponseItems.GetStringByColumnName(nameof(DataDictionary.modifiedBy));
                        currentItem.Modifieddate = DateTime.Parse(odrResponseItems.GetStringByColumnName(nameof(DataDictionary.modifiedDate)));
                        currentItem.Uniqueexcludegroup = odrResponseItems.GetStringByColumnName(nameof(DataDictionary.uniqueExcludeGroup));
                        currentItem.Uniquesku = odrResponseItems.GetStringByColumnName(nameof(DataDictionary.uniqueSku));
                        currentItem.Uniquestyleno = odrResponseItems.GetStringByColumnName(nameof(DataDictionary.uniqueStyleNo));
                        currentItem.Uniquedepartment = odrResponseItems.GetStringByColumnName(nameof(DataDictionary.uniqueDepartment));
                        currentItem.Uniqueclass = odrResponseItems.GetStringByColumnName(nameof(DataDictionary.uniqueClass));
                        currentItem.Uniquesubclass = odrResponseItems.GetStringByColumnName(nameof(DataDictionary.uniqueSubclass));
                        searchResponsesList.Add(currentItem);
                    }
                    response.ProductsRespondList = searchResponsesList;
                }

                response.Code = cmd.Parameters["P_RETURNCODE"].Value.NullToEpmptyString();
                response.Message = cmd.Parameters["P_RETURNMESSAGE"].Value.NullToEpmptyString();
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
