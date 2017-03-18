using UnityEngine;
using trackingRoom.interfaces;

public class LampBehaviour : MonoBehaviour, IListener, ISwitch 
{
	//PUBLIC INSTANCE VARIABLES
	public LightModel Parent { set { parent = value; } }
	private LightModel parent;

	public float VisualRange { set { visualRange = value; this.GetComponent<SphereCollider>().radius = value; } }
	private float visualRange;
	
	public float MinIntensity { get; set; }
	private float minIntensity;
	
	public float SustainIntensity { get; set; }
	private float sustainIntensity;
	
	public float MaxIntensity { get; set; }
	private float maxIntensity;
		
	public int Role { get; set; }
	private int role;
	
	public bool isOrdered;

	//PRIVATE INSTANCE VARIABLES
	private int switchSetting;
	
	private int attackTime;
	private int decayTime;
	private int sustainTime;
	private int releaseTime;
	
	
	//PUBLIC FUNCTIONS
	//Temporary debugging function
	public void Awake() {
		attackTime = 1;
		decayTime = 1;
		sustainTime = 1;
		releaseTime = 1;
		MinIntensity = 2;
		MaxIntensity = 4;
		VisualRange = 1;
	}
	
	public void ApplyADSR(int[] adsr) {
		attackTime = adsr[0];
		decayTime = adsr[1];
		sustainTime = adsr[2];
		releaseTime = adsr[3];
	}
	
	//INTERFACE IMPLEMENTATION
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
	//END OF INTERFACE IMPLEMENTATION

	//TODO: Do in seperate thread
	public int Switch(int setting){
		switch (setting){
		case Dictionary.Off:
			switchSetting = Dictionary.Off;
			ASDR(new int[] {Dictionary.Release});
			return releaseTime;
		case Dictionary.On:
			switchSetting = Dictionary.On;
			ASDR(new int[] {Dictionary.Attack, Dictionary.Decay});
			return attackTime + decayTime;
		case Dictionary.Flicker:
			switchSetting = Dictionary.Flicker;
			ASDR(new int[] {Dictionary.Attack, Dictionary.Decay, Dictionary.Sustain, Dictionary.Release});
			return attackTime + decayTime + sustainTime + releaseTime;
		case Dictionary.Flickering:
			switchSetting = Dictionary.Flickering;
			while (setting != Dictionary.Off)
				ASDR(new int[] {Dictionary.Attack, Dictionary.Decay, Dictionary.Sustain, Dictionary.Release});
			return 0;
		}
		return 0;
	}
	
	//PRIVATE FUNCTIONS
	private void ASDR(params int[] adsrOptions) {
		foreach (int option in adsrOptions) {
			switch (option) {
			case Dictionary.Attack:
				scaleIntensityOverTime(Time.time, Time.time + attackTime, MinIntensity, MaxIntensity);
				break;
			case Dictionary.Decay:
				scaleIntensityOverTime(Time.time, Time.time + decayTime, MaxIntensity, SustainIntensity);
				break;
			case Dictionary.Sustain:
				scaleIntensityOverTime(Time.time, Time.time + sustainTime, SustainIntensity, SustainIntensity);
				break;
			case Dictionary.Release:
				scaleIntensityOverTime(Time.time, Time.time + releaseTime, SustainIntensity, MinIntensity);
				break;
			}
		}
	}
	
	private void scaleIntensityOverTime(float startTime, float endTime, float startIntensity, float endIntensity) {
		
	}
}
