using UnityEngine;
using System.Collections;

public class Switch
{

	public string Name { get { return name; }  set { name = value; } }

	private string name;

	public int Steps { get { return steps; }  set { steps = value; } }

	private int steps;

	public ADSRSetting CurrentPhase { get { return currentPhase; }  set { currentPhase = value; } }

	private ADSRSetting currentPhase;

	public Switch (string name, int steps)
	{
		this.Name = name;
		this.Steps = steps;
	}
}

class On : Switch
{
	public On () : base ("On", 1)
	{
	}
}

class Off : Switch
{
	public Off () : base ("Off", 1)
	{
	}
}

class Flicker : Switch
{
	public Flicker () : base ("Flicker", 4)
	{
	}
}

public class ADSRSetting {

	public string Name { get; set; }

	private string name;

	public int StartIntensity { get; set; }

	private int startIntensity;

	public int EndIntensity { get; set; }

	private int endIntensity;

	public int StepsToCompleteAction { get; set; }

	private int stepsToCompleteAction;

	public ADSRSetting(string name, int startIntensity, int endIntensity, int stepsToCompleteAction) {
		this.Name = name;
		this.StartIntensity = startIntensity;
		this.EndIntensity = endIntensity;
		this.StepsToCompleteAction = stepsToCompleteAction;
	}
}
