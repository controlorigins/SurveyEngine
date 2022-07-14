﻿using ControlOrigins.Survey.SoapClient.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ControlOrigins.Survey.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SurveyController : Controller
    {
        private readonly ISurveyService surveyService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SurveyService"></param>
        public SurveyController(ISurveyService SurveyService)
        { surveyService = SurveyService; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("/survey/")]
        public async Task<IActionResult> Index()
        { return View(await surveyService.GetSurveyCollection().ConfigureAwait(false)); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("/survey/{Id}")]
        public async Task<IActionResult> Edit(int Id)
        {
            var survey = await surveyService.GetSurveyBySurveyId(Id).ConfigureAwait(false);
            return View("Edit", survey);
        }
    }
}