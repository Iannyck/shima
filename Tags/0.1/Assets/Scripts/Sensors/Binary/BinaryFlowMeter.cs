using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryFlowMeter : IESensor, ISensorObserver {

	public string SensorType = "BinaryFlowMeter";
	public bool state = true;

	protected override void IESensorInit ()
	{
		base.IESensorInit ();
	}

	protected override void IESensorUpdate ()
	{
		base.IESensorUpdate ();
	}

	protected override void IESensorStop ()
	{
		base.IESensorStop ();
	}

	void ISensorObserver.Notify () {
		state = !state;
		SmartHomeServer.InsertBinarySensorData (name, SensorType, state);
	}

}
