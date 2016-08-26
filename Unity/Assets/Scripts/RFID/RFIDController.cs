using UnityEngine;
using System.Collections;

public class RFIDController : MonoBehaviour
{
    public GameObject capteur1;
    public GameObject capteur2;
    public GameObject capteur3;
    public GameObject capteur4;

    private int countTokenRing;
//    private DatabaseService databaseService;
//
//    public string url = "http://localhost:8080/test/rfid";

//    public DatabaseService DatabaseService { get { return this.databaseService; } }


    
    // Use this for initialization
    void Start()
    {
//        databaseService = new DatabaseService(url);

        capteur1.transform.GetChild(0).gameObject.SetActive(false);
        capteur1.transform.GetChild(1).gameObject.SetActive(false);
        capteur1.transform.GetChild(2).gameObject.SetActive(false);

        capteur2.transform.GetChild(0).gameObject.SetActive(false);
        capteur2.transform.GetChild(1).gameObject.SetActive(false);
        capteur2.transform.GetChild(2).gameObject.SetActive(false);

        capteur3.transform.GetChild(0).gameObject.SetActive(false);
        capteur3.transform.GetChild(1).gameObject.SetActive(false);
        capteur3.transform.GetChild(2).gameObject.SetActive(false);

        capteur4.transform.GetChild(0).gameObject.SetActive(false);
        capteur4.transform.GetChild(1).gameObject.SetActive(false);
        capteur4.transform.GetChild(2).gameObject.SetActive(false);

        countTokenRing = 0;
        InvokeRepeating("TokenRing", 1f, 0.25f);
    }

    void TokenRing()
    {
        // if (capteur1.activeSelf == true)
        if (countTokenRing == 0)
        {
            capteur1.GetComponent<TriggerRFID>().SetInactive();

            capteur1.transform.GetChild(0).gameObject.SetActive(false);
            capteur1.transform.GetChild(1).gameObject.SetActive(false);
            capteur1.transform.GetChild(2).gameObject.SetActive(false);

            capteur2.transform.GetChild(0).gameObject.SetActive(true);
            capteur2.transform.GetChild(1).gameObject.SetActive(true);
            capteur2.transform.GetChild(2).gameObject.SetActive(true);

            countTokenRing = 1;

            // capteur1.SetActive(false);
            // capteur2.SetActive(true);
        }

        // else if (capteur2.activeSelf == true)
        else if (countTokenRing == 1)
        {
            capteur2.GetComponent<TriggerRFID>().SetInactive();

            capteur2.transform.GetChild(0).gameObject.SetActive(false);
            capteur2.transform.GetChild(1).gameObject.SetActive(false);
            capteur2.transform.GetChild(2).gameObject.SetActive(false);

            capteur3.transform.GetChild(0).gameObject.SetActive(true);
            capteur3.transform.GetChild(1).gameObject.SetActive(true);
            capteur3.transform.GetChild(2).gameObject.SetActive(true);

            countTokenRing = 2;

            // capteur2.SetActive(false);
            // capteur3.SetActive(true);
        }

        // else if (capteur3.activeSelf == true)
        else if (countTokenRing == 2)
        {
            capteur3.GetComponent<TriggerRFID>().SetInactive();

            capteur3.transform.GetChild(0).gameObject.SetActive(false);
            capteur3.transform.GetChild(1).gameObject.SetActive(false);
            capteur3.transform.GetChild(2).gameObject.SetActive(false);

            capteur4.transform.GetChild(0).gameObject.SetActive(true);
            capteur4.transform.GetChild(1).gameObject.SetActive(true);
            capteur4.transform.GetChild(2).gameObject.SetActive(true);

            countTokenRing = 3;

            // capteur3.SetActive(false);
            // capteur4.SetActive(true);
        }

        // else if (capteur4.activeSelf == true)
        else if (countTokenRing == 3)
        {
            capteur4.GetComponent<TriggerRFID>().SetInactive();

            capteur4.transform.GetChild(0).gameObject.SetActive(false);
            capteur4.transform.GetChild(1).gameObject.SetActive(false);
            capteur4.transform.GetChild(2).gameObject.SetActive(false);

            capteur1.transform.GetChild(0).gameObject.SetActive(true);
            capteur1.transform.GetChild(1).gameObject.SetActive(true);
            capteur1.transform.GetChild(2).gameObject.SetActive(true);

            countTokenRing = 0;

            // capteur4.SetActive(false);
            // capteur1.SetActive(true);
        }

    }

}