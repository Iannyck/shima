using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactSensor : IESensor, ISensorObserver {

	public int state = 1;
	public float delay = 1f;
	public string SensorType = "ContactSensor";
	private float currentTime;

	protected override void IESensorInit ()
	{
		base.IESensorInit ();
	}

	protected override void IESensorUpdate ()
	{
		base.IESensorUpdate ();
		if(currentTime > 0)
			currentTime -= Time.deltaTime;
	}

	protected override void IESensorStop ()
	{
		base.IESensorStop ();
	}

	void ISensorObserver.Notify () {
		if (currentTime <= 0) {
			//state = !state;
			if (state == 1)
				state = 0;
			else
				state = 1;
			currentTime = delay;
			SmartHomeServer.InsertBinarySensorData (name, SensorType, state);
		}
	}

}
