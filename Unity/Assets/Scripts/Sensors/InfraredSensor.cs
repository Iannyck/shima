using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfraredSensor : IESensor {

	public float range = 20f;
	public float cooldownTime = 0.3f;
	private float cooldown;

	private bool isDetectionEnable = true;

	protected override void IESensorInit ()
	{
		base.IESensorInit ();
		cooldown = cooldownTime;
	}

	protected override void IESensorUpdate ()
	{
		base.IESensorUpdate ();
		if (isDetectionEnable) {
		} else {
			cooldown -= Time.deltaTime;
			if (cooldown <= 0f) {
				isDetectionEnable = true;
				cooldown = cooldownTime;
			}
		}
	}

	protected override void IESensorStop ()
	{
		base.IESensorStop ();
	}

	void Raycasting()
	{
		RaycastHit hit;
		Physics.Raycast(transform.position, transform.up, out hit, range);
		//		Debug.DrawRay (transform.position, transform.up * range, Color.red);
		if (hit.collider != null) {
			if (hit.collider.tag == "Player") {
				float distance = hit.distance;
				isDetectionEnable = false;
				SmartHomeServer.InsertUltrasoundData (name, distance);
			}
		}

	}
}
