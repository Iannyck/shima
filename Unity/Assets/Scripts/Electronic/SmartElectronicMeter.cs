using UnityEngine;
using System.Collections;

public class SmartElectronicMeter : MonoBehaviour {

	/// <summary>
	/// The phase1.
	/// </summary>
	private Phase phase1;

	/// <summary>
	/// The phase2.
	/// </summary>
	private Phase phase2;

	/// <summary>
	/// The phase3.
	/// </summary>
	private Phase phase3;

	private ArrayList requestPool;

	// Use this for initialization
	void Start () {
		phase1 = new Phase (0, 0);
		phase2 = new Phase (0, 0);
		phase3 = new Phase (0, 0);

		requestPool = new ArrayList ();
	}
	
	// Update is called once per frame
	void Update () {
		if(requestPool.Count > 0) {
			foreach(Request request in requestPool) {
				request.Execute (phase1, phase2, phase3);
			}
		}
		requestPool.Clear();
	}

	public Request RequestForEnergy(int delta_active_power_phase1, int delta_reactive_power_phase1, int delta_active_power_phase2, 
		int delta_reactive_power_phase2, int delta_active_power_phase3, int delta_reactive_power_phase3) {

		Request request = new Request (delta_active_power_phase1, delta_reactive_power_phase1, delta_active_power_phase2, 
			delta_reactive_power_phase2, delta_active_power_phase3, delta_reactive_power_phase3);
		requestPool.Add (request);
		return request;
	}

}
