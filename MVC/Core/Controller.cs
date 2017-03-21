using UnityEngine;
using System.Collections;

namespace trackingRoom.mvc
{
	public class Controller<T> : Controller where T : BaseApplication
	{
		public T app { get { return (T)GameObject.FindObjectOfType<T> (); } }
	}

	public class Controller : Element
	{
		virtual public void OnNotification (string p_event_path, Object target, params object[] p_data)
		{
		}
	}
}
