
namespace trackingRoom.interfaces
{
	public interface IMode
	{
		void OnUserTriggerChange (LampBehaviour originScript, string userTag, bool inRange);

		void OnTimerEvent (string eventPath, int target);
	}
}