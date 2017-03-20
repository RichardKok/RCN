using UnityEngine;
using System.Collections.Generic;
using System;
using trackingRoom.mvc;

public class TrackerModel : Model<TrackerApplication>
{
	private GameObject user;
	private List<KeyValuePair<Vector2, DateTime>> positionsMappedToDate;
	private KeyValuePair<Vector2, DateTime>[] lastPositions;
	
	public void Awake() {
		user = GameObject.FindGameObjectWithTag("User");
		positionsMappedToDate = new List<KeyValuePair<Vector2, DateTime>>();
		lastPositions = new KeyValuePair<Vector2, DateTime>[2];
	}
	
	public KeyValuePair<Vector2, DateTime>[] getLastTwoUserPositions() {
		int listCount = positionsMappedToDate.Count;
		if (listCount > 1) { 
			lastPositions[0] = positionsMappedToDate[listCount - 2];
			lastPositions[1] = positionsMappedToDate[listCount - 1];
		} 
		return lastPositions;
		
	}
	
	public void addUserPosition() {
		addUserPosition(new Vector2(user.transform.position.x, user.transform.position.y));
	}
	
	public void addUserPosition(Vector2 position) {
		positionsMappedToDate.Add(new KeyValuePair<Vector2, DateTime>(position, DateTime.Now));
	}
}
