using ControlOrigins.Survey.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControlOrigins.Survey.Api.Controllers.Api
{
    /// <summary>
    /// Status Controller
    /// </summary>
    [Route("/status")]
    public class StatusController : BaseApiController
    {
        /// <summary>
        /// Returns Current Application Status
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApplicationStatus), 200)]
        public ApplicationStatus Get()
        {
            return GetApplicationStatus();
        }
    }
}