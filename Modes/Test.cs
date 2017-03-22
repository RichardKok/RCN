using UnityEngine;
using System.Collections;
using trackingRoom.interfaces;

public class Test : MonoBehaviour, IMode
{
	private LightModel parent;
	
	public Test(LightModel parent) {
		this.parent = parent;
		foreach (LampBehaviour lamp in parent.LampScripts) {
			bool allOn = parent.app.view.testAllLightsOn;
			lamp.SwitchSetting = (parent.app.view.testAllLightsOn)
				? (Switch)new On()
				: (Switch)new Off();
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
