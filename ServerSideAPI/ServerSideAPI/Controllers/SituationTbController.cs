using Microsoft.AspNetCore.Mvc;
using ServerSideAPI.Model;
using ServerSideAPI.Model.SituationTb;
using System.Collections.Generic;

namespace ServerSideAPI.Controllers
{
    [ApiController]
    [Route("api/Situation")]
    public class SituationTbController : Controller
	{

		private readonly ISituationTbRepository _SituationTbRepository;
		public SituationTbController(ISituationTbRepository situationTbRepository)
		{
			_SituationTbRepository = situationTbRepository;
		}

		[HttpGet]
		public IActionResult GetSituations()
		{

			try
			{
                var AllSituations = _SituationTbRepository.AllSituationTb;

				return Ok(AllSituations);
            }
			catch
			{
                return StatusCode(500, "Internal server error");
            }
		}
	}
}
