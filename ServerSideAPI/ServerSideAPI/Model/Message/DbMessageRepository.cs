using Microsoft.VisualBasic;
using Newtonsoft.Json;
using ServerSideAPI.Model.SituationTb;
using System.Linq;

namespace ServerSideAPI.Model.Message
{
	public class DbMessageRepository : IMessageRepository
	{
		private IntraRaddningstjanstDbContext _dbContext;

		//public Message allMessagesBySituationId { get; set; }

		public DbMessageRepository(IntraRaddningstjanstDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IEnumerable<Message> AllMessages => _dbContext.Message.OrderByDescending(i => i.CreationTime).Take(20);


		//public IEnumerable<Message> AllMessagesWithSituationId(string situationId)
		//{
		//	return _dbContext.Message.Where(i => i.SituationId == situationId).Take(20);
		//}


		public Message GetMessageBySituationId(string situationId)
		{
			Message allMessagesBySituationId = null;
			foreach (var message in _dbContext.Message)
			{
				if (message.SituationId.Equals(situationId))
				{
					allMessagesBySituationId = message;
				}
			}
			return allMessagesBySituationId;
		}

		//public SituationTb GetSituationByAmount(int amount)
		//{
		//	return _dbContext.SituationTb.OrderByDescending(i => i.CreationTime).Take(20).ToList();
		//}
	}
}
