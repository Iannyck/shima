using UnityEngine;
using System.Collections;

public class MindMap : MonoBehaviour {

	public GameObject kitchenPoint;
	public GameObject livingroomPoint;
	public GameObject bedroomPoint;

	private Hashtable mindMap;

	private Hashtable objectsMap;

	private Hashtable objectsRoomMap;

	// Use this for initialization
	void Start () {
		mindMap = new Hashtable ();
		mindMap.Add ("kitchen", kitchenPoint);
		mindMap.Add ("livingroom", livingroomPoint);
		mindMap.Add ("bedroom", bedroomPoint);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public GameObject GetRoomPoint(string name) {
		return (GameObject)mindMap[name];
	}
}
