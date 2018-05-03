using UnityEngine;
using System.Collections;

public class DDContactSensor : IESensor {

	public bool state = true;
	public float delay = 1f;
	public string SensorType = "ContactSensor";
	private float currentTime;

	public void Switch() {
		if (currentTime <= 0) {
			state = !state;
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
