using UnityEngine;
using System.Collections;

public class ElectronicDevice : MonoBehaviour {

	public int phase;
	public int power;

	private GameObject smartElectronicMeter;

	// Use this for initialization
	void Start () {
		smartElectronicMeter = GameObject.FindGameObjectWithTag ("smartElectronicMeter");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RequestPower() {
		SmartElectronicMeter smartElectronicMeterScript = smartElectronicMeter.GetComponent<SmartElectronicMeter> ();
		if (smartElectronicMeterScript.RequestForEnergy (phase, power))
			Debug.Log ("Do on action");
	}
}
