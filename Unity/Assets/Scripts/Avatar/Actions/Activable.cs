using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activable : MonoBehaviour {

	[SerializeField]
	private EntityBehaviour behavior;
	[SerializeField]
	private bool started = false;

	[SerializeField]
	private IESensor iESensor;

	// Use this for initialization
	void Start () {
		if (started) {
			Activate ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Activate() {
//		Debug.Log ("Activated");
		behavior.EBStart ();
		if (iESensor != null) {
			((ISensorObserver)iESensor).Notify ();
		}
	}
}
