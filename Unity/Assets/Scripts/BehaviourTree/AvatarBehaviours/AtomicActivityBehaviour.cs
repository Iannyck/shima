﻿using UnityEngine;
using System.Collections;

public class AtomicActivityBehaviour : AbstractBehaviour {

	public GameObject smartHomeServer;

	public enum AnimationType
	{
		NOTHING
	};

	public AnimationType animation = AnimationType.NOTHING;

	public float duration = 3.0f;

	private float durationLeft;

	public override void Init ()
	{
		if (smartHomeServer != null) {
			SensorsGUI gui = smartHomeServer.GetComponent<SensorsGUI> ();
			gui.SetDebugText (7, BName);
		}
		durationLeft = duration;
	}

	public override State Execute ()
	{
		// TODO play animation
		durationLeft -= Time.deltaTime;
		if (durationLeft <= 0) {
			return State.Suceeded;
		}
		return State.Running;
	}



}
