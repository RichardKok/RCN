using UnityEngine;
using System.Collections;

public class LampBehaviour : MonoBehaviour
{
	public LightModel Parent { set { parent = value; } }
	private LightModel parent;
	
	public float VisualRange { set { visualRange = value; this.GetComponent<SphereCollider>().radius = value; } }
	private float visualRange;
	
	public int SwitchSetting { set { switchSetting = value; } }
	private int switchSetting;
	
	public int Role { set { role = value; }  }
	private int role;
	
	private float attack;
	private float sustain;
	private float decay;
	private float release;
	
	public void ApplyASDR(int[] asdr){
		attack = asdr[0];
		sustain = asdr[1];
		decay = asdr[2];
		release = asdr[3];
	}

	public void OnTriggerEnter(Collider col_data) {
		if (parent.ModeBehaviour != null && 
			(col_data.gameObject.tag.Equals(Dictionary.User) || col_data.gameObject.tag.Equals(Dictionary.EstimatedUserPosition))) {
			parent.ModeBehaviour.UserInLampsVisualRange(role, true);
		}
	}
	
	public void OnTriggerExit(Collider col_data) {
		if (parent.ModeBehaviour != null && 
			(col_data.gameObject.tag.Equals(Dictionary.User) || col_data.gameObject.tag.Equals(Dictionary.EstimatedUserPosition))) {
			parent.ModeBehaviour.UserInLampsVisualRange(role, false);
		}
	}
}
