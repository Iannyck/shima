using UnityEngine;
using System.Collections;

public abstract class UsableDevice : MonoBehaviour {

	public enum State : byte {Off, On, Opening, Closing}

	public bool OnOffButtonPressed = false;
	protected State deviceState;

	public bool IsInit = false;

	public State DeviceState {
		get {
			return this.deviceState;
		}
	}

	protected abstract bool Init ();

	// Update is called once per frame
	void Update () {
		if (IsInit) {
			if (OnOffButtonPressed) {
				if (DeviceState == State.Off) {
					deviceState = OnOn ();
				} else if (DeviceState == State.On) {
					deviceState = OnClose ();
				} else if (DeviceState == State.Opening) {
					deviceState = OnOpening ();
					OnOffButtonPressed = false;
				} else if (DeviceState == State.Closing) {
					deviceState = OnClosing ();
					OnOffButtonPressed = false;
				}
			}
		} else
			IsInit = Init ();
	}

	public void ActOn(){ 
		OnOffButtonPressed = !OnOffButtonPressed; 
	}

	protected abstract State OnOn ();

	protected abstract State OnClose ();

	protected abstract State OnOpening ();

	protected abstract State OnClosing ();
}
