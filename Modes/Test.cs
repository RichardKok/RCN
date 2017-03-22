using UnityEngine;
using System.Collections;
using trackingRoom.interfaces;

public class Test : MonoBehaviour, IMode
{
	private LightModel parent;
	
	public Test(LightModel parent) {
		this.parent = parent;
		foreach (LampBehaviour lamp in parent.LampScripts) {
			lamp.Switch((parent.app.view.testAllLightsOn)
				? Dictionary.On
				: Dictionary.Off);
		}
	}

	#region IMode implementation
	public void OnUserTriggerChange (LampBehaviour originScript, string userTag, bool inRange)
	{
	}
	public void OnTimerEvent (string eventPath, int target)
	{
	}
	#endregion
}
