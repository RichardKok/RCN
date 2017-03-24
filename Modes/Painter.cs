using UnityEngine;
using trackingRoom.interfaces;

public class Painter : MonoBehaviour, IMode
{
	//PRIVATE INSTANCE VARIABLES
	LightModel parent;
	
	//CONSTRUCTOR
	public Painter (LightModel parent) {
		this.parent = parent;
	}
	
	#region IMode implementation
	
	public void OnUserDetectionChange (LampBehaviour originScript, string userTag, bool inRange){
		
		
	}
	
	#endregion
}
