using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerSideAPI.Model.Message;
using ServerSideAPI.Model.SituationTb;
using System.Data;
using System;
using ServerSideAPI.Model;
using Microsoft.SqlServer.Server;

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
		public IActionResult GetAllMessages()
		{

			try
			{
				var AllMessages = _MessageRepository.AllMessages;

				return Ok(AllMessages);
			}
			catch
			{
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpGet("{SituationId}")]
		public IActionResult GetMessageBySituationId(string SituationId)
		{

			try
			{
				var message = _MessageRepository.GetMessageBySituationId(SituationId);
				return Ok(message);
			}
			catch
			{
				return StatusCode(500, "Internal server error");
			}
		}

		//[HttpGet]
		//public IActionResult AllMessagesWithSituationId()
		//{

		//	try
		//	{
		//		var message = _MessageRepository.AllMessagesWithSituationId;
		//		return Ok(message);
		//	}
		//	catch
		//	{
		//		return StatusCode(500, "Internal server error");
		//	}
		//}


		[HttpPost]
		[ActionName("/Add")]
		public IActionResult AddFilm(string userId, string situationId, string messageText)
		{
			var guidString = System.Guid.NewGuid().ToString();

			var message = new Message()
			{
				Id = guidString,
				UserId = userId,
				SituationId = situationId,
				MessageText = messageText,
				CreationTime= DateTime.Now,
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
