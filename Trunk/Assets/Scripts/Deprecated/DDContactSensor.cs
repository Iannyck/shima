using UnityEngine;
using System.Collections;

public class DDContactSensor : IESensor {

	public int state = 1;
	public float delay = 1f;
	public string SensorType = "ContactSensor";
	private float currentTime;

	public void Switch() {
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

	protected override void IESensorInit ()
	{
		base.IESensorInit ();
		currentTime = delay;
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
}
