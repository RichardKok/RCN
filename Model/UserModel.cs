using UnityEngine;
using trackingRoom.mvc;

public class UserModel : Model<UserApplication>
{

	GameObject nextUser;
	GameObject user;  
	
	public Vector3 PrevPos { get; set; } //set in controller
	public Vector3 Pos { get; set; }  //set in controller
	public Vector3 GoalPos { get; set; }
	float Push { get { return (float)app.view.nextPosPush / 100.0f; } }
	float Pull { get { return (float)app.view.nextPosPull / 100.0f; } }
	
	Vector3 prevPos;
	Vector3 pos;
	Vector3 prevVel;
	Vector3 vel;
	Vector3 nextVel;
	Vector3 acc;
	
	public void Awake() {
		nextUser = GameObject.FindGameObjectWithTag (Dictionary.EstimatedUserPosition); 
		user = GameObject.FindGameObjectWithTag (Dictionary.User);
	}
	
	public void Update() {
		if (Pos != PrevPos) {
			vel = SubtractedVector(Pos, PrevPos);
			GoalPos = AddedVector(Pos, vel);
			acc = SubtractedVector(vel, prevVel);
			nextVel = AddedVector(nextVel, acc);
			prevVel = vel;
			nextVel = AddedVector(nextVel, acc);
			nextVel.Scale(new Vector3(Push, Push, Push));
			nextUser.transform.position = AddedVector(nextUser.transform.position, nextVel);
			GoalPos = nextUser.transform.position;
		}
		Vector3 returnVel = SubtractedVector(user.transform.position, nextUser.transform.position);		
		returnVel.Scale(new Vector3(Pull, Pull, Pull));
		nextUser.transform.position = AddedVector(nextUser.transform.position, returnVel);
	}
	
	 Vector3 SubtractedVector(Vector3 vec1, Vector3 vec2) {
		return new Vector3(vec1.x - vec2.x, vec1.y - vec2.y, vec1.z - vec2.z);
	}
	
     Vector3 AddedVector(Vector3 vec1, Vector3 vec2) {
		return new Vector3(vec1.x + vec2.x, vec1.y + vec2.y, vec1.z + vec2.z);
	}
}