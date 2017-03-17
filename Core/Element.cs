using UnityEngine;
using System.Collections;

namespace trackingRoom.mvc {

	public class Element : MonoBehaviour
	{
		public T Assert<T>(T p_var) where T : Object {
			return p_var == null ? transform.GetComponentInChildren<T>() : p_var;
		}
		
		public T[] AssertArray<T>(T[] p_var) where T : Object {
			return p_var == null ? transform.GetComponentsInChildren<T>() : p_var;
		}
		
		public GameObject FindTaggedObject(GameObject p_var, string tag) {
			return p_var == null ? GameObject.FindGameObjectWithTag(tag) : p_var;
		}
		
		public void Log(int p_msg) {
			Log(p_msg.ToString());
		}
		
		public void Log(float p_msg) {
			Log(p_msg.ToString());
		}
		
		public void Log(double p_msg) {
			Log(p_msg.ToString());
		}
		
		public void Log(string p_msg) {
			Debug.Log(GetType().Name + "> " + p_msg);
		}
	}
}