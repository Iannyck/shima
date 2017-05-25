using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BSaveManager : MonoBehaviour {

    public GameObject furnituresFolder;
    public GameObject sensorsFolder;

    struct SomeObject
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    public void Save()
    {
        StreamWriter writer = new StreamWriter("SaveRoom.txt");

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

        int nbFurnitures = furnituresFolder.transform.childCount;
        int nbSensors = sensorsFolder.transform.childCount;

        Transform savingObject = null;

        for (int i = 0; i < nbFurnitures;i++)
        {
            savingObject = furnituresFolder.transform.GetChild(i);
            writer.WriteLine(savingObject.name);

            writer.WriteLine(savingObject.transform.position.x);
            writer.WriteLine(savingObject.transform.position.y);
            writer.WriteLine(savingObject.transform.position.z);

            writer.WriteLine(savingObject.transform.rotation.w);
            writer.WriteLine(savingObject.transform.rotation.x);
            writer.WriteLine(savingObject.transform.rotation.y);
            writer.WriteLine(savingObject.transform.rotation.z);
        }

        writer.WriteLine("!"); // Marque la separation des meubles et des capteurs dans le fichier texte

        for (int j = 0; j < nbSensors; j++)
        {
            savingObject = sensorsFolder.transform.GetChild(j);
            int nbChild = savingObject.childCount;

            writer.WriteLine(savingObject.name);
            writer.WriteLine(nbChild);

            writer.WriteLine(savingObject.transform.position.x);
            writer.WriteLine(savingObject.transform.position.y);
            writer.WriteLine(savingObject.transform.position.z);

            writer.WriteLine(savingObject.transform.rotation.w);
            writer.WriteLine(savingObject.transform.rotation.x);
            writer.WriteLine(savingObject.transform.rotation.y);
            writer.WriteLine(savingObject.transform.rotation.z);

            for (int k = 0; k < nbChild; k++)
            {
                Transform savingChild = savingObject.GetChild(k);

                writer.WriteLine(savingChild.transform.position.x);
                writer.WriteLine(savingChild.transform.position.y);
                writer.WriteLine(savingChild.transform.position.z);

                writer.WriteLine(savingChild.transform.rotation.w);
                writer.WriteLine(savingChild.transform.rotation.x);
                writer.WriteLine(savingChild.transform.rotation.y);
                writer.WriteLine(savingChild.transform.rotation.z);
            }
        }

        writer.Close();
    }
}