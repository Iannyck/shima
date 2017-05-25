using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class JSON_Save : MonoBehaviour {

    public GameObject furnituresFolder;
    public GameObject sensorsFolder;
    public GameObject wallsFolder;

    public GameObject displayText;

    public float timer = 0;

    void Start()
    {
        if (furnituresFolder == null)
        {
            furnituresFolder = GameObject.Find("Furnitures");

            if (furnituresFolder = null)
                Debug.Log("Select a default furnitures folder or rename it to Furnitures");
        }

        if (sensorsFolder == null)
        {
            sensorsFolder = GameObject.Find("Sensors");

            if (sensorsFolder = null)
                Debug.Log("Select a default sensors folder or rename it to Sensors");
        }

        if (wallsFolder == null)
        {
            wallsFolder = GameObject.Find("Walls");

            if (wallsFolder = null)
                Debug.Log("Select a default sensors folder or rename it to Walls");
        }

        displayText.SetActive(false);
    }

    public void Save()
    {
        StreamWriter writer = new StreamWriter("SaveRoom_Walls.txt");

        int nbFurnitures = furnituresFolder.transform.childCount;
        int nbSensors = sensorsFolder.transform.childCount;
        int nbWalls = wallsFolder.transform.childCount;

        Furniture savingFurniture = new global::Furniture();
        Sensor savingSensor = new global::Sensor();
        Wall savingWall = new global::Wall();

        for (int i = 0; i < nbWalls; i++)
        {
            Transform savingObject = wallsFolder.transform.GetChild(i);

            savingWall.name = savingObject.name;
            savingWall.position = savingObject.position;
            savingWall.rotation = savingObject.rotation;

            writer.WriteLine(JsonUtility.ToJson(savingWall));
        }

        writer.Close();


        writer = new StreamWriter("SaveRoom_Furnitures.txt");

        for (int i=0; i<nbFurnitures; i++)
        {
            Transform savingObject = furnituresFolder.transform.GetChild(i);

            savingFurniture.name = savingObject.name;
            savingFurniture.position = savingObject.position;
            savingFurniture.rotation = savingObject.rotation;

            writer.WriteLine(JsonUtility.ToJson(savingFurniture));
        }

        writer.Close();


        /* La fonction de sauvegarde des Sensors (voir ci-dessous) n'a pas été testé */

        writer = new StreamWriter("SaveRoom_Sensors.txt");

        for (int i = 0; i < nbSensors; i++)
        {
            Transform savingObject = furnituresFolder.transform.GetChild(i);

            savingSensor.name = savingObject.name;
            savingSensor.position = savingObject.position;
            savingSensor.rotation = savingObject.rotation;

            List<Sensor> listChildren = new List<Sensor>();

            for (int j=0; j < savingObject.childCount; j++)
            {
                Transform savingChild = savingObject.GetChild(0);
                Sensor childSensor = new Sensor();

                childSensor.name = savingChild.name;
                childSensor.position = savingChild.position;
                childSensor.rotation = savingChild.rotation;
                childSensor.children = null;

                listChildren.Add(childSensor);

            }
            writer.WriteLine(JsonUtility.ToJson(savingSensor));
        }

        writer.Close();

        displayText.SetActive(true);
        StartCoroutine(WaitTimer());
    }

    IEnumerator WaitTimer()
    {
       yield return new WaitForSeconds(timer);
        displayText.SetActive(false);
    }


}
