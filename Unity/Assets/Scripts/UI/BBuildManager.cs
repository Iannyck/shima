using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BBuildManager : MonoBehaviour {

	private int wallIndex = 0;
	private int furnitureIndex = 0;

	public void AddWall() {
//		GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
//		cube.name = "wall" + wallIndex;
//		wallIndex++;
//		cube.transform.position = new Vector3 (0, 0, 0);
//		cube.transform.localScale = new Vector3 (4,1,1);
		GameObject wall = Instantiate(Resources.Load("Wall") , new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
		wall.name = "wall" + wallIndex;
		wallIndex++;
	}

	public void AddFurniture(string id) {
		GameObject furniture = Instantiate(Resources.Load("Furniture/"+id) , new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
		furniture.name = "furniture" + furnitureIndex;
		furnitureIndex++;
	}

	public void AddSpeaker() {
	}

//	public void AddFurniture(Button button) {
//		Debug.Log (button.GetComponentsInChildren<Text> ());
////		GameObject furniture = Instantiate(Resources.Load(button.GetComponentsInChildren<Text>()) , new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
////		furniture.name = "furniture" + furnitureIndex;
////		furnitureIndex++;
//	}

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
