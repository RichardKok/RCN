using UnityEngine;
using System.Collections;
using trackingRoom.util;
using trackingRoom.mvc;

public class LightController : Controller<LightApplication>
{
	private bool firstCall = true;
	
	override public void OnNotification(string p_event_path, Object p_target, params object[] p_data) {		
		switch (p_event_path) {
		case Dictionary.MasterApplyLights:
			if (firstCall) {
				app.model.SetLampsParent();
				firstCall = false;
			}
			int[] asdr = new int[4];
			for (int i = 0; i < asdr.Length; i++) asdr[i] = (int)p_data[i];
			app.model.ApplySettingsToLamps(asdr, (float)p_data[4]);
			string mode = (string)p_data[5];
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
		}
	}

}
