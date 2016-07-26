using UnityEngine;
using System.Collections;
using System.IO;

public class SaveSensors : MonoBehaviour {

    string winDir = System.Environment.GetEnvironmentVariable("windir");
    

    GameObject sensorParent;
    Transform sensorChild;

    struct RFIDCapteur
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    struct UltrasonsCapteur
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    struct PressionCapteur
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    struct MouvementCapteur
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    // Use this for initialization
    void Awake()
    {
        Save();
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

            switch (sensorChild.tag)
            {
                default: break;

                case "RFID":
                    {
                        int numberChildrenRFID = sensorChild.transform.childCount;
                        RFIDCapteur[] tableauRFID = new RFIDCapteur[numberChildrenRFID];

                        writer.WriteLine("RFID");

                        for (int i = 0; i < numberChildrenRFID; i++)
                        {
                            tableauRFID[i].position = sensorChild.transform.GetChild(i).position;
                            tableauRFID[i].rotation = sensorChild.transform.GetChild(i).rotation;

                            writer.Write("Position =");
                            writer.Write(tableauRFID[i].position.x);
                            writer.Write(";");
                            writer.Write(tableauRFID[i].position.y);
                            writer.Write(";");
                            writer.Write(tableauRFID[i].position.z);
                            writer.WriteLine();

                            writer.Write("Rotation =");
                            writer.Write(tableauRFID[i].rotation.x);
                            writer.Write(";");
                            writer.Write(tableauRFID[i].rotation.y);
                            writer.Write(";");
                            writer.Write(tableauRFID[i].rotation.z);
                            writer.Write(";");
                            writer.WriteLine();
                        }

                        writer.WriteLine();
                        break;
                    }

                case "Ultrasons":
                    {
                        int numberChildrenUltrasons = sensorChild.transform.childCount;
                        UltrasonsCapteur[] tableauUltrasons = new UltrasonsCapteur[numberChildrenUltrasons];

                        writer.WriteLine("Ultrasons");

                        for (int i = 0; i < numberChildrenUltrasons; i++)
                        {   
                            tableauUltrasons[i].position = sensorChild.transform.GetChild(i).position;
                            tableauUltrasons[i].rotation = sensorChild.transform.GetChild(i).rotation;

                            writer.Write("Position =");
                            writer.Write(tableauUltrasons[i].position.x);
                            writer.Write(";");
                            writer.Write(tableauUltrasons[i].position.y);
                            writer.Write(";");
                            writer.Write(tableauUltrasons[i].position.z);
                            writer.WriteLine();

                            writer.Write("Rotation =");
                            writer.Write(tableauUltrasons[i].rotation.x);
                            writer.Write(";");
                            writer.Write(tableauUltrasons[i].rotation.y);
                            writer.Write(";");
                            writer.Write(tableauUltrasons[i].rotation.z);
                            writer.Write(";");
                            writer.WriteLine();
                        }

                        writer.WriteLine();
                        break;
                    }

                case "Pression":
                    {
                        int numberChildrenPression = sensorChild.transform.childCount;
                        PressionCapteur[] tableauPression = new PressionCapteur[numberChildrenPression];

                        writer.WriteLine("Pression");

                        for (int i = 0; i < numberChildrenPression; i++)
                        {
                            tableauPression[i].position = sensorChild.transform.GetChild(i).position;
                            tableauPression[i].rotation = sensorChild.transform.GetChild(i).rotation;

                            writer.Write("Position =");
                            writer.Write(tableauPression[i].position.x);
                            writer.Write(";");
                            writer.Write(tableauPression[i].position.y);
                            writer.Write(";");
                            writer.Write(tableauPression[i].position.z);
                            writer.WriteLine();

                            writer.Write("Rotation =");
                            writer.Write(tableauPression[i].rotation.x);
                            writer.Write(";");
                            writer.Write(tableauPression[i].rotation.y);
                            writer.Write(";");
                            writer.Write(tableauPression[i].rotation.z);
                            writer.Write(";");
                            writer.WriteLine();

                        }

                        writer.WriteLine();
                        break;
                    }

                case "Mouvement":
                    {
                        int numberChildrenMouvement = sensorChild.transform.childCount;
                        MouvementCapteur[] tableauMouvement = new MouvementCapteur[numberChildrenMouvement];

                        writer.WriteLine("Mouvement");

                        for (int i = 0; i < numberChildrenMouvement; i++)
                        {
                            tableauMouvement[i].position = sensorChild.transform.GetChild(i).position;
                            tableauMouvement[i].rotation= sensorChild.transform.GetChild(i).rotation;

                            writer.Write("Position =");
                            writer.Write(tableauMouvement[i].position.x);
                            writer.Write(";");
                            writer.Write(tableauMouvement[i].position.y);
                            writer.Write(";");
                            writer.Write(tableauMouvement[i].position.z);
                            writer.WriteLine();

                            writer.Write("Rotation =");
                            writer.Write(tableauMouvement[i].rotation.x);
                            writer.Write(";");
                            writer.Write(tableauMouvement[i].rotation.y);
                            writer.Write(";");
                            writer.Write(tableauMouvement[i].rotation.z);
                            writer.Write(";");
                            writer.WriteLine();

                        }

                        writer.WriteLine();
                        break;
                    }
            }  
        }

    writer.Close();

    } 
}
