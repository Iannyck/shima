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
		database = new DatabaseService (url,GetComponent<SensorsGUI>() as SensorsGUI);
	}
	
	// Update is called once per frame
	void Update () {
		currentTime -= Time.deltaTime;
		if (currentTime <= 0) {
			WriteData ();
			currentTime = deltaTime;
		}
	}

	public void InsertRFIDData(string antenaId, float signalStrenght, string tagId) {
		string timestamp = System.DateTime.UtcNow.ToLongTimeString() + ":"+ System.DateTime.UtcNow.Millisecond;
		database.InsertRFIDData (timestamp, antenaId, signalStrenght, tagId);
	}

	public void InsertElectricityData(string phaseId, int activePower, int reactivePower) {
		string timestamp = System.DateTime.UtcNow.ToLongTimeString() + ":"+ System.DateTime.UtcNow.Millisecond;
		database.InsertElectricityData (timestamp, phaseId, activePower, reactivePower);
	}

	public void InsertElectricityData(string timestamp, string phaseId, int activePower, int reactivePower) {
		database.InsertElectricityData (timestamp, phaseId, activePower, reactivePower);
	}

	public void InsertBinarySensorData(string id, string type, bool value) {
		string timestamp = System.DateTime.UtcNow.ToLongTimeString() + ":"+ System.DateTime.UtcNow.Millisecond;
		database.InsertBinarySensorData(timestamp, id, type, value);
	}

	public void InsertUltrasoundData(string id, float value) {
		string timestamp = System.DateTime.UtcNow.ToLongTimeString() + ":"+ System.DateTime.UtcNow.Millisecond;
		database.InsertUltrasoundData(timestamp, id, value);
	}

	private void WriteData() {
		StartCoroutine(database.commitElectricityData ());
		StartCoroutine(database.commitRFIDData ());
		StartCoroutine(database.commitBinarySensorData ());
		StartCoroutine(database.commitUltrasoundData ());
	}
}
