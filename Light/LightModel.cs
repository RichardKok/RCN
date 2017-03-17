using UnityEngine;
using trackingRoom.mvc;

public class LightModel : Model<LightApplication>
{
	public LampBehaviour[] LampScripts { get { return (lampScripts == null) ? lampScripts = app.LampScripts : lampScripts; } } 
	private LampBehaviour[] lampScripts;
	
	public IMode ModeBehaviour { get; set; }
	private IMode modeBehaviour;
	
	public void SetLampsParent() {
		foreach (LampBehaviour lamp in LampScripts) lamp.Parent = this;	
	}
	
	public void ApplySettingsToLamps(int[] asdr, float visualRange){
		foreach (LampBehaviour lamp in LampScripts) {
			lamp.VisualRange = visualRange;
			lamp.ApplyASDR(asdr);
		}
	}
}
