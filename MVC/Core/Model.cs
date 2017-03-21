using UnityEngine;

namespace trackingRoom.mvc {
	public class Model<T> : Model where T : BaseApplication
	{
		public T app { get { return (T)GameObject.FindObjectOfType<T>(); } }
	}

	public class Model : Element
	{
	}
}