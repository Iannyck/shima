using UnityEngine;
using System.Collections;

public class ElectronicDevice : UsableDevice {

	public AudioClip audio;

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
		deviceState = State.Off;
		powerRequest = null;
	}

	protected override bool Init ()
	{
		if (smartElectronicMeterScript == null) {
			GameObject smartElectronicMeter = GameObject.Find ("SmartPanel");
			if(smartElectronicMeter == null)
				Debug.Log("Not Loaded");
			smartElectronicMeterScript = smartElectronicMeter.GetComponent<SmartElectronicMeter> ();
			return true;
		}
		return false;
	}

	protected override State OnOn ()
	{
		if (audio != null)
			AudioSource.PlayClipAtPoint (audio,transform.position);

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
		return State.Opening;
	}

	protected override State OnClose ()
	{
		powerRequest.Revert();
		smartElectronicMeterScript.ResquestForRevert(powerRequest);
		return State.Closing;
	}

	protected override State OnOpening ()
	{
		if(powerRequest.IsRequestDone == true)
			return State.On;
		return State.Opening;
	}

	protected override State OnClosing ()
	{
		if(powerRequest.IsRequestDone == true)
			return State.Off;
		return State.Closing;
	}

}
