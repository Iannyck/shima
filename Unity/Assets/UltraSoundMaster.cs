using UnityEngine;
using System.Collections;

public class UltraSoundMaster : MonoBehaviour {

    public GameObject capteur1;
    public GameObject capteur2;
    public GameObject capteur3;

    static public int nombreCapteur = 3;

    private TableauResultat[] unTableau = new TableauResultat[nombreCapteur];

    bool detection1;
    bool detection2;
    bool detection3;

    struct TableauResultat
    {
        public int numeroCapteur;
        public float distanceCapteur;
        public float tempsCapteur;
    }

    // Use this for initialization
    void Start()
    {
        ResetDetection();
    }

    // Update is called once per frame
    void Update()
    {
//        CheckDetection();
    }

	/*
    void CheckDetection()
    {
        if (detection1 == true)
        {
            if (capteur1.GetComponent<UltrasoundCaptor>().detection == false)
            {
                unTableau[0].numeroCapteur = capteur1.GetComponent<UltrasoundCaptor>().numeroCapteur;
                unTableau[0].distanceCapteur = capteur1.GetComponent<UltrasoundCaptor>().GetDistance();
                unTableau[0].tempsCapteur = capteur1.GetComponent<UltrasoundCaptor>().GetTemps();

                detection1 = false;
            }

        }

        if (detection2 == true)
        {
            if (capteur2.GetComponent<UltrasoundCaptor>().detection == false)
            {
                unTableau[1].numeroCapteur = capteur2.GetComponent<UltrasoundCaptor>().numeroCapteur;
                unTableau[1].distanceCapteur = capteur2.GetComponent<UltrasoundCaptor>().GetDistance();
                unTableau[1].tempsCapteur = capteur2.GetComponent<UltrasoundCaptor>().GetTemps();

                detection2 = false;
            }

        }

        if (detection3 == true)
        {

            if (capteur3.GetComponent<UltrasoundCaptor>().detection == false)
            {
                unTableau[2].numeroCapteur = capteur3.GetComponent<UltrasoundCaptor>().numeroCapteur;
                unTableau[2].distanceCapteur = capteur3.GetComponent<UltrasoundCaptor>().GetDistance();
                unTableau[2].tempsCapteur = capteur3.GetComponent<UltrasoundCaptor>().GetTemps();

                detection3 = false;
            }
        }

        if (detection1 == false && detection2 == false && detection3 == false)
        {
            DisplayResult();
            ResetAll();
        }

    }
    */

    void ResetAll()
    {
        ResetTableau();

        capteur1.GetComponent<UltrasoundCaptor>().Reset();
        capteur2.GetComponent<UltrasoundCaptor>().Reset();
        capteur3.GetComponent<UltrasoundCaptor>().Reset();

        ResetDetection();
    }

    void ResetTableau()
    {
        for (int i=0; i < nombreCapteur; i++)
        {
            unTableau[i].numeroCapteur = 0;
            unTableau[i].distanceCapteur= 0;
            unTableau[i].tempsCapteur = 0;
        }
    }

    void ResetDetection()
    {
        detection1 = true;
        detection2 = true;
        detection3 = true;
    }

    void DisplayResult()
    {
        Debug.Log("Affichage des resultats");

        // Affichage des resultats
    }

}

