using UnityEngine;
using System.Collections;
using trackingRoom.interfaces;

public class Test : MonoBehaviour, IMode
{
	private LightModel parent;
	
	public Test(LightModel parent) {
		this.parent = parent;
	}
	
	public void OnUserDetection ()
	{
	}
}
