using UnityEngine;
using System.Collections;
using trackingRoom.mvc;

public class LightView : View<LightApplication>
{
	public float splashInterval = 3.0f;			
	public float yieldRefreshInterval = 0.2f;		
	public bool testAllLightsOn;
}
