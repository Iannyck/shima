using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TokenRing : MonoBehaviour {

    public List<GameObject> listeCapteurs;
    public float delay;
    private int token;
    private float activeTimer;
    private bool activate;

    void Start()
    {
        for (int i = 0; i < listeCapteurs.Count; i++)
            listeCapteurs[i].transform.GetChild(0).gameObject.SetActive(false);

        activate = false;
        activeTimer = 0.25f;

        token = listeCapteurs.Count - 1;
    }

    void Update()
    {
        if (CheckTimer() && activate == false)
        {
            listeCapteurs[token].transform.GetChild(0).gameObject.SetActive(true);

            activate = true;
            ResetTimer();
        }

        if (CheckTimer() && activate == true)
        {
            listeCapteurs[token].GetComponentInChildren<ForceSignal>().Desactivate();
            listeCapteurs[token].transform.GetChild(0).gameObject.SetActive(false);

            GiveToken();

            activate = false;
            ResetTimer();
        }
    }

   bool CheckTimer()
    {
        activeTimer = activeTimer - Time.deltaTime;

        if (activeTimer <= 0)
            return true;

        else
            return false;
    }

    void ResetTimer()
    {
        activeTimer = delay;
    }


    void GiveToken()
    {
        token = token - 1;

        if (token < 0)
            token = listeCapteurs.Count - 1;
    }

};
