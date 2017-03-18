using UnityEngine;
using trackingRoom.util;
using trackingRoom.interfaces;

public class GoalSetting : IMode
{	
	//PUBLIC INSTANCE VARIABLES
	public int Active { get; set; }
	private int active;
	
	//PRIVATE INSTANCE VARIABLES
	private LampBehaviour[] lampScripts; 
	private LampBehaviour master;
	private LampBehaviour seducer;
	
	//CONSTRUCTOR
	public GoalSetting(LampBehaviour[] lampScripts) {
		this.lampScripts = lampScripts;
		SetRoles(true);
	}
	
	//PUBLIC FUNCTIONS
	//INTERFACE IMPLEMENTATION
	public void OnUserTriggerChange(int role, bool inRange) {
		switch(role) {
		case Dictionary.Seducer:
			if (inRange) {
				SwitchRoles();
				Seduce();
			}
			break;
		}
	}
	
	public void OnTimerEvent(string eventPath, int target) {
		switch(eventPath) {
		case Dictionary.TimerSeduce:
			if(target == Dictionary.Seducer) {
				Seduce();
			}
			break;
		}
	}
	//END OF INTERFACE IMPLEMENTATION
	//PRIVATE FUNCTIONS
	private void SwitchRoles() {
		SetRoles(false);
		seducer = master;
		seducer.Role = Dictionary.Seducer;
		master = GetRandomSlave();
		master.Role = Dictionary.Master;
	}
	
	//TODO: On a interval, send a splash through the slave lamps ordered on distance
	private void Seduce() {
		LampBehaviour[] orderedLamps = OrderNonMasterLampsToDistanceFromSeducer();
		foreach (LampBehaviour lamp  in orderedLamps) {
			//lamp.Switch(Dictionary.Flicker);
			//StartCoroutine(Wait(timeForLampToReturnToFirstState));
		}
		//get an array of slave lamps proportional to distance, then in an interval activate them. the deactivatian happens by the lamps adsr settings themselve
	}
	
	private void SetRoles(bool hierarchy){
		foreach (LampBehaviour lamp in lampScripts) lamp.Role = Dictionary.Slave;
		if (hierarchy) {
			master = GetRandomSlave();
			master.Role = Dictionary.Master;
			seducer = GetRandomSlave();
			seducer.Role = Dictionary.Seducer;
		}
	}
	
	private LampBehaviour GetRandomSlave(){
		LampBehaviour randomSlave = null;
		bool gotSlave = false;
		while (!gotSlave) {
			LampBehaviour randomLamp = lampScripts[Random.Range(0, lampScripts.Length)];
			if(randomLamp.Role.Equals(Dictionary.Slave)) {
				randomSlave = randomLamp;
				gotSlave = true;
			}
		}
		return randomSlave;
	}

	private LampBehaviour[] OrderNonMasterLampsToDistanceFromSeducer() {
		foreach (LampBehaviour lamp in lampScripts) lamp.isOrdered = false;
		LampBehaviour[] orderedLamps = new LampBehaviour[lampScripts.Length - 1];
		for (int i = 0; i < orderedLamps.Length; i++) orderedLamps[i] = GetClosestUnorderedLamp ();
		return orderedLamps;
	}

	private LampBehaviour GetClosestUnorderedLamp () {
		float minDistance = 1000.0f; //Random high value
		float distFromSeducer = 0.0f;
		LampBehaviour closestLamp = null;
		foreach (LampBehaviour lamp in lampScripts) {
			distFromSeducer = Util.Magnitude (seducer.transform.position, lamp.transform.position);
			if (!lamp.isOrdered && !lamp.Role.Equals (Dictionary.Seducer) && distFromSeducer < minDistance) {
				minDistance = distFromSeducer;
				closestLamp = lamp;
				closestLamp.isOrdered = true;
			}
		}
		return closestLamp;	
	}
}
