using UnityEngine;
using System.Collections;
using System.IO;

public class SaveSensors : MonoBehaviour {

    public GameObject prefabRFID;
    public GameObject prefabUltrasons;
    public GameObject prefabPression;
    public GameObject prefabMouvement;

    string winDir = System.Environment.GetEnvironmentVariable("windir");
    
    GameObject sensorParent;
    Transform sensorChild;

    struct SomeCapteur
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    // Use this for initialization
    void Start()
    {
        Save();
        Load();
    }

    // Update is called once per frame
    void Update() {}

    void Save()
    {
        StreamWriter writer = new StreamWriter("SaveSensors.txt");
        int numberChildren = transform.childCount;

        for (int j = 0; j < numberChildren; j++)
        {
            sensorChild = transform.GetChild(j);
            writer.WriteLine(sensorChild.tag);                                              // Ecrit le tag du sensor (RFID,Mouvement,Ultrasons ou Pression)

            writer.WriteLine(sensorChild.transform.position.x);
            writer.WriteLine(sensorChild.transform.position.y);
            writer.WriteLine(sensorChild.transform.position.z);

            writer.WriteLine(sensorChild.transform.rotation.w);
            writer.WriteLine(sensorChild.transform.rotation.x);
            writer.WriteLine(sensorChild.transform.rotation.y);
            writer.WriteLine(sensorChild.transform.rotation.z);

            int numberChildrenCapteur = sensorChild.transform.childCount;
            SomeCapteur[] tableauCapteur = new SomeCapteur[numberChildrenCapteur];

            for (int i = 0; i < numberChildrenCapteur; i++)
            {
                tableauCapteur[i].position = sensorChild.transform.GetChild(i).position;
                tableauCapteur[i].rotation = sensorChild.transform.GetChild(i).rotation;

                writer.WriteLine(tableauCapteur[i].position.x);
                writer.WriteLine(tableauCapteur[i].position.y);
                writer.WriteLine(tableauCapteur[i].position.z);

                writer.WriteLine(tableauCapteur[i].rotation.w);
                writer.WriteLine(tableauCapteur[i].rotation.x);
                writer.WriteLine(tableauCapteur[i].rotation.y);
                writer.WriteLine(tableauCapteur[i].rotation.z);
            }

            // writer.WriteLine(";");
        }

        writer.Close();
    }

    void Load()
    {
        string buffer;
        GameObject prefab;

        StreamReader reader = new StreamReader("SaveSensors.txt");
        buffer = reader.ReadLine();

        while (buffer != null)
        {
            switch (buffer)
            {
                case "RFID":
                    {
                        prefab = prefabRFID;
                        LoadSensor(buffer, reader, prefab,4);
                        break;
                    }


                case "Ultrasons":
                    {
                        prefab = prefabUltrasons;
                        LoadSensor(buffer, reader, prefab,3);
                        break;
                    }

                case "Mouvement":
                    {
                        prefab = prefabMouvement;
                        LoadSensor(buffer, reader, prefab, 1);
                        break;
                    }

                case "Pression":
                    {
                        prefab = prefabPression;
                        LoadSensor(buffer, reader, prefab,1);
                        break;
                    }
            }

            buffer = reader.ReadLine();
        }
    }

    void LoadSensor(string buffer, StreamReader reader, GameObject prefab,int nbChildren)
    {
        buffer = reader.ReadLine();
        float xPosition = System.Convert.ToSingle(buffer);

        buffer = reader.ReadLine();
        float yPosition = System.Convert.ToSingle(buffer);

        buffer = reader.ReadLine();
        float zPosition = System.Convert.ToSingle(buffer);

        buffer = reader.ReadLine();
        float wRotation = System.Convert.ToSingle(buffer);

        buffer = reader.ReadLine();
        float xRotation = System.Convert.ToSingle(buffer);

        buffer = reader.ReadLine();
        float yRotation = System.Convert.ToSingle(buffer);

        buffer = reader.ReadLine();
        float zRotation = System.Convert.ToSingle(buffer);

        Vector3 finalPosition = new Vector3(xPosition, yPosition, zPosition);
        Quaternion finalRotation = new Quaternion(wRotation, xRotation, yRotation, zRotation);
        GameObject readRFID = (GameObject)Instantiate(prefab, finalPosition, finalRotation);


        for (int i = 0; i < nbChildren; i++)
        {
            buffer = reader.ReadLine();
            xPosition = System.Convert.ToSingle(buffer);

            buffer = reader.ReadLine();
            yPosition = System.Convert.ToSingle(buffer);

            buffer = reader.ReadLine();
            zPosition = System.Convert.ToSingle(buffer);

            buffer = reader.ReadLine();
            wRotation = System.Convert.ToSingle(buffer);

            buffer = reader.ReadLine();
            xRotation = System.Convert.ToSingle(buffer);

            buffer = reader.ReadLine();
            yRotation = System.Convert.ToSingle(buffer);

            buffer = reader.ReadLine();
            zRotation = System.Convert.ToSingle(buffer);

            finalPosition = new Vector3(xPosition, yPosition, zPosition);
            finalRotation = new Quaternion(wRotation, xRotation, yRotation, zRotation);

            readRFID.transform.GetChild(i).position = finalPosition;
            readRFID.transform.GetChild(i).rotation = finalRotation;
        }
    }
}