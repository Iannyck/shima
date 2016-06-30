using UnityEngine;
using System.Collections;

public class RFIDController : MonoBehaviour
{
    public GameObject capteur1;
    public GameObject capteur2;
    public GameObject capteur3;
    public GameObject capteur4;

    private int countTokenRing;

    // Use this for initialization
    void Start()
    {
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
        InvokeRepeating("TokenRing", 1f, 1f);
    }

    void TokenRing()
    {
        // if (capteur1.activeSelf == true)
        if (countTokenRing == 0)
        {
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