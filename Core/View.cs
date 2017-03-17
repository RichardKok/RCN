using UnityEngine;
using System.Collections;

namespace trackingRoom.mvc  {
	public class View<T> : View where T : BaseApplication
	{
		public T app { get { return (T)GameObject.FindObjectOfType<T>();} }
	}

	public class View : Element
	{
	}
}
