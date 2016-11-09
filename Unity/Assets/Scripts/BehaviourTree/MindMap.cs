using UnityEngine;
using System.Collections;

public class MindMap : MonoBehaviour {

	private Hashtable mindMap;

	private Hashtable objectsMap;

	private Hashtable objectsRoomMap;

	// Use this for initialization
	void Start () {
		GameObject[] points = GameObject.FindGameObjectsWithTag ("RoomPoint");

		mindMap = new Hashtable ();
		foreach(GameObject point in points){
			mindMap.Add (point.name, point);
		}

//		Debug.Log (mindMap.Keys.Count);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public GameObject GetRoomPoint(string name) {
		if(mindMap.ContainsKey(name+"Point"))
			return (GameObject)mindMap[name+"Point"];
		return null;
	}
}
