using UnityEngine;
using System.Collections;
using trackingRoom.mvc;

public class LightApplication : BaseApplication<LightModel, LightView, LightController>
{	
	public LampBehaviour[] LampScripts { get { return lampScripts = AssertArray<LampBehaviour>(lampScripts); } } 
	private LampBehaviour[] lampScripts; 
}
