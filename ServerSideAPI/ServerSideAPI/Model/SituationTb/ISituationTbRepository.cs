using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ServerSideAPI.Model.SituationTb
{
	public interface ISituationTbRepository
	{
		public IEnumerable<SituationTb> AllSituationTb { get; }

		//SituationTb GetSituationByAmount(int amount);
	}
}
