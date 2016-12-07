using UnityEngine;
using System.Collections;

public class DDContactSensor : MonoBehaviour {

//	private GameObject smartHomeServer;
	private SmartHomeServer smartHomeServerScript;
	private bool state;
	public float delay = 1f;
	private float currentTime;

	// Use this for initialization
	void Start () {
		state = true;
		GameObject smartHomeServer = GameObject.Find ("smarthomeserver");
		smartHomeServerScript = smartHomeServer.GetComponent<SmartHomeServer> ();
		currentTime = delay;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentTime > 0)
			currentTime -= Time.deltaTime;
	}

	public void Switch() {
		if (currentTime <= 0) {
			state = !state;
			currentTime = delay;
			smartHomeServerScript.InsertBinarySensorData (name, "ContactSensor", state);
		}
	}
}
