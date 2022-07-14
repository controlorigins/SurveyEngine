using ControlOrigins.Survey.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ControlOrigins.Survey.Api.Controllers.Api
{
    /// <summary>
    /// Base for all Api Controllers in this project
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    public abstract class BaseApiController : Controller
    {
        /// <summary>
        /// GetApplicationStatus
        /// </summary>
        /// <returns></returns>
        protected ApplicationStatus GetApplicationStatus()
        {
            return new ApplicationStatus(Assembly.GetExecutingAssembly());
        }
    }
}