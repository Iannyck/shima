using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewTokenRing : MonoBehaviour {

    private List<GameObject> listeCapteurs;
    private int token = 0;

    void Start ()
    {
        listeCapteurs = new List<GameObject>();

        for (int i = 0; i<this.transform.childCount; i++)
        {
            listeCapteurs.Add(this.transform.GetChild(i).gameObject);
        }

        for (token = listeCapteurs.Count - 1; token >= 0; token--)
        {
            listeCapteurs[token].transform.Find("ZoneDetection").GetComponent<NewRFID>().OnDisable();
        }

        token = listeCapteurs.Count -1;

        InvokeRepeating("TokenRing", 1f, 0.25f);
    }
	
	void TokenRing ()
    {
        listeCapteurs[token].transform.Find("ZoneDetection").GetComponent<NewRFID>().OnDisable();
        PassToken();
        listeCapteurs[token].transform.Find("ZoneDetection").GetComponent<NewRFID>().OnEnable();      
	}

    void PassToken()
    {
        if (token != 0)
            token--;

        else
            token = listeCapteurs.Count -1;
    }
}
