using UnityEngine;
using System.Collections;
using trackingRoom.mvc;

public class LightView : View<LightApplication>
{
	public int visualRange;
	public int intensityRefresh = 20;	
	public int dimIntensity;
	public int brightIntensity;
	public int attackDuration;
	public int releaseDuration;
}
