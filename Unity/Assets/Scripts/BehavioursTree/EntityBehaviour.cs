using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBehaviour : MonoBehaviour {


	private bool isStarted;

	public bool IsStarted {
		get {
			return this.isStarted;
		}
		set {
			isStarted = value;
		}
	}

	// Use this for initialization
	void Start () {
		isStarted = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isStarted) {
			EBUpdate ();
		}
	}

	public void EBStart() {
		isStarted = true;
	}

	public abstract void EBUpdate();
}
