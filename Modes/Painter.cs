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
	
	public void OnUserDetection ()
	{
	}

}
