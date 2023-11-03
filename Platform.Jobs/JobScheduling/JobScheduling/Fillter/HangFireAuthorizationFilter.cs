using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Authorization;

namespace JobScheduling.Fillter
{
    public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        //we recive an policy 
        private string _policyName;

        public HangFireAuthorizationFilter(string policyName)
        {
            _policyName = policyName;
        }

        //return bool if this person access the dashbord or not
        public bool Authorize([NotNull] DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            var authService = httpContext.RequestServices.GetRequiredService<IAuthorizationService>();


            //method sync we need implement async method 
            var isauthorize = authService.AuthorizeAsync(httpContext.User, _policyName)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult()
                .Succeeded;

            return isauthorize;
        }
    }
}
