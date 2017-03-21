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

		private M m_model;

		public V view { get { return m_view = base.Assert<  V> (m_view); } }

		private V m_view;

		public C controller { get { return m_controller = Assert<C> (m_controller); } }

		private C m_controller;
		
	}

	public class BaseApplication : Element
	{
				
		public int levelId {
			get {
				#if UNITY_5_3_OR_NEWER
				return SceneManager.GetActiveScene ().buildIndex;
				#else
				return Application.loadedLevel;
				#endif
			}
		}

		public string levelName {
			get {
				#if UNITY_5_3_OR_NEWER
				return SceneManager.GetActiveScene ().name;
				#else
				return Application.loadedLevelName;
				#endif
			}
		}

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
}