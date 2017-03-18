using UnityEngine;
using trackingRoom.mvc;
using trackingRoom.util;

public class TrackerController : Controller<TrackerApplication>
{
	override public void OnNotification(string p_event_path, Object p_target, params object[] p_data) {		
		switch (p_event_path) {
		case Dictionary.TimerObserve:
			string itemToObserve = (string)p_data[0];
			if (itemToObserve.Equals(Dictionary.User)) {
				app.model.addUserPosition();
				app.Notify(Dictionary.TrackerUpdate, itemToObserve, app.model.getLastTwoUserPositions());
			}
			break;
		}
	}
}


