﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPutInto : EntityBehaviour {

	[SerializeField]
	private GameObject objectToTransfer;

	[SerializeField]
	private GameObject container;

	public override void EBStart() {
		Debug.Log ("Start " + Ebname + " sequence");
		State = BTState.RUNNING;
	}

	public override BTState EBUpdate ()
	{
		return BTState.RUNNING;
	}
}
