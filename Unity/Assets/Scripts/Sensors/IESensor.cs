using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IESensor : MonoBehaviour {

	[SerializeField]
	private string smartHomeServerObjectName = "smarthomeserver";

	public bool isStarted = false;

	private SmartHomeServer smartHomeServer;

	public SmartHomeServer SmartHomeServer {
		get {
			return this.smartHomeServer;
		}
	}

	private void InitSmartHomeServerConnection() {
		GameObject smartHomeServerObject = GameObject.Find (smartHomeServerObjectName);
		if(smartHomeServerObject == null)
			Debug.Log(smartHomeServerObjectName+" Not found");
		else
			smartHomeServer = smartHomeServerObject.GetComponent<SmartHomeServer> ();
	}

	void OnDisable() {
		IESensorStop ();
	}

	// Use this for initialization
	void Start () {
		if (isStarted) {
			InitSmartHomeServerConnection ();
			IESensorInit ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isStarted) {
			if (smartHomeServer == null)
				InitSmartHomeServerConnection ();
			else
				IESensorUpdate ();
		}
	}

	/// <summary>
	/// IEs the sensor init.
	/// </summary>
	/// <returns>The sensor init.</returns>
	protected virtual void IESensorInit() {}

	/// <summary>
	/// IEs the sensor update.
	/// </summary>
	/// <returns>The sensor update.</returns>
	protected virtual void IESensorUpdate() {}

	/// <summary>
	/// IEs the sensor stop.
	/// </summary>
	/// <returns>The sensor stop.</returns>
	protected virtual void IESensorStop() {}
}
