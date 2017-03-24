using UnityEngine;

#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace trackingRoom.mvc
{
	
	public class BaseApplication<M,V,C> : BaseApplication
		where M : Element
		where V : Element
		where C : Element
	{
		public M model { get { return m_model = base.Assert<M> (m_model); } }

		M m_model;

		public V view { get { return m_view = base.Assert<V> (m_view); } }

		V m_view;

		public C controller { get { return m_controller = Assert<C> (m_controller); } }

		C m_controller;
		
	}

	public class BaseApplication : Element
	{
				
		public void Notify (string p_event_path, params object[] p_data)
		{
			Notify (p_event_path, this, p_data);
		}

		public void Notify (string p_event_path, Object p_target, params object[] p_data)
		{
			Controller[] list = transform.GetComponentsInChildren<Controller> ();
			foreach (Controller c in list)
				c.OnNotification (p_event_path, p_target, p_data);
		}

	}
	
	public class Element : MonoBehaviour
	{
		public T Assert<T> (T p_var) where T : Object
		{
			return p_var == null ? transform.GetComponentInChildren<T> () : p_var;
		}

		public T[] AssertArray<T> (T[] p_var) where T : Object
		{
			return p_var == null ? transform.GetComponentsInChildren<T> () : p_var;
		}

		public void Log (int p_msg)
		{
			Log (p_msg.ToString ());
		}

		public void Log (float p_msg)
		{
			Log (p_msg.ToString ());
		}

		public void Log (double p_msg)
		{
			Log (p_msg.ToString ());
		}

		public void Log (string p_msg)
		{
			Debug.Log (GetType ().Name + "> " + p_msg);
		}
	}
	
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
	
	public class Model<T> : Model where T : BaseApplication
	{
		public T app { get { return (T)GameObject.FindObjectOfType<T>(); } }
	}

	public class Model : Element
	{
		
	}
	
	public class View<T> : View where T : BaseApplication
	{
		public T app { get { return (T)GameObject.FindObjectOfType<T> (); } }
	}

	public class View : Element
	{
	}
}