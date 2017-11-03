using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFIDDetectionZone : MonoBehaviour {

	private RFIDSensor rfidSensor;

	// Use this for initialization
	void Start () {
		InitRFIDSensor();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

//	void OnTriggerEnter(Collider collider){
//		Debug.Log ("test 2");
//	}

	void OnTriggerEnter(Collider collider) {
		SenseResolution (collider);
	}

	void OnTriggerStay(Collider collider) {
		SenseResolution (collider);
	}

	private void InitRFIDSensor() {
		rfidSensor = GetComponentInParent<RFIDSensor>();
	}

	private void SenseResolution(Collider collider) {
		if (rfidSensor != null) {
			if (collider.gameObject.layer == LayerMask.NameToLayer ("RFID")) {
				rfidSensor.Trigger (collider.gameObject);
			}
		} else {
			InitRFIDSensor();
		}
	}
}
