using UnityEngine;
using System.Collections;

public class SmartElectronicMeter : MonoBehaviour {

	public enum NoiseFunctionType : byte {Random, Gaussian}

	public NoiseFunctionType noiseFunctionType;

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

	public string url = "http://localhost:8080/test/electricity";

	private ArrayList requestPool;

	private ElectricityLogger logger;

	private DatabaseService database;

	// Use this for initialization
	void Start () {
		phase1 = new Phase (0, 0);
		phase2 = new Phase (0, 0);
		phase3 = new Phase (0, 0);

		requestPool = new ArrayList ();

		database = new DatabaseService (url);
        InitLogger ();
		if(logger != null)
			logger.PhasesStates (phase1, phase2, phase3);
	}
	
	// Update is called once per frame
	void Update () {
		if(requestPool.Count > 0) {
			string timestamp = System.DateTime.Now.ToLongTimeString();
			foreach(Request request in requestPool) {
				request.Execute (phase1, phase2, phase3);
				request.State = Request.RequestState.DeltaGiven;
				StartCoroutine(database.InsertElectricityData(timestamp, 1, phase1.Active_power, phase2.Reactive_power));
				StartCoroutine(database.InsertElectricityData(timestamp, 2, phase1.Active_power, phase2.Reactive_power));
				StartCoroutine(database.InsertElectricityData(timestamp, 3, phase1.Active_power, phase2.Reactive_power));
				if (logger != null)
					logger.PhasesStates (phase1, phase2, phase3);
				else {
					InitLogger ();
				}
			}
		}
		requestPool.Clear();
	}

	private void InitLogger() {
		GameObject smartHomeServer = GameObject.Find ("smarthomeserver");
		if(smartHomeServer == null)
			Debug.Log("SmartHomeServer Not Loaded");

		logger = smartHomeServer.GetComponent<ElectricityLogger> ();
	}

	/// <summary>
	/// Requests for energy.
	/// </summary>
	/// <returns>The for energy.</returns>
	/// <param name="delta_active_power_phase1">Delta active power phase1.</param>
	/// <param name="delta_reactive_power_phase1">Delta reactive power phase1.</param>
	/// <param name="delta_active_power_phase2">Delta active power phase2.</param>
	/// <param name="delta_reactive_power_phase2">Delta reactive power phase2.</param>
	/// <param name="delta_active_power_phase3">Delta active power phase3.</param>
	/// <param name="delta_reactive_power_phase3">Delta reactive power phase3.</param>
	public Request RequestForEnergy(int delta_active_power_phase1, int delta_reactive_power_phase1, int delta_active_power_phase2, 
		int delta_reactive_power_phase2, int delta_active_power_phase3, int delta_reactive_power_phase3) {

		Request request = new Request (delta_active_power_phase1, delta_reactive_power_phase1, delta_active_power_phase2, 
			delta_reactive_power_phase2, delta_active_power_phase3, delta_reactive_power_phase3);
		requestPool.Add (request);
		return request;
	}


	/// <summary>
	/// Gets the phase1.
	/// </summary>
	/// <value>The phase1.</value>
	public Phase Phase1 {
		get {
			return this.phase1;
		}
	}

	/// <summary>
	/// Gets the phase2.
	/// </summary>
	/// <value>The phase2.</value>
	public Phase Phase2 {
		get {
			return this.phase2;
		}
	}

	/// <summary>
	/// Gets the phase3.
	/// </summary>
	/// <value>The phase3.</value>
	public Phase Phase3 {
		get {
			return this.phase3;
		}
	}

	/// <summary>
	/// Gets the database.
	/// </summary>
	/// <value>The database.</value>
	public DatabaseService Database {
		get {
			return this.database;
		}
	}

}
