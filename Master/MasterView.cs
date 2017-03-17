using UnityEngine;
using System.Collections;
using trackingRoom.mvc;

public class MasterView : View<MasterApplication>
{
	public int maxADSR = 50;
	public float maxVisRange = 10.0f;
	public float visRangeStart = 0.5f;
	public int adsrStart = 25;
	public float visualRange;
	public float speedSensitivity = 1.0f;
	public int attack;
	public int decay;
	public int sustain;
	public int release;
	public  string[] modeOptions = {Dictionary.Disabled, Dictionary.Default, Dictionary.GoalMode, Dictionary.Snooker};

	public void Awake() {
		visualRange = visRangeStart;
		attack = adsrStart;
		decay = adsrStart;
		sustain = adsrStart;
		release = adsrStart;
	}

	public void ApplyTrackerChanges(){
		app.Notify(Dictionary.MasterApplyTracker, speedSensitivity);
	}
	
	public void ApplyLightChanges(string mode) {
		app.Notify(Dictionary.MasterApplyLights, attack, decay, sustain, release, visualRange, mode);
	}
}
