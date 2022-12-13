using ServerSideAPI.Model.SituationTb;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ServerSideAPI.Model.Message
{
	public interface IMessageRepository
	{
		public IEnumerable<Message> AllMessages { get; }
		//public IEnumerable<Message> AllMessagesWithSituationId { get; }

		Message GetMessageBySituationId(string situationId);

		//Message AllMessagesWithSituationId(string situationId);
		//SituationTb GetSituationByAmount(int amount);
	}
}
