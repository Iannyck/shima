using SQLite4Unity3d;
using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DatabaseService : MonoBehaviour {

	private SQLiteConnection sqliteConnection;

	public DatabaseService(string databaseName){
		var dbPath = string.Format(@"Assets/StreamingAssets/{0}", databaseName);
		#if UNITY_EDITOR
		dbPath = string.Format(@"Assets/StreamingAssets/{0}", databaseName);
		#endif
		sqliteConnection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
		Debug.Log("Final PATH: " + dbPath); 
	}

	public void CreateDatabase(){
		sqliteConnection.DropTable<RFIDData> ();
		sqliteConnection.CreateTable<RFIDData> ();
		sqliteConnection.DropTable<ElectricityData> ();
		sqliteConnection.CreateTable<ElectricityData> ();
	}

	/// <summary>
	/// Inserts the RFID data.
	/// </summary>
	/// <param name="timestamp">Timestamp.</param>
	/// <param name="antenaId">Antena identifier.</param>
	/// <param name="signalStrength">Signal strength.</param>
	/// <param name="tagId">Tag identifier.</param>
	public void InsertRFIDData(string timestamp, string antenaId, float signalStrength, string tagId) {
		sqliteConnection.Insert (new RFIDData(timestamp, antenaId, signalStrength, tagId));
	}

	/// <summary>
	/// Insert the specified timestamp, phaseId, activePower and reactivePower.
	/// </summary>
	/// <param name="timestamp">Timestamp.</param>
	/// <param name="phaseId">Phase identifier.</param>
	/// <param name="activePower">Active power.</param>
	/// <param name="reactivePower">Reactive power.</param>
	public void InsertElectricityData(string timestamp, short phaseId, int activePower, int reactivePower) {
		sqliteConnection.Insert (new ElectricityData(timestamp, phaseId, activePower, reactivePower));
	}


}
