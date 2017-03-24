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

		public static float Speed (Vector2 prevPos, Vector2 curPos, DateTime prevDateTime, DateTime curDateTime)
		{
			return Speed (Vector3.Distance(prevPos, curPos), curDateTime.Second - prevDateTime.Second);
		}

		public static float Speed (float distance, float timeDiff)
		{
			return (timeDiff > 0) ? distance / timeDiff : 0;
		}
	}
}