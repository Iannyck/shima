using UnityEngine;
using System.Collections;

public class UltraSoundMaster : MonoBehaviour {

    public GameObject capteur1;
    public GameObject capteur2;
    public GameObject capteur3;

    float temps1;
    float temps2;
    float temps3;

    float vitesse1;
    float vitesse2;
    float vitesse3;

    float vitesseMoyenne;

    bool detection1;
    bool detection2;
    bool detection3;

    // Use this for initialization
    void Start()
    {
        detection1 = true;
        detection2 = true;
        detection3 = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDetection();
    }

    void CheckDetection()
    {
        if (detection1 == true)
        {
            if (capteur1.GetComponent<UltrasoundCaptor>().detection == false)
            {
                temps1 = capteur1.GetComponent<UltrasoundCaptor>().GetTemps();
                detection1 = false;
            }

        }

        if (detection2 == true)
        {

            if (capteur2.GetComponent<UltrasoundCaptor>().detection == false)
            {
                temps2 = capteur2.GetComponent<UltrasoundCaptor>().GetTemps();
                detection2 = false;
            }
        }

        if (detection3 == true)
        {

            if (capteur3.GetComponent<UltrasoundCaptor>().detection == false)
            {
                temps3 = capteur3.GetComponent<UltrasoundCaptor>().GetTemps();
                detection3 = false;
            }
        }

        if (detection1 == false && detection2 == false && detection3 == false)
        {
            CalculTemps();
            DisplayResult();
            ResetAll();
        }

    }

    void CalculTemps()
    {
        vitesse1 = 20 / (temps2 - temps1);
        vitesse2 = 20 / (temps3 - temps2);
        vitesse3 = 40 / (temps3 - temps1);

        vitesseMoyenne = (vitesse1 + vitesse2 + vitesse3) / 3;
    }

    void ResetAll()
    {
        capteur1.GetComponent<UltrasoundCaptor>().detection = true;
        capteur2.GetComponent<UltrasoundCaptor>().detection = true;
        capteur3.GetComponent<UltrasoundCaptor>().detection = true;

        capteur1.GetComponent<UltrasoundCaptor>().SetTemps(0);
        capteur2.GetComponent<UltrasoundCaptor>().SetTemps(0);
        capteur3.GetComponent<UltrasoundCaptor>().SetTemps(0);

        temps1 = 0;
        temps2 = 0;
        temps3 = 0;

        vitesse1 = 0;
        vitesse2 = 0;
        vitesse3 = 0;

        vitesseMoyenne = 0;

        detection1 = true;
        detection2 = true;
        detection3 = true;
    }

    void DisplayResult()
    {
        Debug.Log(vitesse1);
        Debug.Log(vitesse2);
        Debug.Log(vitesse3);
        Debug.Log(vitesseMoyenne);
        // Affichage des resultats
    }

}

