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
		ActionableEntity actionableEntity = GetComponent<ActionableEntity> ();
		if (actionableEntity != null) {
			actionableEntity.subscribe (this);
		}
	}

	protected override void IESensorUpdate ()
	{
		base.IESensorUpdate ();
	}

	protected override void IESensorStop ()
	{
		base.IESensorStop ();
	}

	void ISensorObserver.Notify (object sender, EventArgs e) {
		state = !state;
		SmartHomeServer.InsertBinarySensorData (name, SensorType, state);
	}

}
