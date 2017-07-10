using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityConsumption : MonoBehaviour {

	public bool isStarted = false;

	[SerializeField]
	private List<int> phases;

	[SerializeField]
	private string smartElectricMeterObjectName = "smarthomeserver";

	private SmartElectricMeter smartElectricMeter;

	private List<ElectricPhase> electricPhases;

	// Use this for initialization
	void Start () {
		if (isStarted) {
			GetSmartElectricMeter ();
		}
	}

	private void GetSmartElectricMeter() {
		GameObject smartElectricMeterObject = GameObject.Find (smartElectricMeterObjectName);
		if(smartElectricMeterObject == null)
			Debug.Log(smartElectricMeterObjectName+" Not found");
		else
			smartElectricMeter = smartElectricMeterObject.GetComponent<SmartElectricMeter> ();
	}

	public bool Consume(int phase, int activePower, int reactivePower){
		if (smartElectricMeter == null)
			GetSmartElectricMeter ();
		else
			return smartElectricMeter.Consume(phase, activePower, reactivePower);
		return false;
	}

	public void Release(int phase, int activePower, int reactivePower){
		if (smartElectricMeter == null)
			GetSmartElectricMeter ();
		else
			smartElectricMeter.Release(phase, activePower, reactivePower);
	}

}
