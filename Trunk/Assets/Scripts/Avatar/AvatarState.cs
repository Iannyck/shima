using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarState : MonoBehaviour {

	[SerializeField] private int forgetRate = 100;
	[SerializeField] private int increaseForgetRate = 1;

	[SerializeField] private int forgetTime = 100;

	[SerializeField] private GameObject hand;

	private Hashtable itemsLocation;
	private Hashtable roomMap;

	private Hashtable containerLocation;

	private GameObject itemInHand;


	// Use this for initialization
	void Start () {
		GameObject[] points = GameObject.FindGameObjectsWithTag ("Waypoint");

		roomMap = new Hashtable ();
		foreach(GameObject point in points){
			roomMap.Add (point.name, point);
		}

		points = GameObject.FindGameObjectsWithTag ("PickUp");

		itemsLocation = new Hashtable ();
		foreach(GameObject point in points){
			itemsLocation.Add (point.name, point);
		}
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
		if(roomMap != null && roomMap.ContainsKey(name+"Waypoint"))
			return (GameObject)roomMap[name+"Waypoint"];
		return null;
	}

	public void forget(){
		if (Random.Range (1, 101) > forgetRate) {
			int result = Random.Range (1, 3);
			if (result == 1) {
				if(itemsLocation.Count > 0)
					itemsLocation.Remove (itemsLocation[Random.Range(0,itemsLocation.Count)]);
			} else if (result == 2) {
				if(containerLocation.Count > 0)
					containerLocation.Remove (containerLocation[Random.Range(0,containerLocation.Count)]);
			} else {
				if(roomMap.Count > 0)
					roomMap.Remove (roomMap[Random.Range(0,roomMap.Count)]);
			}
		}
	}

	public void PickUp(GameObject item) {
		item.transform.SetParent (this.transform.parent);
		item.transform.position = hand.transform.position;
		itemInHand = item;
	}

	public void releaseAt(Vector3 position) {
		itemInHand.transform.SetParent (null);
		itemInHand.transform.position = position;
		itemInHand = null;
	}
}
