using UnityEngine;
using trackingRoom.mvc;
using trackingRoom.util;
using trackingRoom.interfaces;
using System.Collections;

public class LightModel : Model<LightApplication>
{
	//PUBLIC INSTANCE VARIABLES
	public LampBehaviour[] LampScripts { get { return (lampScripts == null) ? lampScripts = app.LampScripts : lampScripts; } }

	private LampBehaviour[] lampScripts;

	public IMode ModeBehaviour { get; set; }

	private IMode modeBehaviour;

	public ADSRSetting Attack { get; set; }

	private ADSRSetting attack;

	public ADSRSetting Decay { get; set; }

	private ADSRSetting decay;

	public ADSRSetting Sustain { get; set; }

	private ADSRSetting sustain;

	public ADSRSetting Release { get; set; }

	private ADSRSetting release;

	public float VisualRange { 
		set {
			foreach (LampBehaviour lamp in LampScripts)
				lamp.VisualRange = value;
		} 
	}

	public float refreshTimeInSeconds;
	
	//PUBLIC FUNCTIONS
	public void Start ()
	{ 
		foreach (LampBehaviour lamp in LampScripts)
			lamp.Parent = this;	
	}
	
	//Make splash happen with a time interval between the lamps
	public void Splash (LampBehaviour origin)
	{		
		LampBehaviour[] orderedLamps = OrderLampsToDistance (origin);
		foreach (LampBehaviour lamp  in orderedLamps) { 
			if (lamp != null) {
				Flicker flicker = new Flicker (); 
				flicker.CurrentPhase = Attack;
				lamp.SwitchSetting = flicker;
			}
		}
	}
	
	public ADSRSetting NextPhase (ADSRSetting current)
	{
		switch (current.Name) {
		case "Attack":
			return Decay;
		case "Decay":
			return Sustain;
		case "Sustain":
			return Release;
		}
		return null;
	}

	public LampBehaviour GetRandomLamp ()
	{
		return GetRandomLamp (Dictionary.Slave);
	}

	public LampBehaviour GetRandomLamp (int role)
	{
		LampBehaviour randomLampToReturn = null;
		bool gotLamp = false;
		while (!gotLamp) {
			LampBehaviour randomLamp = lampScripts [Random.Range (0, lampScripts.Length)];
			if (randomLamp.Role.Equals (role)) {
				randomLampToReturn = randomLamp;
				gotLamp = true;
			}
		}
		return randomLampToReturn;
	}

	public LampBehaviour[] OrderLampsToDistance (LampBehaviour originLamp)
	{
		foreach (LampBehaviour lamp in lampScripts)
			lamp.IsOrdered = false;
		LampBehaviour[] orderedLamps = new LampBehaviour[lampScripts.Length - 1];
		for (int i = 0; i < orderedLamps.Length; i++)
			orderedLamps [i] = GetClosestUnorderedLamp (originLamp);
		return orderedLamps;
	}

	//PRIVATE FUNCTIONS
	private LampBehaviour GetClosestUnorderedLamp (LampBehaviour originLamp)
	{
		float minDistance = 1000.0f; //Random high value
		float distance;
		LampBehaviour closestLamp = null;
		foreach (LampBehaviour lamp in lampScripts) {
			distance = Util.Magnitude (originLamp.transform.position, lamp.transform.position);
			if (!lamp.IsOrdered && !lamp.Role.Equals (Dictionary.Seducer) && distance < minDistance) {
				minDistance = distance;
				closestLamp = lamp;
				closestLamp.IsOrdered = true;
			}
		}
		return closestLamp;	
	}
}

