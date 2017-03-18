using UnityEngine;
using trackingRoom.mvc;
using trackingRoom.interfaces;

public class LightModel : Model<LightApplication>
{
	public LampBehaviour[] LampScripts { get { return (lampScripts == null) ? lampScripts = app.LampScripts : lampScripts; } } 
	private LampBehaviour[] lampScripts;
	
	public IMode ModeBehaviour { get; set; }
	private IMode modeBehaviour;
	
	public void Start()
	{ 
		foreach (LampBehaviour lamp in LampScripts) lamp.Parent = this;	
	}

	public void ApplySettingsToLamps(params object[] p_data){
		int[] adsr = new int[4]; 
		int i;
		for (i = 0; i < adsr.Length; i++) adsr[i] = (int)p_data[i];
		foreach (LampBehaviour lamp in LampScripts) {
			lamp.ApplyADSR(adsr);
			lamp.MinIntensity = (float)p_data[i++];
			lamp.SustainIntensity = (float)p_data[i++];
			lamp.MaxIntensity = (float)p_data[i++];
			lamp.VisualRange = (float) p_data[i];
		}
	}
}
