using UnityEngine;
using System.Collections;
using System;

namespace trackingRoom.util
{
	public class Util : MonoBehaviour
	{
		public static String GetAction (string eventPath)
		{
			return eventPath.Split (new char[]{ '.' }) [0];
		}
		
		public static float Map (int x, int maxStart, float maxEnd)
		{
			return Map ((float)x, 0.0f, (float)maxStart, 0, maxEnd);
		}

		public static float Map (float x, float maxStart, float maxEnd)
		{
			return Map (x, 0.0f, maxStart, 0.0f, maxEnd);
		}

		public static float Map (int x, int minStart, int maxStart, float minEnd, float maxEnd)
		{
			return Map ((float)x, (float)minStart, (float)maxStart, minEnd, maxEnd);
		}

		public static float Map (float x, float minStart, float maxStart, float minEnd, float maxEnd)
		{
			return minEnd + (maxEnd - minEnd) * ((x - minStart) / (maxStart - minStart)); 
		}

		public static Vector2 EstimatedPosition (Vector2 previousPosition, Vector2 currentPosition, float scaling)
		{
			return new Vector2 (currentPosition.x + (currentPosition.x - previousPosition.x) * scaling,
				currentPosition.y + (currentPosition.y - previousPosition.y) * scaling);
		}

		public static float Speed (Vector2 prevPos, Vector2 curPos, DateTime prevDateTime, DateTime curDateTime)
		{
			return Speed (Magnitude (prevPos, curPos), curDateTime.Second - prevDateTime.Second);
		}

		public static float Speed (float magnitude, float timeDiff)
		{
			return (timeDiff > 0) ? magnitude / timeDiff : 0;
		}

		public static float Magnitude (Vector2 firstPos, Vector2 secondPos)
		{ 
			return Magnitude (new Vector3 (firstPos.x, firstPos.y, 0), new Vector3 (secondPos.x, secondPos.y, 0));
		}

		public static float Magnitude (Vector3 firstPos, Vector3 secondPos)
		{ 
			return new Vector3 (secondPos.x - firstPos.x, 
				secondPos.y - firstPos.y, secondPos.z - firstPos.z).magnitude;
		}

	}
}