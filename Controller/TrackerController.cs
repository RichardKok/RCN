using UnityEngine;
using System.Collections;
using trackingRoom.mvc;

public class TrackerController : Controller<TrackerApplication>
{
	override public void OnNotification (string p_event_path, Object p_target, params object[] p_data)
	{		
		if (p_event_path.Equals(Dictionary.TimerObserve)) {
			app.model.addUserPosition ();
			app.Notify (Dictionary.TrackerUpdate, app.model.getLastTwoUserPositions ());
		}
	}
}

