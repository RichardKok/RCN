using UnityEngine;
using trackingRoom.mvc;
using trackingRoom.util;
using System.Collections.Generic;
using System;

public class MasterModel : Model<MasterApplication>
{
	public float EstimatedPositionScaling { set { estimatedPositionScaling = value; } }

	private float estimatedPositionScaling = 1;

	private GameObject EstimatedUserPosAsObj {
		get { 
			return estimatedUserPosAsObj = FindTaggedObject (estimatedUserPosAsObj, Dictionary.EstimatedUserPosition); 
		}
	}

	private GameObject estimatedUserPosAsObj;

	public void extrapolateVectors (KeyValuePair<Vector2, DateTime>[] prevCurPositions)
	{
		EstimatedUserPosAsObj.transform.position = Util.EstimatedPosition (prevCurPositions [0].Key, prevCurPositions [1].Key, estimatedPositionScaling);
	}
		
}
