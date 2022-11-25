using Newtonsoft.Json;
using System.Linq;

namespace ServerSideAPI.Model.Message
{
	public class DbMessageRepository : IMessageRepository
	{
		private IntraRaddningstjanstDbContext _dbContext;

		public DbMessageRepository(IntraRaddningstjanstDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IEnumerable<Message> AllMessages => _dbContext.Message.OrderByDescending(i => i.CreationTime).Take(20);

		//public SituationTb GetSituationByAmount(int amount)
		//{
		//	return _dbContext.SituationTb.OrderByDescending(i => i.CreationTime).Take(20).ToList();
		//}
	}
}
