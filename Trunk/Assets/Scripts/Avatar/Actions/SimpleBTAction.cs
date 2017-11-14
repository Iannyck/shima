using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBTAction {

	private BTState state;

	public enum BTState
	{
		STOP,
		RUNNING,
		SUCCEEDED,
		FAILED
	};

	public SimpleBTAction ()
	{
		this.state = BTState.STOP;
	}

	public BTState State {
		get {
			return this.state;
		}
	}
		
}
