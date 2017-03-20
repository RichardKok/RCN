using UnityEngine;
using trackingRoom.mvc;
using trackingRoom.util;
using System.Collections.Generic;
using System;

public class MasterController : Controller<MasterApplication>
{
	override public void OnNotification(string p_event_path, UnityEngine.Object p_target, params object[] p_data) {	
		switch (p_event_path) {
		case Dictionary.TrackerUpdate:
			string itemToTrace = (string)p_data[0];
			KeyValuePair<Vector2, DateTime>[] lastTwoUserPositions = (KeyValuePair<Vector2, DateTime>[])p_data[1];
			if (itemToTrace.Equals(Dictionary.User) && lastTwoUserPositions.Length == 2) 
				app.model.extrapolateVectors(lastTwoUserPositions);
			break;
		case Dictionary.MasterApplyTracker:
			app.model.EstimatedPositionScaling = (float)p_data[0];
			break;
		}
	}
	
}
