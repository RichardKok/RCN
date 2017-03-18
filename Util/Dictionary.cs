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
	public const string GoalMode = "GoalOrientation";
	
	//light modes
	public const int Off = 0;
	public const int On = 1;
	public const int Flicker = 2;
	public const int Flickering = 3;
	
	//Individual lamp roles
	public const int Slave = 0;
	public const int Seducer = 1;
	public const int Master = 2;
	
	//tags
	public const string User = "User";
	public const string EstimatedUserPosition = "EstimatedUserPosition";
}
