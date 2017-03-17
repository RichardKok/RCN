
public class TrackerTimer : Timer<TrackerApplication>
{
	new public void Awake() {
		eventPath = Dictionary.TimerObserve;
		data = new object[]{"user"};
	}
}
