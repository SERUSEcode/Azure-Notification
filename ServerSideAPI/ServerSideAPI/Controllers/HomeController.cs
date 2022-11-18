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
			;
		}

		//[HttpGet("{amount}")]
		//public IEnumerable<SituationTb> GetSituationByAmount(int amount)
		//{
		//	var SelectedAmountOfSituations = _SituationTbRepository.GetSituationByAmount(amount);

		//	return SelectedAmountOfSituations;

  //      }
	}
}
