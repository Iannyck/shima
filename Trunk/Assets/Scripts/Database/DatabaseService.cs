using UnityEngine;
using System.Collections;

public class DatabaseService {

	private string url;
	private WWWForm electricityWWWForm;
	private int electricityDataCounter;
	private WWWForm rfidWWWForm;
	private int rfidDataCounter;
	private WWWForm binarySensorWWWForm;
	private int binarySensorDataCounter;
	private WWWForm ultrasoundWWWForm;
	private int ultrasoundDataCounter;

	private SensorsGUI gui;

	public DatabaseService(string url, SensorsGUI gui){
		this.url = url;
		electricityWWWForm = new WWWForm ();
		electricityDataCounter = 0;
		rfidWWWForm = new WWWForm ();
		rfidDataCounter = 0;
		binarySensorWWWForm = new WWWForm ();
		binarySensorDataCounter = 0;
		ultrasoundWWWForm = new WWWForm ();
		ultrasoundDataCounter = 0;
		this.gui = gui;
	}

	public void InsertRFIDData(string timestamp, string antenaId, float signalStrenght, string tagId) {
		rfidWWWForm.AddField ("timestamp"+rfidDataCounter,""+timestamp);
		rfidWWWForm.AddField ("antenaId"+rfidDataCounter,""+antenaId);
		rfidWWWForm.AddField ("signalStrenght"+rfidDataCounter,""+signalStrenght);
		rfidWWWForm.AddField ("tagId"+rfidDataCounter,""+tagId);
		rfidDataCounter++;
	}

	public void InsertElectricityData(string timestamp, string phaseId, int activePower, int reactivePower) {
		electricityWWWForm.AddField ("timestamp"+electricityDataCounter,""+timestamp);
		electricityWWWForm.AddField ("phaseId"+electricityDataCounter,""+phaseId);
		electricityWWWForm.AddField ("activePower"+electricityDataCounter,""+activePower);
		electricityWWWForm.AddField ("reactivePower"+electricityDataCounter,""+reactivePower);
		electricityDataCounter++;
	}

	public void InsertBinarySensorData(string timestamp, string id, string type, int value) {
		binarySensorWWWForm.AddField ("timestamp"+binarySensorDataCounter,""+timestamp);
		binarySensorWWWForm.AddField ("id"+binarySensorDataCounter,""+id);
		binarySensorWWWForm.AddField ("type"+binarySensorDataCounter,""+type);
		binarySensorWWWForm.AddField ("value"+binarySensorDataCounter,""+value);
		binarySensorDataCounter++;
	}

	public void InsertUltrasoundData(string timestamp, string id, float value) {
		ultrasoundWWWForm.AddField ("timestamp"+ultrasoundDataCounter,""+timestamp);
		ultrasoundWWWForm.AddField ("id"+ultrasoundDataCounter,""+id);
		ultrasoundWWWForm.AddField ("value"+ultrasoundDataCounter,""+value);
		ultrasoundDataCounter++;
	}

	public IEnumerator commitElectricityData() {
		electricityWWWForm.AddField ("electricityDataCounter",""+electricityDataCounter);
		WWW www = new WWW (url+"electricity", electricityWWWForm);
		yield return www;
		if (www.error == null) {
//			Debug.Log ("WWW post OK: "+ www.text);
			gui.SetDebugText(9,"ElectricityData - [SAVED]");
			electricityWWWForm = new WWWForm ();
			electricityDataCounter = 0;
		} else {
//			Debug.Log ("WWW post ERROR: "+ www.error);
			gui.SetDebugText(9,"ElectricityData - [ERROR]");
			electricityWWWForm = new WWWForm ();
			electricityDataCounter = 0;
		}
	}

	public IEnumerator commitRFIDData() {
		rfidWWWForm.AddField ("rfidDataCounter",""+rfidDataCounter);
		WWW www = new WWW (url+"rfid", rfidWWWForm);
		yield return www;
		if (www.error == null) {
//			Debug.Log ("WWW post OK: "+ www.text);
			gui.SetDebugText(8,"RFIDData - [SAVED]");
			rfidWWWForm = new WWWForm ();
			rfidDataCounter = 0;
		} else {
//			Debug.Log ("WWW post ERROR: "+ www.error);
			gui.SetDebugText(8,"RFIDData - [ERROR]");
			rfidWWWForm = new WWWForm ();
			rfidDataCounter = 0;
		}
	}

	public IEnumerator commitBinarySensorData() {
		binarySensorWWWForm.AddField ("binarySensorDataCounter",""+binarySensorDataCounter);
		WWW www = new WWW (url+"binarySensor", binarySensorWWWForm);
		yield return www;
		if (www.error == null) {
//			Debug.Log ("WWW post OK: "+ www.text);
			gui.SetDebugText(7,"BinarySensorData - [SAVED]");
			binarySensorWWWForm = new WWWForm ();
			binarySensorDataCounter = 0;
		} else {
//			Debug.Log ("WWW post ERROR: "+ www.error);
			gui.SetDebugText(7,"BinarySensorData - [ERROR]");
			binarySensorWWWForm = new WWWForm ();
			binarySensorDataCounter = 0;
		}
	}

	public IEnumerator commitUltrasoundData() {
		ultrasoundWWWForm.AddField ("ultrasoundDataCounter",""+ultrasoundDataCounter);
		WWW www = new WWW (url+"ultrasound", ultrasoundWWWForm);
		yield return www;
		if (www.error == null) {
//			Debug.Log ("WWW post OK: "+ www.text);
			gui.SetDebugText(6,"UltrasoundData - [SAVED]");
			ultrasoundWWWForm = new WWWForm ();
			ultrasoundDataCounter = 0;
		} else {
//			Debug.Log ("WWW post ERROR: "+ www.error);
			gui.SetDebugText(6,"UltrasoundData - [ERROR]");
			ultrasoundWWWForm = new WWWForm ();
			ultrasoundDataCounter = 0;
		}
	}



}
