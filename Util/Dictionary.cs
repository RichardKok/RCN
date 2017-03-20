using UnityEngine;
using System.Collections;

public class Dictionary : MonoBehaviour
{	
	//controller eventPaths
	public const string TimerSeduce = "Timer.seduce";
	public const string TimerObserve = "Timer.observe";
	public const string TrackerUpdate = "TrackerController.updatePosition";
	public const string MasterActivate= "MasterView.activateLights";
	public const string MasterApplyLights = "MasterView.applyLights";
	public const string MasterApplyTracker = "MasterView.applyTracker";
	
	//scene modes
	public const string Disabled = "Disabled";
	public const string Default = "Default";
	public const string Snooker = "Snooker";
	public const string Paint = "Paint";
	public const string GoalMode = "GoalOrientation";
	
	//Switch Settings 
	public const string On = "On";
	public const string Off = "Off";
	public const string Flicker = "Flicker";
	
	//ASDR options
	public const int Attack = 0;
	public const int Decay = 1;
	public const int Sustain = 2;
	public const int Release = 3;
	public const int Finished = 4;
	
	//Individual lamp roles
	public const int Slave = 0;
	public const int Seducer = 1;
	public const int Master = 2;
	
	//tags
	public const string User = "User";
	public const string EstimatedUserPosition = "EstimatedUserPosition";
}
