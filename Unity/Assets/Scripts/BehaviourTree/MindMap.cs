using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MindMap : MonoBehaviour {

	private Hashtable mindMap;

//	private List<GameObject> doorPointList;
//
//	private Hashtable doorMap;

	private Hashtable objectsMap;

	private Hashtable objectsRoomMap;

	// Use this for initialization
	void Start () {
		GameObject[] points = GameObject.FindGameObjectsWithTag ("RoomPoint");

		mindMap = new Hashtable ();
		foreach(GameObject point in points){
			mindMap.Add (point.name, point);
		}

//		GameObject[] doors = GameObject.FindGameObjectsWithTag ("DoorPoint");
//
//		doorPointList = new List<GameObject> ();
//		doorMap = new Hashtable ();
//		foreach (GameObject door in doors) {
//			doorPointList.Add (door);
//			doorMap.Add (door.name, null);
//		}
//
//		GameObject[] uiobjects = GameObject.FindGameObjectsWithTag ("UI_Object");
//
//		foreach (GameObject uiobject in uiobjects) {
//			if (uiobject.name.Contains ("Door")) {
//				string substring = uiobject.name.Split (' ') [0];
//				doorMap [substring + "In"] = uiobject;
//				doorMap [substring + "Out"] = uiobject;
//			}
//		}
//		Debug.Log (mindMap.Keys.Count);
	}
	
	// Update is called once per frame
	void Update () {

	}

	/// <summary>
	/// Gets the room point.
	/// </summary>
	/// <returns>The room point.</returns>
	/// <param name="name">Name.</param>
	public GameObject GetRoomPoint(string name) {
		if(mindMap.ContainsKey(name+"Point"))
			return (GameObject)mindMap[name+"Point"];
		return null;
	}

	/// <summary>
	/// Gets the door list.
	/// </summary>
	/// <value>The door list.</value>
//	public List<GameObject> DoorPointList {
//		get {
//			return this.doorPointList;
//		}
//	}
}
