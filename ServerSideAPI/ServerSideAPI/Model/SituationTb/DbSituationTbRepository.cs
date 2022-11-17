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

		public IEnumerable<SituationTb> AllSituationTb => _dbContext.SituationTb;

		public SituationTb GetSituationByAmount(int amount)
		{
			IEnumerable<string> test =
				_dbContext.SituationTb.OrderByDescending(i => i.CreationTime).Take(amount).LastOrDefault();

			return test;
		}
	}
}
