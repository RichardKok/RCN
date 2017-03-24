using UnityEngine;
using trackingRoom.util;
using System.Collections;
using System;

public class LampBehaviour : MonoBehaviour
{
	//PUBLIC INSTANCE VARIABLES
	public Switch SwitchSetting {
		get { return switchSetting; } 
		set { switchSetting = value;
			StartCoroutine (Scale ()); } } 	
	Switch switchSetting;
	
	public float Intensity {  
		get { return intensity; }
		set { 
			Debug.Log(value);
			intensity = value; 
			Color final = color * Mathf.LinearToGammaSpace(value);
			renderer.material.SetColor("_EmissionColor", final); 
			DynamicGI.SetEmissive(renderer, final); } }
	float intensity;
	
	public int Role { get; set; }
	int role;
	public bool IsOrdered { get; set; }
	bool isOrdered;
	
	public LightModel Parent { get; set; } 
	LightModel parent;
	
	//PRIVATE INSTANCE VARIABLES
	Material material;
	new Renderer renderer;
	Color baseColor;

	ADSRSetting Attack { get { return Parent.attack; } }
	ADSRSetting  Release { get { return Parent.release; } }
	float VisualRange {	get { return Parent.VisualRange; } }
	Vector3 ThisPos { get { return transform.position; } }
	Vector3 PrevUserPos { get { return Parent.PrevPos; } }
	Vector3 UserPos { get { return Parent.Pos;} }
	Vector3 UserGoalPos{ get { return Parent.GoalPos; } }
	Color color;
	
	//PUBLIC FUNCTIONS
	public void Awake ()
	{
		renderer =  GetComponent<Renderer>();
		material = renderer.material;
		color = material.color;
	}
	
	public void Update() {
		if (Parent != null) {
			if (InReach(UserPos)) TurnLampOn();
			else if (OutOfReach(UserPos)) TurnLampOff();
		}
	}
	
	bool InReach(Vector3 position) {
		return ((SwitchSetting == null || SwitchSetting.Name.Equals(Dictionary.Off))
			&& Vector3.Distance(ThisPos, position) <= VisualRange);
	}
	
	bool OutOfReach(Vector3 position) {
		return ((SwitchSetting != null &&  SwitchSetting.Name.Equals(Dictionary.On)) 
			&& Vector3.Distance(ThisPos, position) > VisualRange);
	}
	
	void TurnLampOn() {
		Switch onSwitch = new On();
		onSwitch.CurrentPhase = Attack;
		onSwitch.CurrentPhase.StartIntensity = Intensity;
		SwitchSetting = onSwitch;
		Debug.Log("Switching on ");
	}
	
	void TurnLampOff() {
		Switch onSwitch = new Off();
		onSwitch.CurrentPhase = Release;
		onSwitch.CurrentPhase.StartIntensity = Intensity;
		SwitchSetting = onSwitch;
		Debug.Log("Switching off ");
	}
	
	public IEnumerator Scale ()  
	{
		while (Intensity <= SwitchSetting.CurrentPhase.StartIntensity) {
			for (int i = 0; i < SwitchSetting.Steps; i++) {
				int ADSRSteps = SwitchSetting.CurrentPhase.StepsToCompleteAction;
				for (int j = 0; j < ADSRSteps; j++) {
					yield return new WaitForSeconds (0.01f);
					try {
						Intensity = Util.Map ((float)j, 0.0f, (float)ADSRSteps, 
						SwitchSetting.CurrentPhase.StartIntensity, SwitchSetting.CurrentPhase.EndIntensity);
					} catch (Exception) { }
				}

			}
		}
	}	
}
