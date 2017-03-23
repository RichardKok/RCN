using UnityEngine;
using System.Collections;

public class Dictionary : MonoBehaviour
{
	//controller eventPaths
	public const string TimerSeduce = "Timer.seduce";
	public const string TimerObserve = "Timer.observe";
	public const string TrackerUpdate = "TrackerController.updatePosition";
	public const string UserUpdate = "UserController.updatePosition";
	public const string MasterActivate = "MasterView.activateLights";
	public const string MasterApplyLights = "MasterView.applyLights";
	public const string MasterApplyTracker = "MasterView.applyTracker";
	
	//scene modes
	//public const string Disabled = "Disabled";
	public const string Test = "Test";
	//public const string Snooker = "Snooker";
	public const string Painter = "Painter";
	public const string GoalMode = "GoalOrientation";
	
	//Flicker setttings
	public const string Flicker = "Flicker";
	public const string On = "On";
	public const string Off = "Off";
	
	//Individual lamp roles
	public const int Slave = 0;
	public const int Seducer = 1;
	public const int Master = 2;
	
	//tags
	public const string User = "User";
	public const string EstimatedUserPosition = "EstimatedUserPosition";
}
