using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionableEntity : MonoBehaviour {

	private event EventHandler observer;

	private bool action = false;

	public void subscribe(ISensorObserver iSensorObserver) {
//		observer += new EventHandler (iSensorObserver.Notify);
	}

	public void ActOn(){ 
		action = !action;
		if (observer != null)
			observer (this, null);
	}


}
