using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class OperableEntity : MonoBehaviour {

	public enum State : byte {Off, On, Opening, Closing}

	private event EventHandler observer;

	private bool OnOffButtonPressed = false;
	protected State deviceState;

	private bool isInit = false;

	public bool IsInit {
		get {
			return this.isInit;
		}
	}

	public State DeviceState {
		get {
			return this.deviceState;
		}
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (isInit) {
			if (OnOffButtonPressed) {
				//				Debug.Log ("ITEM "+name+" : "+deviceState);
				if (DeviceState == State.Off) {
					deviceState = OnOn ();
				} else if (DeviceState == State.On) {
					deviceState = OnClose ();
				} else if (DeviceState == State.Opening) {
					deviceState = OnOpening ();
					if(deviceState == State.On)
						OnOffButtonPressed = false;
				} else if (DeviceState == State.Closing) {
					deviceState = OnClosing ();
					if(deviceState == State.Off)
						OnOffButtonPressed = false;
				}
			}
		} else
			isInit = EntityInit ();
	}

	public void subscribe(ISensorObserver iSensorObserver) {
//		observer += new EventHandler (iSensorObserver.Notify);
	}

	protected abstract bool EntityInit ();

	public void ActOn(){ 
		OnOffButtonPressed = !OnOffButtonPressed;
		if (observer != null)
			observer (this, null);
	}

	/// <summary>
	/// Raises the on event.
	/// </summary>
	protected abstract State OnOn ();

	/// <summary>
	/// Raises the close event.
	/// </summary>
	protected abstract State OnClose ();


	/// <summary>
	/// Raises the opening event. 
	/// </summary>
	protected abstract State OnOpening ();

	/// <summary>
	/// Raises the closing event.
	/// </summary>
	protected abstract State OnClosing ();
}
