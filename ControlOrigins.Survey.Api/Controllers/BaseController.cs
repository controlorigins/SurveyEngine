using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace ControlOrigins.Survey.Api.Controllers
{
    /// <summary>
    /// BaseController
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public abstract class BaseController : Controller
    {
        /// <summary>
        ///
        /// </summary>
        public readonly CancellationTokenSource cts;

        /// <summary>
        /// BaseController
        /// </summary>
        protected BaseController()
        {
            cts = new CancellationTokenSource();
        }
    }
}