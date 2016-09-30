using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForceSignal : MonoBehaviour
{
    public string antennaID;
    public GameObject pere;
    private SmartHomeServer smartHomeServer;
    private List<ColliderData> dataList;

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

    void Start()
    {
        dataList = new List<ColliderData>();
    }

    private void InitSmartHomeServerConnection()
    {
        GameObject smartHomeServerObject = GameObject.Find("smarthomeserver");
        if (smartHomeServer == null)
            Debug.Log("SmartHomeServer Not Loaded");
        smartHomeServer = smartHomeServerObject.GetComponent<SmartHomeServer>();
    }

    private float CalculDistance(GameObject collider)
    {
        Vector3 positionCollider = collider.transform.position;
        // Vector3 positionCapteur = GetComponentInParent<AntennePosition>().PositionAntenne();
        Vector3 positionCapteur = pere.transform.position;

        Debug.Log(pere.name + " - " + pere.transform.position + " - " + Vector3.Distance(positionCollider, positionCapteur));

        return Vector3.Distance(positionCollider, positionCapteur);
    }

    private float CalculForceSignal(float distance)
    {
        float forceSignal = 0;

        // Debug.Log("distance - " + GetComponentInParent<Transform>().name + " - " + distance);

        forceSignal = ((-9.1333f * Mathf.Log(distance)) - 10.726f);

       return forceSignal;
    }

    private void StockData(float forceSignal, GameObject collider)
    {
        ColliderData colliderData = new ColliderData(forceSignal,collider);
        dataList.Add(colliderData);
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

    public void Desactivate()
    {
        SendData();
        dataList.Clear();
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject collider = other.gameObject;

        float distance = 0;
        float forceSignal = 0;

        if (collider.tag == "RFID")
        {
            distance = CalculDistance(collider);
            forceSignal = CalculForceSignal(distance);

            StockData(forceSignal, collider);
        }

        else
            return;
    }
};