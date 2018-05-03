using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleActivateDevice : SimpleBTAction {

	private GameObject device;

	public SimpleActivateDevice (int forgetErrorRate, GameObject device) : base (forgetErrorRate)
	{
		this.device = device;
	}

	public GameObject Device {
		get {
			return this.device;
		}
		set {
			device = value;
		}
	}

}
