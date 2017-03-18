using UnityEngine;
using System.Collections;

public class LampBehaviour : MonoBehaviour, IListener, ISwitch 
{
	public LightModel Parent { set { parent = value; } }
	private LightModel parent;
	
	public bool isOrdered;
	public float VisualRange { set { visualRange = value; this.GetComponent<SphereCollider>().radius = value; } }
	private float visualRange;
	
	private int switchSetting;
	
	public int Role { get; set; }
	private int role;
	
	private int attackTime;
	private int decayTime;
	private int sustainTime;
	private int releaseTime;
	
	public void ApplyADSR(int[] adsr) {
		attackTime = adsr[0];
		decayTime = adsr[1];
		sustainTime = adsr[2];
		releaseTime = adsr[3];
	}
	
	public int Switch(int setting){
		switch (setting){
		case Dictionary.Off:
			switchSetting = Dictionary.Off;
			ASDR(new int[] {Dictionary.Release});
			return releaseTime;
		case Dictionary.On:
			switchSetting = Dictionary.On;
			ASDR(new int[] {Dictionary.Attack, Dictionary.Decay});
			return attackTime;
		case Dictionary.Flicker:
			switchSetting = Dictionary.Flicker;
			ASDR(new int[] {Dictionary.Attack, Dictionary.Decay, Dictionary.Sustain, Dictionary.Release});
			return attackTime + decayTime + sustainTime + releaseTime;
		case Dictionary.Flickering:
			switchSetting = Dictionary.Flickering;
			ASDR(new int[] {Dictionary.Attack, Dictionary.Decay, Dictionary.Sustain, Dictionary.Release});
			return attackTime + decayTime + sustainTime + releaseTime;
		}
		return 0;
	}
	
	public void ASDR(params int[] adsrOptions) {
		foreach (int option in adsrOptions) {
			switch (option) {
			case Dictionary.Attack:
				break;
			case Dictionary.Decay:
				break;
			case Dictionary.Sustain:
				break;
			case Dictionary.Release:
				break;
			}
		}
	}
	

	public void OnTriggerEnter(Collider col_data) {
		if (parent.ModeBehaviour != null && 
			(col_data.gameObject.tag.Equals(Dictionary.User) || col_data.gameObject.tag.Equals(Dictionary.EstimatedUserPosition))) {
			parent.ModeBehaviour.OnUserTriggerChange(role, true);
		}
	}
	
	public void OnTriggerExit(Collider col_data) {
		if (parent.ModeBehaviour != null && 
			(col_data.gameObject.tag.Equals(Dictionary.User) || col_data.gameObject.tag.Equals(Dictionary.EstimatedUserPosition))) {
			parent.ModeBehaviour.OnUserTriggerChange(role, false);
		}
	}

}
