using UnityEngine;
using System.Collections;
using trackingRoom.mvc;
using trackingRoom.interfaces;

public class Timer<T> : View<T>
	where T : BaseApplication
{
	new public T app { get { return (T)base.app; } }

	public float intervalInSeconds = 1;

	public string eventPath;
	protected object[] data;
	
	float trigger_time;

	public void Awake ()
	{
		trigger_time = Time.time;		
		UpdateTrigger ();
	}

	public void Update ()
	{
		if (Time.time > trigger_time) {
			UpdateTrigger ();
			app.Notify (eventPath, data);
		}
	}

	void UpdateTrigger ()
	{
		trigger_time += intervalInSeconds;
	}

}
