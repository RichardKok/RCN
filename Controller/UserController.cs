using UnityEngine;
using System.Collections;
using System;
using trackingRoom.mvc;
using System.Collections.Generic;

public class UserController : Controller<UserApplication>
{
	override public void OnNotification (string p_event_path, UnityEngine.Object p_target, params object[] p_data)
	{	
		if (p_event_path.Equals(Dictionary.TrackerUpdate) && p_data[0] != null) {
			KeyValuePair<Vector3, DateTime>[] lastTwoUserPositions =(KeyValuePair<Vector3, DateTime>[])p_data [0];
			app.model.PrevPos = lastTwoUserPositions[0].Key;
			app.model.Pos = lastTwoUserPositions[1].Key;
			app.Notify(Dictionary.UserUpdate, app.model.PrevPos, app.model.Pos, app.model.GoalPos);
		}
	}
}
