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
	
	private int attack;
	private int decay;
	private int sustain;
	private int release;
	
	public void ApplyADSR(int[] adsr) {
		attack = adsr[0];
		decay = adsr[1];
		sustain = adsr[2];
		release = adsr[3];
	}
	
	public int Switch(int setting){
		switch (setting){
		case Dictionary.Off:
			return decay + sustain + release;
		case Dictionary.On:
			return attack;
		case Dictionary.Flicker:
			return attack + decay + sustain + release;
		case Dictionary.Flickering:
			return attack + decay + sustain + release;
		}
		return 0;
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
