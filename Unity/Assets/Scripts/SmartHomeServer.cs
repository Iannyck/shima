using UnityEngine;
using System.Collections;

public class SmartHomeServer : MonoBehaviour {

	public string url = "http://localhost:8080/data/";

	public float deltaTime = 1f;

	private float currentTime;

	private DatabaseService database;

	// Use this for initialization
	void Start () {
		currentTime = deltaTime;
		database = new DatabaseService (url);
	}
	
	// Update is called once per frame
	void Update () {
		currentTime -= Time.deltaTime;
		if (currentTime <= 0) {
			WriteData ();
			currentTime = deltaTime;
		}
	}

	public void InsertRFIDData(string timestamp, string antenaId, float signalStrenght, string tagId) {
		database.InsertRFIDData (timestamp, antenaId, signalStrenght, tagId);
	}

	public void InsertElectricityData(string timestamp, string phaseId, int activePower, int reactivePower) {
		database.InsertElectricityData (timestamp, phaseId, activePower, reactivePower);
	}

	public void InsertFlowMeasurementData(string timestamp, string id, bool value) {
		database.InsertFlowMeasurementData(timestamp, id, value);
	}

	private void WriteData() {
		StartCoroutine(database.commitElectricityData ());
		StartCoroutine(database.commitRFIDData ());
		StartCoroutine(database.commitFlowmeasurementData ());
	}
}
