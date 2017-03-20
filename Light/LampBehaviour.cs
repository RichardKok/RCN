using UnityEngine;
using trackingRoom.util;
using trackingRoom.interfaces;
using System.Collections;

public class LampBehaviour : MonoBehaviour, IListener
{
	//PUBLIC INSTANCE VARIABLES
	public LightModel Parent { set { parent = value;} }
	private LightModel parent;

	public float VisualRange { set { visualRange = value; this.GetComponent<SphereCollider>().radius = value; } }
	private float visualRange;
		
	public float Intensity { set { transform.localScale = new Vector3(value, value, value); intensity = value; } }
	private float intensity;
	
	public int Role { get; set; }
	private int role;
	
	public bool IsOrdered { get; set; }
	private bool isOrdered;
	
	public Switch SwitchSetting {
		get {
			return switchSetting;
		}
		set {
			switchSetting = value;
			StartCoroutine(Scale());
		}
	}
	private Switch switchSetting;
	
	//PRIVATE INSTANCE VARIABLES	
	private Vector3 originalScale;
	
	private int adsrPhase;
	
	//PUBLIC FUNCTIONS
	public void Awake() {
		originalScale = this.transform.localScale;
	}

	public IEnumerator Scale() {
		if (!SwitchSetting.GetCurrentPhase().Equals(Dictionary.Finished)) {
			for (int i = 0; i < SwitchSetting.GetSteps(); i++) {
				Intensity = Util.Map(i, 0, SwitchSetting.GetSteps(), 
					parent.StartIntensity(SwitchSetting.GetCurrentPhase()), parent.EndIntensity(SwitchSetting.GetCurrentPhase()));
				yield return new WaitForSeconds(1.0f);
			}
			SwitchSetting.SetNextPhase();
		}
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
	
	
}
