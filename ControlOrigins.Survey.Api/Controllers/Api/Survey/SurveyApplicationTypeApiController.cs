using ControlOrigins.Survey.SoapClient.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlOrigins.Survey.Api.Controllers.Api.Survey
{
    /// <summary>
    /// Application Type Api
    /// </summary>
    [Route("/api/survey/applicationtype")]
    public class SurveyApplicationTypeApiController : BaseApiController
    {
        private readonly ISurveyService surveyService;
        public SurveyApplicationTypeApiController(ISurveyService SurveyService) : base() { surveyService = SurveyService; }
        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ApplicationTypeItem>), 200)]
        public async Task<IEnumerable<ApplicationTypeItem>> GetApplicationTypeAsync()
        {
            return await surveyService.GetApplicationTypeCollection().ConfigureAwait(false);
        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(ApplicationTypeItem), 200)]
        public async Task<ApplicationTypeItem> GetApplicationTypeByApplicationTypeIDAsync(int id)
        {
            return await surveyService.GetApplicationTypeByApplicationTypeID(id).ConfigureAwait(false);
        }
    }
}