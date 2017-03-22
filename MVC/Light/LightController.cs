using UnityEngine;
using System.Collections;
using trackingRoom.util;
using trackingRoom.mvc;

public class LightController : Controller<LightApplication>
{
	
	override public void OnNotification (string p_event_path, Object p_target, params object[] p_data)
	{		
		switch (p_event_path) {
		case Dictionary.MasterApplyLights:
			int attackTime = (int)p_data [0];
			int decayTime = (int)p_data [1];
			int sustainTime = (int)p_data [2];
			int releaseTime = (int)p_data [3];
			float offIntensity = (float)p_data [4];
			float sustainIntensity = (float)p_data [5];
			float maxIntensity = (float)p_data [6];
			float visualRange = (float)p_data [7];
			app.model.Attack = new ADSRSetting ("Attack", offIntensity, maxIntensity, attackTime);
			app.model.Decay = new ADSRSetting ("Decay", maxIntensity, sustainIntensity, decayTime);
			app.model.Sustain = new ADSRSetting ("Sustain", sustainIntensity, sustainIntensity, sustainTime);
			app.model.Release = new ADSRSetting ("Release", sustainIntensity, offIntensity, releaseTime);
			app.model.VisualRange = visualRange;
			
			string mode = (string)p_data [p_data.Length - 1];
			switch (mode) {
			case Dictionary.Test:
				app.model.ModeBehaviour = new Test (app.model);
				break;
			case Dictionary.Painter:
				app.model.ModeBehaviour = new Painter (app.model);
				break;
			case Dictionary.GoalMode:
				app.model.ModeBehaviour = new GoalSetting (app.model);
				break;
			}
			break;
		case Dictionary.TimerSeduce:
			if (app.model.ModeBehaviour != null)
				app.model.ModeBehaviour.OnTimerEvent (p_event_path, (int)p_data [0]);
			break;
		}
	}
}

