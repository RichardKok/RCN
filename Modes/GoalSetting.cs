using UnityEngine;
using trackingRoom.util;
using trackingRoom.interfaces;
using System.Collections;

public class GoalSetting : MonoBehaviour, IMode
{	
	//PUBLIC INSTANCE VARIABLES
	public int Active { get; set; }
	private int active;
	
	//PRIVATE INSTANCE VARIABLES
	private LightModel parent;
	private LampBehaviour[] lampScripts; 
	private LampBehaviour master;
	private LampBehaviour seducer;
	
	
	//CONSTRUCTOR
	public GoalSetting(LightModel parent, LampBehaviour[] lampScripts) {
		this.parent = parent;
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
			}
			break;
		}
	}
	
	private bool splashed = false; //temp boolean to make the splash happen only once
	public void OnTimerEvent(string eventPath, int target) {
		switch(eventPath) {
		case Dictionary.TimerSeduce:
			LampBehaviour origin = seducer;
			if(origin != null && !splashed){
				parent.Splash(origin);
				splashed = true;
			}
			break;
		}
	}

	private void SwitchRoles() {
		SetRoles(false);
		seducer = master;
		seducer.Role = Dictionary.Seducer;
		master = parent.GetRandomLamp(Dictionary.Slave);
		master.Role = Dictionary.Master;
	}
		
	private void SetRoles(bool hierarchy){
		foreach (LampBehaviour lamp in lampScripts) lamp.Role = Dictionary.Slave;
		if (hierarchy) {
			master = parent.GetRandomLamp(Dictionary.Slave);
			master.Role = Dictionary.Master;
			seducer = parent.GetRandomLamp(Dictionary.Slave);
			seducer.Role = Dictionary.Seducer;
		}
	}
}
