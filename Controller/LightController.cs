using UnityEngine;
using System.Collections;
using trackingRoom.mvc;

public class LightController : Controller<LightApplication>
{	
	override public void OnNotification (string p_event_path, UnityEngine.Object p_target, params object[] p_data)
	{	
		if (p_event_path.Equals(Dictionary.UserUpdate)) {
			app.model.PrevPos = (Vector3) p_data[0];
			app.model.Pos = (Vector3) p_data[1];
			app.model.GoalPos = (Vector3) p_data[2];
		}
	}
}

