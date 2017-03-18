using UnityEngine;
using System.Collections;
using trackingRoom.util;
using trackingRoom.mvc;

public class LightController : Controller<LightApplication>
{
	//Temporary debugging function
	public void Awake() {
		app.model.ModeBehaviour = new GoalSetting(app.model.LampScripts);
	}
	
	override public void OnNotification(string p_event_path, Object p_target, params object[] p_data) {		
		switch (p_event_path) {
		case Dictionary.MasterApplyLights:
			app.model.ApplySettingsToLamps(p_data);
			string mode = (string)p_data[p_data.Length];
			switch(mode) {
			case Dictionary.Disabled:
				app.model.ModeBehaviour = null;
				break;
			case Dictionary.Default:
				break;
			case Dictionary.GoalMode:
				app.model.ModeBehaviour = new GoalSetting(app.model.LampScripts);
				break;
			case Dictionary.Snooker:
				break;
			}
			break;
		case Dictionary.TimerSeduce:
			if (app.model.ModeBehaviour != null)
				app.model.ModeBehaviour.OnTimerEvent(p_event_path, (int)p_data[0]);
			break;
		}
	}
}
