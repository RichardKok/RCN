using UnityEngine;
using trackingRoom.util;
using trackingRoom.interfaces;
using System.Collections;

public class LampBehaviour : MonoBehaviour, IListener, ISwitch
{
	//PUBLIC INSTANCE VARIABLES
	public LightModel Parent { set { parent = value; } }

	private LightModel parent;

	public float VisualRange {
		set {
			visualRange = value;
			this.GetComponent<SphereCollider> ().radius = value;
		}
	}

	private float visualRange;

	public float Intensity {
		set {
			transform.localScale = new Vector3 (value, value, value);
			intensity = value;
		}
	}

	private float intensity;

	public Switch SwitchSetting {
		get {
			return switchSetting;
		}
		set {
			switchSetting = value;
			StartCoroutine (Scale ());
		}
	}

	private Switch switchSetting;

	public int Role { get; set; }

	private int role;

	public bool IsOrdered { get; set; }

	private bool isOrdered;
	
	//PRIVATE INSTANCE VARIABLES
	private Vector3 originalScale;
	
	private int adsrPhase;
	
	//PUBLIC FUNCTIONS
	public void Awake ()
	{
		originalScale = this.transform.localScale;
	}

	public IEnumerator Scale ()
	{
		while (SwitchSetting.CurrentPhase != null) {
			for (int i = 0; i < SwitchSetting.Steps; i++) {
				int ADSRSteps = SwitchSetting.CurrentPhase.StepsToCompleteAction;
				for (int j = 0; j < ADSRSteps; j++) {
					yield return new WaitForSeconds (parent.app.view.yieldRefreshInterval);
					Intensity = Util.Map (j, 0, ADSRSteps, 
						SwitchSetting.CurrentPhase.StartIntensity, SwitchSetting.CurrentPhase.EndIntensity);
				}
				SwitchSetting.CurrentPhase = SwitchSetting.Name.Equals ("Flicker")
					? parent.NextPhase (SwitchSetting.CurrentPhase) 
					: null;
			}
		}
	}
	
	#region IListener implementation
	
	public void OnTriggerEnter (Collider col_data)
	{
		if (parent.ModeBehaviour != null &&
		    (col_data.gameObject.tag.Equals (Dictionary.User) || col_data.gameObject.tag.Equals (Dictionary.EstimatedUserPosition))) {
			parent.ModeBehaviour.OnUserTriggerChange (this, col_data.gameObject.tag, true);
		}
	}

	public void OnTriggerExit (Collider col_data)
	{
		if (parent.ModeBehaviour != null &&
		    (col_data.gameObject.tag.Equals (Dictionary.User) || col_data.gameObject.tag.Equals (Dictionary.EstimatedUserPosition))) {
			parent.ModeBehaviour.OnUserTriggerChange (this, col_data.gameObject.tag, false);
		}
	}
	
	#endregion
	
	#region ISwitch implementation
	
	public void Switch(string setting) {
		Switch newSwitch;
		switch (setting) {
		case Dictionary.Flicker:
			newSwitch = new Flicker();
			newSwitch.CurrentPhase = parent.Attack;
			SwitchSetting = newSwitch;
			break;
		case Dictionary.On:
			newSwitch = new On();
			newSwitch.CurrentPhase = parent.Attack;
			SwitchSetting = newSwitch;
			break;
		case Dictionary.Off:
			newSwitch = new Off();
			newSwitch.CurrentPhase = parent.Release;
			SwitchSetting = newSwitch;
			break;
		}
	}
	
	#endregion
	
}
