using UnityEngine;
using trackingRoom.util;
using System.Collections;

public class LampBehaviour : MonoBehaviour
{
	//PUBLIC INSTANCE VARIABLES
	public Switch SwitchSetting { get { return switchSetting; } 
		set { switchSetting = value; StartCoroutine (Scale ()); } }
	
	private Switch switchSetting;
	public float Intensity { 
		set { 
			Debug.Log("Switchsetting " + SwitchSetting + " value" + value);
			DynamicGI.SetEmissive(renderer, color * value);
		} 
	}
	
	public int Role { get; set; }
	private int role;
	public bool IsOrdered { get; set; }
	private bool isOrdered;
	
	public LightModel Parent { get; set; } 
	private LightModel parent;
	
	//PRIVATE INSTANCE VARIABLES
	private Material material;
	private Renderer renderer;
	private Color baseColor;
	private GameObject user;
	private GameObject goal;
	
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
		if (Parent != null) CheckUserProximity();		
	}
	
	public void CheckUserProximity ()
	{
		if ((SwitchSetting == null || SwitchSetting.Name.Equals(Dictionary.Off)) && Vector3.Distance(ThisPos,UserPos) <= VisualRange) {
			Switch onSwitch = new On();
			onSwitch.CurrentPhase = Parent.attack;
			SwitchSetting = onSwitch;
		} else if (SwitchSetting != null && SwitchSetting.Name.Equals(Dictionary.On) && Vector3.Distance(ThisPos,UserPos) > VisualRange){
			Switch onSwitch = new Off();
			onSwitch.CurrentPhase = Parent.release;
			SwitchSetting = onSwitch;
		}
	}
	
	//catch Nullpointer exception
	public IEnumerator Scale () 
	{
		while (SwitchSetting.CurrentPhase != null) {
			for (int i = 0; i < SwitchSetting.Steps; i++) {
				int ADSRSteps = SwitchSetting.CurrentPhase.StepsToCompleteAction;
				for (int j = 0; j < ADSRSteps; j++) {
					yield return new WaitForSeconds (0.01f);
					float intensity = Util.Map ((float)j, 0.0f, (float)ADSRSteps, 
						SwitchSetting.CurrentPhase.StartIntensity, SwitchSetting.CurrentPhase.EndIntensity);
					this.Intensity = intensity;
				}
				SwitchSetting.CurrentPhase = SwitchSetting.Name.Equals ("Flicker")
					? parent.NextPhase (SwitchSetting.CurrentPhase) 
					: null;
			}
		}
	}
		
}
