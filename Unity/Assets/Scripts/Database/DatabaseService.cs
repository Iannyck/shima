using UnityEngine;
using System.Collections;

public class DatabaseService {

	private string url;

	public DatabaseService(string url){
		this.url = url;
	}

	IEnumerator WaitForRequest(WWW www) {
		yield return www;
		if (www.error == null) {
			Debug.Log ("WWW Ok:" + www.text);
		} else {
			Debug.Log ("WWW Error:"+www.error);
		}
	}

    /// <summary>
    /// Inserts the RFID data.
    /// </summary>
    /// <param name="timestamp">Timestamp.</param>
    /// <param name="antenaId">Antena identifier.</param>
    /// <param name="signalStrenght">Signal strength.</param>
    /// <param name="tagId">Tag identifier.</param>
	public IEnumerator InsertRFIDData(string timestamp, string antenaId, float signalStrenght, string tagId) {
		WWWForm wWWform = new WWWForm ();
		wWWform.AddField ("timestamp",""+timestamp);
		wWWform.AddField ("antenaId",""+antenaId);
		wWWform.AddField ("signalStrenght",""+signalStrenght);
		wWWform.AddField ("tagId",""+tagId);
		WWW www = new WWW (url, wWWform);

		yield return www;
		if (www.error == null) {
			Debug.Log ("WWW post OK: "+ www.text);
		} else {
			Debug.Log ("WWW post ERROR: "+ www.error);
		}
	}

	/// <summary>
	/// Insert the specified timestamp, phaseId, activePower and reactivePower.
	/// </summary>
	/// <param name="timestamp">Timestamp.</param>
	/// <param name="phaseId">Phase identifier.</param>
	/// <param name="activePower">Active power.</param>
	/// <param name="reactivePower">Reactive power.</param>
	public IEnumerator InsertElectricityData(string timestamp, string phaseId, int activePower, int reactivePower) {
		WWWForm wWWform = new WWWForm ();
		wWWform.AddField ("timestamp",""+timestamp);
		wWWform.AddField ("phaseId",""+phaseId);
		wWWform.AddField ("activePower",""+activePower);
		wWWform.AddField ("reactivePower",""+reactivePower);
		WWW www = new WWW (url, wWWform);

		yield return www;
		if (www.error == null) {
			Debug.Log ("WWW post OK: "+ www.text);
		} else {
			Debug.Log ("WWW post ERROR: "+ www.error);
		}
	}


}
