using UnityEngine;
using System.Collections;

public class ElectronicDevice : MonoBehaviour {

	private enum State : byte {Off, On, Opening, Closing}

	private bool OnOffButtonPressed;
	private State deviceState;

	public string keyTopress = "e";

	public int noise_range_percent = 5;

	public int delta_active_power_phase1 = 0;
	public int delta_reactive_power_phase1 = 0;
	private int effective_delta_active_power_phase1 = 0;
	private int effective_delta_reactive_power_phase1 = 0;
//	public int delta_spike_active_power_phase1 = 0;
//	public int time_spike_active_power_phase1 = 0;

	public int delta_active_power_phase2 = 0;
	public int delta_reactive_power_phase2 = 0;
	private int effective_delta_active_power_phase2 = 0;
	private int effective_delta_reactive_power_phase2 = 0;
//	public int delta_spike_active_power_phase2 = 0;
//	public int time_spike_active_power_phase2 = 0;

	public int delta_active_power_phase3 = 0;
	public int delta_reactive_power_phase3 = 0;
	private int effective_delta_active_power_phase3 = 0;
	private int effective_delta_reactive_power_phase3 = 0;
//	public int delta_spike_active_power_phase3 = 0;
//	public int time_spike_active_power_phase3 = 0;

	private SmartElectronicMeter smartElectronicMeterScript;

	private Request powerRequest;

	// Use this for initialization
	void Start () {
		smartElectronicMeterScript = null;
		GameObject smartElectronicMeter = GameObject.Find ("SmartPanel");
		if(smartElectronicMeter == null)
			Debug.Log("Not Loaded");
		smartElectronicMeterScript = smartElectronicMeter.GetComponent<SmartElectronicMeter> ();
		OnOffButtonPressed = false;
		deviceState = State.Off;
		powerRequest = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (smartElectronicMeterScript != null) {
			if (Input.GetKeyDown (keyTopress)) {
				OnOffButtonPressed = !OnOffButtonPressed;
			}
			if (OnOffButtonPressed) {
				if (deviceState == State.Off) {
					OnOn ();
				} else if (deviceState == State.On) {
					OnClose ();
				} else if (deviceState == State.Opening) {
					OnOpening ();
					OnOffButtonPressed = false;
				} else if (deviceState == State.Closing) {
					OnClosing ();
					OnOffButtonPressed = false;
				}
			}
		}
	}

	protected void OnOn() {
		int noise_range = (int)(delta_active_power_phase1 * ((float)noise_range_percent / 100.0));
		effective_delta_active_power_phase1 = delta_active_power_phase1 + Random.Range(-noise_range, noise_range);
		noise_range = (int)(delta_reactive_power_phase1 * ((float)noise_range_percent / 100.0));
		effective_delta_reactive_power_phase1 = delta_reactive_power_phase1 + Random.Range(-noise_range, noise_range);
		noise_range = (int)(delta_active_power_phase2 * ((float)noise_range_percent / 100.0));
		effective_delta_active_power_phase2 = delta_active_power_phase2 + Random.Range(-noise_range, noise_range);
		noise_range = (int)(delta_reactive_power_phase2 * ((float)noise_range_percent / 100.0));
		effective_delta_reactive_power_phase2 = delta_reactive_power_phase2 + Random.Range(-noise_range, noise_range);
		noise_range = (int)(delta_active_power_phase3 * ((float)noise_range_percent / 100.0));
		effective_delta_active_power_phase3 = delta_active_power_phase3 + Random.Range(-noise_range, noise_range);
		noise_range = (int)(delta_reactive_power_phase3 * ((float)noise_range_percent / 100.0));
		effective_delta_reactive_power_phase3 = delta_reactive_power_phase3 + Random.Range(-noise_range, noise_range);
		powerRequest = smartElectronicMeterScript.RequestForEnergy (effective_delta_active_power_phase1, effective_delta_reactive_power_phase1
			, effective_delta_active_power_phase2, effective_delta_reactive_power_phase2, effective_delta_active_power_phase3, effective_delta_reactive_power_phase3);
		deviceState = State.Opening;
	}

	protected void OnClose() {
		powerRequest = smartElectronicMeterScript.RequestForEnergy (-effective_delta_active_power_phase1, -effective_delta_reactive_power_phase1
			, -effective_delta_active_power_phase2, -effective_delta_reactive_power_phase2, -effective_delta_active_power_phase3, -effective_delta_reactive_power_phase3);
		deviceState = State.Closing;
	}

	protected void OnOpening() {
		if(powerRequest.State == Request.RequestState.DeltaGiven)
			deviceState = State.On;
	}

	protected void OnClosing() {
		if(powerRequest.State == Request.RequestState.DeltaGiven)
			deviceState = State.Off;
	}
}
