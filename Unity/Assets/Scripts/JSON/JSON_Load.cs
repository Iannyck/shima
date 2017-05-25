using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class JSON_Load : MonoBehaviour
{
    public GameObject furnituresFolder;
    public GameObject sensorsFolder;
    public GameObject wallsFolder;

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
    }

    public void Load()
    {
        StreamReader reader = new StreamReader("SaveRoom_Walls.txt");
        string buffer = reader.ReadLine();

        while (buffer != null)
        {
            buffer = reader.ReadLine();
            Wall loadingWall = JsonUtility.FromJson<Wall>(buffer);
        }

        reader.Close();

        reader = new StreamReader("SaveRoom_Furnitures.txt");
        buffer = reader.ReadLine();

        while (buffer != null)
        {
            buffer = reader.ReadLine();
            Sensor loadingFurniture = JsonUtility.FromJson<Sensor>(buffer);
        }

        reader.Close();

        /* La fonction de chargement des Sensors (voir ci-dessous) n'a pas été testé */

        reader = new StreamReader("SaveRoom_Sensors.txt");
        buffer = reader.ReadLine();

        while (buffer != null)
        {
            buffer = reader.ReadLine();
            Sensor loadingSensor = JsonUtility.FromJson<Sensor>(buffer);
        }

        reader.Close();

    }
}
