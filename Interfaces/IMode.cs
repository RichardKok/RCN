
namespace trackingRoom.interfaces
{
	public interface IMode
	{
		void OnUserTriggerChange (int role, bool inRange);

		void OnTimerEvent (string eventPath, int target);
	}
}