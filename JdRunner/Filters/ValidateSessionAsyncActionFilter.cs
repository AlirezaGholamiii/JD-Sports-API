using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using JdRunner.Services.Packages.Interfaces;
using JdRunner.Extentions;
using JdRunner.Models.Custom.RequestResponse;

namespace JdRunner.Filters
{
    public class ValidateSessionAsyncActionFilter : IAsyncActionFilter
    {
        private readonly IAuthenticationDataService _dataService;
        public ValidateSessionAsyncActionFilter(IAuthenticationDataService AuthenticationDataService)
        {
            _dataService = AuthenticationDataService;
            //DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // execute any code before the action executes
            Console.WriteLine($"-------context.ActionDescriptor.DisplayName:{context.ActionDescriptor.DisplayName}--------");



            //UserSessionResponse userSessionResponse = new UserSessionResponse();
            if (!context.HttpContext.Request.Headers.TryGetValue(HttpRequestExtension.HeaderNameApiKey, out var potentialApiKey))
            {
                //context.Result = new UnauthorizedResult();
                //return;
            }
            if (context.ActionDescriptor.RouteValues.TryGetValue("action", out var action) && context.ActionDescriptor.RouteValues.TryGetValue("controller", out var controller))
            {
                Console.WriteLine($"controller:{controller}, action:{action}");
                if (!"Login".Equals(action))
                {
                    if (!context.HttpContext.Request.Headers.TryGetValue(HttpRequestExtension.HeaderNameAuthToken, out var potentialAuthToken))
                    {
                        //userSessionResponse.ReturnResponse = new ReturnResponse { Code = 400, Message = $"{HttpRequestExtension.HeaderNameAuthToken} is required" };
                        //context.Result = new BadRequestObjectResult(userSessionResponse.ReturnResponse);
                        //context.Result = new BadRequestResult();
                        //return;
                    }
                    //Console.WriteLine($"apiKey:{potentialApiKey} sessionId:{potentialApiKey}");
                    if (!"Logout".Equals(action))
                    {
                        UserSessionResponse userSessionResponse = _dataService.ValidateAndExtendSession(potentialApiKey, potentialAuthToken, controller, action);
                        //Console.WriteLine($"code:{userSessionResponse.ReturnResponse.Code} message:{userSessionResponse.ReturnResponse.Message}");

                        //:TODO if user is not allowed to do the specific action return a forbid response to the cleint and dont preceed to the action
                        //if (userSessionResponse.ReturnResponse.Code == 998)
                        //{
                        //    context.Result = new ForbidResult();
                        //    return;
                        //}

                        if (userSessionResponse.ReturnResponse.Code == "999")
                        {
                            //context.Result = new UnauthorizedObjectResult(userSessionResponse.ReturnResponse);
                            context.Result = new UnauthorizedResult();
                            return;
                        }

                        // pass information to controllers
                        context.HttpContext.Request.SetUserSessionResponse(userSessionResponse);

                    }
                }
            }



            var result = await next();
            // execute any code after the action executes
            //Console.WriteLine("execute any code after the action executes");

        }
    }
}