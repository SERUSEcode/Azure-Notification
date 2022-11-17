using Microsoft.AspNetCore.Mvc;
using ServerSideAPI.Model;
using ServerSideAPI.Model.SituationTb;
using System.Collections.Generic;

namespace ServerSideAPI.Controllers
{
    [ApiController]
    [Route("api/Situation")]
    public class HomeController : Controller
	{

		private readonly ISituationTbRepository _SituationTbRepository;
		public HomeController(ISituationTbRepository situationTbRepository)
		{
			_SituationTbRepository = situationTbRepository;
		}

		[HttpGet]
		public IEnumerable<SituationTb> GetSituations()
		{
			var AllSituations = _SituationTbRepository.AllSituationTb;

			return AllSituations;
		}

		[HttpGet("{amount}")]
		public IActionResult GetSituationByAmount(int amount)
		{
			try
			{
				var Situation = _SituationTbRepository.GetSituationByAmount(amount);
				return Ok(Situation);
			}
			catch
			{
				return StatusCode(500, "Internal server error");
			}
		}
	}
}
