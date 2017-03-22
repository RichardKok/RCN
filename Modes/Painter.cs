using UnityEngine;
using trackingRoom.interfaces;

public class Painter : MonoBehaviour, IMode
{
	//PRIVATE INSTANCE VARIABLES
	private LightModel parent;
	
	//CONSTRUCTOR
	public Painter (LightModel parent) {
		this.parent = parent;
	}
	
	#region IMode implementation
	
	public void OnUserTriggerChange (LampBehaviour originScript, string userTag, bool inRange)
	{
		if (userTag.Equals(Dictionary.EstimatedUserPosition) && inRange) 
			originScript.Switch(Dictionary.Flicker);
		
	}

	public void OnTimerEvent (string eventPath, int target)
	{

	}

	#endregion

}
