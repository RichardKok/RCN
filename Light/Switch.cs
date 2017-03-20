using UnityEngine;
using System.Collections;

public class Switch {
	
	protected string name;
	protected int steps;
	protected int currentPhase;

	public Switch(string name, int steps, int currentPhase) {
		this.name = name;
		this.steps = steps;
		this.currentPhase = currentPhase;
	}
	
	public int GetPhase() {
		return currentPhase;
	}

	public int GetSteps() {
		return steps;	
	}

	public int GetCurrentPhase() {
		return currentPhase;
	}
	
	virtual public void SetNextPhase() {
		currentPhase = Dictionary.Finished;
	}
	
}
public class On : Switch{
	
	public On() : base(Dictionary.On, 1, Dictionary.Attack){
		
	}
}

public class Off : Switch{

	public Off() : base(Dictionary.Off, 1, Dictionary.Release){
		
	}
}

public class Flicker : Switch{

	public Flicker() : base(Dictionary.Flicker, 4, Dictionary.Attack){
		
	}

	override public void SetNextPhase() {
		switch (currentPhase) {
		case Dictionary.Attack:
			currentPhase = Dictionary.Decay;
			break;
		case Dictionary.Decay:
			currentPhase = Dictionary.Sustain;
			break;
		case Dictionary.Sustain:
			currentPhase = Dictionary.Release;
			break;
		case Dictionary.Release:
			currentPhase = Dictionary.Finished;
			break;
		}
	}
}

