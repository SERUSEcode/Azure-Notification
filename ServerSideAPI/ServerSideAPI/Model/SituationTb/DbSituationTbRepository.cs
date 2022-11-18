using Newtonsoft.Json;
using System.Linq;

namespace ServerSideAPI.Model.SituationTb
{
	public class DbSituationTbRepository : ISituationTbRepository
	{
		private IntraRaddningstjanstDbContext _dbContext;

		public DbSituationTbRepository(IntraRaddningstjanstDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IEnumerable<SituationTb> AllSituationTb => _dbContext.SituationTb.OrderByDescending(i => i.CreationTime).Take(20);

		//public SituationTb GetSituationByAmount(int amount)
		//{
		//	return _dbContext.SituationTb.OrderByDescending(i => i.CreationTime).Take(20).ToList();
		//}
	}
}
