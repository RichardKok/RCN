using UnityEngine;
using System.Collections;
using System;

namespace trackingRoom.util {
	public class Util : MonoBehaviour
	{			
		public static String GetAction(string eventPath) {
			return eventPath.Split(new char[]{'.'})[0];
		}
		
		public static int Map(float x, float maxStart, float maxEnd) {
			return Map(x, 0, maxStart, 0, maxEnd);
		}
		
		public static int Map(float x, float minStart, float maxStart, float minEnd, float maxEnd) {
			float mappedValue = (x - minStart) *
				((maxEnd - minEnd) / (maxStart - minStart));
			return (int)(mappedValue < minEnd ? minEnd 
				: mappedValue > maxEnd ? maxEnd
				: mappedValue);
		}
	 
		public static Vector2 EstimatedPosition(Vector2 previousPosition, Vector2 currentPosition, float scaling) {
			return new Vector2(currentPosition.x + (currentPosition.x - previousPosition.x) * scaling,
				currentPosition.y + (currentPosition.y - previousPosition.y) * scaling);
		}
		
		public static float Speed(Vector2 prevPos, Vector2 curPos, DateTime prevDateTime , DateTime curDateTime) {
			return Speed(Magnitude(prevPos, curPos), curDateTime.Second - prevDateTime.Second);
		}
		
		public static float Speed(float magnitude, float timeDiff) {
			return (timeDiff > 0) ? magnitude / timeDiff : 0;
		}
		
		public static float Magnitude(Vector2 firstPos, Vector2 secondPos) { 
			return Magnitude(new Vector3(firstPos.x, firstPos.y, 0), new Vector3(secondPos.x, secondPos.y, 0));
		}
		
		public static float Magnitude(Vector3 firstPos, Vector3 secondPos) { 
			return new Vector3(secondPos.x - firstPos.x, 
				secondPos.y - firstPos.y, secondPos.z - firstPos.z).magnitude;
		}

	}
}