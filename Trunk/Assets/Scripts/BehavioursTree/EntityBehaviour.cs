using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBehaviour : MonoBehaviour {

	[SerializeField]
	private string ebname = "noname";

	[SerializeField]
	private bool autoStart = false;

	public enum BTState
	{
		STOP,
		RUNNING,
		SUCCEEDED,
		FAILED
	};

	[SerializeField]
	private BTState state = BTState.STOP;

	public BTState State {
		get {
			return this.state;
		}
		set {
			state = value;
		}
	}

	public string Ebname {
		get {
			return this.ebname;
		}
	}

	public bool AutoStart {
		get {
			return this.autoStart;
		}
	}

	// Use this for initialization
	void Start () {
		if (autoStart)
			EBStart ();
	}
	
	// Update is called once per frame
	void Update () {
		if (state == BTState.RUNNING) {
			state = EBUpdate ();
		}
	}

	public virtual void EBStart() {
//		Debug.Log (ebname+" Started");
		state = BTState.RUNNING;
	}

	public abstract BTState EBUpdate();
}
