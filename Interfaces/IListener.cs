using UnityEngine;
using System.Collections;

public interface IListener 
{
	void OnTriggerEnter(Collider col_data);
	void OnTriggerExit(Collider col_data);
}
