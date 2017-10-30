using UnityEngine;
using System.Collections;

public abstract class UsableDevice : MonoBehaviour {

	public enum State : byte {Off, On, Opening, Closing}

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

	protected abstract bool Init ();

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
			isInit = Init ();
	}

	public void ActOn(){ 
		OnOffButtonPressed = !OnOffButtonPressed; 
	}

	protected abstract State OnOn ();

	protected abstract State OnClose ();


	/// <summary>
	/// Raises the opening event. 
	/// </summary>
	protected abstract State OnOpening ();

	protected abstract State OnClosing ();
}
