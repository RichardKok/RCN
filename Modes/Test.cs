using UnityEngine;
using System.Collections;
using trackingRoom.interfaces;

public class Test : MonoBehaviour, IMode
{
	LightModel parent;
	
	public Test(LightModel parent) {
		this.parent = parent;
	}
	
	public void OnUserDetectionChange (LampBehaviour originScript, string userTag, bool inRange){


	}
}
