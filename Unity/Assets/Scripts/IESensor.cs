using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IESensor : MonoBehaviour {

	public bool isSimulating = false;

	private SmartHomeServer smartHomeServer;

	public SmartHomeServer SmartHomeServer {
		get {
			return this.smartHomeServer;
		}
	}

	private void InitSmartHomeServerConnection() {
		GameObject smartHomeServerObject = GameObject.Find ("smarthomeserver");
		if(smartHomeServerObject == null)
			Debug.Log("SmartHomeServer Not Loaded");
		else
			smartHomeServer = smartHomeServerObject.GetComponent<SmartHomeServer> ();
	}

	// Use this for initialization
	void Start () {
		if (isSimulating)
			InitSmartHomeServerConnection ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isSimulating) {
			Sense (smartHomeServer);
//			if (smartHomeServer == null)
//				InitSmartHomeServerConnection ();
//			else
//				Sense (smartHomeServer);
		}
	}

	/// <summary>
	/// Sense the specified smartHomeServer.
	/// Method to override to add specific sensing behavior
	/// </summary>
	/// <param name="smartHomeServer">Smart home server.</param>
	protected virtual void Sense(SmartHomeServer smartHomeServer) {}
}
