using UnityEngine;
using System.Collections;
using trackingRoom.mvc;
using trackingRoom.interfaces;

public class Timer<T> : View<T>, IUpdatable
	where T : BaseApplication
{
	new public T app { get { return (T)base.app; } }

	public int intervalInSeconds;

	public string eventPath;
	protected object[] data;
	
	private float trigger_time;

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

	private void UpdateTrigger ()
	{
		trigger_time += (float)intervalInSeconds;
	}

}
