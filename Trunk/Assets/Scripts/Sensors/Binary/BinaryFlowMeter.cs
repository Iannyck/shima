using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryFlowMeter : IESensor, ISensorObserver {

	public string SensorType = "BinaryFlowMeter";
	public int state = 1;

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
		//state = !state;
		if (state == 1)
			state = 0;
		else
			state = 1;
		SmartHomeServer.InsertBinarySensorData (name, SensorType, state);
	}

}
