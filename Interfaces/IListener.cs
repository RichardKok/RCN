using UnityEngine;

namespace trackingRoom.interfaces {
	public interface IListener 
	{
		void OnTriggerEnter(Collider col_data);
		void OnTriggerExit(Collider col_data);
	}   
}