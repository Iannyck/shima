using UnityEngine;
using System.Collections;

public class DatabaseService {

	private string url;
	private WWWForm electricityWWWForm;
	private int electricityDataCounter;
	private WWWForm rfidWWWForm;
	private int rfidDataCounter;
	private WWWForm flowmeasurementWWWForm;
	private int flowmeasurementDataCounter;

	public DatabaseService(string url){
		this.url = url;
		electricityWWWForm = new WWWForm ();
		electricityDataCounter = 0;
		rfidWWWForm = new WWWForm ();
		rfidDataCounter = 0;
		flowmeasurementWWWForm = new WWWForm ();
		flowmeasurementDataCounter = 0;
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

	public void InsertFlowMeasurementData(string timestamp, string id, bool value) {
		flowmeasurementWWWForm.AddField ("timestamp"+flowmeasurementDataCounter,""+timestamp);
		flowmeasurementWWWForm.AddField ("id"+flowmeasurementDataCounter,""+id);
		flowmeasurementWWWForm.AddField ("value"+flowmeasurementDataCounter,""+value);
		flowmeasurementDataCounter++;
	}

	public IEnumerator commitElectricityData() {
		electricityWWWForm.AddField ("electricityDataCounter",""+electricityDataCounter);
		WWW www = new WWW (url+"electricity", electricityWWWForm);
		yield return www;
		if (www.error == null) {
			Debug.Log ("WWW post OK: "+ www.text);
			electricityWWWForm = new WWWForm ();
			electricityDataCounter = 0;
		} else {
			Debug.Log ("WWW post ERROR: "+ www.error);
			electricityWWWForm = new WWWForm ();
			electricityDataCounter = 0;
		}
	}

	public IEnumerator commitRFIDData() {
		rfidWWWForm.AddField ("rfidDataCounter",""+rfidDataCounter);
		WWW www = new WWW (url+"rfid", rfidWWWForm);
		yield return www;
		if (www.error == null) {
			Debug.Log ("WWW post OK: "+ www.text);
			rfidWWWForm = new WWWForm ();
			rfidDataCounter = 0;
		} else {
			Debug.Log ("WWW post ERROR: "+ www.error);
			rfidWWWForm = new WWWForm ();
			rfidDataCounter = 0;
		}
	}

	public IEnumerator commitFlowmeasurementData() {
		flowmeasurementWWWForm.AddField ("flowmeasurementDataCounter",""+flowmeasurementDataCounter);
		WWW www = new WWW (url+"flowmeasurement", flowmeasurementWWWForm);
		yield return www;
		if (www.error == null) {
			Debug.Log ("WWW post OK: "+ www.text);
			flowmeasurementWWWForm = new WWWForm ();
			flowmeasurementDataCounter = 0;
		} else {
			Debug.Log ("WWW post ERROR: "+ www.error);
			flowmeasurementWWWForm = new WWWForm ();
			flowmeasurementDataCounter = 0;
		}
	}



}
