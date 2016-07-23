using UnityEngine;
using System.Collections;

public class SaveSensors : MonoBehaviour {

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
        public int numeroCapteur;
    }

    struct PressionCapteur
    {
        public Vector3 position;
    }

    struct MouvementCapteur
    {
        public Vector3 position;
    }

    // Use this for initialization
    void Start() {}

    // Update is called once per frame
    void Update() {}

    void Save()
    {
        int numberChildren = transform.childCount;

        for (int i = 0; i < numberChildren; i++)
        {
            sensorChild = transform.GetChild(i);

            switch (sensorChild.tag)
            {
                default: break;

                case "RFID":
                    {
                        int numberChildrenRFID = sensorChild.transform.childCount;
                        RFIDCapteur[] tableauRFID = new RFIDCapteur[numberChildrenRFID];

                        for (i = 0; i < numberChildrenRFID; i++)
                        {
                            tableauRFID[i].position = sensorChild.transform.GetChild(i).position;
                            tableauRFID[i].rotation = sensorChild.transform.GetChild(i).rotation;
                        }

                        // Sauvegarde le tableau dans un fichier X

                        break;
                    }

                case "Ultrasons":
                    {
                        int numberChildrenUltrasons = sensorChild.transform.childCount;
                        UltrasonsCapteur[] tableauUltrasons = new UltrasonsCapteur[numberChildrenUltrasons];

                        for (i = 0; i < numberChildrenUltrasons; i++)
                        {   
                            tableauUltrasons[i].position = sensorChild.transform.GetChild(i).position;
                            tableauUltrasons[i].rotation = sensorChild.transform.GetChild(i).rotation;
                            tableauUltrasons[i].numeroCapteur = sensorChild.GetComponent<UltrasoundCaptor>().numeroCapteur;
                        }

                        // Sauvegarde le tableau dans un fichier X

                        break;
                    }

                case "Pression":
                    {
                        int numberChildrenPression = sensorChild.transform.childCount;
                        PressionCapteur[] tableauPression = new PressionCapteur[numberChildrenPression];

                        for (i = 0; i < numberChildrenPression; i++)
                        {
                            tableauPression[i].position = sensorChild.transform.GetChild(i).position;
                        }

                        // Sauvegarde le tableau dans un fichier X

                        break;
                    }

                case "Mouvement":
                    {
                        int numberChildrenMouvement = sensorChild.transform.childCount;
                        MouvementCapteur[] tableauMouvement = new MouvementCapteur[numberChildrenMouvement];

                        for (i = 0; i < numberChildrenMouvement; i++)
                        {
                            tableauMouvement[i].position = sensorChild.transform.GetChild(i).position;
                        }

                        // Sauvegarde le tableau dans un fichier X

                        break;
                    }
            }
        }

    } 
}
