using SQLite4Unity3d;
using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DatabaseService : MonoBehaviour {

	private SQLiteConnection sqliteConnection;

	public DatabaseService(string DatabaseName){
		#if UNITY_EDITOR
		var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
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


}
