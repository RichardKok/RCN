using UnityEngine;
using System.Collections.Generic;
using System;
using trackingRoom.mvc;

public class TrackerModel : Model<TrackerApplication>
{
	private GameObject user;
	private List<KeyValuePair<Vector3, DateTime>> positionsMappedToDate;
	private KeyValuePair<Vector3, DateTime>[] lastPositions;
	
	public void Awake() {
		user = GameObject.FindGameObjectWithTag("User");
		positionsMappedToDate = new List<KeyValuePair<Vector3, DateTime>>();
		lastPositions = new KeyValuePair<Vector3, DateTime>[2];
	}
	
	public KeyValuePair<Vector3, DateTime>[] getLastTwoUserPositions() {
		int listCount = positionsMappedToDate.Count;
		if (listCount > 1) { 
			lastPositions[0] = positionsMappedToDate[listCount - 2];
			lastPositions[1] = positionsMappedToDate[listCount - 1];
			return lastPositions;
		} 
		return null;
	}
	
	public void addUserPosition() {
		positionsMappedToDate.Add(new KeyValuePair<Vector3, DateTime>(user.transform.position, DateTime.Now));
	}
}
