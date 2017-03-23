using UnityEngine;
using trackingRoom.interfaces;

public class GoalSetting : MonoBehaviour, IMode
{	
	//PRIVATE INSTANCE VARIABLES
	private LightModel parent;
	private LampBehaviour master;
	private LampBehaviour seducer;
	
	private bool splashSequenceHappening;
	
	//CONSTRUCTOR
	public GoalSetting (LightModel parent)
	{
		this.parent = parent;
		SetRoles (true);
	}

	//PUBLIC FUNCTIONS
	public void OnUserTriggerChange (LampBehaviour originScript, string userTag, bool inRange)
	{
		if (originScript.Role == Dictionary.Slave && inRange) 
			SwitchRoles ();
	}

	public void OnUserDetection ()
	{
	}
	
	public void Splash()
	{	
		if (seducer != null && !splashSequenceHappening) {
			splashSequenceHappening = true;
			parent.InitSplashFromOrigin = seducer;
		}
		splashSequenceHappening = false;
	}
	
	//PRIVATE FUNCTIONS
	private void SwitchRoles ()
	{
		SetRoles (false);
		seducer = master;
		seducer.Role = Dictionary.Seducer;
		master = parent.GetRandomLamp (Dictionary.Slave);
		master.Role = Dictionary.Master;
	}

	private void SetRoles (bool hierarchy)
	{
		LampBehaviour[] lampScripts = parent.LampScripts;
		foreach (LampBehaviour lamp in lampScripts)
			lamp.Role = Dictionary.Slave;
			if (hierarchy) {
				master = parent.GetRandomLamp (Dictionary.Slave);
				master.Role = Dictionary.Master;
				seducer = parent.GetRandomLamp (Dictionary.Slave);
				seducer.Role = Dictionary.Seducer;
			}
	}
}
