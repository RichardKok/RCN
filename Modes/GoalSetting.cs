using UnityEngine;
using System.Collections;

public class GoalSetting : IMode
{
	private LampBehaviour[] lampScripts; 
	
	public int Active { get; set; }
	private int active;
	
	public GoalSetting(LampBehaviour[] lampScripts) {
		this.lampScripts = lampScripts;
	}
	
	public void UserInLampsVisualRange(int role, bool inRange) {
		switch(role) {
		case Dictionary.Master:
			if (!inRange) {
				//TODO: Turn off light
			}
			break;
		case Dictionary.Seducer:
			if (inRange) {
				//TODO: set as master, choose new random seducer
				//get an array of slave lamps proportional to distance, then in an interval activate them. the deactivatian happens by the lamps adsr settings themselve
			}
			break;
		}
	}
	
	public void SetLightMode(int mode){
		
	}

}
