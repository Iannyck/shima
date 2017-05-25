using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BBuildManager : MonoBehaviour {

    public GameObject furnituresFolder;
    public GameObject sensorsFolder;
    public GameObject wallsFolder;

    private int wallIndex = 0;
	private int furnitureIndex = 0;
    private int sensorIndex = 0;

	public void AddWall() {
//		GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
//		cube.name = "wall" + wallIndex;
//		wallIndex++;
//		cube.transform.position = new Vector3 (0, 0, 0);
//		cube.transform.localScale = new Vector3 (4,1,1);

		GameObject wall = Instantiate(Resources.Load("Wall") , new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;

        wall.transform.SetParent(wallsFolder.transform);
		wall.name = "wall" + wallIndex;
		wallIndex++;
	}

	public void AddFurniture(string id) {
		GameObject furniture = Instantiate(Resources.Load("Furniture/"+id) , new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
        furniture.transform.SetParent(furnituresFolder.transform);
        furniture.name = "furniture" + furnitureIndex;
		furnitureIndex++;
	}

    public void AddSensor(string id)
    {
        GameObject sensor= Instantiate(Resources.Load("Sensor/" + id), new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        sensor.transform.SetParent(sensorsFolder.transform);
        sensor.name = "sensor" + sensorIndex;
        sensorIndex++;
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
	void Start ()
    {
        if (furnituresFolder == null)
        {
            furnituresFolder = GameObject.Find("Furnitures");

            if (furnituresFolder == null)
             Debug.Log("Select a default furnitures folder or rename it to Furnitures");
        }

        if (sensorsFolder == null)
        {
            sensorsFolder = GameObject.Find("Sensors");

            if (sensorsFolder == null)
                Debug.Log("Select a default sensors folder or rename it to Sensors");
        }

        if (wallsFolder == null)
        {
            sensorsFolder = GameObject.Find("Walls");

            if (sensorsFolder == null)
                Debug.Log("Select a default walls folder or rename it to Walls");
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
