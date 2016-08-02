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
		#if UNITY_EDITOR
		var dbPath = string.Format(@"Assets/StreamingAssets/{0}", databaseName);
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

	public void InsertRFIDData(long timestamp, string antenaId, int signalStrength, string tagId) {
		sqliteConnection.Insert (new RFIDData(timestamp, antenaId, signalStrength, tagId));
	}

	public void Insert(long timestamp, short phaseId, int activePower, int reactivePower) {
		sqliteConnection.Insert (new ElectricityData(timestamp, phaseId, activePower, reactivePower));
	}


}
