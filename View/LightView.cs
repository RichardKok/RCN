using UnityEngine;
using System.Collections;
using trackingRoom.mvc;

public class LightView : View<LightApplication>
{
	public int visualRange;
	public float minEmission;
	public float maxEmission;
	public int attackDuration;
	public int releaseDuration;
}
