using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBTAction {

	protected BTState state;

	public enum BTState
	{
		SUCCEEDED,
		FAILED,
		UNKNOWN,
		RUNNING
	};

	private int forgetErrorRate;

	protected ArrayList subsActList;

	protected ArrayList requireActionDoneList;

	public SimpleBTAction (int forgetErrorRate)
	{
		this.forgetErrorRate = forgetErrorRate;
		this.state = BTState.UNKNOWN;
		requireActionDoneList = new ArrayList ();
		subsActList = new ArrayList ();
//		state = BTState.STOP;
	}
	

//	public BTState State {
//		get {
//			return this.state;
//		}
//	}

	public int ForgetErrorRate {
		get {
			return this.forgetErrorRate;
		}
	}

	public ArrayList SubsActList {
		get {
			return this.subsActList;
		}
	}

	public BTState State {
		get {
			return this.state;
		}
	}

	public ArrayList RequireActionDoneList {
		get {
			return this.requireActionDoneList;
		}
	}

	public void Start() {
		state = BTState.RUNNING;
	}

	public void Succeeded() {
		state = BTState.SUCCEEDED;
	}

	public void Failed() {
		state = BTState.FAILED;
	}
}
