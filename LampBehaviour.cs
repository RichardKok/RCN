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
	private Switch switchSetting;
	
	public float Intensity {  
		get { return intensity; }
		set { 
			Debug.Log(value);
			intensity = value; 
			Color final = color * Mathf.LinearToGammaSpace(value);
			renderer.material.SetColor("_EmissionColor", final); 
			DynamicGI.SetEmissive(renderer, final); } }
	private float intensity;
	
	public int Role { get; set; }
	private int role;
	public bool IsOrdered { get; set; }
	private bool isOrdered;
	
	public LightModel Parent { get; set; } 
	private LightModel parent;
	
	//PRIVATE INSTANCE VARIABLES
	private Material material;
	private new Renderer renderer;
	private Color baseColor;

	private ADSRSetting Attack { get { return Parent.attack; } }
	private ADSRSetting  Release { get { return Parent.release; } }
	private float VisualRange {	get { return Parent.VisualRange; } }
	private Vector3 ThisPos { get { return transform.position; } }
	private Vector3 PrevUserPos { get { return Parent.PrevPos; } }
	private Vector3 UserPos { get { return Parent.Pos;} }
	private Vector3 UserGoalPos{ get { return Parent.GoalPos; } }
	private Color color;
	
	//PUBLIC FUNCTIONS
	public void Awake ()
	{
		renderer =  GetComponent<Renderer>();
		material = renderer.material;
		color = material.color;
	}
	
	public void Update() {
		if (InReach(UserPos)) TurnLampOn();
		else if (OutOfReach(UserPos)) TurnLampOff();
	}
	
	public bool InReach(Vector3 position) {
		return ((SwitchSetting == null || SwitchSetting.Name.Equals(Dictionary.Off))
			&& Vector3.Distance(ThisPos, position) <= VisualRange);
	}
	
	public bool OutOfReach(Vector3 position) {
		return ((SwitchSetting != null &&  SwitchSetting.Name.Equals(Dictionary.On)) 
			&& Vector3.Distance(ThisPos, position) > VisualRange);
	}
	
	public void TurnLampOn() {
		Switch onSwitch = new On();
		onSwitch.CurrentPhase = Attack;
		onSwitch.CurrentPhase.StartIntensity = Intensity;
		SwitchSetting = onSwitch;
		Debug.Log("Switching on ");
	}
	
	public void TurnLampOff() {
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
