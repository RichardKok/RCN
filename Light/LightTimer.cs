using UnityEngine;
using System.Collections;

public class LightTimer : Timer<LightApplication>
{
	new public void Awake ()
	{
		eventPath = Dictionary.TimerSeduce;
		data = new object[] { Dictionary.Seducer };
	}
}
