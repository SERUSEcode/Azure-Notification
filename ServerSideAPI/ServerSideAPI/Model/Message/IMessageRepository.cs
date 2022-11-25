using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ServerSideAPI.Model.Message
{
	public interface IMessageRepository
	{
		public IEnumerable<Message> AllMessages { get; }


		//SituationTb GetSituationByAmount(int amount);
	}
}
