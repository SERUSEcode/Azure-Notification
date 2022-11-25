using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerSideAPI.Model.Message;
using ServerSideAPI.Model.SituationTb;
using System.Data;
using System;
using ServerSideAPI.Model;

namespace ServerSideAPI.Controllers
{
	[ApiController]
	[Route("api/Message")]
	public class MessageController : Controller
	{
		private readonly IMessageRepository _MessageRepository;
		public MessageController(IMessageRepository messageRepository)
		{
			_MessageRepository = messageRepository;
		}

		[HttpGet]
		public IActionResult GetSituations()
		{

			try
			{
				var AllSituations = _MessageRepository.AllMessages;

				return Ok(AllSituations);
			}
			catch
			{
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpPost]
		[ActionName("/Add")]
		public IActionResult AddFilm(string userId, string situationId, string messageText)
		{
			var message = new Message()
			{
				UserId = userId,
				SituationId = situationId,
				MessageText = messageText
			};

			using (var db = new IntraRaddningstjanstDbContext())
			{
				db.Add(message);
				db.SaveChanges();
			}

			return Ok(message);
		}
	}
}
