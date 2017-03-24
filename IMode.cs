using UnityEngine;
using System.Collections;

namespace trackingRoom.interfaces {
	public interface IMode 
	{
		void OnUserDetectionChange (LampBehaviour originScript, string userTag, bool inRange);
	
	}
}
