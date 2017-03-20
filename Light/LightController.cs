using UnityEngine;
using System.Collections;
using trackingRoom.util;
using trackingRoom.mvc;

public class LightController : Controller<LightApplication>
{
	
	override public void OnNotification(string p_event_path, Object p_target, params object[] p_data) {		
		switch (p_event_path) {
		case Dictionary.MasterApplyLights:
			int i = 0;
			app.model.AttackTime = (int)p_data[i++];
			app.model.DecayTime = (int)p_data[i++];
			app.model.SustainTime = (int)p_data[i++];
			app.model.ReleaseTime = (int)p_data[i++];
			app.model.OffIntensity = (float)p_data[i++];
			app.model.SustainIntensity = (float)p_data[i++];
			app.model.MaxIntensity = (float)p_data[i++];
			app.model.VisualRange = (float)p_data[i];
			string mode = (string)p_data[p_data.Length - 1];
			switch(mode) {
			case Dictionary.Disabled:
				app.model.ModeBehaviour = null;
				break;
			case Dictionary.Default:
				break;
			case Dictionary.GoalMode:
				app.model.ModeBehaviour = new GoalSetting(app.model, app.model.LampScripts);
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
