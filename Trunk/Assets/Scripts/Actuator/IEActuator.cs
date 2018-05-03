using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEActuator : MonoBehaviour {

	[SerializeField]
	private string smartHomeServerObjectName = "SmartHomeServer";

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
		IEActuatorStop ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected virtual void IEActuatorInit() {}

	protected virtual void IEActuatorUpdate() {}

	protected virtual void IEActuatorStop() {}
}
