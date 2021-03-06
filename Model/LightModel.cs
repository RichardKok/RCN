﻿using UnityEngine;
using trackingRoom.mvc;
using trackingRoom.util;
using trackingRoom.interfaces;
using System.Collections;


public class LightModel : Model<LightApplication>
{
	//PUBLIC INSTANCE VARIABLES
	public LampBehaviour[] LampScripts { get { return app.LampScripts; } } 
	public Vector3 PrevPos { get; set; } //set in controller
	private Vector3 prevPos;
	public Vector3 Pos { get; set; }  //set in controller
	private Vector3 pos;
	public Vector3 GoalPos { get; set; }  //set in controller
	private Vector3 goalPos;
	
	public float MinEmission { get { return app.view.minEmission; } }
	public float MaxEmission { get { return app.view.maxEmission; } }
	public int VisualRange { get { return app.view.visualRange; } }
	public int AttackDuration { get { return app.view.attackDuration; } }
	public int ReleaseDuration { get { return app.view.releaseDuration; } }
	
	public IMode mode;
	public ADSRSetting attack;
	public ADSRSetting sustain;
	public ADSRSetting release;
	
	public void Start() {
		foreach (LampBehaviour lamp in LampScripts) {
			lamp.Parent = this;
			lamp.Intensity = MinEmission;
		}
		attack = new ADSRSetting("attack", MinEmission, MaxEmission, AttackDuration);
		sustain = new ADSRSetting("sustain", MaxEmission, MaxEmission, 1);
		release = new ADSRSetting("release", MaxEmission, MinEmission, ReleaseDuration);
	}
	
	public LampBehaviour InitSplashFromOrigin {
		set { 
			StartCoroutine(Splash(value));
		} 
	} 
	
	public ADSRSetting NextADSRPhase (ADSRSetting current)
	{
		switch (current.Name) {
		case "Attack":
			return sustain;
		case "Sustain":
			return release;
		}
		return null;
	}

	public LampBehaviour GetRandomLamp ()
	{
		return LampScripts [Random.Range (0, LampScripts.Length - 1)];
	}

	public LampBehaviour GetRandomLamp (int role)
	{
		LampBehaviour randomLampToReturn = null;
		bool gotLamp = false;
		while (!gotLamp) {
			LampBehaviour randomLamp = LampScripts [Random.Range (0, LampScripts.Length - 1)];
			if (randomLamp.Role.Equals (role)) {
				randomLampToReturn = randomLamp;
				gotLamp = true;
			}
		}
		return randomLampToReturn;
	}

	public LampBehaviour[] OrderLampsToDistance (LampBehaviour originLamp)
	{
		foreach (LampBehaviour lamp in LampScripts)
			lamp.IsOrdered = false;
		LampBehaviour[] orderedLamps = new LampBehaviour[LampScripts.Length - 1];
		for (int i = 0; i < orderedLamps.Length; i++)
			orderedLamps [i] = GetClosestUnorderedLamp (originLamp);
		return orderedLamps;
	}

	//PRIVATE FUNCTIONS
	IEnumerator Splash (LampBehaviour origin)
	{		
		LampBehaviour[] orderedLamps = OrderLampsToDistance (origin);
		foreach (LampBehaviour lamp in orderedLamps) {
			if (lamp != null) {
				yield return new WaitForSeconds(4);
				//flicker lamp
			}
		}
	}

     LampBehaviour GetClosestUnorderedLamp (LampBehaviour originLamp)
	{
		float minDistance = 1000.0f; //Random high value
		float distance;
		LampBehaviour closestLamp = null;
		foreach (LampBehaviour lamp in LampScripts) {
			distance = Vector3.Distance(originLamp.transform.position, lamp.transform.position);
			if (!lamp.IsOrdered && !lamp.Role.Equals (originLamp) && distance < minDistance) {
				minDistance = distance;
				closestLamp = lamp;
				closestLamp.IsOrdered = true;
			}
		}
		return closestLamp;	
	}
}

