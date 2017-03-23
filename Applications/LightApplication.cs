using UnityEngine;
using trackingRoom.mvc;
using System.Collections.Generic;
using System;

public class LightApplication : BaseApplication<LightModel, LightView, LightController>
{
	public LampBehaviour[] LampScripts { get { return lampScripts = AssertArray<LampBehaviour> (lampScripts); } }

	private LampBehaviour[] lampScripts;
}

