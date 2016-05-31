using UnityEngine;
using System.Collections;

public class ElectronicDevice : MonoBehaviour {

	public int delta_active_power_phase1 = 0;
	public int delta_reactive_power_phase1 = 0;
	public int delta_spike_active_power_phase1 = 0;
	public int time_spike_active_power_phase1 = 0;

	public int delta_active_power_phase2 = 0;
	public int delta_reactive_power_phase2 = 0;
	public int delta_spike_active_power_phase2 = 0;
	public int time_spike_active_power_phase2 = 0;

	public int delta_active_power_phase3 = 0;
	public int delta_reactive_power_phase3 = 0;
	public int delta_spike_active_power_phase3 = 0;
	public int time_spike_active_power_phase3 = 0;

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
		if (smartElectronicMeterScript.RequestForEnergy(delta_active_power_phase1, delta_reactive_power_phase1
			,delta_active_power_phase2, delta_reactive_power_phase2, delta_active_power_phase3, delta_reactive_power_phase3) != null)
			Debug.Log ("Do on action");
	}
}
