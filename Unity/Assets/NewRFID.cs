using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewRFID : MonoBehaviour
{
    // Contient l'ensemble des paramètres publics et privés de la classe
    #region Paramètres

    public string antennaID;
    public float seuilDetection = -71f;

    public GameObject gestionnaire;
    private SmartHomeServer smartHomeServer;
    private List<ColliderData> dataList;
    private Transform zoneDetection;

    #endregion

    // Contient la structure ColliderData et les fonctions qui permettent de la gérer
    #region ColliderData 

    struct ColliderData
    {
        public float signal;
        public GameObject collider;

        public ColliderData(float a, GameObject b)
        {
            signal = a;
            collider = b;
        }
    }

    private void StockData(float forceSignal, GameObject collider)
    {
        ColliderData colliderData = new ColliderData(forceSignal, collider);
        dataList.Add(colliderData);
    }

    #endregion

    // Contient l'ensemble des fonctions MonoBehavior (Start,OnTriggerEnter et OnTriggerExit)
    #region MonoBehavior

    void Awake ()
    {
        dataList = new List<ColliderData>();

        zoneDetection = this.transform.Find("ZoneDetection");

        if (gestionnaire == null)
            gestionnaire = transform.parent.gameObject;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ListTagRFID>() != null || other.gameObject.tag == "TEST")
        {
            List<GameObject> listeTags = other.gameObject.GetComponent<ListTagRFID>().ListeTags;

            foreach (GameObject a in listeTags)
            {
                float forceSignal = CalculSignalResultant(a);

                if (isForceStrongEnough(forceSignal))
                    StockData(forceSignal, a);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        foreach(ColliderData a in dataList)
        {
            if (a.collider == other.gameObject)
            {
                dataList.Remove(a);
                return;
            }

            // Debug.Log("ERREUR: " + other.name + " ne se trouve pas dans la liste");
        }
    }

    #endregion

    // Contient l'ensemble des fonctions qui permettent d'envoyer les données au serveur
    #region Serveur

    private void InitSmartHomeServerConnection()
    {
        GameObject smartHomeServerObject = GameObject.Find("smarthomeserver");
        if (smartHomeServerObject == null)
            Debug.Log("SmartHomeServer Not Loaded");
        else
            smartHomeServer = smartHomeServerObject.GetComponent<SmartHomeServer>();
    }

    private void SendData()
    {
        string timestamp = System.DateTime.Now.ToLongTimeString();

        if (smartHomeServer == null)
            InitSmartHomeServerConnection();

        else
        {
            for (int i = 0; i < dataList.Count; i++)
                smartHomeServer.InsertRFIDData(timestamp, antennaID, dataList[i].signal, dataList[i].collider.name);
        }
    }
    #endregion

    // Contient l'ensemble des fonctions mathématiques
    #region Calcul

    float CalculSignalResultant(GameObject collider) // Calcule la force du signal résultante compte tenu de tous les facteurs (Angle,Obstacle,etc.)
    {
        float distance = CalculDistance(collider.transform.position);
        float angle = CalculAngle(collider.transform.position);

        float forceSignal = CalculSignalDistance(distance);

        forceSignal -= CalculPerteSignalAngle(angle);
        forceSignal -= CalculPerteSignalObstacle(collider);
        forceSignal -= CalculPerteSignalBruit(collider);

        return forceSignal;
    }

    float CalculDistance(Vector3 pos_Cible) // Calcule la distance en fonction de la distance entre le capteur et la cible
    {
        return Vector3.Distance(this.transform.position, pos_Cible);
    }
    float CalculAngle(Vector3 pos_Cible) // Calcule l'angle entre le capteur et la cible
    {
        float angle = Vector3.Angle(this.transform.position, pos_Cible);
        return angle;
    }

    float CalculSignalDistance(float distance) // Calcule la force du signal en fonction de la distance
    {
        float forceSignal = 0;
        forceSignal = ((-9.1333f * Mathf.Log(distance)) - 10.726f);

        return forceSignal;
    }

    float CalculPerteSignalAngle(float angle) // Calcule la perte du signal en fonction de l'angle (A REMPLIR)
    {
        // Utiliser la valeur de l'angle afin de déterminer la dégradation
        return 0;
    }
    float CalculPerteSignalObstacle(GameObject collider) // Calcule la perte du signal en fonction des obstacles (A REMPLIR)
    {
        RaycastHit hitInfo;

        Debug.DrawLine(this.transform.position, collider.transform.position, Color.white, 10f);

        Physics.Linecast(this.transform.position, collider.transform.position, out hitInfo);
        {
            if (hitInfo.collider.CompareTag("Fixed") || hitInfo.collider.CompareTag("Door"))                                   // Si le collider est un objet de type Fixed, la puissance doit etre diminue
            {
                // Déterminer la dégradation en fonction du type d'objet entre le capteur et le tag
                return 0;
            }

            else
                return 0;
        }
    }
    float CalculPerteSignalBruit(GameObject collider) // Calcule la perte du signal en fonction du bruit (A REMPLIR)
    {
        return 0;
    }

    bool isForceStrongEnough(float forceSignal) // Vérifie si la force de signal est assez forte pour que le capteur détecte la cible
    {
        if (forceSignal >= seuilDetection)
            return true;

        else
            return false;
    } 

    #endregion

    // Contient l'ensemble des fonctions de gestions qui peuvent être utilisé par le gestionnaire du capteur (TokenRing)
    #region Gestion

    public void OnDisable()
    {
        SendData();
        dataList.Clear();
        this.gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        this.gameObject.SetActive(true);
    }

    #endregion
}
