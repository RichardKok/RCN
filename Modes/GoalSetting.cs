using UnityEngine;
using trackingRoom.interfaces;

public class GoalSetting : MonoBehaviour, IMode
{	
	//PRIVATE INSTANCE VARIABLES
	LightModel parent;
	LampBehaviour master;
	LampBehaviour seducer;
	
	bool splashSequenceHappening;
	
	//CONSTRUCTOR
	public GoalSetting (LightModel parent)
	{
		this.parent = parent;
		SetRoles (true);
	}

	//PUBLIC FUNCTIONS
	#region IMode implementation
	
	public void OnUserDetectionChange (LampBehaviour originScript, string userTag, bool inRange)
	{
		if (originScript.Role == Dictionary.Slave && inRange) 
			SwitchRoles ();
	}

	#endregion
	
	public void Splash()
	{	
		if (seducer != null && !splashSequenceHappening) {
			splashSequenceHappening = true;
			parent.InitSplashFromOrigin = seducer;
		}
		splashSequenceHappening = false;
	}
	
	//PRIVATE FUNCTIONS
	void SwitchRoles ()
	{
		SetRoles (false);
		seducer = master;
		seducer.Role = Dictionary.Seducer;
		master = parent.GetRandomLamp (Dictionary.Slave);
		master.Role = Dictionary.Master;
	}

	void SetRoles (bool hierarchy)
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
