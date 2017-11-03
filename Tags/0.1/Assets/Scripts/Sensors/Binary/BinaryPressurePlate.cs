using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryPressurePlate : IESensor {

	public string SensorType = "BinaryPressurePlate";
	private float totalMassPressure;

	public float minimumValue = 0f;

	protected override void IESensorInit ()
	{
		base.IESensorInit ();
		totalMassPressure = 0f;
	}

	protected override void IESensorUpdate ()
	{
		base.IESensorUpdate ();
	}

	protected override void IESensorStop ()
	{
		base.IESensorStop ();
	}

	void OnCollisionEnter(Collision other)
	{
		if (isStarted) {
			if (other.gameObject.tag != "Floor") {
				Rigidbody rgbd = other.gameObject.GetComponent<Rigidbody> ();
				totalMassPressure += rgbd.mass;
				if (totalMassPressure >= minimumValue) {
					SmartHomeServer.InsertBinarySensorData (name, SensorType, true);
				}
			}
		}
	}

	void OnCollisionExit(Collision other)
	{
		if (isStarted) {
			Rigidbody rgbd = other.gameObject.GetComponent<Rigidbody> ();
			totalMassPressure -= rgbd.mass;
			if (totalMassPressure == 0f) {
				SmartHomeServer.InsertBinarySensorData (name, SensorType, false);
			}
		}
	}

}
